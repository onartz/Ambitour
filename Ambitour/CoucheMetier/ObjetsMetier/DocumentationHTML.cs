using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Ambitour
{
   public class DocumentationHTML
    {
       /// <summary>
       /// Arborescence HTML
       /// </summary>
        private DirectoryInfo infosDossier;
        public DirectoryInfo InfosDossier
        {
            get
            { return infosDossier; }set{infosDossier = value;}
        }
       /// <summary>
       /// Fichier html sur lequel pointer
       /// </summary>
       private string fichierIndex;
       public string FichierIndex
       {
           get
           {
               return fichierIndex;
           }
           set
           {
               fichierIndex = value;
           }
       }

    }

}
