using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ambitour
{
    /// <summary>
    /// Classe programme pièce. Contient le fichier du programme et son numéro de programme
    /// </summary>
    public class ProgrammePiece
    {
        #region Constantes
        /// <summary>
        /// Numéro de programme alloué si pas dans le fichier xpi
        /// </summary>
        public const Int32 NUMERODEPREOGRAMMEPARDEFAUT = 1000;
        #endregion

        #region Variables locales
        private Int32 numeroDeProgramme;
        private FileInfo infosFichier;
        #endregion

        #region Attributs publics

        /// <summary>
        /// Numero de programme
        /// </summary>
        public Int32 NumeroDeProgramme
        {get{return numeroDeProgramme;}set{numeroDeProgramme = value;}}

    
      /// <summary>
      /// Informations sur le fichier
      /// </summary>
        public FileInfo InfosFichier
        { get { return infosFichier; } set { infosFichier = value; } }

        #endregion

        #region Constructeurs
        /// <summary>
        /// Constructeur surchargé 1, le numéro de programme est contenu dans le fichierr pièce
        /// </summary>
        /// <param name="fullPath">Chemin d'accès complet</param>
        public ProgrammePiece(string pFullPath)
        {
            Int32 lNumeroDeProgramme;
            try
            {
                infosFichier = new FileInfo(pFullPath);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            lNumeroDeProgramme = LireNumeroDeProgramme(pFullPath);

            if (lNumeroDeProgramme == -1) numeroDeProgramme = NUMERODEPREOGRAMMEPARDEFAUT;
            else
                numeroDeProgramme = lNumeroDeProgramme;
        }

        /// <summary>
        /// Constructeur surchargé 2
        /// </summary>
        /// <param name="pFullPath">chemin complet du fichier</param>
        /// <param name="pNumeroDeProgramme">Numéro de programme souhaité</param>
        public ProgrammePiece(string pFullPath, Int32 pNumeroDeProgramme)
        {
            try
            {
                infosFichier = new FileInfo(pFullPath);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            numeroDeProgramme = pNumeroDeProgramme;
        }


        #endregion

        #region Méthodes privées
        /// <summary>
        /// Récupération du numéro de programme dans un fichier
        /// On ne lit que la première ligne
        /// </summary>
        /// <param name="fullPath">chemin du fichier</param>
        /// <returns></returns>
        private Int32 LireNumeroDeProgramme(string fullPath)
        {
            FileInfo InfosFichier = new FileInfo(fullPath);

            using (System.IO.StreamReader sr = System.IO.File.OpenText(fullPath))
            {
                //string s = "";
                string s = sr.ReadLine();
                if (s.Length != 0)
                {
                    if ((char)s[0] == '%')
                        return System.Convert.ToInt32(s.TrimStart(new char[] { '%' }));
                }   
                return -1;
            }
        }
#endregion

        #region Méthodes publiques

        /// <summary>
        /// Formatage du fichier pour être transféré dans la CN
        /// </summary>
        public void SupprimerNumeroDeProgramme()
        {
            Fichier f = new Fichier(infosFichier.FullName);
            f.deleteLine(0);
         }

        /// <summary>
        /// Modification de la référence du programme pièce après transfert sur le disque
        /// </summary>
        /// <param name="programme"></param>
        /// <param name="repertoireLocal"></param>
        public string ModifierRef(string repertoireLocal)
        { 
            try
            {
                FileInfo f = new FileInfo(repertoireLocal);
                infosFichier = f;
                return f.FullName;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
    }  
}
       


