using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ambitour.CoucheMetier;
using Ambitour.CoucheMetier.LogiqueMetier;
using Ambitour.CoucheMetier.ObjetsMetier;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;

namespace Ambitour.GUI
{
    public partial class frmRequests : Form
    {
        //A concurrent FIFO Queue to store incoming requests
        //ConcurrentQueue<ACLMessage> queue = new ConcurrentQueue<ACLMessage>();
        IncomingMessageHandler imHandler;
        //A timer to pool new incoming files (requests)
        protected System.Windows.Forms.Timer timer;
        //The current message to be processed
        ACLMessage currentMessage;
        //The current content of the message
        Content currentContent;
        //To be able to stop thread task
        protected bool stop;
        //To be able to know if new files has come
        string[] inputFiles;
        //Serializer to serialize/deserialize from files
        XmlSerializer SerializerObj = new XmlSerializer(typeof(ACLMessage));
        //Jade server Address to communicate with
        string jadeServerAddress = GlobalSettings.Default.jadeServerAddress;
        //can be from or to, depending on Handle
        ProductInventory currentProductInventory;
        private delegate void UpdateForm();
        
            
        /// <summary>
        /// Constructor
        /// </summary>
        public frmRequests()
        {
            InitializeComponent();
            currentMessage = null;
            currentContent = null;
            currentProductInventory = null;
            groupBox1.Visible = false;
           
        }
        /// <summary>
        /// Another Constructor with message handler
        /// </summary>
        /// <param name="imHandler"></param>
        public frmRequests(ref IncomingMessageHandler imHandler) : this()
        {
            this.imHandler = imHandler;
            imHandler.NewMessageReceived += new NewMessageEventHandler(imHandler_NewMessageReceived);
        }

        /// <summary>
        /// Method triggered when new messages arrives
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void imHandler_NewMessageReceived(object sender, ObjectEventArgs e)
        {
            ACLMessage msg = (ACLMessage)e.Content;
            Trace.TraceInformation(DateTime.Now + String.Format("New request received : {0}", 
               ((ACLMessage)(e.Content)).ToString()));
            if(currentMessage == null)
                Trace.TraceInformation("CurrentMessage is null");
            else
                 Trace.TraceInformation("CurrentMessage is not null");
            if (imHandler.Queue.IsEmpty)
                Trace.TraceInformation("imHandler.Queue IsEmpty");
            else
                Trace.TraceInformation("imHandler.Queue is not Empty");
           
            if (currentMessage == null && !imHandler.Queue.IsEmpty)
            {
                if (InvokeRequired)
                {
                    Trace.TraceInformation("Display request");
                    Invoke(new UpdateForm(displayNext));
                }
            }
        }



        /// <summary>
        /// Display next message to be processed by user
        /// </summary>
        private void displayNext()
        {
            object obj = null;
            if (imHandler.Queue.TryDequeue(out obj))
            {
                currentMessage = (ACLMessage)obj;
                this.groupBox1.Visible = true;
                StringBuilder sb = new StringBuilder();
                
                currentContent = currentMessage.Content;
                if (currentContent.GetType() == typeof(Handle))
                {
                    Handle h = (Handle)currentContent;
                    sb.Append(Environment.NewLine + "Contenu à transférer");
                    sb.Append(Environment.NewLine + "Product : " + h.ProductId);
                    sb.Append(Environment.NewLine + "Quantity : " + h.Quantity);
                    sb.Append(Environment.NewLine + "Lot : " + h.ProductLotId);
                    sb.Append(Environment.NewLine + "Prendre de : " + h.Sender);
                    sb.Append(Environment.NewLine + "Ranger dans : " + h.Receiver);
    
                    foreach(ProductInventory pi in Pilotage.INSTANCE.InInventories){
                        if (pi.ProductID == Int16.Parse(h.ProductId))
                        {
                            currentProductInventory = pi;
                            break;
                        }
                    }
                   
                   
                }
                richTextBox1.Text = sb.ToString();
            }
            else
                this.groupBox1.Visible = false;
        }



        /// <summary>
        /// Fill the Form with a message
        /// </summary>
        /// <param name="msg"></param>
        public void Initialize(IncomingMessageHandler imHandler)
        {
            this.imHandler = imHandler;
            if (!imHandler.Queue.IsEmpty)
            {
                if(currentMessage == null)
                    displayNext();
            }
        }

        /// <summary>
        /// Send an inform-done
        /// </summary>
        private void informDoneHandle()
        {
            if (currentMessage != null)
            {
                if (currentContent.GetType() == typeof(Handle))
                {
                    Handle h = (Handle)currentContent;
                    if (currentProductInventory != null)
                    {
                        if (currentProductInventory.Type == ProductInventory.inout.OUTPUT)
                            currentProductInventory.Quantity -= (short)(h.Quantity);
                        else
                            currentProductInventory.Quantity += (short)(h.Quantity);
                    }

                    String dest = currentMessage.Sender;

                    StringBuilder sb = new StringBuilder();
                    sb.Append("(INFORM");
                    sb.AppendLine(" :receiver  (set ( agent-identifier :name ");
                    sb.Append(currentMessage.Receiver);
                    sb.AppendFormat("  :addresses (sequence http://{0}:7778/acc )) )", jadeServerAddress);
                    sb.AppendLine(" :content  \"((done (action (agent-identifier :name ");
                    sb.Append(currentMessage.Sender);
                    sb.Append(") (Handle :content (ProductLot :confirmed false :delivered true :lotId \\\"");
                    sb.Append(((Handle)currentMessage.Content).ProductLotId);
                    sb.Append("\\\" :productId ");
                    sb.Append(((Handle)currentMessage.Content).ProductId);
                    sb.Append(":quantity ");
                    sb.Append(((Handle)currentMessage.Content).Quantity);
                    sb.AppendFormat(") :recipient (Participant :adress (agent-identifier :name {0}", ((Handle)currentMessage.Content).Receiver);
                    sb.AppendFormat(") :location (Location :id {0} :name TBI-540", 23);
                    sb.AppendFormat(")) :sender (Participant :adress (agent-identifier :name {0}) :location (Location :id 23 :name TBI-540))))))\" ", ((Handle)currentMessage.Content).Sender);
                    sb.AppendFormat(" :language  fipa-sl  :ontology  ambiflux-logistic  :conversation-id  {0}", currentMessage.ConversationId);
                    sb.Append(" )");

                    String result = ProxySocket.SocketSend(jadeServerAddress, 6789, sb.ToString());

                    //TODO : do like that above!
                    try
                    {
                        Thread t = new Thread(new ThreadStart(this.requestUpdateQuantity));
                        t.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur de socket");
                    }


                    currentMessage = null;
                    currentContent = null;
                }
            }
        }

