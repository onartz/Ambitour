using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace Ambitour
{
    public partial class frm3D : Form
    {
        private string document;
        public string Document
        {
            get { return document; }
            set { document = value; }
        }

        /// <summary>
        /// constructeur simple
        /// </summary>
        public frm3D()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructeur surchargé
        /// </summary>
        /// <param name="documentFile"></param>
        public frm3D(string documentFile)
        {
            InitializeComponent();
            this.document = documentFile;
        }
     
        /// <summary>
        /// Au chargement du formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm3D_Load(object sender, EventArgs e)
        {
            this.axVIA3DXMLPlugin1.DocumentFile = document;         
        }  
    }
}