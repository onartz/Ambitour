namespace Ambitour.GUI
{
    partial class frmWorkorder
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(126, 203);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 33;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(26, 203);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 32;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblScrapReason
            // 
            this.lblScrapReason.AutoSize = true;
            this.lblScrapReason.Location = new System.Drawing.Point(23, 175);
            this.lblScrapReason.Name = "lblScrapReason";
            this.lblScrapReason.Size = new System.Drawing.Size(40, 13);
            this.lblScrapReason.TabIndex = 31;
            this.lblScrapReason.Text = "Raison";
            // 
            // txtProduct
            // 
            this.txtProduct.Enabled = false;
            this.txtProduct.Location = new System.Drawing.Point(132, 76);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(134, 20);
            this.txtProduct.TabIndex = 30;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(23, 83);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(40, 13);
            this.lblProduct.TabIndex = 29;
            this.lblProduct.Text = "Produit";
            // 
            // txtOrderQty
            // 
            this.txtOrderQty.Enabled = false;
            this.txtOrderQty.Location = new System.Drawing.Point(132, 50);
            this.txtOrderQty.Name = "txtOrderQty";
            this.txtOrderQty.Size = new System.Drawing.Size(134, 20);
            this.txtOrderQty.TabIndex = 28;
            // 
            // lblorderQty
            // 
            this.lblorderQty.AutoSize = true;
            this.lblorderQty.Location = new System.Drawing.Point(23, 57);
            this.lblorderQty.Name = "lblorderQty";
            this.lblorderQty.Size = new System.Drawing.Size(47, 13);
            this.lblorderQty.TabIndex = 26;
            this.lblorderQty.Text = "Quantité";
            // 
            // txtWorkorderNo
            // 
            this.txtWorkorderNo.Enabled = false;
            this.txtWorkorderNo.Location = new System.Drawing.Point(132, 24);
            this.txtWorkorderNo.Name = "txtWorkorderNo";
            this.txtWorkorderNo.Size = new System.Drawing.Size(134, 20);
            this.txtWorkorderNo.TabIndex = 25;
            // 
            // cmbScrapReason
            // 
            this.cmbScrapReason.FormattingEnabled = true;
            this.cmbScrapReason.Location = new System.Drawing.Point(132, 167);
            this.cmbScrapReason.Name = "cmbScrapReason";
            this.cmbScrapReason.Size = new System.Drawing.Size(158, 21);
            this.cmbScrapReason.TabIndex = 24;
            // 
            // lblScrappedQty
            // 
            this.lblScrappedQty.AutoSize = true;
            this.lblScrappedQty.Location = new System.Drawing.Point(23, 144);
            this.lblScrappedQty.Name = "lblScrappedQty";
            this.lblScrappedQty.Size = new System.Drawing.Size(41, 13);
            this.lblScrappedQty.TabIndex = 23;
            this.lblScrappedQty.Text = "Rebuts";
            // 
            // txtScrappedQty
            // 
            this.txtScrappedQty.Location = new System.Drawing.Point(132, 141);
            this.txtScrappedQty.Name = "txtScrappedQty";
            this.txtScrappedQty.Size = new System.Drawing.Size(52, 20);
            this.txtScrappedQty.TabIndex = 22;
            // 
            // lblStockedQty
            // 
            this.lblStockedQty.AutoSize = true;
            this.lblStockedQty.Location = new System.Drawing.Point(23, 118);
            this.lblStockedQty.Name = "lblStockedQty";
            this.lblStockedQty.Size = new System.Drawing.Size(86, 13);
            this.lblStockedQty.TabIndex = 21;
            this.lblStockedQty.Text = "Quantité réalisée";
            // 
            // txtStockedQty
            // 
            this.txtStockedQty.Location = new System.Drawing.Point(132, 115);
            this.txtStockedQty.Name = "txtStockedQty";
            this.txtStockedQty.Size = new System.Drawing.Size(52, 20);
            this.txtStockedQty.TabIndex = 20;
            // 
            // frmWorkorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(484, 262);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblScrapReason);
            this.Controls.Add(this.txtProduct);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.txtOrderQty);
            this.Controls.Add(this.lblorderQty);
            this.Controls.Add(this.txtWorkorderNo);
            this.Controls.Add(this.cmbScrapReason);
            this.Controls.Add(this.lblScrappedQty);
            this.Controls.Add(this.txtScrappedQty);
            this.Controls.Add(this.lblStockedQty);
            this.Controls.Add(this.txtStockedQty);
            this.Name = "frmWorkorder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Workorder";
            this.Load += new System.EventHandler(this.frmWorkorder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblScrapReason;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtOrderQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblorderQty;
        private System.Windows.Forms.TextBox txtWorkorderNo;
        private System.Windows.Forms.ComboBox cmbScrapReason;
        private System.Windows.Forms.Label lblScrappedQty;
        private System.Windows.Forms.TextBox txtScrappedQty;
        private System.Windows.Forms.Label lblStockedQty;
        private System.Windows.Forms.TextBox txtStockedQty;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}