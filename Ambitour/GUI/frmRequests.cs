using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ambitour.CoucheMetier.LogiqueMetier;
using Ambitour.CoucheMetier.ObjetsMetier;

namespace Ambitour.GUI
{
    public partial class frmRequests : Form
    {
        RequestBrokerAg rba;
        Dictionary<string, Object> requests;
        public delegate void UpdateFormHandler(KeyValuePair<string, Object> kvp);
        public delegate void UpdateFormHandler2();
        string selectedKey;
        Handle selectedHandle;

        public frmRequests()
        {
            InitializeComponent();
            this.components = new System.ComponentModel.Container();
           // this.contextMenu1 = new System.Windows.Forms.ContextMenu();

        }

        private void frmRequests_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Dock = DockStyle.Fill;
            listView1.Columns.Add("Id", 0);
            listView1.Columns.Add("Type", 100);
            listView1.Columns.Add("ProductLotId", 100);
            listView1.Columns.Add("Quantity", 100);
            listView1.Columns.Add("De", 100);
            listView1.Columns.Add("Vers", 100);
            rba = new RequestBrokerAg();
            rba.MessageReceived += new EventHandler<CoucheMetier.ObjetsMetier.RequestEventArgs>(rba_MessageReceived);
            requests = rba.Requests;
            updateForm();
        }

        void rba_MessageReceived(object sender, CoucheMetier.ObjetsMetier.RequestEventArgs e)
        {
            //NotifyIcon ni = new NotifyIcon(this.components);
            //ni.ContextMenu = this.ContextMenu;
            //ni.Text = "Alert";
            //ni.ShowBalloonTip(1000);
            //ni.Visible = true;
            //DialogResult dialogResult = MessageBox.Show(e.Content.Value,"New request", MessageBoxButtons.YesNo);
            //this.BeginInvoke(new UpdateFormHandler(updateForm), e.Content);
            this.BeginInvoke(new UpdateFormHandler2(updateForm));


            //if (dialogResult == DialogResult.Yes)
            //{
            //    rba.RemoveFromQueue(e.Content.Key);
            //}
            //else if (dialogResult == DialogResult.No)
            //{
            //    //do something else
            //}

            // MessageBox.Show(e.Content.Key + " "  + e.Content.Value);
        }

 

        //update form from request list
        private void updateForm()
        {
            listView1.Items.Clear();
            foreach(KeyValuePair<string, Object> kvp in rba.Requests){
                //string[] row = {};
                if (kvp.Value is Handle)
                {
                    Handle h = (Handle)kvp.Value;

                    string[] row = { kvp.Key, kvp.Value.GetType().Name, h.ProductLotId, h.Quantity.ToString(), h.Sender, h.Receiver };

                    ListViewItem listViewItem = new ListViewItem(row);
                    listView1.Items.Add(listViewItem);
                  //  this.notifyIcon1.ShowBalloonTip(3000);
                }
               // string[] row = { kvp.Key, kvp.Value.GetType().Name };
                //string[] row = { kvp.Key, kvp.Value.GetType().Name};
              
               // NotifyIcon ni = new NotifyIcon(this.components);
               // //ni.ContextMenu = this.ContextMenu;
               //// ni.Text = "Alert";
               // ni.BalloonTipTitle = "Nouvelle requete";
               // ni.BalloonTipText = "Nouvelle requete à traiter";
               // ni.BalloonTipIcon = ToolTipIcon.Error;
                
               // ni.Visible = true;
               // ni.ShowBalloonTip(3000);

            }
           
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            selectedKey = listView1.SelectedItems[0].Text;
            Object request = requests[selectedKey];

        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            Object obj = requests[selectedKey];
            MessageBox.Show("Are you sure", ((Handle)obj).ProductLotId);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count >0)
                MessageBox.Show(listView1.SelectedItems[0].ToString());
        }

       
    }
}
