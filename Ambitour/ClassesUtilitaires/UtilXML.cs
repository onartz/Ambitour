using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Ambitour
{
    public static class UtilXML
    {
        public static XmlDocument loadXMLString(string strContent)
        {
            XmlDocument objXMLDoc = new XmlDocument();
            objXMLDoc.LoadXml(strContent);
            return (objXMLDoc);
        }
    }
}
