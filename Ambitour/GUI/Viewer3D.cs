using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Ambitour
{
        /// <summary>
        /// Classe obligatoire pour pouvoir passer un param�tre au thread du viewer
        /// </summary>
        public class Viewer3D
        {
            private string document;

            // Constructeur
            public Viewer3D(string strDocument)
            {
                document = strDocument;
            }

            // Ex�cution de la m�thode du thread
            public void AfficheViewer()
            {
                Application.Run(new frm3D(document));
            }
        }  
}
