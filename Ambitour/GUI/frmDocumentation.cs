using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Ambitour
{
    public partial class frmDocumentation : Form
    {        
        private List<DocumentationHTML> listeDocumentationHTML;
        private List<ProgrammePiece> listeProgrammes;
        private List<FileInfo> listeFichiers;

        /// <summary>
        /// Constructeur
        /// </summary>
        public frmDocumentation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Au chargement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Documentation_Load(object sender, EventArgs e)
        {
            InitialiserTreeview();
            richTextBoxProgramme.LoadFile(SessionInfos.Utilisateur.ProgrammeCourant.InfosFichier.FullName, RichTextBoxStreamType.PlainText);
 
        }

        /// <summary>
        /// Initialisation du treeview
        /// </summary>
        private void InitialiserTreeview()
        {   
            treeView1.Nodes.Clear();
            int i = 0;

            //Recherche des dossiers CATIA
            listeDocumentationHTML = SessionInfos.Utilisateur.DossierCourant.GetDocumentationsHTML();
            if (listeDocumentationHTML.Count != 0)
            {
                treeView1.Nodes.Add("Doc CATIA");
                treeView1.Nodes[i].ImageIndex = 2;

                foreach (DocumentationHTML d in listeDocumentationHTML)
                {
                    TreeNode t = new TreeNode(d.InfosDossier.Name);
                    t.ImageIndex = 2;
                    treeView1.Nodes[i].Nodes.Add(t);
                }

                i++;
            }
            //Recherche des fichiers 3DXMl
            listeFichiers = SessionInfos.Utilisateur.DossierCourant.GetFichiers("*.3Dxml");
            if (listeFichiers.Count != 0)
            {
                treeView1.Nodes.Add("3DXML");
                treeView1.Nodes[i].ImageIndex = 3;
                foreach (FileInfo f in listeFichiers)
                {
                    TreeNode t = new TreeNode(f.Name);
                    t.ImageIndex = 3;
                    treeView1.Nodes[1].Nodes.Add(t);
                }
                i++;
            }
    
            treeView1.ExpandAll();
        }

        /// <summary>
        /// Clic dans un élément du treeview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {          
            if (e.Action == TreeViewAction.ByMouse)
            {
                treeView1.SelectedImageIndex = e.Node.ImageIndex;

                try
                {
                    switch (e.Node.Parent.Index)
                    {
                        case 0:
                            ProcessStartInfo startInfo = new ProcessStartInfo("IExplore.exe");
                            
                            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                            Process.Start(startInfo);
                            startInfo.Arguments = listeDocumentationHTML[e.Node.Index].FichierIndex;
                            Process.Start(startInfo);

                            //System.Diagnostics.Process.Start("IExplore.exe", listeDocumentationHTML[e.Node.Index].FichierIndex);
                            break;
                      
                        case 1:                           
                            Viewer3D v = new Viewer3D(listeFichiers[e.Node.Index].FullName);
                            Thread t = new Thread(new ThreadStart(v.AfficheViewer));
                            t.SetApartmentState(ApartmentState.STA);
                            t.Start();
                            break;                        
                    }
                }
                catch
                {}
            }     
        }
    }
}