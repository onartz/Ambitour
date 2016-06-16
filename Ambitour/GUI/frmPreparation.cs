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
using System.Diagnostics;
using Ambitour.CoucheMetier;
using Ambitour.CoucheMetier.ObjetsMetier;

namespace Ambitour
{
    public partial class frmPreparation : Form
    {
        #region Constantes
        const string MESSAGE_AUCUNDOSSIER = "Aucun dossier n'a été trouvé. Vous devez en copier un dans la file d'attente.";    
        const string MESSAGE_AUCUNDOSSIERCOURANT = "NC";
        const string MESSAGE_AUCUNPROGRAMMECOURANT = "NC";
        const string MESSAGE_AUCUNPROGRAMMEPIECETROUVE = "Pas de programme dans le dossier";
        const bool RechercheDansDossierTransfere = true;
        const bool RechercheDansDossierOrigine = false;
        #endregion

        #region Variables privées
        private DossierDeFabrication lDossierCourant;
        private ProgrammePiece lProgrammeCourant;
        private List<DossierDeFabrication> lListeDossiers;
        private List<ProgrammePiece> lListeProgrammes;
        private Utilisateur lUtilisateur=SessionInfos.Utilisateur;

        private ToolStripMenuItem toolStripMenuItemDocumentation;

        private delegate void UpdateForm(ProcessEventArgs state);

        #endregion

        #region Constructeurs
        /// <summary>
        /// Cosntructeur par défaut
        /// </summary>
        public frmPreparation()
        {
            InitializeComponent();
            //lUtilisateur = SessionInfos.Utilisateur;
            ////lListeDossiers = SessionInfos.Utilisateur.GetDossiers(CoucheMetier.GlobalSettings.Default.strQueuePath);
            //if (Pilotage.INSTANCE.Mode != "DEMO")
            //    lListeDossiers = Pilotage.INSTANCE.FileAttenteCFAO.GetDossiersByUser(lUtilisateur);;
            
            //lProgrammeCourant = lUtilisateur.ProgrammeCourant;
            //lDossierCourant = lUtilisateur.DossierCourant;
        }
        #endregion

        #region Méthodes publiques
        public void PreparerCN()
        {
            MessageBox.Show("PreparationCN");
            //TODO : réactiver sur TBI
           // Pilotage.INSTANCE.PreparerCN();
        }
        #endregion
        #region Méthodes privées

        /// <summary>
        /// Chargement du formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPreparation_Load(object sender, EventArgs e)
        {
            MenuStrip ms = (MenuStrip)this.MdiParent.Controls["menuStrip"];
            toolStripMenuItemDocumentation = (ToolStripMenuItem)ms.Items["documentationToolStripMenuItem"];
            switch (Pilotage.INSTANCE.Mode)
            {
                case "DEMO":
                    InitialiserModeDEMO();
                    break;
                default:
                    InitialiserModeCFAO();
                    break;
            }
            Pilotage.INSTANCE.StatusPreparationChanged += new EventHandler<ProcessEventArgs>(INSTANCE_StatusPreparationChanged);
        }

      

        /// <summary>
        /// Fermeture du Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPreparation_FormClosed(object sender, FormClosedEventArgs e)
        {
            //On se désabonne
            Pilotage.INSTANCE.StatusPreparationChanged -= new EventHandler<ProcessEventArgs>(INSTANCE_StatusPreparationChanged);
        }


        /// <summary>
        /// Clic sur refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //On vide le treeview
            treeView.Nodes.Clear();
            lblProgression.Text = "";
            //On recharge la liste des dossiers dont l'utilisateur est propriétaire
            lListeDossiers = Pilotage.INSTANCE.FileAttenteCFAO.GetDossiersByUser(lUtilisateur);
            InitialiserTreeview(lListeDossiers);
        }
        
