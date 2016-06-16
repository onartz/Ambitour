using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ambitour.CoucheMetier.ObjetsMetier;
using System.Diagnostics;

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
                Trace.TraceError(DateTime.Now + " : " + ex.Message);
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

        private void button4_Click(object sender, EventArgs e)
        {
            //Log.Write("Send socket to server");
            string dest = "invTBI540-1@" + textServerAddress.Text + ":1099/JADE";
            string request = String.Format("(REQUEST\r\n :receiver  (set ( agent-identifier :name {0} ) )\r\n :content  \"((action (agent-identifier :name {0}) (UpdateQuantity\r\n :command Add :qty 5)))\"\r\n  :language  fipa-sl  :ontology  ambiflux-logistic )", dest);
            try
            {
                String res = Ambitour.CoucheMetier.LogiqueMetier.ProxySocket.SocketSend(textServerAddress.Text, 6789, request);
                //Log.Write("res = " + res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// To generate a new OF and store it in the incoming directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateOF_Click(object sender, EventArgs e)
        {
            int productId = 6;
            OF.Save(OF.Generate(productId), Ambitour.CoucheMetier.GlobalSettings.Default.incomingOFDirectory);
        }


    }
}
