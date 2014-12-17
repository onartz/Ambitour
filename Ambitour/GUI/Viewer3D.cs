using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Ambitour
{
        /// <summary>
        /// Classe obligatoire pour pouvoir passer un paramètre au thread du viewer
        /// </summary>
        public class Viewer3D
        {
            private string document;

            // Constructeur
            public Viewer3D(string strDocument)
            {
                document = strDocument;
            }

            // Exécution de la méthode du thread
            public void AfficheViewer()
            {
                Application.Run(new frm3D(document));
            }
        }  
}
