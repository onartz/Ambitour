using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace Ambitour.CoucheMetier.LogiqueMetier
{
    /*
     * This class observes a queue on the disk and notify observers when new messages arrives
     */
    class RequestBrokerAg
    {
        Thread mainLoop;
        bool stop;
        bool busy;
       
        
        string requestQueue;
        public event EventHandler MessageReceived;

       // Dictionary<string, string> dic;

        Queue<string> queue;

        ////Maintain a current list of requests
        //List<string> requests;

        //public List<string> Requests
        //{
        //    get { return requests; }
        //    set { requests = value; }
        //}


        public RequestBrokerAg()
        {
            if (!Directory.Exists(requestQueue))
            {
                return;
            }
            //dic = new Dictionary<string, string>();
            queue = new Queue<string>(Directory.GetFiles(requestQueue));
            //requests = new List<string>();
            mainLoop = new Thread(new ThreadStart(loop));
            mainLoop.Start();
        }

        void loop()
        {
            busy = true;
            while (!stop)
            {
                string[] fileEntries = Directory.GetFiles(requestQueue);
               // requests.Clear();
                foreach (string fileName in fileEntries)
                {
                    lock (queue)
                    {
                        //In not in the queue
                        if (!queue.Contains(fileName))
                        {
                            //StreamReader sr =
                            // new StreamReader(fileName);
                            //string content = sr.ReadToEnd();
                            queue.Enqueue(fileName);
                            //requests.Add(content);

                            //sr.Close();

                            MessageReceived(this, new EventArgs());
                            ProcessFile(fileName);
                        }
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

        private void ProcessFile(string fileName)
        {
        }

        
    }
}
