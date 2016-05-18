﻿using System;
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
        //To be able to stop thread task
        protected bool stop;
        //To be able to know if new files has come
        string[] inputFiles;
        //Serializer to serialize/deserialize from files
        XmlSerializer SerializerObj = new XmlSerializer(typeof(ACLMessage));
        //Jade server Address to communicate with
        string jadeServerAddress = GlobalSettings.Default.jadeServerAddress;
        
            
        /// <summary>
        /// Constructor
        /// </summary>
        public frmRequests()
        {
            InitializeComponent();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            currentMessage = null;
            groupBox1.Visible = false;
            inputFiles = new string[] { };
        }
        /// <summary>
        /// Another Constructor with message handler
        /// </summary>
        /// <param name="imHandler"></param>
        public frmRequests(ref IncomingMessageHandler imHandler) : this()
        {
            this.imHandler = imHandler;          
        }

        /// <summary>
        /// Every x sec, we are looking for new messages to be displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            if (currentMessage == null && !imHandler.Queue.IsEmpty)
                displayNext();
        }

      

        /// <summary>
        /// Display next message to be processed by user
        /// </summary>
        private void displayNext()
        {
            currentMessage = null;
            object obj = null;
            //imHandler.Queue.TryDequeue(out obj);
            if (imHandler.Queue.TryDequeue(out obj))
            {
                currentMessage = (ACLMessage)obj;
                this.groupBox1.Visible = true;
               // richTextBox1.Text = currentMessage.Sender;
                StringBuilder sb = new StringBuilder();
                
                Content content = currentMessage.Content;
                if (content.GetType() == typeof(Handle))
                {
                    Handle h = (Handle)content;
                    sb.Append(Environment.NewLine + "Contenu à transférer");
                    sb.Append(Environment.NewLine + "Product : " + h.ProductId);
                    sb.Append(Environment.NewLine + "Quantity : " + h.Quantity);
                    sb.Append(Environment.NewLine + "Lot : " + h.ProductLotId);
                    sb.Append(Environment.NewLine + "Prendre de : " + h.Sender);
                    sb.Append(Environment.NewLine + "Ranger dans : " + h.Receiver);
    
                    ProductInventory productInventory = null;
                    foreach(ProductInventory pi in Pilotage.INSTANCE.InInventories){
                        if (pi.ProductID == Int16.Parse(h.ProductId))
                        {
                            productInventory = pi;
                            break;
                        }
                    }
                    if (productInventory!=null)
                    {
                        productInventory.Quantity += (short)(h.Quantity);
                    }

                   // ProductInventory pi = Pilotage.INSTANCE.InInventories.F
                   
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
        /// Fill the Form with a message
        /// </summary>
        /// <param name="msg"></param>
        public void Fill(ACLMessage msg)
        {
            StringBuilder sb = new StringBuilder();
            Content content = msg.Content;
            if (content.GetType() == typeof(Handle))
            {
                Handle h = (Handle)content;
                sb.Append(Environment.NewLine + "Contenu à transférer");
                sb.Append(Environment.NewLine + "Product : " + h.ProductId);
                sb.Append(Environment.NewLine + "Quantity : " + h.Quantity);
                sb.Append(Environment.NewLine + "Lot : " + h.ProductLotId);
                sb.Append(Environment.NewLine + "Prendre de : " + h.Sender);
                sb.Append(Environment.NewLine + "Ranger dans : " + h.Receiver);
            }
            richTextBox1.Text = sb.ToString();
        }


        /// <summary>
        /// To bea able to stop current task if needed
        /// </summary>
        public void Stop()
        {
            stop = true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (currentMessage != null)
            {
                
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


            }
            displayNext();

        }

    

       
    } 
}
