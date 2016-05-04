using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.IO;
using Ambitour.GUI;
using Ambitour.CoucheMetier;
using Ambitour.CoucheMetier.ObjetsMetier;
using Ambitour.CoucheMetier.LogiqueMetier;
using System.Collections.Concurrent;

namespace Ambitour
{
    public partial class frmPrincipal : Form
    {

        IncomingMessageHandler imRequestHandler;
        //A concurrent FIFO Queue to store incoming requests
        //ConcurrentQueue<ACLMessage> queue;
         /// <summary>
         /// Constructeur
         /// </summary>
        public frmPrincipal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Au chargement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPrincipal_Load(object sender, EventArgs e)
        {

            ////Création du form Utilisateur (Haut)
            frmUtilisateur frmUtilisateur = new frmUtilisateur();
            frmUtilisateur.MdiParent = this;
            frmUtilisateur.BackColor = GUI.GraphicSettings.Default.BgColor;
            frmUtilisateur.FormBorderStyle = GUI.GraphicSettings.Default.FormBorderStyle;
            frmUtilisateur.Size = GUI.GraphicSettings.Default.TopPanelSize;
            frmUtilisateur.Location = GUI.GraphicSettings.Default.TopPanelPoint;

            frmUtilisateur.Show();

            frmPreparation frmPreparation = new frmPreparation();
            frmPreparation.BackColor = GUI.GraphicSettings.Default.BgColor;
            frmPreparation.FormBorderStyle = GUI.GraphicSettings.Default.FormBorderStyle;
            frmPreparation.MdiParent = this;
            frmPreparation.Size = GUI.GraphicSettings.Default.LeftPanelSize;
            frmPreparation.FormBorderStyle = GUI.GraphicSettings.Default.FormBorderStyle;
            frmPreparation.Location = GUI.GraphicSettings.Default.LeftPanelPoint;

            frmPreparation.Show();

            //Création et affichage du form CN (Droite)
            frmCN frmCN = new frmCN();
            frmCN.MdiParent = this;
            frmCN.Show();
            frmCN.BackColor = GUI.GraphicSettings.Default.BgColor;
            frmCN.FormBorderStyle = GUI.GraphicSettings.Default.FormBorderStyle;
            frmCN.Size = GUI.GraphicSettings.Default.RightPanelSize;
            frmCN.Location = GUI.GraphicSettings.Default.RightPanelPoint;

            //Création du form Securité (Centre)
            frmSecurite frmSecurite = new frmSecurite();
            frmSecurite.MdiParent = this;
            frmSecurite.BackColor = GUI.GraphicSettings.Default.BgColor;
            frmSecurite.FormBorderStyle = GUI.GraphicSettings.Default.FormBorderStyle;
            frmSecurite.Size = GUI.GraphicSettings.Default.LeftPanelSize;
            frmSecurite.Location = GUI.GraphicSettings.Default.LeftPanelPoint;

            //Création du form Execution (Centre)
            frmExecution frmExecution = new frmExecution();
            frmExecution.MdiParent = this;
            frmExecution.BackColor = GUI.GraphicSettings.Default.BgColor;
            frmExecution.FormBorderStyle = GUI.GraphicSettings.Default.FormBorderStyle;
            frmExecution.Size = GUI.GraphicSettings.Default.LeftPanelSize;
            frmExecution.Location = GUI.GraphicSettings.Default.LeftPanelPoint;


            //Création du form Monitoring (Centre)
            frmMonitoring frmMonitoring = new frmMonitoring();
            frmMonitoring.MdiParent = this;
            frmMonitoring.BackColor = GUI.GraphicSettings.Default.BgColor;
            frmMonitoring.FormBorderStyle = GUI.GraphicSettings.Default.FormBorderStyle;
            frmMonitoring.Size = GUI.GraphicSettings.Default.LeftPanelSize;
            frmMonitoring.Location = GUI.GraphicSettings.Default.LeftPanelPoint;

            monitoringToolStripMenuItem.Visible = (SessionInfos.Utilisateur.Role == "Personnel");

            frmOF frmOf = new frmOF();
            frmOf.MdiParent = this;
            frmOf.BackColor = GUI.GraphicSettings.Default.BgColor;
            frmOf.FormBorderStyle = GUI.GraphicSettings.Default.FormBorderStyle;
            frmOf.Size = GUI.GraphicSettings.Default.LeftPanelSize;
            frmOf.Location = GUI.GraphicSettings.Default.LeftPanelPoint;

 
            //queue = new ConcurrentQueue<ACLMessage>();
            imRequestHandler = new IncomingMessageHandler(GlobalSettings.Default.incomingRequestDirectory);
            imRequestHandler.NewMessageReceived += new NewMessageEventHandler(imRequestHandler_NewMessageReceived);
          


            frmRequests frmRequests = new frmRequests(ref imRequestHandler);
            frmRequests.MdiParent = this;
            frmRequests.BackColor = GUI.GraphicSettings.Default.BgColor;
            frmRequests.FormBorderStyle = GUI.GraphicSettings.Default.FormBorderStyle;
            frmRequests.Size = GUI.GraphicSettings.Default.LeftPanelSize;
            frmRequests.Location = GUI.GraphicSettings.Default.LeftPanelPoint;

            notifyIcon1.Icon = SystemIcons.Application;


        }

