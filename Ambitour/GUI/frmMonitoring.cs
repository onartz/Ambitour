using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ambitour
{
    public partial class frmMonitoring : Form
    {
        private delegate void UpdateForm(Num1050.CNEventArgs state);
        private const string MESSAGE_OUI = "Oui";
        private const string MESSAGE_NON = "Non";


        #region Constructeurs
        public frmMonitoring()
        {
            InitializeComponent();
            Num1050.INSTANCE.StatusChanged += new EventHandler<Num1050.CNEventArgs>(INSTANCE_NotifierEtat);
        }
        #endregion

        #region Méthodes privées
        private void Update(Num1050.CNEventArgs state)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateForm(Update), state);
                return;
            }
            this.lblDernierArretBroche.Text = state.Etat.DernierStopBroche.ToString();
            this.lblBrocheTourne.Text = state.Etat.BrocheTourne.ToString();
            this.lblDemandePrechauffage.Text = state.Etat.DemandePrechauffage.ToString();
        }

        

        /// <summary>
        /// Gestion évènement venant de la classe Num1050
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
        /// <summary>
        /// Fermeture du Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMonitoring_FormClosed(object sender, FormClosedEventArgs e)
        {
            Num1050.INSTANCE.StatusChanged += new EventHandler<Num1050.CNEventArgs>(INSTANCE_NotifierEtat);
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            ProgrammePiece p =new ProgrammePiece("c:\\ambitour\\queue\\clio2\\dylcharles.xpi");
            RetourFonction r = Num1050.INSTANCE.ChargerProg(p);
            textBox1.Text = r.MessageErreur.ToString() + " %" + p.NumeroDeProgramme.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProgrammePiece p =new ProgrammePiece("c:\\ambitour\\queue\\clio2\\dylcharles.xpi");
            p.NumeroDeProgramme = System.Convert.ToInt32(textBox2.Text);
            RetourFonction r = Num1050.INSTANCE.SupprimerProg(p);
            textBox3.Text = r.MessageErreur.ToString();
        }

  
  
        private void button3_Click(object sender, EventArgs e)
        {
            RetourFonction r = Num1050.INSTANCE.SetProgCourant(System.Convert.ToInt32(textBox5.Text));
            textBox4.Text = r.MessageErreur.ToString();
        }


    }
}
