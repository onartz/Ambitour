using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.DirectoryServices;
using System.Security.AccessControl;
using System.Security.Principal;


namespace Ambitour
{
    static class ServiceUHP
    {

        /// <summary>
        /// Obtenir une liste d'utilisateurs AD à partir du nom, du prénom et du type
        /// </summary>
        /// <param name="pNom">Nom</param>
        /// <param name="pPrenom">Prénom</param>
        /// <returns>Liste des utilisateurs répondant aux critères</returns>
        public static List<Utilisateur> GetUtilisateursAD(string pNom, string pPrenom, string pEmployeeType)
        {
            string nom = "";
            string prenom = "";
            string login = "";
            string role = "";
            string businessCategory = "";
            List<Utilisateur> listeUtilisateurs = new List<Utilisateur>();
            DirectoryEntry ldap = new DirectoryEntry(CoucheMetier.GlobalSettings.Default.strADPath, CoucheMetier.GlobalSettings.Default.strLogin, CoucheMetier.GlobalSettings.Default.strPasswd);
            DirectorySearcher searcher = new DirectorySearcher(ldap);
            if (pPrenom.Length == 0)
                searcher.Filter = ("(&(sn=" + pNom + "))");
            else
                //searcher.Filter = ("(&(sn=" + pNom + ")(givenName=" + pPrenom + ")(employeeType=" + pEmployeeType + "))");
                searcher.Filter = ("(&(sn=" + pNom + ")(givenName=" + pPrenom + "))");      

            SearchResultCollection results = searcher.FindAll();
            foreach (SearchResult s in results)
            {
                DirectoryEntry de = new DirectoryEntry(s.Path);
                try
                {
                    nom = de.Properties["sn"].Value.ToString();
                    prenom = de.Properties["givenName"].Value.ToString();
                    login = de.Properties["sAMAccountName"].Value.ToString();

                    if (de.Properties["businessCategory"].Value == null)
                    {
                        role = "FB";
                    }
                    else
                    {
                        switch (de.Properties["businessCategory"].Value.ToString())
                        {
                            case "ET":
                                role = "Etudiant";
                                break;
                            case "FB":
                                role = "Personnel";
                                break;
                            case "FE":
                                role = "Personnel";
                                break;
                            case "VE":
                                role = "Personnel";
                                break;
                            case "HE":
                                role = "Personnel";
                                break;
                            default:
                                role = "Unknown";
                                break;
                        }

                    }
                }
                catch (NullReferenceException e)
                {
                    throw e;

                }
               
                //string ss = de.Properties["employeeType"].Value.ToString();
                /*switch (de.Properties["businessCategory"].Value.ToString())
                {
                    case "ET":
                        role = "Etudiant";
                        break;
                    case "FB":
                        role = "Personnel";
                        break;
                    case "FE":
                        role = "Personnel";
                        break;
                    case "VE":
                        role = "Personnel";
                        break;
                    case "HE":
                        role = "Personnel";
                        break;
                    default:
                        role = "Unknown";
                        break;
                }*/

                Utilisateur u = new Utilisateur(nom, prenom, login, role);
                listeUtilisateurs.Add(u);
            }
            return listeUtilisateurs;
        }

