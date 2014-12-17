using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ambitour
{
    public partial class frmUtilisateur : Form
    {
    /// <summary>
    /// Constructeur par défaut
    /// </summary>
        public frmUtilisateur()
        {
            InitializeComponent();
        }
    
      
        /// <summary>
        /// Au chargement du formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUtilisateur_Load(object sender, EventArgs e)
        {
            this.lblBienvenue.Text = SessionInfos.Utilisateur.Prenom;
            this.lblNom.Text = SessionInfos.Utilisateur.Nom;

            //pour aligner le Nom après le Prénom
            Size s = TextRenderer.MeasureText(lblBienvenue.Text, lblBienvenue.Font);
            this.lblNom.Location = new Point(lblBienvenue.Location.X + s.Width, lblNom.Location.Y);
            lblMode.Text = Pilotage.INSTANCE.Mode;

            //frmPreparation frmPreparation = new frmPreparation();
            //frmPreparation.BackColor = GUI.GraphicSettings.Default.BgColor;
            //frmPreparation.FormBorderStyle = GUI.GraphicSettings.Default.FormBorderStyle;
            //frmPreparation.MdiParent = this.MdiParent;
            //frmPreparation.Show();
            //frmPreparation.Size = GUI.GraphicSettings.Default.LeftPanelSize;
            //frmPreparation.FormBorderStyle = GUI.GraphicSettings.Default.FormBorderStyle;
            //frmPreparation.Location = GUI.GraphicSettings.Default.LeftPanelPoint;

            }
        }

}