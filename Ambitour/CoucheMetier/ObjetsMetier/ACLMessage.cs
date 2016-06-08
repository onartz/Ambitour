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
        Content content;
        String conversationId;

        public String ConversationId
        {
            get { return conversationId; }
            set { conversationId = value; }
        }

        public Content Content
        {
            get { return content; }
            set { content = value; }
        }

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

        /// <summary>
        /// Default constructor
        /// </summary>
        public ACLMessage()
        {
            // TODO: Complete member initialization
        }

        /// <summary>
        /// Create a reply message
        /// </summary>
        /// <returns></returns>
        internal ACLMessage CreateReply()
        {
            ACLMessage msg = new ACLMessage();
            msg.Sender = this.Receiver;
            msg.Receiver = this.Sender;
            return msg;
        }
    }
}