        /// <summary>
        /// Obtenir une liste d'utilisateurs AD à partir du nom, du prénom et du type
        /// </summary>
        /// <param name="pNom">Nom</param>
        /// <param name="pPrenom">Prénom</param>
        /// <returns>Liste des utilisateurs répondant aux critères</returns>
        public static List<string> GetStrUtilisateursAD(string pNom, string pPrenom, string pEmployeeType)
        {
            string nom;
            string prenom;
            string login;
            string role;
            List<string> listeUtilisateurs = new List<string>();
            DirectoryEntry ldap = new DirectoryEntry(CoucheMetier.GlobalSettings.Default.strADPath, CoucheMetier.GlobalSettings.Default.strLogin, CoucheMetier.GlobalSettings.Default.strPasswd);
            DirectorySearcher searcher = new DirectorySearcher(ldap);
            if (pPrenom.Length == 0)
                searcher.Filter = ("(&(sn=" + pNom + "))");
            else
                //searcher.Filter = ("(&(sn=" + pNom + ")(givenName=" + pPrenom + ")(employeeType=" + pEmployeeType + "))");
                searcher.Filter = ("(&(sn=" + pNom + ")(givenName=" + pPrenom + "))");

            SearchResultCollection results = searcher.FindAll();
            foreach (SearchResult s in results)
            {
                DirectoryEntry de = new DirectoryEntry(s.Path);

                nom = de.Properties["sn"].Value.ToString();
                prenom = de.Properties["givenName"].Value.ToString();
                login = de.Properties["sAMAccountName"].Value.ToString();
                switch (de.Properties["businessCategory"].Value.ToString())
                {
                    case "ET":
                        role = "Etudiant";
                        break;
                    case "FB":
                        role = "Personnel";
                        break;
                    case "FE":
                        role = "Personnel";
                        break;
                    default:
                        role = "Unknown";
                        break;
                }

                //Utilisateur u = new Utilisateur(nom, prenom, login, role);
                listeUtilisateurs.Add(nom+" " +prenom+" " +login);
            }
            return listeUtilisateurs;
        }


        //public static void DecrypterCarte(ref Carte pCarte)
        //{
        //    HttpWebResponse reponse;
        //    //Carte lCarte = new Carte();
        //    //lCarte.CardId = pCarteId;
        //    try
        //    {
        //        WebRequest wr = WebRequest.Create("http://samclegap.uhp-nancy.fr/appel_prog3.php?param=KX" + pCarte.CardId + "&file=xml");
        //        reponse = (HttpWebResponse)wr.GetResponse();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }

        //    Stream str = reponse.GetResponseStream();
        //    StreamReader sr = new StreamReader(str);
        //    string strXMlResult = sr.ReadToEnd();
        //    sr.Close();
        //    str.Close();
        //    reponse.Close();

        //    XmlDocument xmlResult = UtilXML.loadXMLString(strXMlResult);
         
        //    //C'est une carte CLE UHP
        //    //Les données renvoyées correspondent aux données Harpege d'un unique utilisateur
        //    try
        //    {
        //        pCarte.Nom = xmlResult.SelectSingleNode("UTILISATEURS/UTILISATEUR/NOM").InnerText;
        //        pCarte.Prenom = xmlResult.SelectSingleNode("UTILISATEURS/UTILISATEUR/PRENOM").InnerText;
        //        pCarte.Login = xmlResult.SelectSingleNode("UTILISATEURS/UTILISATEUR/LOGIN").InnerText;
        //        pCarte.Role = xmlResult.SelectSingleNode("UTILISATEURS/UTILISATEUR/OU").InnerText;
        //    }
        //    //Pas de données utilisateur retournées par l'UHP
        //    catch (XmlException ex)
        //    {
        //        throw new Exception("Carte illisible par le service UHP");
        //    }

        //    pCarte.Type = "UHP";
        //    return;
        //}

       

        ///// <summary>
        ///// Obtenir le role de l'utilisateur en fonction de l'OU d'appartenance
        ///// </summary>
        ///// <param name="de">DirectoryEntru dans l'AD</param>
        ///// <returns>Enseignant, Etudiant ou Personnel</returns>
        //private static string GetRole(DirectoryEntry de)
        //{
        //    if (de.Parent.Name == "OU=Enseignants") return ("Enseignant");
        //    else
        //        if ((de.Parent.Parent.Name == "OU=ETUDIANT") || (de.Parent.Parent.Name == "OU=Etudiants")) return ("Etudiant");
        //        else
        //            return ("Personnel");
        //}
    }
}
