using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ambitour
{
    public partial class frmSecurite : Form
    {
        public frmSecurite()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
      
             
            Pilotage.INSTANCE.InInventories[0].Quantity = 20;
       
        }

      

    }
}