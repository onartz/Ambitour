using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using Ambitour.GUI;

namespace Ambitour.CoucheMetier.ObjetsMetier
{
    class OFAgent
    {
        const string inputQueue = @"c:\Ambitour\Queue\inputQueue";
        string outputQueue = @"c:\Ambitour\Queue\outputQueue";
        string archiveQueue = @"c:\Ambitour\Queue\inputQueue\Archives";

        // event raised before the door opens
        public delegate void NewOF();
        public event NewOF RaiseNewOF;

        private Ambitour.frmOFs frame;
       
       
        private List<OF> oFList;

        internal List<OF> OFList
        {
            get { return oFList; }
            set { oFList = value; }
        }
        BackgroundWorker bg;

        public OFAgent()
        {
            frame = null;
            init();    
        }

        public OFAgent(frmOFs frame)
        {
            this.frame = frame;
            init();
        }

        private void init()
        {
            oFList = new List<OF>();
           
            bg = new BackgroundWorker();
            bg.WorkerReportsProgress = false;
            bg.WorkerSupportsCancellation = true;
            bg.DoWork += new DoWorkEventHandler(bg_DoWork);
            bg.Disposed += new EventHandler(bg_Disposed);
            bg.RunWorkerAsync();
        }

        void bg_Disposed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!bg.CancellationPending)
            {
                string[] fileEntries = Directory.GetFiles(inputQueue);
                foreach (string fileName in fileEntries)
                {
                    RaiseNewOF();
                    ProcessFile(fileName);
                }

                System.Threading.Thread.Sleep(1000);
                
            }
        }

              // Insert logic for processing found files here.
            public static void ProcessFile(string path) 
            {
                
                Console.WriteLine("Processed file '{0}'.", path);	    
            }



     
    }
}
