using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ambitour
{
    public partial class frmSelUtilisateur : Form
    {

        const string MESSAGE_AUCUNUTILISATEUR = "Aucun utilisateur trouvé dans l'AD pour ";
        const string MESSAGE_PLUSIEURSUTILISATEURS = "Plusieurs logins ont été trouvés. Veuillez vous identifier.";


        private List<Utilisateur> listeUtilisateurs;

        private Utilisateur utilisateur;
        public Utilisateur Utilisateur
        {
            get { return utilisateur; }
            set { utilisateur = value; }
        }

       
        public frmSelUtilisateur(List<Utilisateur> pListeUtilisateurs)
        {        
            InitializeComponent();
            listeUtilisateurs = pListeUtilisateurs;
        }

        private void frmSelUtilisateur_Load(object sender, EventArgs e)
        {
            foreach (Utilisateur u in listeUtilisateurs)
                listBox1.Items.Add(u.Login);
            listBox1.Visible = true;
            lblMessage.Visible = true;
            lblMessage.Text = MESSAGE_PLUSIEURSUTILISATEURS;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            utilisateur = (Utilisateur)listeUtilisateurs[listBox1.SelectedIndex];
            this.DialogResult = DialogResult.OK;
        }




    }
}
