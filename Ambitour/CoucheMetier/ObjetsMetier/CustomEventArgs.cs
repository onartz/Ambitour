﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ambitour.CoucheMetier.ObjetsMetier
{
    // Define a class to hold custom event info
    public class RequestEventArgs : EventArgs
    {
        public RequestEventArgs(KeyValuePair<string,Object> kvp)
        {
            content = kvp;
        }
        private KeyValuePair<string, Object> content;

        public KeyValuePair<string, Object> Content
        {
            get { return content; }
            set { content = value; }
        }
    }

    // Define a class to hold custom ACLMessage event
    public class ACLMessageEventArgs : EventArgs
    {
        private ACLMessage content;

        public ACLMessage Content
        {
            get { return content; }
            set { content = value; }
        }

        public ACLMessageEventArgs(ACLMessage msg)
        {
            content = msg;
        }
    }

    // Define a class to hold custom ACLMessage event
    public class ObjectEventArgs : EventArgs
    {
        private object content;

        public object Content
        {
            get { return content; }
            set { content = value; }
        }

        public ObjectEventArgs(object msg)
        {
            content = msg;
        }
    }
}
