using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ambitour.CoucheMetier;
using Ambitour.CoucheMetier.LogiqueMetier;
using Ambitour.CoucheMetier.ObjetsMetier;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Ambitour.CoucheMetier.LogiqueMetier
{

    public delegate void NewMessageEventHandler(object sender, ACLMessageEventArgs e);

    public class IncomingMessageHandler
    {
        //A concurrent FIFO Queue to store incoming requests
        ConcurrentQueue<ACLMessage> queue;

        public ConcurrentQueue<ACLMessage> Queue
        {
            get { return queue; }
            set { queue = value; }
        }
        //A timer to pool new incoming files (requests)
        protected System.Windows.Forms.Timer timer;
        bool stop;
        //To be able to know if new files has come
        string[] inputFiles;
        //Serializer to serialize/deserialize from files
        XmlSerializer SerializerObj;
        //The current message to be processed
        ACLMessage currentMessage;

        public event NewMessageEventHandler NewMessageReceived;

        protected virtual void OnChanged(ACLMessageEventArgs e)
        {
            if (NewMessageReceived != null)
                NewMessageReceived(this, e);
        }

       

        //public ACLMessage CurrentMessage
        //{
        //    get { return currentMessage; }
        //    set { currentMessage = value; }
        //}
        string incomingDirectory;
        string tempDirectory;

        public IncomingMessageHandler(string incomingDirectory)
        {
            this.incomingDirectory = incomingDirectory;
            tempDirectory = incomingDirectory + @"\temp";
            inputFiles = new string[] { };

            SerializerObj = new XmlSerializer(typeof(ACLMessage));
            queue = new ConcurrentQueue<ACLMessage>();
           
            // A simple blocking producer with no cancellation.
            Task.Factory.StartNew(() =>
            {
                while (!stop)
                {
                    getFiles();
                    Thread.Sleep(1000);
                }
            });
        }

        ///// <summary>
        ///// Every x sec, we are looking for new messages to be displayed
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void timer_Tick(object sender, EventArgs e)
        //{
        //    if (currentMessage == null && !queue.IsEmpty)
        //        displayNext();
        //}

        /// <summary>
        /// Enqueue new incoming ACLMessages from files
        /// </summary>
        private void getFiles()
        {
            IEnumerable<string> newFiles = Directory.GetFiles(incomingDirectory).Except(inputFiles);
            foreach (string s in newFiles)
            {
                try
                {
                    TextReader tr = new StreamReader(s);
                    ACLMessage msg = (ACLMessage)SerializerObj.Deserialize(tr);
                    tr.Close();

                    queue.Enqueue(msg);
                    NewMessageReceived(this, new ACLMessageEventArgs(msg));

                    string fileName = Guid.NewGuid().ToString() + ".xml";
                    if (File.Exists(tempDirectory + @"\" + fileName))
                        File.Delete(tempDirectory + @"\" + fileName);
                    File.Move(s, tempDirectory + @"\" + fileName);
                }
                catch (InvalidCastException ex)
                {
                    throw ex;
                }
            }
            inputFiles = Directory.GetFiles(incomingDirectory);
        }

        /// <summary>
        /// To bea able to stop current task if needed
        /// </summary>
        public void Stop()
        {
            stop = true;
        }
    }
}
