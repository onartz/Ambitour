using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ambitour
{
    public partial class frmCN : Form
    {
        private delegate void UpdateForm(Num1050.CNEventArgs state);
       
        
        private const string MESSAGE_OUI = "Oui";
        private const string MESSAGE_NON = "Non";
        private const string MESSAGE_POM = "POM Obligatoires";
        private const string MESSAGE_PRECHAUFFAGE = "Préchauffage conseillé";


        #region Constructeurs
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public frmCN()
        {
            InitializeComponent();
            Num1050.INSTANCE.StatusChanged += new EventHandler<Num1050.CNEventArgs>(INSTANCE_NotifierEtat);
        }
        #endregion

        #region Méthodes privées

        /// <summary>
        /// Au chargement du Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCN_Load(object sender, EventArgs e)
        {
            this.lblMessage.Visible = false;
        }

        private void frmCN_FormClosed(object sender, FormClosedEventArgs e)
        {
            Num1050.INSTANCE.StatusChanged -= new EventHandler<Num1050.CNEventArgs>(INSTANCE_NotifierEtat);
        }

        /// <summary>
        /// Méthode de mise à jour du form depuis un autre thread ou non
        /// </summary>
        /// <param name="state"></param>
        private void Update(Num1050.CNEventArgs state)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateForm(Update), state);
                return;
            }

            lblMode.Text = state.Etat.Mode;
            lblNumeroProgrammeCourant.Text = state.Etat.Programme.ToString();
            lblEnCycle.Text = state.Etat.EnCycle.ToString();

            if (!state.Etat.POMOK)
            {
                lblMessage.Visible = true;
                lblMessage.Text = MESSAGE_POM;
                return;
            }
            if (state.Etat.DemandePrechauffage)
            {
                lblMessage.Visible = true;
                lblMessage.Text = MESSAGE_PRECHAUFFAGE;
                return;
            }
        }

        /// <summary>
        /// Lorsque l'état de la CN a changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void INSTANCE_NotifierEtat(object sender, Num1050.CNEventArgs e)
        {           
            try
            {
                Update(e);
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                return;
            }
        }

     
        #endregion

     
    }
}