using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ambitour.CoucheMetier.ObjetsMetier
{
    [Serializable]
    public class Response
    {
        Handle action;

        public Handle Action
        {
            get { return action; }
            set { action = value; }
        }
        String result;

        public String Result
        {
            get { return result; }
            set { result = value; }
        }

        public Response()
        {
            action = null;
            result = "";
        }

        public Response(Handle action, string result)
        {
            this.action = action;
            this.result = result;
        }

    }
}
