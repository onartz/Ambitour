using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ambitour
{
    /// <summary>
    /// Classe programme pi�ce. Contient le fichier du programme et son num�ro de programme
    /// </summary>
    public class ProgrammePiece
    {
        #region Constantes
        /// <summary>
        /// Num�ro de programme allou� si pas dans le fichier xpi
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
        /// Constructeur surcharg� 1, le num�ro de programme est contenu dans le fichierr pi�ce
        /// </summary>
        /// <param name="fullPath">Chemin d'acc�s complet</param>
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
        /// Constructeur surcharg� 2
        /// </summary>
        /// <param name="pFullPath">chemin complet du fichier</param>
        /// <param name="pNumeroDeProgramme">Num�ro de programme souhait�</param>
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

        #region M�thodes priv�es
        /// <summary>
        /// R�cup�ration du num�ro de programme dans un fichier
        /// On ne lit que la premi�re ligne
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

        #region M�thodes publiques

        /// <summary>
        /// Formatage du fichier pour �tre transf�r� dans la CN
        /// </summary>
        public void SupprimerNumeroDeProgramme()
        {
            Fichier f = new Fichier(infosFichier.FullName);
            f.deleteLine(0);
         }

        /// <summary>
        /// Modification de la r�f�rence du programme pi�ce apr�s transfert sur le disque
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
       


