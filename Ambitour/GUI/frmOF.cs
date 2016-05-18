using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Ambitour.CoucheMetier;
using Ambitour.CoucheMetier.ObjetsMetier;
using Ambitour.CoucheMetier.LogiqueMetier;


namespace Ambitour
{
    public partial class frmOF : Form
    {
        
        
       // Array errorList;
        List<String> errorList;

        ConcurrentDictionary<string, OF> cd = new ConcurrentDictionary<string, OF>();
        string[] incomingFiles;
        string INPUT_DIR = GlobalSettings.Default.incomingOFDirectory;
        string TEMP_DIR = GlobalSettings.Default.tempOFDirectory;
        string ARCHIVE_DIR = GlobalSettings.Default.archivesOFDirectory;
        string OUTPUT_DIR = GlobalSettings.Default.outgoingOFDirectory;
        bool stop;
        int step = 0;
        const int WAITING = 0;
        const int STARTED = 1;
        const int CLOSED = 2;
        const int END = 3;
        OF currentOF;
        List<ProductInventory> inInventories;
        List<ProductInventory> outInventories;
        DataClassesDataContext dc = new DataClassesDataContext();
        
        delegate void invokeDelegate();

        public frmOF()
        {
            InitializeComponent();
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            incomingFiles = Directory.GetFiles(INPUT_DIR);
            currentOF = null;
            button1.Visible = false;
            groupBoxOFDetails.Visible = false;
            groupBoxResult.Visible = false;
            groupBoxRebuts.Visible = false;
            errorList = new List<string>();
            inInventories = Pilotage.INSTANCE.InInventories;
            outInventories = Pilotage.INSTANCE.OutInventories;
            
            
            //var reqInputInventories = (from pi in dc.ProductInventory
            //                    where (pi.ProductID == 1 && pi.LocationID == ProductInventory.LOCATION_ID)
            //                    select pi);
            //inInventories = reqInputInventories.ToList();

            //var reqOutputInventories = (from pi in dc.ProductInventory
            //           where (pi.ProductID == 6 && pi.LocationID == ProductInventory.LOCATION_ID)
            //           select pi);
            //outInventories = reqOutputInventories.ToList();
            
           
            //inInventories.Add(new ProductInventory(1, ProductInventory.LOCATION_ID));
            //outInventories.Add(new ProductInventory(6, ProductInventory.LOCATION_ID));
            //Only one input inventory for the moment
            //inputInventories = new List<Inventory> { 
            //    new Inventory(1,Inventory.LOCATION_ID)
            //};
            //Only one output inventory for the moment
            //outputInventories = new List<Inventory>(); { 
            //    new Inventory(6, Inventory.LOCATION_ID) 
            //};


            //inInventories[0].Type = ProductInventory.inout.INPUT;
            //uC_Inventory1.Initialize(inInventories[0]);
            uC_Inventory1.Initialize(inInventories[0]);
            uC_Inventory2.Initialize(outInventories[0]);
            //outInventories[0].Type = ProductInventory.inout.OUTPUT;
            //uC_Inventory2.Initialize(outInventories[0]);
            //queryDB();
            step = 0;
        }


      


        //private void queryDB()
        //{
        //    DataClassesDataContext dc = new DataClassesDataContext();
        //    foreach (Inventory i in inputInventories)
        //    {
        //        var inv = (from pi in dc.ProductInventory
        //                   where (pi.ProductID == i.ProductId && pi.LocationID == i.LocationId)
        //                   select pi).SingleOrDefault();
        //        if (inv != null)
        //            i.Update((ushort)(inv.Quantity), (ushort)(inv.Capacity));
        //    }

        //    foreach (Inventory i in outputInventories)
        //    {
        //        var inv = (from pi in dc.ProductInventory
        //                   where (pi.ProductID == i.ProductId && pi.LocationID == i.LocationId)
        //                   select pi).SingleOrDefault();
        //        if (inv != null)
        //            i.Update((ushort)(inv.Quantity), (ushort)(inv.Capacity));
        //    }
        //    updateInventories();
        //}

        private void updateInventories()
        {

        }


        // Custom comparer for the Product class
        class OFComparer : IEqualityComparer<OF>
        {
            // Products are equal if their names and product numbers are equal.
            public bool Equals(OF x, OF y)
            {
                //Check whether the compared objects reference the same data.
                if (Object.ReferenceEquals(x, y)) return true;

                //Check whether any of the compared objects is null.
                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

                //Check whether the OF's properties are equal.
                return x.Id == y.Id;
            }

            // If Equals() returns true for a pair of objects 
            // then GetHashCode() must return the same value for these objects.

            public int GetHashCode(OF of)
            {
                //Check whether the object is null
                if (Object.ReferenceEquals(of, null)) return 0;

                //Get hash code for the Name field if it is not null.
                int hashProductName = of.Id == null ? 0 : of.Id.GetHashCode();

                //Get hash code for the Code field.
                int hashProductCode = of.ProductId.GetHashCode();

                //Calculate the hash code for the product.
                return hashProductName ^ hashProductCode;
            }
        }


