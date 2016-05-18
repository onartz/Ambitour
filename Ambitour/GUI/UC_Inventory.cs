﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ambitour.CoucheMetier.ObjetsMetier;
using Ambitour.CoucheMetier.LogiqueMetier;
using Ambitour.CoucheMetier;
using System.Net.Sockets;
using System.Threading;


namespace Ambitour.GUI
{
    public partial class UC_Inventory : UserControl
    {
        ProductInventory productInventory;
        int value;

       public ProductInventory ProductInventory
        {
            get { return productInventory; }
            set { productInventory = value; }
        }

     
        public UC_Inventory()
        {
            InitializeComponent();  
        }

        /// <summary>
        /// When inventory property changed, update form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void productInventory_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            updateForm();
        }

        /// <summary>
        /// Send a message to proxy Agent tu update quantity of agent
        /// </summary>
        private void sendSocket()
        {
            String command = "";
            if (productInventory.Type == ProductInventory.inout.INPUT)
                command = "Remove";
            else
                command = "Add";
                
            string dest = "invTBI540-" + productInventory.ProductID.ToString() + "@" + GlobalSettings.Default.jadeServerAddress + ":1099/JADE";
            string request = String.Format("(REQUEST\r\n :receiver  (set ( agent-identifier :name {0} ) )\r\n :content  \"((action (agent-identifier :name {0}) (UpdateQuantity\r\n :command {1} :qty {2})))\"\r\n  :language  fipa-sl  :ontology  ambiflux-logistic )", dest, command, this.value.ToString());
            string result = "";
            value = 0;
            try
            {
                result = ProxySocket.SocketSend(GlobalSettings.Default.jadeServerAddress, 6789, request);
            }
            catch (SocketException ex)
            {
                //errorList.Add(ex.Message);
                //updateLogView();
        
                return;
            }
            
        }


        public UC_Inventory(ProductInventory productInventory)
            : this()
        {
            this.productInventory = productInventory;
            //getFromDB();
        }

        public void Initialize(ProductInventory productInventory)
        {
            this.productInventory = productInventory;
            productInventory.PropertyChanged += new PropertyChangedEventHandler(productInventory_PropertyChanged);
            updateForm();
           // getFromDB();
        }

        //private void getFromDB()
        //{
        //    DataClassesDataContext dc = new DataClassesDataContext();
        //    productInventory = (from pi in dc.ProductInventory
        //                        where (pi.ProductID == productInventory.ProductID && pi.LocationID == productInventory.LocationID)
        //                        select pi).SingleOrDefault();
        //    if(productInventory != null)
        //        updateForm(); 
           
        //}

        private void btnPickPlace_Click(object sender, EventArgs e)
        {
            value = (int)udQty.Value;
            try
            {
                Thread t = new Thread(new ThreadStart(this.sendSocket));
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de socket");
            }

            if (productInventory.Type == ProductInventory.inout.INPUT)
            {
                if (productInventory.Modify((Int16)(-(udQty.Value))))
                    updateForm();
            }
            else
                if (productInventory.Modify((Int16)((udQty.Value))))
                    updateForm();
        }


        private void updateForm(){
            if (productInventory.Type == ProductInventory.inout.INPUT)
                btnPickPlace.Text = "Pick";
            else
                btnPickPlace.Text = "Place";
            lblInventoryId.Text = "invTBI540-" + productInventory.ProductID.ToString();
            txtInventoryLevel.Text = productInventory.Quantity.ToString();
            udQty.Value = 0;

        }
    }
}