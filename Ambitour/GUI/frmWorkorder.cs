using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ambitour.GUI
{
    public partial class frmWorkorder : Form
    {
        Object dataSource;

        public Object DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }
        public frmWorkorder()
        {
            InitializeComponent();
        }

        private void frmWorkorder_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = dataSource;
           
        }

        private void refresh()
        {
            txtWorkorderNo.DataBindings.Add("Text", bindingSource1, "WorkOrderRoutingNo");
        }
    }
}