        void imRequestHandler_NewMessageReceived(object sender, ObjectEventArgs e)
        {
            //blavla

            ACLMessage msg = (ACLMessage)(e.Content);
            notifyIcon1.ShowBalloonTip(5000, msg.ConversationId, msg.Content.GetType().Name, ToolTipIcon.Info);
        }

      

        /// <summary>
        /// Clic sur préparation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void préparationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {     
                if (f.Name == "frmPreparation")
                {
                    f.Show();
                    f.BringToFront();
                    break;
                }
            }
        }

        /// <summary>
        /// Clic sur documentation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "frmDocumentation")
                {
                    f.Show();
                    f.BringToFront();
                    break;
                }
            }
        }


        /// <summary>
        /// Clic sur Quitter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pilotage.INSTANCE.FermerSession();
            foreach (Form f in this.MdiChildren)
            {
                f.Close();
            }


            this.Dispose();

        }
        /// <summary>
        /// Clic sur Sécurité
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sécuritéToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "frmSecurite")
                {
                    f.Show();
                    f.BringToFront();
                    break;
                }
            }
        }

  
        /// <summary>
        /// Clic sur Execution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exécecutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "frmExecution")
                {
                    f.Show();
                    f.BringToFront();
                    break;
                }
            }
        }

        /// <summary>
        /// Clic sur Monitoring
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monitoringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "frmMonitoring")
                {
                    f.Show();
                    f.BringToFront();
                    break;
                }
            }
        }

        /// <summary>
        /// CLic sur Aide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sommaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, ".\\aideambitour\\ambitour.chm");
        }

        /// <summary>
        /// Clic sur A Propos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void àproposdeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAboutBox frmAbout = new frmAboutBox();
            frmAbout.Show();
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void OFsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmOF))
                {
                    f.Show();
                    f.BringToFront();
                    break;
                }
            }
        }

        private void requêtesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "frmRequests")
                {
                    f.Show();
                    f.BringToFront();
                    break;
                }
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmRequests))
                {
                    if (this.ActiveMdiChild != f)
                    {
                        // ((frmRequests)f).Initialize();
                        f.Show();
                        f.BringToFront();
                        break;
                    }
                }
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == typeof(frmRequests))
                {
                    if(this.ActiveMdiChild != f)
                    // ((frmRequests)f).Initialize();
                    f.Show();
                    f.BringToFront();
                    break;
                }
            }
        }

     
    }
}
