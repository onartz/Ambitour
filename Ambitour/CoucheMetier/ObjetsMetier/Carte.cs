using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;

namespace Ambitour
{
    public class Carte
    {
        #region Variables privées
        private string cardId="";
        private string type="";
        private string nom="";
        private string prenom="";
        private string dossier="";
        private string role="";
        //private string login="";
        private string uidUL = "";
        #endregion

        #region Attributs publics
        public string CardId { get { return cardId; } set { cardId = value; }}
        public string Type { get { return type; } set { type = value; } }
        public string Nom { get { return nom; } set { nom = value; } }
        public string Prenom { get { return prenom; } set { prenom = value; } }
        public string Dossier { get { return dossier; } set { dossier = value; } }
        public string Role { get { return role; } set { role = value; } }
        //public string Login { get { return login; } set { login = value; } }
        public string UidUL { get { return uidUL; } set { uidUL = value; } }
        #endregion


        #region Méthodes publiques
        /// <summary>
        /// Retourne le contenu de la carte sous forme de string xml
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            XmlDocument objXMLDoc = new XmlDocument();
            objXMLDoc.LoadXml("<Card/>");
  
            XmlElement elt = objXMLDoc.CreateElement("CardId");
            elt.InnerText = cardId;
            objXMLDoc.DocumentElement.AppendChild((XmlNode)elt);

            elt = objXMLDoc.CreateElement("FirstName");
            elt.InnerText = prenom;
            objXMLDoc.DocumentElement.AppendChild((XmlNode)elt);

            elt= objXMLDoc.CreateElement("LastName");
            elt.InnerText = nom;
            objXMLDoc.DocumentElement.AppendChild((XmlNode)elt);

            elt= objXMLDoc.CreateElement("Dossier");
            elt.InnerText = dossier;
            objXMLDoc.DocumentElement.AppendChild((XmlNode)elt);

            elt = objXMLDoc.CreateElement("Type");
            elt.InnerText = type;
            objXMLDoc.DocumentElement.AppendChild((XmlNode)elt);

            elt = objXMLDoc.CreateElement("UidUL");
            elt.InnerText = uidUL;
            objXMLDoc.DocumentElement.AppendChild((XmlNode)elt);

            elt = objXMLDoc.CreateElement("Role");
            elt.InnerText =role;
            objXMLDoc.DocumentElement.AppendChild((XmlNode)elt);

            return (objXMLDoc.InnerXml);
        }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Carte()
        {       
        }

        /// <summary>
        /// Constructeur surchargé 1
        /// </summary>
        /// <param name="xmlString">xml fourni</param>
        public Carte(string xmlString)
        {
            try
            {
                XmlDocument objXMLDoc = UtilXML.loadXMLString(xmlString);
                cardId = objXMLDoc.SelectSingleNode("Card/CardId").InnerText;
                nom = objXMLDoc.SelectSingleNode("Card/LastName").InnerText;
                prenom = objXMLDoc.SelectSingleNode("Card/FirstName").InnerText;
                dossier = objXMLDoc.SelectSingleNode("Card/Dossier").InnerText;
                type = objXMLDoc.SelectSingleNode("Card/Type").InnerText;
                uidUL = objXMLDoc.SelectSingleNode("Card/UidUL").InnerText;
                role = objXMLDoc.SelectSingleNode("Card/Role").InnerText;          
            }
            catch (XmlException ex)
            {
                throw;
                //Log.Write("Erreur classe Carte. Constructeur 1");
                //return;
            }       
        }
        #endregion
    }
}
