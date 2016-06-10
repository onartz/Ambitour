using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Ambitour.CoucheMetier.ObjetsMetier
{
    public static class ContentManager
    {

        public static Object ExtractContent(string s){
            XmlDocument doc = new XmlDocument();
            Object result = null;
            try
            {
                doc.LoadXml(s);
                if (doc.SelectSingleNode("java/object").Attributes["class"].Value.Contains("logistic.Handle"))
                {
                    result = new Handle();
                    int productId = Int32.Parse(((XmlElement)doc.SelectSingleNode("java/object/void[@property='productLot']/object/void[@property='productId']/int")).InnerText);
                    string productLotId = ((XmlElement)doc.SelectSingleNode("java/object/void[@property='productLot']/object/void[@property='lotId']/string")).InnerText;
                    int quantity = Int32.Parse(((XmlElement)doc.SelectSingleNode("java/object/void[@property='productLot']/object/void[@property='quantity']/int")).InnerText);
                    string sender = ((XmlElement)doc.SelectSingleNode("java/object/void[@property='sender']/object/void[@property='adress']/object/void/string")).InnerText;
                    string receiver = ((XmlElement)doc.SelectSingleNode("java/object/void[@property='recipient']/object/void[@property='adress']/object/void/string")).InnerText;
                    ((Handle)result).ProductId = productId.ToString() ;
                    ((Handle)result).ProductLotId = productLotId;
                    ((Handle)result).Quantity = quantity;
                    ((Handle)result).Sender = sender;
                    ((Handle)result).Receiver = receiver;
                }
            }
            catch (XmlException ex)
            {
                throw ex;
            }
            return result;
        }

      
    }
}
