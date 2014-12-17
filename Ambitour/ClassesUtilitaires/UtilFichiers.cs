using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
namespace Ambitour
{
    public static class UtilFichiers
    {
      /// <summary>
      /// Copie
      /// </summary>
      /// <param name="sourceDirectory"></param>
      /// <param name="targetDirectory"></param>
      /// <param name="recursive"></param>
        public static void CopieFichiers(string sourceDirectory, string targetDirectory, bool recursive)
        {
            if (sourceDirectory == null)          
                throw new ArgumentNullException("sourceDirectory");
               
            if (targetDirectory == null)           
                throw new ArgumentNullException("targetDirectory");


          
            // Call the recursive method.
            CopieFichiers(new DirectoryInfo(sourceDirectory), new DirectoryInfo(targetDirectory), recursive);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="recursive"></param>
        public static void CopieFichiers(DirectoryInfo source, DirectoryInfo target, bool recursive)
        {
            

            if (source == null)
                throw new ArgumentNullException("source");
            if (target == null)
                throw new ArgumentNullException("target");

            // If the source doesn't exist, we have to throw an exception.
            if (!source.Exists)
                throw new DirectoryNotFoundException("Source directory not found: " + source.FullName);
            // If the target doesn't exist, we create it.
            if (!target.Exists)
                target.Create();
           
            // Get all files and copy them over.
            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }

            // Return if no recursive call is required.
            if (!recursive) return;

            // Do the same for all sub directories.
            foreach (DirectoryInfo directory in source.GetDirectories())
            {
                CopieFichiers(directory, new DirectoryInfo(Path.Combine(target.FullName, directory.Name)),
                    recursive);
            }
        }

        /// <summary>
        /// Récupération des dossiers d'un utilisateur 
        /// </summary>
        /// <param name="login">Login de l'utilisateur</param>
        /// <returns>Liste des dossiers de l'utilisateur</returns>
       
        public static List<string> GetListeDossiers(string sourceDirectory, string login)
        {
            List<string> liste = new List<string>();
            string[] strDossiers = Directory.GetDirectories(sourceDirectory);
            foreach (string s in strDossiers)
            {
                DirectorySecurity dirSecurity = Directory.GetAccessControl(s);
                IdentityReference proprietaire = dirSecurity.GetOwner(typeof(NTAccount));
                string p = proprietaire.Value.ToString();
                string[] tStr = p.Split('\\');
                p = tStr[1];

                if (p == login)
                {
                    liste.Add(s);

                }
            }
            return liste;
        }
        /// <summary>
        /// Dir
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="pattern">Filtre</param>
        /// <returns></returns>
        public static List<string> GetFichiers(string sourceDirectory,string pattern )
        {
            if (sourceDirectory == null || sourceDirectory=="")
                throw new ArgumentNullException("source");
            if (pattern == null || pattern == "")
                throw new ArgumentNullException("pattern");

            List<string> liste = new List<string>();
            try
            {
                string[] strProgrammes = Directory.GetFiles(sourceDirectory, pattern);

                if (strProgrammes.Length == 0)
                    throw new FileNotFoundException("Aucun fichier trouvé");
                foreach (string s in strProgrammes)
                {
                    liste.Add(s);
                }
                return (liste);
            }
            catch (Exception e)
            {
                throw (e);
            }
        }














    }
}
