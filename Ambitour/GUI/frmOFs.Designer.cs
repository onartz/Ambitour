namespace Ambitour
{
    partial class frmOFs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblWorkorderNo = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblScrapReason = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtOrderQty = new System.Windows.Forms.TextBox();
            this.lblorderQty = new System.Windows.Forms.Label();
            this.txtWorkorderNo = new System.Windows.Forms.TextBox();
            this.cmbScrapReason = new System.Windows.Forms.ComboBox();
            this.lblScrappedQty = new System.Windows.Forms.Label();
            this.txtScrappedQty = new System.Windows.Forms.TextBox();
            this.lblStockedQty = new System.Windows.Forms.Label();
            this.txtStockedQty = new System.Windows.Forms.TextBox();
            this.lblWorkorderRoutingNo = new System.Windows.Forms.Label();
            this.lblScheduledEndDate = new System.Windows.Forms.Label();
            this.lblScheduledStartDate = new System.Windows.Forms.Label();
            this.txtScheduledEndDate = new System.Windows.Forms.TextBox();
            this.txtScheduledStartDate = new System.Windows.Forms.TextBox();
            this.txtWorkorderRoutingNo = new System.Windows.Forms.TextBox();
            this.lblActualEndDate = new System.Windows.Forms.Label();
            this.lblActualStartDate = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataClassesDataContextBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataClassesDataContextBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-4, 74);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(622, 104);
            this.dataGridView1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(-4, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(622, 395);
            this.splitContainer1.SplitterDistance = 169;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lblWorkorderNo);
            this.splitContainer2.Panel1.Controls.Add(this.btnCancel);
            this.splitContainer2.Panel1.Controls.Add(this.btnOK);
            this.splitContainer2.Panel1.Controls.Add(this.lblScrapReason);
            this.splitContainer2.Panel1.Controls.Add(this.txtProduct);
            this.splitContainer2.Panel1.Controls.Add(this.lblProduct);
            this.splitContainer2.Panel1.Controls.Add(this.txtOrderQty);
            this.splitContainer2.Panel1.Controls.Add(this.lblorderQty);
            this.splitContainer2.Panel1.Controls.Add(this.txtWorkorderNo);
            this.splitContainer2.Panel1.Controls.Add(this.cmbScrapReason);
            this.splitContainer2.Panel1.Controls.Add(this.lblScrappedQty);
            this.splitContainer2.Panel1.Controls.Add(this.txtScrappedQty);
            this.splitContainer2.Panel1.Controls.Add(this.lblStockedQty);
            this.splitContainer2.Panel1.Controls.Add(this.txtStockedQty);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lblWorkorderRoutingNo);
            this.splitContainer2.Panel2.Controls.Add(this.lblScheduledEndDate);
            this.splitContainer2.Panel2.Controls.Add(this.lblScheduledStartDate);
            this.splitContainer2.Panel2.Controls.Add(this.txtScheduledEndDate);
            this.splitContainer2.Panel2.Controls.Add(this.txtScheduledStartDate);
            this.splitContainer2.Panel2.Controls.Add(this.txtWorkorderRoutingNo);
            this.splitContainer2.Panel2.Controls.Add(this.lblActualEndDate);
            this.splitContainer2.Panel2.Controls.Add(this.lblActualStartDate);
            this.splitContainer2.Panel2.Controls.Add(this.button2);
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Size = new System.Drawing.Size(622, 216);
            this.splitContainer2.SplitterDistance = 302;
            this.splitContainer2.SplitterWidth = 10;
            this.splitContainer2.TabIndex = 0;
            // 
            // lblWorkorderNo
            // 
            this.lblWorkorderNo.AutoSize = true;
            this.lblWorkorderNo.Location = new System.Drawing.Point(18, 16);
            this.lblWorkorderNo.Name = "lblWorkorderNo";
            this.lblWorkorderNo.Size = new System.Drawing.Size(71, 13);
            this.lblWorkorderNo.TabIndex = 47;
            this.lblWorkorderNo.Text = "WorkorderNo";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(127, 178);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 46;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(21, 178);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 45;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblScrapReason
            // 
            this.lblScrapReason.AutoSize = true;
            this.lblScrapReason.Location = new System.Drawing.Point(18, 150);
            this.lblScrapReason.Name = "lblScrapReason";
            this.lblScrapReason.Size = new System.Drawing.Size(40, 13);
            this.lblScrapReason.TabIndex = 44;
            this.lblScrapReason.Text = "Raison";
            // 
            // txtProduct
            // 
            this.txtProduct.Enabled = false;
            this.txtProduct.Location = new System.Drawing.Point(127, 65);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(134, 20);
            this.txtProduct.TabIndex = 43;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(18, 72);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(40, 13);
            this.lblProduct.TabIndex = 42;
            this.lblProduct.Text = "Produit";
            // 
            // txtOrderQty
            // 
            this.txtOrderQty.Enabled = false;
            this.txtOrderQty.Location = new System.Drawing.Point(127, 39);
            this.txtOrderQty.Name = "txtOrderQty";
            this.txtOrderQty.ReadOnly = true;
            this.txtOrderQty.Size = new System.Drawing.Size(134, 20);
            this.txtOrderQty.TabIndex = 41;
            // 
            // lblorderQty
            // 
            this.lblorderQty.AutoSize = true;
            this.lblorderQty.Location = new System.Drawing.Point(18, 46);
            this.lblorderQty.Name = "lblorderQty";
            this.lblorderQty.Size = new System.Drawing.Size(47, 13);
            this.lblorderQty.TabIndex = 40;
            this.lblorderQty.Text = "Quantité";
            // 
            // txtWorkorderNo
            // 
            this.txtWorkorderNo.Enabled = false;
            this.txtWorkorderNo.Location = new System.Drawing.Point(127, 13);
            this.txtWorkorderNo.Name = "txtWorkorderNo";
            this.txtWorkorderNo.Size = new System.Drawing.Size(134, 20);
            this.txtWorkorderNo.TabIndex = 39;
            // 
            // cmbScrapReason
            // 
            this.cmbScrapReason.FormattingEnabled = true;
            this.cmbScrapReason.Location = new System.Drawing.Point(127, 142);
            this.cmbScrapReason.Name = "cmbScrapReason";
            this.cmbScrapReason.Size = new System.Drawing.Size(158, 21);
            this.cmbScrapReason.TabIndex = 38;
            // 
            // lblScrappedQty
            // 
            this.lblScrappedQty.AutoSize = true;
            this.lblScrappedQty.Location = new System.Drawing.Point(18, 119);
            this.lblScrappedQty.Name = "lblScrappedQty";
            this.lblScrappedQty.Size = new System.Drawing.Size(41, 13);
            this.lblScrappedQty.TabIndex = 37;
            this.lblScrappedQty.Text = "Rebuts";
            // 
            // txtScrappedQty
            // 
            this.txtScrappedQty.Location = new System.Drawing.Point(127, 116);
            this.txtScrappedQty.Name = "txtScrappedQty";
            this.txtScrappedQty.Size = new System.Drawing.Size(52, 20);
            this.txtScrappedQty.TabIndex = 36;
            // 
            // lblStockedQty
            // 
            this.lblStockedQty.AutoSize = true;
            this.lblStockedQty.Location = new System.Drawing.Point(18, 93);
            this.lblStockedQty.Name = "lblStockedQty";
            this.lblStockedQty.Size = new System.Drawing.Size(86, 13);
            this.lblStockedQty.TabIndex = 35;
            this.lblStockedQty.Text = "Quantité réalisée";
            // 
            // txtStockedQty
            // 
            this.txtStockedQty.Location = new System.Drawing.Point(127, 90);
            this.txtStockedQty.Name = "txtStockedQty";
            this.txtStockedQty.Size = new System.Drawing.Size(52, 20);
            this.txtStockedQty.TabIndex = 34;
            // 
            // lblWorkorderRoutingNo
            // 
            this.lblWorkorderRoutingNo.AutoSize = true;
            this.lblWorkorderRoutingNo.Location = new System.Drawing.Point(8, 16);
            this.lblWorkorderRoutingNo.Name = "lblWorkorderRoutingNo";
            this.lblWorkorderRoutingNo.Size = new System.Drawing.Size(34, 13);
            this.lblWorkorderRoutingNo.TabIndex = 33;
            this.lblWorkorderRoutingNo.Text = "WOR";
            // 
            // lblScheduledEndDate
            // 
            this.lblScheduledEndDate.AutoSize = true;
            this.lblScheduledEndDate.Location = new System.Drawing.Point(9, 72);
            this.lblScheduledEndDate.Name = "lblScheduledEndDate";
            this.lblScheduledEndDate.Size = new System.Drawing.Size(63, 13);
            this.lblScheduledEndDate.TabIndex = 32;
            this.lblScheduledEndDate.Text = "Fin planifiée";
            // 
            // lblScheduledStartDate
            // 
            this.lblScheduledStartDate.AutoSize = true;
            this.lblScheduledStartDate.Location = new System.Drawing.Point(9, 46);
            this.lblScheduledStartDate.Name = "lblScheduledStartDate";
            this.lblScheduledStartDate.Size = new System.Drawing.Size(75, 13);
            this.lblScheduledStartDate.TabIndex = 31;
            this.lblScheduledStartDate.Text = "Début planifié ";
            // 
            // txtScheduledEndDate
            // 
            this.txtScheduledEndDate.Location = new System.Drawing.Point(89, 65);
            this.txtScheduledEndDate.Name = "txtScheduledEndDate";
            this.txtScheduledEndDate.Size = new System.Drawing.Size(214, 20);
            this.txtScheduledEndDate.TabIndex = 30;
            // 
            // txtScheduledStartDate
            // 
            this.txtScheduledStartDate.Location = new System.Drawing.Point(89, 39);
            this.txtScheduledStartDate.Name = "txtScheduledStartDate";
            this.txtScheduledStartDate.Size = new System.Drawing.Size(214, 20);
            this.txtScheduledStartDate.TabIndex = 29;
            // 
            // txtWorkorderRoutingNo
            // 
            this.txtWorkorderRoutingNo.Location = new System.Drawing.Point(89, 13);
            this.txtWorkorderRoutingNo.Name = "txtWorkorderRoutingNo";
            this.txtWorkorderRoutingNo.Size = new System.Drawing.Size(214, 20);
            this.txtWorkorderRoutingNo.TabIndex = 28;
            // 
            // lblActualEndDate
            // 
            this.lblActualEndDate.AutoSize = true;
            this.lblActualEndDate.Location = new System.Drawing.Point(233, 154);
            this.lblActualEndDate.Name = "lblActualEndDate";
            this.lblActualEndDate.Size = new System.Drawing.Size(13, 13);
            this.lblActualEndDate.TabIndex = 27;
            this.lblActualEndDate.Text = "--";
            // 
            // lblActualStartDate
            // 
            this.lblActualStartDate.AutoSize = true;
            this.lblActualStartDate.Location = new System.Drawing.Point(9, 154);
            this.lblActualStartDate.Name = "lblActualStartDate";
            this.lblActualStartDate.Size = new System.Drawing.Size(13, 13);
            this.lblActualStartDate.TabIndex = 26;
            this.lblActualStartDate.Text = "--";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(228, 128);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "StopOF";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "StartOF";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataClassesDataContextBindingSource
            // 
            this.dataClassesDataContextBindingSource.DataSource = typeof(Ambitour.DataClassesDataContext);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(613, 56);
            this.dataGridView2.TabIndex = 0;
            // 
            // frmOFs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(621, 419);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.splitContainer1);
            this.Location = new System.Drawing.Point(0, 85);
            this.Name = "frmOFs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmOFs";
            this.Load += new System.EventHandler(this.frmOFs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataClassesDataContextBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource dataClassesDataContextBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Label lblWorkorderRoutingNo;
        private System.Windows.Forms.Label lblScheduledEndDate;
        private System.Windows.Forms.Label lblScheduledStartDate;
        private System.Windows.Forms.TextBox txtScheduledEndDate;
        private System.Windows.Forms.TextBox txtScheduledStartDate;
        private System.Windows.Forms.TextBox txtWorkorderRoutingNo;
        private System.Windows.Forms.Label lblActualEndDate;
        private System.Windows.Forms.Label lblActualStartDate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblScrapReason;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtOrderQty;
        private System.Windows.Forms.Label lblorderQty;
        private System.Windows.Forms.TextBox txtWorkorderNo;
        private System.Windows.Forms.ComboBox cmbScrapReason;
        private System.Windows.Forms.Label lblScrappedQty;
        private System.Windows.Forms.TextBox txtScrappedQty;
        private System.Windows.Forms.Label lblStockedQty;
        private System.Windows.Forms.TextBox txtStockedQty;
        private System.Windows.Forms.Label lblWorkorderNo;
        private System.Windows.Forms.DataGridView dataGridView2;


    }
}