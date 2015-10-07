using System;
using System.Collections.Generic;
using System.Text;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO;

namespace Ambitour
{
    public class EmplacementDossier
    {
        #region Variables privées
        private string chemin;
        #endregion

        #region Attributs publics
        public string Chemin { get { return chemin; } set { chemin = value; } }
        #endregion

        #region Constructeurs

        public EmplacementDossier(string pRepertoire)
        {
            chemin = pRepertoire;
        }
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Récupère la liste de tous les dossiers de l'emplacement
        /// </summary>
        /// <returns></returns>
        public List<DossierDeFabrication> GetAllDossiers()
        {
            List<DossierDeFabrication> l = new List<DossierDeFabrication>();
            string[] strDossiers = Directory.GetDirectories(chemin);
            foreach (string s in strDossiers)
            {
                DirectorySecurity dirSecurity = Directory.GetAccessControl(s);
                IdentityReference proprietaire = dirSecurity.GetOwner(typeof(NTAccount));
                string p = proprietaire.Value.ToString();
                string[] tStr = p.Split('\\');
                p = tStr[1];
                DossierDeFabrication d = new DossierDeFabrication(s);
                l.Add(d);              
            }
            return l;
        }

        /// <summary>
        /// Récupère la liste de tous les dossiers dont l'utilisateur passé en paramètre est propriétaire
        /// </summary>
        /// <param name="pUtilisateur">Utilisateur</param>
        /// <returns></returns>
        public List<DossierDeFabrication> GetDossiersByUser(Utilisateur pUtilisateur)
        {
            List<DossierDeFabrication> l = new List<DossierDeFabrication>();
            string[] strDossiers = Directory.GetDirectories(chemin);
            foreach (string s in strDossiers)
            {
                DirectorySecurity dirSecurity = Directory.GetAccessControl(s);
                IdentityReference proprietaire = dirSecurity.GetOwner(typeof(NTAccount));
                string p = proprietaire.Value.ToString();
                string[] tStr = p.Split('\\');
                p = tStr[1];

                if (p == pUtilisateur.Login)
                {
                    DossierDeFabrication d = new DossierDeFabrication(s);
                    l.Add(d);
                }
            }
            return l;
        }

        /// <summary>
        /// Récupère un dossier de fabrication par son nom
        /// </summary>
        /// <param name="pName">Nom</param>
        /// <returns></returns>
        public DossierDeFabrication GetDossierByName(string pName)
        {
            List<DossierDeFabrication> l = new List<DossierDeFabrication>();
            DirectoryInfo di = new DirectoryInfo(chemin);
            DirectoryInfo[] tab=di.GetDirectories(pName);
            if (tab.Length == 0)
                throw (new Exception("Aucun dossier trouvé"));
            return new DossierDeFabrication(tab[0]); 
        }

        //public static bool Exists(string pName)
        //{
        //    try
        //    {
        //        DirectoryInfo di = new DirectoryInfo(chemin);
        //    }
        //    catch (DirectoryNotFoundException ex)
        //    {
        //        return false;
        //    }
        //    return true;

        //}


        #endregion

    }
}
