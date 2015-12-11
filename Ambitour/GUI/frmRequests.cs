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
            test();
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
                ACLMessage reply = currentMessage.CreateReply();
                TextWriter tw = new StreamWriter(GlobalSettings.Default.tempRequestDirectory + "\\" + Guid.NewGuid() + ".xml");
                SerializerObj.Serialize(tw, reply);
                tw.Close();
            }
            displayNext();
        }

        private void test()
        {
            ACLMessage msg = new ACLMessage();
            msg.Sender = "PATROLBOT";
            msg.Receiver = "TBI540";
            ACLMessage response = msg.CreateReply();
            TextWriter tw = new StreamWriter(GlobalSettings.Default.tempRequestDirectory + @"\" +Guid.NewGuid() + ".xml");
            SerializerObj.Serialize(tw, response);
            tw.Close();

        }
    } 
}