        /// <summary>
        /// Send a message to proxy Agent tu update quantity of agent
        /// </summary>
        private void requestUpdateQuantity()
        {
            if (currentMessage != null)
            {
                //content is a handle operation
                if (currentContent.GetType() == typeof(Handle))
                {
                    Handle h = (Handle)currentContent;
                    //Pilotage.INSTANCE.InInventories.Exists(
                    //TODO : if h.receiver in inputinventories, Add
                    //TODO : if h.sender in outputinventories, Remove
                    string request = String.Format("(REQUEST\r\n :receiver  (set ( agent-identifier :name {0} ) )\r\n :content  \"((action (agent-identifier :name {0}) (UpdateQuantity\r\n :command Add :qty {1})))\"\r\n  :language  fipa-sl  :ontology  ambiflux-logistic )", h.Receiver, h.Quantity);
                    string result = "";
                    try
                    {
                        //Log.Write(String.Format("Sending {0} to {1} ", request, GlobalSettings.Default.jadeServerAddress));
                        result = ProxySocket.SocketSend(GlobalSettings.Default.jadeServerAddress, 6789, request);
                       // Log.Write(String.Format("Result : {0} ", result));
                    }
                    catch (Exception ex)
                    {
                        //errorList.Add(ex.Message);
                        //updateLogView();
                        throw ex;

                        //  return;
                    }

                }
                else
                {
                    //TODO : do something
                }
            }
        }



        /// <summary>
        /// To bea able to stop current task if needed
        /// </summary>
        public void Stop()
        {
            stop = true;
        }


        /// <summary>
        /// When operator validates operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //Send updateQuantity
                Thread thSend1 = new Thread(new ThreadStart(this.requestUpdateQuantity));
                thSend1.Start();
                Thread.Sleep(200);

                //Send inform-done
                Thread thSend2 = new Thread(new ThreadStart(this.informDoneHandle));
                thSend2.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de socket");
            }

            displayNext();

            //if (currentMessage != null)
            //{
            //    if (currentContent.GetType() == typeof(Handle))
            //    {
            //        Handle h = (Handle)currentContent;
            //        if (currentProductInventory != null)
            //        {
            //            if (currentProductInventory.Type == ProductInventory.inout.OUTPUT)
            //                currentProductInventory.Quantity -= (short)(h.Quantity);
            //            else
            //                currentProductInventory.Quantity += (short)(h.Quantity);
            //        }

            //        String dest = currentMessage.Sender;

            //        StringBuilder sb = new StringBuilder();
            //        sb.Append("(INFORM");
            //        sb.AppendLine(" :receiver  (set ( agent-identifier :name ");
            //        sb.Append(currentMessage.Receiver);
            //        sb.AppendFormat("  :addresses (sequence http://{0}:7778/acc )) )", jadeServerAddress);
            //        sb.AppendLine(" :content  \"((done (action (agent-identifier :name ");
            //        sb.Append(currentMessage.Sender);
            //        sb.Append(") (Handle :content (ProductLot :confirmed false :delivered true :lotId \\\"");
            //        sb.Append(((Handle)currentMessage.Content).ProductLotId);
            //        sb.Append("\\\" :productId ");
            //        sb.Append(((Handle)currentMessage.Content).ProductId);
            //        sb.Append(":quantity ");
            //        sb.Append(((Handle)currentMessage.Content).Quantity);
            //        sb.AppendFormat(") :recipient (Participant :adress (agent-identifier :name {0}", ((Handle)currentMessage.Content).Receiver);
            //        sb.AppendFormat(") :location (Location :id {0} :name TBI-540", 23);
            //        sb.AppendFormat(")) :sender (Participant :adress (agent-identifier :name {0}) :location (Location :id 23 :name TBI-540))))))\" ", ((Handle)currentMessage.Content).Sender);
            //        sb.AppendFormat(" :language  fipa-sl  :ontology  ambiflux-logistic  :conversation-id  {0}", currentMessage.ConversationId);
            //        sb.Append(" )");

            //        String result = ProxySocket.SocketSend(jadeServerAddress, 6789, sb.ToString());

            //        //TODO : do like that above!
            //        try
            //        {
            //            Thread t = new Thread(new ThreadStart(this.requestUpdateQuantity));
            //            t.Start();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Erreur de socket");
            //        }


            //        currentMessage = null;
            //        currentContent = null;
            //    }
            //}
            
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }      
    } 
}
