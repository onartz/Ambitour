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
    public delegate void NewMessageEventHandler(object sender, ObjectEventArgs e);
    /// <summary>
    /// A class to store incoming messages from file to FIFO and notify subscribers when a new message arrives
    /// </summary>
    public class IncomingMessageHandler
    {
        //A concurrent FIFO Queue to store incoming requests
        ConcurrentQueue<Object> queue;

        public ConcurrentQueue<Object> Queue
        {
            get { return queue; }
            set { queue = value; }
        }
        //A timer to pool new incoming files (requests)
        //protected Timer timer;
        bool stop;
        //To be able to know if new files has come
        string[] inputFiles;
        //Serializer to serialize/deserialize from files
        XmlSerializer SerializerObj;
        //The current message to be processed
        ACLMessage currentMessage;

        public event NewMessageEventHandler NewMessageReceived;

        protected virtual void OnChanged(ObjectEventArgs e)
        {
            if (NewMessageReceived != null)
                NewMessageReceived(this, e);
        }

        string incomingDirectory;
        string archiveDirectory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="incomingDirectory">Directory where requests arrive</param>
        public IncomingMessageHandler(string incomingDirectory)
        {
            this.incomingDirectory = incomingDirectory;
            this.archiveDirectory = Path.Combine(incomingDirectory, GlobalSettings.Default.archivedDirectoryName);
            inputFiles = new string[] { };

            SerializerObj = new XmlSerializer(typeof(ACLMessage));
            queue = new ConcurrentQueue<Object>();
           
            Task.Factory.StartNew(() =>
            {
                while (!stop)
                {
                    try
                    {
                        getFiles();
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                    Thread.Sleep(1000);
                }
            });
        }

      
        /// <summary>
        /// Enqueue new incoming ACLMessages from files to FIFO Queue
        /// </summary>
        private void getFiles()
        {
            IEnumerable<string> newFiles = Directory.GetFiles(incomingDirectory).Except(inputFiles);
            TextReader tr = null;
            foreach (string s in newFiles)
            {
                try
                {
                    tr = new StreamReader(s);
                    object msg = SerializerObj.Deserialize(tr);
                    tr.Close(); 
                    queue.Enqueue(msg);
                    NewMessageReceived(this, new ObjectEventArgs(msg));
                    FileInfo fi = new FileInfo(s);
                    string fileName = fi.Name;
                    //File.Delete(fileName);
                    if (File.Exists(archiveDirectory + @"\" + fileName))
                        File.Delete(archiveDirectory + @"\" + fileName);
                    File.Move(s, archiveDirectory + @"\" + fileName);
                }
                catch (Exception ex)
                {
                    tr.Close();
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