        /// <summary>
        /// Retrieve OFs from files located in incoming dir
        /// </summary>
        void getOfs()
        {

            List<OF> ofs = OF.GetFromDir(INPUT_DIR);
            //NO new file
            if (ofs.Count == 0)
                return;

            //New OFs does not exists in cd
            IEnumerable<OF> newOFS = ofs.Except(cd.Values, new OFComparer());
            if (newOFS.Count() > 0)
            {
                //populate dictionary
                foreach (OF of in newOFS)
                {
                    cd.TryAdd(of.Id.ToString(), of);
                }
                updateListView();
            }
        }

        private void updateListView()
        {
            listView1.BeginInvoke(new invokeDelegate(updateForm));
        }


        private void updateLogView()
        {
            listView3.BeginInvoke(new invokeDelegate(updateErrorList));
        }

        private void updateErrorList()
        {
            listView3.Items.Clear();
            foreach (string s in errorList)
            {
                ListViewItem item = new ListViewItem();
                item.Text = s;
                listView3.Items.Add(item);
            }
        }


        private void updateForm()
        {

            listView1.Items.Clear();
            foreach (var kvp in cd)
            {
                string[] rowItems = { kvp.Key, kvp.Value.ProductId.ToString(), kvp.Value.Qty.ToString(), kvp.Value.DateDue.ToString() };
                ListViewItem lvi = new ListViewItem(rowItems);
                lvi.Name = kvp.Key;
                listView1.Items.Add(lvi);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //initialize inventories

            errorList.Clear();
            Task.Factory.StartNew(() =>
                {
                    while (!stop)
                    {
                        getOfs();
                        Thread.Sleep(1000);
                    }
                });

           
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string key = listView1.SelectedItems[0].Name;
                if (cd.ContainsKey(key))
                {
                    currentOF = cd[key];
                    groupBoxOFDetails.Visible = true;
                    groupBoxRebuts.Visible = false;
                    button1.Visible = true;
                }
                updateDetails();
            }
            else
            {
                groupBoxOFDetails.Visible = false;
                groupBoxRebuts.Visible = false;
                button1.Visible = false;
                currentOF = null;
            }
           
        }

        /// <summary>
        /// Update OFdetails
        /// </summary>
        void updateDetails()
        {
            listView2.Items.Clear();
            if (currentOF != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(currentOF))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(currentOF);
                    string[] rowitems = { name, value.ToString() };
                    listView2.Items.Add(new ListViewItem(rowitems));
                }
            }
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            UInt16 rebuts;
            if(currentOF == null)
                return;
            
            switch (step)
            {
                case WAITING:
                    ///Not started
                    if (currentOF.DateStarted == DateTime.MinValue)
                    {
                        currentOF.Start();
                        //TODO : update DB
                        listView1.Enabled = false;
                        button1.Text = "Stop";
                        updateDetails();
                        uC_Inventory1.Visible = true;
                        uC_Inventory2.Visible = true;
                        //TODO : réactiver
                        //prepareCNFromWO();
                        step++;
                    }
                    break;
                case STARTED:
                     currentOF.Stop();
                    button1.Text = "End";
                    groupBoxResult.Visible = true;
                    updateDetails();
                    step++;
                    break;
                    
              
                case CLOSED:
                     uC_Inventory1.Visible = false;
                        uC_Inventory2.Visible = false;
                     if (!checkBoxComplet.Checked && (txtRebut.Text == "" || txtCause.Text == ""))
                    {
                        MessageBox.Show("OF incomplet, vous devez renseigner la quantité de rebuts et la cause", "Info");
                        return;
                    }

                    //Uncomplete
                    if (!checkBoxComplet.Checked)
                    {
                        try
                        {
                            rebuts = UInt16.Parse(txtRebut.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("OF incomplet, erreur dans la quantité de rebuts", "Info");
                            break;
                        }
                        catch (ArgumentNullException)
                        {
                            MessageBox.Show("OF incomplet, erreur dans la quantité de rebuts", "Info");
                            break; ;
                        }
                        catch (OverflowException)
                        {
                            MessageBox.Show("OF incomplet, erreur dans la quantité de rebuts", "Info");
                            break; ;
                        }
                       

                        if (rebuts > currentOF.Qty)
                        {
                            MessageBox.Show("OF incomplet, erreur dans la quantité de rebuts", "Info");
                            break; ;
                        }
                        currentOF.RealizedQty = currentOF.Qty - rebuts;
                        currentOF.ScrapReason = txtCause.Text;
                    }

                    //complete
                    else
                    {
                        currentOF.RealizedQty = currentOF.Qty;
                        step = WAITING;
                    }
                   
                    OF.Save(currentOF, OUTPUT_DIR);
                    if (cd.TryRemove(currentOF.Id.ToString(), out currentOF))
                    {
                        currentOF = null;
                        updateDetails();
                        updateListView();
                        listView1.Enabled = true;
                        groupBoxOFDetails.Visible = false;
                        checkBoxComplet.Checked = true;
                        groupBoxRebuts.Visible = false;
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                   break;
                case END:
                   break;
            }
            
            
        }

        //private static Socket ConnectSocket(string server, int port)
        //{
        //    Socket s = null;
        //    IPHostEntry hostEntry = null;

        //    // Get host related information.
        //    hostEntry = Dns.GetHostEntry(server);

        //    // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
        //    // an exception that occurs when the host IP Address is not compatible with the address family
        //    // (typical in the IPv6 case).
        //    foreach (IPAddress address in hostEntry.AddressList)
        //    {
        //        IPEndPoint ipe = new IPEndPoint(address, port);
        //        Socket tempSocket =
        //            new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //        try
        //        {
        //            tempSocket.Connect(ipe);
        //        }
        //        catch (SocketException e)
        //        {
        //            throw e;
        //        }

        //        if (tempSocket.Connected)
        //        {
        //            s = tempSocket;
        //            break;
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }
        //    return s;
        //}

