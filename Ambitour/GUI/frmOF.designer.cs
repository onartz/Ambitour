namespace Ambitour
{
    partial class frmOF
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxOFDetails = new System.Windows.Forms.GroupBox();
            this.listView3 = new System.Windows.Forms.ListView();
            this.errorMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxResult = new System.Windows.Forms.GroupBox();
            this.groupBoxRebuts = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCause = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRebut = new System.Windows.Forms.TextBox();
            this.checkBoxComplet = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.ColumnId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uC_Inventory2 = new Ambitour.GUI.UC_Inventory();
            this.uC_Inventory1 = new Ambitour.GUI.UC_Inventory();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxOFDetails.SuspendLayout();
            this.groupBoxResult.SuspendLayout();
            this.groupBoxRebuts.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxOFDetails);
            this.splitContainer1.Size = new System.Drawing.Size(783, 582);
            this.splitContainer1.SplitterDistance = 159;
            this.splitContainer1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(753, 159);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Identifiant OF";
            this.columnHeader1.Width = 207;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ProductID";
            this.columnHeader2.Width = 139;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Quantity";
            this.columnHeader3.Width = 125;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Date due";
            this.columnHeader4.Width = 186;
            // 
            // groupBoxOFDetails
            // 
            this.groupBoxOFDetails.Controls.Add(this.uC_Inventory2);
            this.groupBoxOFDetails.Controls.Add(this.uC_Inventory1);
            this.groupBoxOFDetails.Controls.Add(this.listView3);
            this.groupBoxOFDetails.Controls.Add(this.groupBoxResult);
            this.groupBoxOFDetails.Controls.Add(this.button1);
            this.groupBoxOFDetails.Controls.Add(this.listView2);
            this.groupBoxOFDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxOFDetails.Location = new System.Drawing.Point(0, 0);
            this.groupBoxOFDetails.Name = "groupBoxOFDetails";
            this.groupBoxOFDetails.Size = new System.Drawing.Size(783, 419);
            this.groupBoxOFDetails.TabIndex = 0;
            this.groupBoxOFDetails.TabStop = false;
            this.groupBoxOFDetails.Text = "OFDetails";
            // 
            // listView3
            // 
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.errorMessage});
            this.listView3.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView3.Location = new System.Drawing.Point(6, 306);
            this.listView3.Name = "listView3";
            this.listView3.Scrollable = false;
            this.listView3.Size = new System.Drawing.Size(765, 107);
            this.listView3.TabIndex = 15;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            this.listView3.Visible = false;
            // 
            // errorMessage
            // 
            this.errorMessage.Text = "Log";
            this.errorMessage.Width = 767;
            // 
            // groupBoxResult
            // 
            this.groupBoxResult.Controls.Add(this.groupBoxRebuts);
            this.groupBoxResult.Controls.Add(this.checkBoxComplet);
            this.groupBoxResult.Controls.Add(this.label1);
            this.groupBoxResult.Location = new System.Drawing.Point(483, 19);
            this.groupBoxResult.Name = "groupBoxResult";
            this.groupBoxResult.Size = new System.Drawing.Size(275, 203);
            this.groupBoxResult.TabIndex = 4;
            this.groupBoxResult.TabStop = false;
            this.groupBoxResult.Text = "Result";
            // 
            // groupBoxRebuts
            // 
            this.groupBoxRebuts.Controls.Add(this.label3);
            this.groupBoxRebuts.Controls.Add(this.txtCause);
            this.groupBoxRebuts.Controls.Add(this.label2);
            this.groupBoxRebuts.Controls.Add(this.txtRebut);
            this.groupBoxRebuts.Location = new System.Drawing.Point(12, 56);
            this.groupBoxRebuts.Name = "groupBoxRebuts";
            this.groupBoxRebuts.Size = new System.Drawing.Size(258, 130);
            this.groupBoxRebuts.TabIndex = 10;
            this.groupBoxRebuts.TabStop = false;
            this.groupBoxRebuts.Text = "Incomplet";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Cause";
            // 
            // txtCause
            // 
            this.txtCause.Location = new System.Drawing.Point(6, 70);
            this.txtCause.Multiline = true;
            this.txtCause.Name = "txtCause";
            this.txtCause.Size = new System.Drawing.Size(246, 57);
            this.txtCause.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Qté Rebuts";
            // 
            // txtRebut
            // 
            this.txtRebut.Location = new System.Drawing.Point(79, 25);
            this.txtRebut.Name = "txtRebut";
            this.txtRebut.Size = new System.Drawing.Size(72, 20);
            this.txtRebut.TabIndex = 5;
            // 
            // checkBoxComplet
            // 
            this.checkBoxComplet.AutoSize = true;
            this.checkBoxComplet.Checked = true;
            this.checkBoxComplet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxComplet.Location = new System.Drawing.Point(99, 22);
            this.checkBoxComplet.Name = "checkBoxComplet";
            this.checkBoxComplet.Size = new System.Drawing.Size(64, 17);
            this.checkBoxComplet.TabIndex = 9;
            this.checkBoxComplet.Text = "Complet";
            this.checkBoxComplet.UseVisualStyleBackColor = true;
            this.checkBoxComplet.CheckedChanged += new System.EventHandler(this.checkBoxComplet_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Production";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 200);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(283, 88);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnId,
            this.columnHeader7});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView2.Location = new System.Drawing.Point(12, 19);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(283, 175);
            this.listView2.TabIndex = 0;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // ColumnId
            // 
            this.ColumnId.Text = "Prop.";
            this.ColumnId.Width = 135;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Value";
            this.columnHeader7.Width = 144;
            // 
            // uC_Inventory2
            // 
            this.uC_Inventory2.Location = new System.Drawing.Point(301, 158);
            this.uC_Inventory2.Name = "uC_Inventory2";
            this.uC_Inventory2.ProductInventory = null;
            this.uC_Inventory2.Size = new System.Drawing.Size(160, 142);
            this.uC_Inventory2.TabIndex = 17;
            this.uC_Inventory2.Visible = false;
            // 
            // uC_Inventory1
            // 
            this.uC_Inventory1.Location = new System.Drawing.Point(301, 19);
            this.uC_Inventory1.Name = "uC_Inventory1";
            this.uC_Inventory1.ProductInventory = null;
            this.uC_Inventory1.Size = new System.Drawing.Size(160, 149);
            this.uC_Inventory1.TabIndex = 16;
            this.uC_Inventory1.Visible = false;
          
            // 
            // frmOF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(783, 582);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(0, 85);
            this.Name = "frmOF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "OFs";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxOFDetails.ResumeLayout(false);
            this.groupBoxResult.ResumeLayout(false);
            this.groupBoxResult.PerformLayout();
            this.groupBoxRebuts.ResumeLayout(false);
            this.groupBoxRebuts.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBoxOFDetails;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader ColumnId;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBoxResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCause;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRebut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxRebuts;
        private System.Windows.Forms.CheckBox checkBoxComplet;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ColumnHeader errorMessage;
        private GUI.UC_Inventory uC_Inventory2;
        private GUI.UC_Inventory uC_Inventory1;
    }
}

