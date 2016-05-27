namespace Ambitour.GUI
{
    partial class UC_Inventory
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBoxInputInventory = new System.Windows.Forms.GroupBox();
            this.lblInventoryId = new System.Windows.Forms.Label();
            this.btnPickPlace = new System.Windows.Forms.Button();
            this.txtInventoryLevel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.udQty = new System.Windows.Forms.NumericUpDown();
            this.groupBoxInputInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udQty)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // groupBoxInputInventory
            // 
            this.groupBoxInputInventory.Controls.Add(this.lblInventoryId);
            this.groupBoxInputInventory.Controls.Add(this.btnPickPlace);
            this.groupBoxInputInventory.Controls.Add(this.txtInventoryLevel);
            this.groupBoxInputInventory.Controls.Add(this.label5);
            this.groupBoxInputInventory.Controls.Add(this.udQty);
            this.groupBoxInputInventory.Location = new System.Drawing.Point(-6, 8);
            this.groupBoxInputInventory.Name = "groupBoxInputInventory";
            this.groupBoxInputInventory.Size = new System.Drawing.Size(162, 134);
            this.groupBoxInputInventory.TabIndex = 18;
            this.groupBoxInputInventory.TabStop = false;
            this.groupBoxInputInventory.Text = "Stock From";
            // 
            // lblInventoryId
            // 
            this.lblInventoryId.AutoSize = true;
            this.lblInventoryId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInventoryId.Location = new System.Drawing.Point(6, 16);
            this.lblInventoryId.Name = "lblInventoryId";
            this.lblInventoryId.Size = new System.Drawing.Size(95, 17);
            this.lblInventoryId.TabIndex = 16;
            this.lblInventoryId.Text = "invTBI540-1";
            // 
            // btnPickPlace
            // 
            this.btnPickPlace.Location = new System.Drawing.Point(81, 97);
            this.btnPickPlace.Name = "btnPickPlace";
            this.btnPickPlace.Size = new System.Drawing.Size(75, 23);
            this.btnPickPlace.TabIndex = 14;
            this.btnPickPlace.Text = "Pick";
            this.btnPickPlace.UseVisualStyleBackColor = true;
            this.btnPickPlace.Click += new System.EventHandler(this.btnPickPlace_Click);
            // 
            // txtInventoryLevel
            // 
            this.txtInventoryLevel.BackColor = System.Drawing.Color.Green;
            this.txtInventoryLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInventoryLevel.ForeColor = System.Drawing.SystemColors.Window;
            this.txtInventoryLevel.Location = new System.Drawing.Point(9, 63);
            this.txtInventoryLevel.Name = "txtInventoryLevel";
            this.txtInventoryLevel.ReadOnly = true;
            this.txtInventoryLevel.Size = new System.Drawing.Size(55, 26);
            this.txtInventoryLevel.TabIndex = 13;
            this.txtInventoryLevel.Text = "12";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "StockLevel";
            // 
            // udQty
            // 
            this.udQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udQty.Location = new System.Drawing.Point(9, 95);
            this.udQty.Name = "udQty";
            this.udQty.Size = new System.Drawing.Size(55, 26);
            this.udQty.TabIndex = 5;
            // 
            // UC_Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxInputInventory);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "UC_Inventory";
            this.Size = new System.Drawing.Size(188, 162);
            this.groupBoxInputInventory.ResumeLayout(false);
            this.groupBoxInputInventory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udQty)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBoxInputInventory;
        private System.Windows.Forms.Label lblInventoryId;
        private System.Windows.Forms.Button btnPickPlace;
        private System.Windows.Forms.TextBox txtInventoryLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown udQty;
    }
}