        /// <summary>
        /// Method to get fabrication folder and call preparation step
        /// fabrication folder is stored ???
        /// </summary>
        private void prepareCNFromWO()
        {
            try{
                //DossierDeFabrication df = new DossierDeFabrication(Path.Combine(GlobalSettings.Default.repertoireDossiersAmbiflux,currentOF.ProductId.ToString()));
                //ProgrammePiece pp = df.GetProgrammesPiece()[0];
                //SessionInfos.Utilisateur.SetDossierCourant(df);
                //SessionInfos.Utilisateur.SetProgrammeCourant(pp);  
                //Activation frm Preparation
                foreach (Form f in this.MdiParent.MdiChildren)
                {     
                    if (f.Name == "frmPreparation")
                    {
                        f.Show();
                        f.BringToFront();
                        ((frmPreparation)f).InitialiserModeOF(currentOF);
                        break;
                    }
                }
            }
            catch(Exception ex){
                throw ex;
            }
            //Get Folder
            //GlobalSettings.Default.repertoireDossiersAmbiflux;
            //SessionInfos.Utilisateur.DossierCourant = GlobalSettings.Default.repertoireDossiersAmbiflux;
            //Pilotage.INSTANCE.PreparerCN();
            //Call PreparationStep
        }

        private void checkBoxComplet_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxRebuts.Visible = !checkBoxComplet.Checked;

            if (checkBoxComplet.Checked && currentOF != null)
            {
                currentOF.ScrappedQty = 0;
                currentOF.RealizedQty = currentOF.Qty;
            }
        }


        //// This method sends a request and wait for answer from agent
        //private static string SocketSendReceive(string server, int port, string request)
        //{
        //    //string address = "TBI540Inv1@192.168.0.21:1099/JADE";
           
        //    Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
        //    Byte[] bytesReceived = new Byte[256];

        //    string page = "";

        //    // Create a socket connection with the specified server and port.
        //    try
        //    {
        //        Socket s = ConnectSocket(server, port);
        //        if (s == null)
        //            return ("Connection failed");
        //        // Send request to the server.
        //        s.Send(bytesSent, bytesSent.Length, 0);

        //        // Receive the server home page content.
        //        int bytes = 0;
                

        //        // The following will block until te page is transmitted.
        //        do
        //        {
        //            bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
        //            page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
        //        }
        //        while (bytes == bytesReceived.Length);
        //    }
        //    catch (SocketException e)
        //    {
        //        throw e;
        //    }


        //    return page;
        //}

        /// <summary>
        /// On click, send request to update inventory agent with new quantity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    errorList.Clear();
        //    updateLogView();
        //    string dest = "invTBI540-1@" + GlobalSettings.Default.jadeServerAddress + ":1099/JADE";
        //    string request = String.Format("(REQUEST\r\n :receiver  (set ( agent-identifier :name {0} ) )\r\n :content  \"((action (agent-identifier :name {0}) (UpdateQuantity\r\n :command Remove :qty {1})))\"\r\n  :language  fipa-sl  :ontology  ambiflux-logistic )", dest, this. .Value.ToString());
        //    string result = "";
        //    try
        //    {
        //        result = ProxySocket.SocketSendReceive(GlobalSettings.Default.jadeServerAddress, 6789, request);
        //        //result = SocketSendReceive(GlobalSettings.Default.jadeServerAddress, 6789, request);
        //        //TODO: à modifier
        //        if (result.Contains("((done"))
        //        {
        //            txtInventoryLevel.Text = (Int16.Parse(txtInventoryLevel.Text) - numericUpDown1.Value).ToString();
        //            numericUpDown1.Value = 0;
        //        }
        //    }
        //    catch (SocketException ex)
        //    {
        //        errorList.Add(ex.Message);
        //        updateLogView();
        //        return;
        //    }
        //}

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
