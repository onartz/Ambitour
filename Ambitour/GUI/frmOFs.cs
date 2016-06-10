using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ambitour.CoucheMetier.ObjetsMetier;
using System.Linq;
using Ambitour.GUI;

namespace Ambitour
{
    public partial class frmOFs : Form
    {
        DataClassesDataContext ctx = new DataClassesDataContext();
        private BindingSource masterBindingSource = new BindingSource();
        private BindingSource detailsBindingSource = new BindingSource();

        public frmOFs()
        {
            InitializeComponent();
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Orientation = Orientation.Vertical;
            splitContainer1.Panel2.Controls.Add(splitContainer2);
        }

        private void frmOFs_Load(object sender, EventArgs e)
        {
          
           // txtProduct.DataBindings.Add("Text", bindingSource1, "ProductName");
            //txtWorkorderNo.DataBindings.Add("Text", bindingSource1, "WorkOrderNo");
            // Initialize other DataGridView properties.
           // this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            this.dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);

            //this.dataGridView2.Dock = DockStyle.Fill;
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            this.dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.DataSource = masterBindingSource;
            dataGridView2.DataSource = detailsBindingSource;

            GetData();

            // Adjust the row heights to accommodate the normal cell content.
            this.dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


        }

        void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }



        void GetData()
        {
            var result = from w in ctx.WorkOrder select w;
            masterBindingSource.DataSource = result;
            detailsBindingSource.DataSource = masterBindingSource.DataSource;
            detailsBindingSource.DataMember = "WorkOrderRouting";
           // bindingSource1.DataSource = masterBindingSource.DataSource;

            //txtWorkorderRoutingNo.DataBindings.Add("WorkOrderNo", bindingSource1, "WorkOrderNo");
        }

        void UpdateWorkOrderRoutings()
        {
            detailsBindingSource.DataSource = masterBindingSource.DataSource;
            detailsBindingSource.DataMember = "WorkOrderRouting";

        }

        void updateForm()
        {
            var result = from wor in ctx.WorkOrderRouting
                         join w in ctx.WorkOrder on wor.WorkOrderID equals w.WorkOrderID
                         where
                             wor.LocationID == 1 && wor.ActualEndDate == null
                         select new
                         {
                             WorkOrderNo = w.WorkOrderNo,
                             //ProductID = wor.ProductID,
                             ProductName = w.Product.Name,
                             OrderQty = w.OrderQty.ToString(),
                             WorkOrderRoutingNo = wor.WorkOrderRoutingNo,
                             ScheduledStartDate = wor.ScheduledStartDate,
                             ScheduledEndDate = wor.ScheduledEndDate,

                             StockedQty = w.StockedQty.ToString(),
                             ScrappedQty = w.ScrappedQty.ToString(),
                             ScrapReason = w.ScrapReasonID.ToString()
                         };
            bindingSource1.DataSource = result;
            dataGridView1.DataSource = result;


            //txtWorkorderRoutingNo.DataBindings.Add("WorkOrderRoutingNo", bindingSource1, "WorkOrderRoutingNo");

        }

        //void getCurrentPosition()
        //{

        //    int position = bindingSource1.Position;
        //    lblActualEndDate.Text = position.ToString();
        //}

     

     
        //private void showData()
        //{
        //    var result = from wor in ctx.WorkOrderRouting
        //                 join w in ctx.WorkOrder on wor.WorkOrderID equals w.WorkOrderID
        //                 where
        //                     wor.LocationID == 1 && wor.ActualEndDate == null
        //                 select new
        //                 {
        //                     //WorkOrderNo = w.WorkOrderNo,
        //                     ProductID = wor.ProductID
        //                     //ProductName = w.Product.Name
        //                     //OrderQty = w.OrderQty.ToString(),
        //                     //WorkOrderRoutingNo = wor.WorkOrderRoutingNo,
        //                     //ScheduledStartDate = wor.ScheduledStartDate,
        //                     //ScheduledEndDate = wor.ScheduledEndDate

        //                     //StockedQty = w.StockedQty.ToString(),
        //                     //ScrappedQty = w.ScrappedQty.ToString(),
        //                     //ScrapReason = w.ScrapReasonID.ToString()
        //                 };
        //    bindingSource1.DataSource = result;
        //    //Workorder section
        //    //txtProduct.DataBindings.Add("Text", bindingSource1, "ProductName");
        //    //txtWorkorderNo.DataBindings.Add("WorkOrderNo", bindingSource1, "WorkOrderNo");
        //    //txtOrderQty.DataBindings.Add("OrderQty", bindingSource1, "OrderQty");
        //    lblProduct.DataBindings.Add("ProductName", bindingSource1, "ProductName");
        //    //txtStockedQty.DataBindings.Add("StockedQty", bindingSource1, "StockedQty");
        //    //this.txtScrappedQty.DataBindings.Add("ScrappedQty", bindingSource1, "ScrappedQty");
        //    //WorkorderRouting section
        //    //txtWorkorderRoutingNo.DataBindings.Add("WorkOrderRoutingNo", bindingSource1, "WorkOrderRoutingNo");
        //    //txtScheduledStartDate.DataBindings.Add("ScheduledStartDate", bindingSource1, "ScheduledStartDate");
        //    //txtScheduledEndDate.DataBindings.Add("ScheduledEndDate", bindingSource1, "ScheduledEndDate");
        //}

     
    }



}
