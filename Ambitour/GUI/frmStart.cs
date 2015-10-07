using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Ambitour
{
    public partial class frmStart : Form
    {
        private delegate void dlgUpdateLabelStatusCN(Num1050.InfosCN infos);
        private delegate void dlgUpdateLecteurStatusLabel(string message);

        private string GetLecteurInvokerParameter(string message)
        {
            string dlgParameter = message;
            return dlgParameter;
        }

        /// <summary>
        /// Méthode de mise à jour d'un champs du formulaire
        /// </summary>
        /// <param name="text"></param>
        private void UpdateLecteurStatusLabel(string message)
        {
            this.labelStatusBadge.Text = message;
        }

        /// <summary>
        /// Constructeur du formulaire
        /// </summary>
        public frmStart()
        {
            InitializeComponent();
        }      

       

        /// <summary>
        /// Au chargement du formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Load(object sender, EventArgs e)
        {
            Pilotage.INSTANCE.StatusLecteurChanged += new EventHandler<CustomEventArgs>(INSTANCE_EventLecteur);
            Pilotage.INSTANCE.StatusCNChanged += new EventHandler<Num1050.CNEventArgs>(INSTANCE_EventNUM);
            Pilotage.INSTANCE.CNLogChanged += new EventHandler<Num1050.CNEventArgs>(INSTANCE_CNLogChanged);
        }

        void INSTANCE_CNLogChanged(object sender, Num1050.CNEventArgs e)
        {
            throw new NotImplementedException();
        }

        void INSTANCE_EventNUM(object sender, Num1050.CNEventArgs e)
        {
            throw new NotImplementedException();
        }

        void INSTANCE_EventLecteur(object sender, CustomEventArgs e)
        {
            string m = GetLecteurInvokerParameter(e.Message);
            try
            {
                Invoke(new dlgUpdateLecteurStatusLabel(UpdateLecteurStatusLabel), m);
            }
            catch(Exception ex)
            {
                DialogResult r = MessageBox.Show(ex.Message, "Start", MessageBoxButtons.OK);
            };     
            
        }

      

      
        /// <summary>
        /// Sur double clic sur l'icone de la barre de notification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Show the form when the user double clicks on the notify icon.

            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            // Activate the form.
            this.Show();

        }

        /// <summary>
        /// Clic sur Quitter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitter_Click(object sender, EventArgs e)
        {
            //BD.CONNEXION.EnregistrerConnexionStop();
            //Ambitour.LecteurBadge.CarteLue -= new EventHandler<LecteurBadge.CustomEventArgs>(OnCarteLue);
            //Ambitour.LecteurBadge.CarteLue -= new EventHandler<LecteurBadge.CustomEventArgs>(LecteurBadge_statusChanged);
            
            //Num1050.INSTANCE.NotifierEtat -= new EventHandler<Num1050.CNEventArgs>(INSTANCE_NotifierEtat);
            //Ambitour.LecteurBadge.Stop();
            //Num1050.INSTANCE.stop();
   
        }

        ///// <summary>
        ///// Le status du lecteur a changé
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void LecteurBadge_statusChanged(object sender, LecteurBadge.CustomEventArgs e)
        //{
        //    string m = GetLecteurInvokerParameter(e.Message);
        //    try
        //    {
        //        Invoke(new dlgUpdateLecteurStatusLabel(UpdateLecteurStatusLabel), m);
        //    }
        //    catch {
        //        throw (new Exception("Erreur Invoke"));
        //    }
        //}


        private object[] GetInvokerParameters(Num1050.InfosCN p)
        {
            object[] delegateParameter = new object[1];
            delegateParameter[0] = p;
            return delegateParameter;
        }

        /// <summary>
        /// Méthode appelée sur évènement de NUM1050
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void INSTANCE_NotifierEtat(object sender, Num1050.CNEventArgs e)
        {
            object[] p = GetInvokerParameters(e.Etat);
            try
            {
                Invoke(new dlgUpdateLabelStatusCN(UpdateLabelStatus), p);
            }
            catch (Exception ex)
            {
                DialogResult r = MessageBox.Show(ex.Message, "Start2", MessageBoxButtons.OK);
            };     
        }

        /// <summary>
        /// Méthode de mise à jour d'un champs du formulaire
        /// </summary>
        /// <param name="text"></param>
        private void UpdateLabelStatus(Num1050.InfosCN p)
        {
            if (p.IsConnected)
                this.LabelStatusCN.Text = "CN connectée";
            else
                this.LabelStatusCN.Text = "CN déconnectée";
        }
     
     

        /// <summary>
        /// Clic sur Fermer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        /// <summary>
        /// Clic sur Quitter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitter_Click_1(object sender, EventArgs e)
        {
            DialogResult dr=MessageBox.Show("Etes-vous sur de vouloir fermer l'application?", "Quitter", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                Pilotage.INSTANCE.Stop();
                this.Close();
                this.Dispose();
            }
        }    
    }
}