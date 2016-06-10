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
    public partial class frmWorkorderDetails : Form
    {
        WorkOrder wo; 
        
        Object dataSource;

        public Object DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }

        public WorkOrder Wo
        {
            get { return wo; }
            set { wo = value; }
        }
        WorkOrderRouting wor;

        public WorkOrderRouting Wor
        {
            get { return wor; }
            set { wor = value; }
        }
        public frmWorkorderDetails()
        {
            InitializeComponent();
            wo = new WorkOrder();
            wor = new WorkOrderRouting();
        }

        private void frmWorkorderDetails_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = dataSource;
            refresh();
        }

        private void refresh()
        {
            //txtWorkorderRoutingNo.DataBindings.Add("Text", bindingSource1, "WorkOrderRoutingNo");
            //lblScheduledStartDate.DataBindings.Add("Text", bindingSource1, "ScheduledStartDate");

        }

   
    }
}
