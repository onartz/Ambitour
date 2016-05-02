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


namespace Ambitour.GUI
{
    public partial class frmRequests : Form
    {
        //A concurrent FIFO Queue to store incoming requests
        ConcurrentQueue<ACLMessage> queue = new ConcurrentQueue<ACLMessage>();
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
        /// Every x sec, we are looking for new messages to be displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            if (currentMessage == null && !queue.IsEmpty)
                displayNext();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRequests_Load(object sender, EventArgs e)
        {
           // test();
            // A simple blocking producer with no cancellation.
            Task.Factory.StartNew(() =>
            {
                while (!stop)
                {
                    getFiles();
                    Thread.Sleep(2000);
                }
            });
        }

        /// <summary>
        /// Enqueue new incoming ACLMessages from files
        /// </summary>
        private void getFiles()
        {
            IEnumerable<string> newFiles = Directory.GetFiles(GlobalSettings.Default.incomingRequestDirectory).Except(inputFiles);
            foreach (string s in newFiles)
            {
                try
                {
                    TextReader tr = new StreamReader(s);
                    ACLMessage msg = (ACLMessage)SerializerObj.Deserialize(tr);
                    tr.Close();

                    queue.Enqueue(msg);

                    string fileName = Guid.NewGuid().ToString() + ".xml";
                    if (File.Exists(GlobalSettings.Default.tempRequestDirectory + @"\" + fileName))
                        File.Delete(GlobalSettings.Default.tempRequestDirectory + @"\" + fileName);
                    File.Move(s, GlobalSettings.Default.tempRequestDirectory + @"\" + fileName);
                }
                catch (InvalidCastException ex)
                {
                    throw ex;
                }
            }
            inputFiles = Directory.GetFiles(GlobalSettings.Default.incomingRequestDirectory);
        }

        /// <summary>
        /// Display next message to be processed by user
        /// </summary>
        private void displayNext()
        {
            if (queue.TryDequeue(out currentMessage))
            {
                this.groupBox1.Visible = true;
                richTextBox1.Text = currentMessage.Sender;
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
                }
                richTextBox1.Text = sb.ToString();
            }
            else
                this.groupBox1.Visible = false;
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
                //ACLMessage reply = currentMessage.CreateReply();
                //TextWriter tw = new StreamWriter(GlobalSettings.Default.tempRequestDirectory + "\\" + Guid.NewGuid() + ".xml");
                //SerializerObj.Serialize(tw, reply);
                //tw.Close();
                  String dest = currentMessage.Sender;
                //string dest = s + "@" + GlobalSettings.Default.jadeServerAddress + ":1099/JADE";
               // string request = String.Format("(REQUEST\r\n :receiver  (set ( agent-identifier :name {0} ) )\r\n :content  \"((action (agent-identifier :name {0}) (UpdateQuantity\r\n :command Remove :qty {1})))\"\r\n  :language  fipa-sl  :ontology  ambiflux-logistic )", dest, numericUpDown1.Value.ToString());
                //String result = ProxySocket.SocketSendReceive(
                StringBuilder sb = new StringBuilder();
                sb.Append("(INFORM");
                sb.AppendLine(" :receiver  (set ( agent-identifier :name ");
                sb.Append(currentMessage.Receiver);
                sb.Append("  :addresses (sequence http://10.10.68.92:7778/acc )) )");
                sb.AppendLine(" :content  \"((done (action (agent-identifier :name ");
                sb.Append(currentMessage.Sender);
                sb.Append(") (Handle :content (ProductLot :confirmed false :delivered true :lotId \\\"");
                sb.Append(((Handle)currentMessage.Content).ProductLotId);
                sb.Append("\\\" :productId ");
                sb.Append(((Handle)currentMessage.Content).ProductId);
                sb.Append(":quantity ");
                sb.Append(((Handle)currentMessage.Content).Quantity);
                sb.AppendFormat(") :recipient (Participant :adress (agent-identifier :name {0}", ((Handle)currentMessage.Content).Receiver);
                sb.AppendFormat(") :location (Location :id {0} :name ", 23, "TBI-540");
                sb.AppendFormat(")) :sender (Participant :adress (agent-identifier :name {0}) :location (Location :id 23 :name TBI-540))))))\" ", ((Handle)currentMessage.Content).Sender);
                sb.AppendFormat(" :language  fipa-sl  :ontology  ambiflux-logistic  :conversation-id  {0}", currentMessage.ConversationId);
                sb.Append(" )");

                richTextBox1.Text = sb.ToString();



               // string request = "DONE \r\n";
                  String result = ProxySocket.SocketSendReceive("10.10.68.92", 6789, sb.ToString());

            }
            displayNext();
        }

       
    } 
}
