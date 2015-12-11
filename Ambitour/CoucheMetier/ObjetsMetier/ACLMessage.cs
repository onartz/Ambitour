using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ambitour.CoucheMetier.ObjetsMetier
{
    [Serializable]
    public class ACLMessage
    {
        static int REQUEST = 0;
        string sender;

        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }
        string receiver;

        public string Receiver
        {
            get { return receiver; }
            set { receiver = value; }
        }

        /// <summary>
        /// Create message from string
        /// </summary>
        /// <param name="fileContent"></param>
        public ACLMessage(string fileContent)
        {
        }

        public ACLMessage()
        {
            // TODO: Complete member initialization
        }

        internal ACLMessage CreateReply()
        {
            ACLMessage msg = new ACLMessage();
            msg.Sender = this.Receiver;
            msg.Receiver = this.Sender;
            return msg;
        }
    }
}
