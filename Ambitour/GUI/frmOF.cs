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
        string ARCHIVE_DIR = GlobalSettings.Default.archivesOFDirectory;
        string OUTPUT_DIR = GlobalSettings.Default.outgoingOFDirectory;
        string PENDING_DIR = GlobalSettings.Default.pendingOFDirectory;
        bool stop;
        int step = 0;
        const int WAITING = 0;
        const int SELECTED = 1;
        const int STARTED = 2;
        const int CLOSED = 3;
        const int END = 4;
        OF currentOF;
        List<ProductInventory> inInventories;
        List<ProductInventory> outInventories;
        DataClassesDataContext dc = new DataClassesDataContext();
        
        delegate void invokeDelegate();

        /// <summary>
        /// Default constructor
        /// </summary>
        public frmOF()
        {
            InitializeComponent();
            //System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            incomingFiles = Directory.GetFiles(INPUT_DIR);
            currentOF = null;
            switchState(WAITING);
          
            errorList = new List<string>();

            //Get globals inventories
            inInventories = Pilotage.INSTANCE.InInventories;
            outInventories = Pilotage.INSTANCE.OutInventories;
               
            uC_Inventory1.Initialize(inInventories[0]);
            uC_Inventory2.Initialize(outInventories[0]);
           
            step = 0;

            Task.Factory.StartNew(() =>
            {
                while (!stop)
                {
                    getOfs();
                    Thread.Sleep(1000);
                }
            });
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

      

        /// <summary>
        /// Modify display of elements
        /// </summary>
        /// <param name="state"></param>
        private void switchState(int state)
        {
            switch (state)
            {
                case WAITING:
                    button1.Text = "Start";
                    button1.Visible = false;
                    groupBoxOFDetails.Visible = false;
                    groupBoxResult.Visible = false;
                    groupBoxRebuts.Visible = false;
                    uC_Inventory1.Visible = false;
                    uC_Inventory2.Visible = false;
                    break;
                case SELECTED:
                    updateDetails();
                    button1.Text = "Start";
                    button1.Visible = true;
                    groupBoxOFDetails.Visible = true;
                    groupBoxResult.Visible = false;
                    groupBoxRebuts.Visible = false;
                    listView1.Enabled = false;
                    uC_Inventory1.Visible = false;
                    uC_Inventory2.Visible = false;
                    break;
                case STARTED:
                    button1.Text = "Stop";
                    button1.Visible = true;
                    groupBoxOFDetails.Visible = true;
                    groupBoxResult.Visible = false;
                    groupBoxRebuts.Visible = false;
                    listView1.Enabled = false;
                    uC_Inventory1.Visible = true;
                    uC_Inventory2.Visible = true;
                    break;
              
                case CLOSED:
                    button1.Text = "Close";
                    button1.Visible = true;
                    groupBoxResult.Visible = true;
                    groupBoxRebuts.Visible = true;
                    listView1.Enabled = false;
                    uC_Inventory1.Visible = true;
                    uC_Inventory2.Visible = true;
                    break;
                default:
                    break;
            }
            step = state;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UInt16 rebuts;
            if (currentOF == null)
                return;

            switch (step)
            {
                //Actual state
                case SELECTED:
                    ///Selected but not Not started
                    if (currentOF.DateStarted == DateTime.MinValue)
                    {
                        currentOF.Start();
                        OF.Save(currentOF, PENDING_DIR);
                        updateDetails();
                        //TODO : update DB
                        switchState(STARTED);
                        //TODO : réactiver
                        prepareCNFromWO();
                    }
                    break;
                case STARTED:
                    currentOF.Stop();
                    OF.Save(currentOF, PENDING_DIR);
                    updateDetails();
                    switchState(CLOSED);
                    break;


                case CLOSED:
                   
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
                        switchState(WAITING);
                    }

                    OF.Save(currentOF, ARCHIVE_DIR);
                    if(File.Exists(Path.Combine(PENDING_DIR, currentOF.Id + ".xml")))
                    {
                        File.Delete(Path.Combine(PENDING_DIR, currentOF.Id + ".xml"));

                    }
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
                    //switchState(WAITING);
                    break;
                case END:
                    break;
            }


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

        /// <summary>
        /// update Listview from other Thread
        /// </summary>
        private void updateListView()
        {
            listView1.BeginInvoke(new invokeDelegate(updateForm));
        }

        /// <summary>
        /// update LogView from other Thread
        /// </summary>
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


        /// <summary>
        /// update OF listview with non incoming OFs
        /// </summary>
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
            errorList.Clear();
            updateForm();     
        }

       

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string key = listView1.SelectedItems[0].Name;
                if (cd.ContainsKey(key))
                {    
                    currentOF = cd[key];
                    switchState(SELECTED);
                  
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


        /// <summary>
        /// Method to get fabrication folder and call preparation step
        /// fabrication folder has the same name as the productId
        /// ex : c:\ambitour\Dossiers\1
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


    
      
    }
}