        /// <summary>
        /// Méthode de mise à jour du form depuis un autre thread ou non
        /// </summary>
        /// <param name="state"></param>
        private void Update(ProcessEventArgs state)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateForm(Update), state);
                    return;
            }

            this.toolStripProgressBar.Visible = true;
            this.toolStripProgressBar.Value = state.Percentage; 
            this.toolStripStatusLabel.Text = state.Message;

            switch (state.Etape)
            {
                case 10:
                    lblProgression.ForeColor = System.Drawing.Color.Red;
                    break;
                default:
                    lblProgression.ForeColor = System.Drawing.Color.Black;
                    break;
            }

            this.lblProgression.Text += state.Message;
            this.lblProgression.Text += " - ";

            switch (state.Etape)
            { 
                    //Début de préparation
                case 1:
                    grpChoix.Enabled = false;
                    break;
                    //Dossier téléchargé
                case 2:
                    //Activation du menu Documentation
                   
                    toolStripMenuItemDocumentation.Visible = true;
                  
                    //Création du formulaire de documentation
                    frmDocumentation frmDocumentation = new frmDocumentation();
                    frmDocumentation.MdiParent = this.MdiParent;
                    frmDocumentation.BackColor = GUI.GraphicSettings.Default.BgColor;
                    frmDocumentation.FormBorderStyle = GUI.GraphicSettings.Default.FormBorderStyle;
                    frmDocumentation.Size = GUI.GraphicSettings.Default.LeftPanelSize;
                    frmDocumentation.Location = GUI.GraphicSettings.Default.LeftPanelPoint;

                    break;
                    //Fin de préparation mode DEMO
                case 6:
                    grpChoix.Enabled = true;
                    this.toolStripProgressBar.Visible = false;
                    this.toolStripProgressBar.Value = 0;
                    this.toolStripStatusLabel.Text = "Prêt";
                    this.lblProgression.Text = "";
                    break;
                    //fin de préparation mode CFAO
                case 7:
                    grpChoix.Enabled = true;
                    this.toolStripProgressBar.Visible = false;
                    this.toolStripProgressBar.Value = 0;
                    this.toolStripStatusLabel.Text = "Prêt";
                    this.lblProgression.Text = "";
                    treeView.Nodes.Clear();
                    //On recharge la liste des dossiers dont l'utilisateur est propriétaire
                    //lListeDossiers = SessionInfos.Utilisateur.GetDossiers(CoucheMetier.GlobalSettings.Default.strQueuePath);
                    lListeDossiers = Pilotage.INSTANCE.FileAttenteCFAO.GetDossiersByUser(lUtilisateur);
                    InitialiserTreeview(lListeDossiers);
                    break;
            }
        }

        /// <summary>
        /// Initialisation du treeview avec les dossiers de l'utilisateur
        /// </summary>
        /// <param name="listeDossiers">Liste des dossiers appartenant à l'utilisateur</param>
        private void InitialiserTreeview(List<DossierDeFabrication> listeDossiers)
        {
            if (listeDossiers.Count != 0)
            {
                foreach (DossierDeFabrication d in listeDossiers)
                {
                    TreeNode nodeDossier = new TreeNode(d.InfosDossierOrigine.Name, 1, 1);
                    treeView.Nodes.Add(nodeDossier);

                    foreach (ProgrammePiece p in d.GetProgrammesPiece())
                    {
                        TreeNode nodeProgramme = new TreeNode(p.InfosFichier.Name + " (%" + p.NumeroDeProgramme.ToString() + ")", 0, 0);
                        treeView.Nodes[nodeDossier.Index].Nodes.Add(nodeProgramme);
                    }
                }
            }
            treeView.ExpandAll();
        }


        /// <summary>
        /// Choix dans le TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            lblProgression.Text = "";
            if (e.Action == TreeViewAction.ByMouse)
            {
                //Suppression d'un éventuel formulaire de doc
                foreach (Form f in this.MdiParent.MdiChildren)
                {
                    if (f.Name == "frmDocumentation")
                    {
                        f.Close();
                        f.Dispose();
                    }
                }
                //choix d'un dossier
                if (e.Node.Level == 0)
                {
                    lDossierCourant = lListeDossiers[e.Node.Index];
                    lblDossierCourant.Text = lDossierCourant.InfosDossierOrigine.Name;
                    lListeProgrammes = lDossierCourant.GetProgrammesPiece();
                    ////Mise à jour du dossier courant de l'utilisateur
                    if (e.Node.Nodes.Count == 1)
                    {
                        lProgrammeCourant = lListeProgrammes[0];
                        lblProgramme.Text = lProgrammeCourant.InfosFichier.Name;
                        SessionInfos.Utilisateur.SetDossierCourant(lDossierCourant);
                        SessionInfos.Utilisateur.SetProgrammeCourant(lProgrammeCourant);
                        //Pilotage.INSTANCE.PreparerCN();
                    }
                }
                else
                {
                    lDossierCourant = lListeDossiers[e.Node.Parent.Index];
                    lblDossierCourant.Text = lDossierCourant.InfosDossierOrigine.Name;
                    lListeProgrammes = lDossierCourant.GetProgrammesPiece();
                    lProgrammeCourant = lListeProgrammes[e.Node.Index];
                    lblProgramme.Text = lProgrammeCourant.InfosFichier.Name;
                    SessionInfos.Utilisateur.SetDossierCourant(lDossierCourant);
                    SessionInfos.Utilisateur.SetProgrammeCourant(lProgrammeCourant);
                    //Pilotage.INSTANCE.PreparerCN();
                }
            }
        }


        /// <summary>
        /// Initialisation du Form pour le mode DEMO
        /// </summary>
        private void InitialiserModeDEMO()
        {
            lblProgression.Text = "";
            grpChoix.Visible = false;
            lblDossierCourant.Text = SessionInfos.Utilisateur.DossierCourant.InfosDossierOrigine.Name;
            lblProgramme.Text = SessionInfos.Utilisateur.ProgrammeCourant.InfosFichier.Name;
            Pilotage.INSTANCE.PreparerCN();
        }

        /// <summary>
        /// Initialisation du Form pour le mode OFs
        /// 
        /// </summary>
        /// <param name="wo"></param>
        public void InitialiserModeOF(OF wo)
        {
            lblProgression.Text = "";
            lblDossierCourant.Text = MESSAGE_AUCUNDOSSIERCOURANT;
            //richTextBox1.Text += CoucheMetier.GlobalSettings.Default.strQueuePath;
            //btnRefresh.Text += CoucheMetier.GlobalSettings.Default.strQueuePath;
            grpChoix.Visible = false;

            toolStripProgressBar.Visible = true;
            toolStripProgressBar.Value = 0;
            toolStripStatusLabel.Text = "Prêt";

            //On vide le treeview
            treeView.Nodes.Clear();
            
            //On recharge le dossier correspondant à l'OF
            try
            {
                DossierDeFabrication df = new DossierDeFabrication(Path.Combine(GlobalSettings.Default.repertoireDossiersAmbiflux, wo.ProductId.ToString()));
                ProgrammePiece pp = df.GetProgrammesPiece()[0];
                SessionInfos.Utilisateur.SetDossierCourant(df);
                SessionInfos.Utilisateur.SetProgrammeCourant(pp);
                lDossierCourant = df;
                lblDossierCourant.Text = lDossierCourant.InfosDossierOrigine.Name;
                lblProgramme.ForeColor = System.Drawing.Color.Green;
                lblProgramme.Text = pp.InfosFichier.Name;
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Il semble que le dossier de fabrication du produit n'existe pas.\r\n Créez un dossier de fabrication " +
                    Path.Combine(GlobalSettings.Default.repertoireDossiersAmbiflux, wo.ProductId.ToString()));
                return;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Il semble que le programme de fabrication du produit n'existe pas.\r\n Créez un programme dans le dossier de fabrication " +
                   Path.Combine(GlobalSettings.Default.repertoireDossiersAmbiflux, wo.ProductId.ToString()));
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
            //on peut lancer la préparation
            Pilotage.INSTANCE.PreparerCN();
                   
        }

        /// <summary>
        /// Initialisation du Form pour le mode courant
        /// </summary>
        private void InitialiserModeCFAO()
        {
            lblProgression.Text = "";
            lblDossierCourant.Text = MESSAGE_AUCUNDOSSIERCOURANT;
            richTextBox1.Text += CoucheMetier.GlobalSettings.Default.strQueuePath;
            btnRefresh.Text += CoucheMetier.GlobalSettings.Default.strQueuePath;
            grpChoix.Visible = true;

            toolStripProgressBar.Visible = false;
            toolStripProgressBar.Value = 0;
            toolStripStatusLabel.Text = "Prêt";

            //On vide le treeview
            treeView.Nodes.Clear();
            //On recharge la liste des dossiers dont l'utilisateur est propriétaire
            lListeDossiers = Pilotage.INSTANCE.FileAttenteCFAO.GetDossiersByUser(lUtilisateur);
            //lListeDossiers = SessionInfos.Utilisateur.GetDossiers(CoucheMetier.GlobalSettings.Default.strQueuePath);
            //On initialise le treeview
            InitialiserTreeview(lListeDossiers);

            switch (lListeDossiers.Count)
            {
                case 0:
                    this.toolStripStatusLabel.Text = MESSAGE_AUCUNDOSSIER;
                    lblDossierCourant.Text = MESSAGE_AUCUNDOSSIERCOURANT;
                    lblProgramme.Text = MESSAGE_AUCUNPROGRAMMECOURANT;
                    break;
                case 1:
                    lDossierCourant = lListeDossiers[0];
                    lblDossierCourant.Text = lDossierCourant.InfosDossierOrigine.Name;
                    //on regardes s'il existe un ou plusieurs programmes pièce
                    lListeProgrammes = lDossierCourant.GetProgrammesPiece();

                    switch (lListeProgrammes.Count)
                    {
                        case 0:
                            this.toolStripStatusLabel.Text = MESSAGE_AUCUNPROGRAMMEPIECETROUVE;
                            break;
                        case 1:
                            lblProgramme.ForeColor = System.Drawing.Color.Green;
                            lblProgramme.Text = SessionInfos.Utilisateur.ProgrammeCourant.InfosFichier.Name;
                            //on peut lancer la préparation
                            //grpChoix.Enabled = false;
                            //TODO: check if OK
                             Pilotage.INSTANCE.PreparerCN();
                            break;
                    }
                    break;
            }
        }

        #endregion

        #region Evènements
        /// <summary>
        /// Sur évènement StatusPreparationChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void INSTANCE_StatusPreparationChanged(object sender, ProcessEventArgs e)
        {        
            try
            { 
                   Update(e);                
            }
            catch (Exception ex)
            {
                Trace.TraceError(DateTime.Now + " : " + ex.Message);
                return;
            }
        }
        #endregion
  
    }
}