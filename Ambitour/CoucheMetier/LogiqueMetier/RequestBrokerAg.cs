using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using  Ambitour.CoucheMetier.ObjetsMetier;


namespace Ambitour.CoucheMetier.LogiqueMetier
{
    /*
     * This class observes a queue on the disk and notify observers when new messages arrives
     */
    class RequestBrokerAg
    {
        Thread mainLoop;
        bool stop = false;
        bool busy = false;

        const string requestQueue = @"c:\Ambitour\Queue\inputQueue";
        string outputQueue = @"c:\Ambitour\Queue\outputQueue";
        string archiveQueue = @"c:\Ambitour\Queue\inputQueue\Archives";

        public event EventHandler<RequestEventArgs> MessageReceived;
        Dictionary<string, Object> requests;

        public Dictionary<string, Object> Requests
        {
            get { return requests; }
            set { requests = value; }
        }
        //Queue<string> queue;

        public void RemoveFromQueue(string s)
        {
            if(requests.ContainsKey(s))
                requests.Remove(s);
        }

        public RequestBrokerAg()
        {
            requests = new Dictionary<string, Object>();
            
            if (!Directory.Exists(requestQueue))
            {
                throw new DirectoryNotFoundException();
            }
            //queue = new Queue<string>(Directory.GetFiles(requestQueue));
            mainLoop = new Thread(new ThreadStart(loop));
            mainLoop.Start();
        }


        void loop()
        {
            busy = true;
            while (stop == false)
            {
                string[] fileEntries = Directory.GetFiles(requestQueue);
               // requests.Clear();
                foreach (string fileName in fileEntries)
                {
                   //if(requests.ContainsKey(Path.GetFileNameWithoutExtension(fileName))){
                   //    requests.Remove(Path.GetFileNameWithoutExtension(fileName));
                   //}
                    //Not in the queue
                    if (!requests.ContainsKey(Path.GetFileNameWithoutExtension(fileName)))
                    {
                        try
                        {
                            StreamReader sr =
                                 new StreamReader(fileName);
                            string str = sr.ReadToEnd();
                            Object content = ContentManager.ExtractContent(str);
                            KeyValuePair<string, Object> kvp = new KeyValuePair<string, Object>(Path.GetFileNameWithoutExtension(fileName), content);
                            requests.Add(Path.GetFileNameWithoutExtension(fileName), content);
                            sr.Close();
                            OnNewMessageReceived(new RequestEventArgs(kvp));
                            ArchiveFile(fileName);

                           
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        //Handle h = new Handle(content);

                      

                    }
                   
                }
                Thread.Sleep(1000);
            }
            busy = false;
        }

      
        public void Stop()
        {
            stop = true;
        }

        private void ArchiveFile(string fileName)
        {
            if(File.Exists(Path.Combine(archiveQueue, Path.GetFileName(fileName)))){
                File.Delete(Path.Combine(archiveQueue, Path.GetFileName(fileName)));
            }
                File.Move(fileName, Path.Combine(archiveQueue, Path.GetFileName(fileName)));

        }

        protected virtual void OnNewMessageReceived(RequestEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<RequestEventArgs> handler = MessageReceived;

            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Format the string to send inside the CustomEventArgs parameter
                //e.Message += String.Format(" at {0}", DateTime.Now.ToString());
               
                // Use the () operator to raise the event.
                handler(this, e);
            }
        }

        
    }
}
