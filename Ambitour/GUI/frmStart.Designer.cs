namespace Ambitour
{
    partial class frmStart
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStart));
            this.labelStatusBadge = new System.Windows.Forms.Label();
            this.LabelStatusCN = new System.Windows.Forms.Label();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btmFermer = new System.Windows.Forms.Button();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStatusBadge
            // 
            this.labelStatusBadge.AutoSize = true;
            this.labelStatusBadge.Location = new System.Drawing.Point(6, 20);
            this.labelStatusBadge.Name = "labelStatusBadge";
            this.labelStatusBadge.Size = new System.Drawing.Size(10, 13);
            this.labelStatusBadge.TabIndex = 0;
            this.labelStatusBadge.Text = "-";
            // 
            // LabelStatusCN
            // 
            this.LabelStatusCN.AutoSize = true;
            this.LabelStatusCN.Location = new System.Drawing.Point(6, 46);
            this.LabelStatusCN.Name = "LabelStatusCN";
            this.LabelStatusCN.Size = new System.Drawing.Size(122, 13);
            this.LabelStatusCN.TabIndex = 1;
            this.LabelStatusCN.Text = "Démarrage Liaison CN...";
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "Ambitour";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LabelStatusCN);
            this.groupBox1.Controls.Add(this.labelStatusBadge);
            this.groupBox1.Location = new System.Drawing.Point(6, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 73);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // btmFermer
            // 
            this.btmFermer.Location = new System.Drawing.Point(6, 96);
            this.btmFermer.Name = "btmFermer";
            this.btmFermer.Size = new System.Drawing.Size(76, 25);
            this.btmFermer.TabIndex = 4;
            this.btmFermer.Text = "Fermer";
            this.btmFermer.UseVisualStyleBackColor = true;
            this.btmFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(329, 96);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(76, 25);
            this.btnQuitter.TabIndex = 5;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.btnQuitter_Click_1);
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(417, 155);
            this.ControlBox = false;
            this.Controls.Add(this.btnQuitter);
            this.Controls.Add(this.btmFermer);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStart";
            this.ShowInTaskbar = false;
            this.Text = "Start";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Start_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelStatusBadge;
        private System.Windows.Forms.Label LabelStatusCN;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btmFermer;
        private System.Windows.Forms.Button btnQuitter;
    }
}