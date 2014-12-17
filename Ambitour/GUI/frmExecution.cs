using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ambitour
{
    public partial class frmExecution : Form
    {
        public frmExecution()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clic sur le bouton de pr�chauffage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrechauffage_Click(object sender, EventArgs e)
        {
            try
            {
                Pilotage.INSTANCE.DemandePrechauffage();
            }
            catch (Exception ex)
            {
            
            }
        }

        /// <summary>
        /// Affichage du bouton d'�x�cution si toutes les cases sont coch�es
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxPrechauffage_SelectedIndexChanged(object sender, EventArgs e)
        {     
            BtnPrechauffage.Enabled = (checkedListBoxPrechauffage.CheckedItems.Count == checkedListBoxPrechauffage.Items.Count);
        }
    }
}