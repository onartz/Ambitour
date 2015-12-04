using System;
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
}
