using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Ambitour
{
    public class DossierDeFabrication
    {

        #region Variables locales
        /// <summary>
        /// System.IO.DirectoryInfo du dossier source avant transfert
        /// </summary>
        private DirectoryInfo infosDossierOrigine;
        private DirectoryInfo infosDossierTransfere;

        #endregion

        #region Attributs publics
        //Infos du dossier windows d'origine
        public DirectoryInfo InfosDossierOrigine
        {get{ return infosDossierOrigine;}set { infosDossierOrigine = value;} }

        //Infos du dossier windows sur le Tour
        public DirectoryInfo InfosDossierTransfere
        { get { return infosDossierTransfere; } set { infosDossierTransfere = value; } }
        #endregion

        #region constructeurs
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public DossierDeFabrication()
        { }

        /// <summary>
        /// Constructeur surchargé 1
        /// </summary>
        /// <param name="fullPath">Chemin d'accès complet au dossier d'origine</param>
        public DossierDeFabrication(string fullPath)
        {
            try
            {
                infosDossierOrigine = new DirectoryInfo(fullPath);
            }
            catch (IOException ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// Constructeur surchargé 2
        /// </summary>
        /// <param name="pDirectoryInfo"></param>
        public DossierDeFabrication(DirectoryInfo pDirectoryInfo)
        {
            infosDossierOrigine = pDirectoryInfo;
        }
        #endregion


        #region Méthodes publiques
        ///// <summary>
        ///// Archiver le dossier
        ///// </summary>
        //public void Archiver(string pDossierArchivage)
        //{
        //    //string dossierArchivage = CoucheMetier.GlobalSettings.Default.strQueuePath + "\\" + CoucheMetier.GlobalSettings.Default.strDefaultArchiveQueueName;
        //    //DirectoryInfo diArchivageSession = new DirectoryInfo(pDossierArchivage + "\\" + SessionInfos.Utilisateur.connexionID.ToString());
        //    //if (diArchivageSession.Exists == false)
        //    //    diArchivageSession.Create();
        //    DirectoryInfo diArchivageDossier = new DirectoryInfo(pDossierArchivage);
        //    if (diArchivageDossier.Exists)
        //        diArchivageDossier.Delete(true);

        //    infosDossierOrigine.MoveTo(pDossierArchivage);
        //    //dossierOrigine.InfosDossierOrigine.MoveTo(dossierArchivage + "\\" + SessionInfos.Utilisateur.connexionID.ToString() + "\\" + pDossier.InfosDossierOrigine.Name);
 
        //}


        /// <summary>
        /// Récupération des programmes pièces .xpi
        /// </summary>
        /// <param name="pDirectoryInfo"></param>
        /// <returns></returns>
        public List<ProgrammePiece> GetProgrammesPiece()
        {
            string lFullpath = infosDossierOrigine.FullName;
            return GetProgrammesPiece(lFullpath);
        }
      /// <summary>
      /// Récupération des programmes pièces .xpi
      /// </summary>
      /// <param name="pDirectoryInfo"></param>
      /// <returns></returns>
        public List<ProgrammePiece> GetProgrammesPiece(DirectoryInfo pDirectoryInfo)
        {
            string lFullpath = pDirectoryInfo.FullName;
            return GetProgrammesPiece(lFullpath);
        }

        /// <summary>
        /// Récupération des programmes pièces .xpi
        /// </summary>
        /// <param name="pDirectoryInfo"></param>
        /// <returns></returns>
        public List<ProgrammePiece> GetProgrammesPiece(string pFullpath)
        {
            DirectoryInfo lDirectoryInfo = new DirectoryInfo(pFullpath);
            List<ProgrammePiece> l = new List<ProgrammePiece>();
            FileInfo[] fi;
            fi = lDirectoryInfo.GetFiles("*" + CoucheMetier.GlobalSettings.Default.ExtensionProgrammePiece, SearchOption.AllDirectories);

            foreach (FileInfo f in fi)
            {
                try
                {
                    ProgrammePiece p = new ProgrammePiece(f.FullName);
                    if (p != null)
                        l.Add(p);
                }
                catch
                { };
            }
            return l;
        }


        /// <summary>
        /// Obtenir la liste des fichiers d'un type particulier
        /// </summary>
        /// <returns></returns>
        public List<FileInfo> GetFichiers(string pattern)
        {
            List<FileInfo> l = new List<FileInfo>();
            FileInfo[] fi = InfosDossierOrigine.GetFiles(pattern, SearchOption.AllDirectories);
            foreach (FileInfo f in fi)
                l.Add(f);            
            return l;
        }

        /// <summary>
        /// Obtenir la liste des docs HTML
        /// </summary>
        /// <returns></returns>
        public List<DocumentationHTML> GetDocumentationsHTML()
        {
            List<DocumentationHTML> l = new List<DocumentationHTML>();
            //Liste des dossiers de prmier niveau (listDir)
            DirectoryInfo[] listDir=infosDossierOrigine.GetDirectories();
            foreach (DirectoryInfo d in listDir)
            {
                FileInfo[] listFiles = d.GetFiles("*" + CoucheMetier.GlobalSettings.Default.ExtensionAideHtml);
                if (listFiles.Length != 0)
                {
                    DocumentationHTML docHTML = new DocumentationHTML();
                    docHTML.InfosDossier = d;
                    docHTML.FichierIndex = listFiles[0].FullName;
                    l.Add(docHTML);
                }
                else
                {
                    GetDocumentationsHTML(ref l, d.FullName);
                }
            }
            return l;

        }

        /// <summary>
        /// Téléchargement des fichiers depuis la queue sur le disque
        /// </summary>
        /// <param name="dossierSource"></param>
        /// <param name="repertoireLocal"></param>
        public void Telecharger(string repertoireLocal)
        {
            try
            {
                UtilFichiers.CopieFichiers(infosDossierOrigine.FullName, repertoireLocal + "\\" + infosDossierOrigine.Name, true);
                DirectoryInfo di = new DirectoryInfo(repertoireLocal + "\\" + infosDossierOrigine.Name);
                infosDossierTransfere = di;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        #endregion

        #region Méthodes privées
        /// <summary>
        /// Obtenir la liste des docs HTML
        /// </summary>
        /// <returns></returns>
        private void GetDocumentationsHTML(ref List<DocumentationHTML> listeDoc, string fullPath)
        {
           DirectoryInfo di = new DirectoryInfo(fullPath);
           
           DirectoryInfo[] listDir = di.GetDirectories();
           foreach (DirectoryInfo d in listDir)
           {
               FileInfo[] listFiles = d.GetFiles("*" + CoucheMetier.GlobalSettings.Default.ExtensionAideHtml);
               if (listFiles.Length != 0)
               {
                   DocumentationHTML docHTML = new DocumentationHTML();
                   docHTML.InfosDossier = d;
                   docHTML.FichierIndex = listFiles[0].Name;
                   listeDoc.Add(docHTML);
                   
               }
               GetDocumentationsHTML(ref listeDoc, d.FullName);
           }
        }
        #endregion
    }
}
