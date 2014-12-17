namespace Ambitour
{
    partial class frmPreparation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPreparation));
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grpDossier = new System.Windows.Forms.GroupBox();
            this.lblDossierCourant = new System.Windows.Forms.Label();
            this.grpProgramme = new System.Windows.Forms.GroupBox();
            this.lblProgramme = new System.Windows.Forms.Label();
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.grpChoix = new System.Windows.Forms.GroupBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblProgression = new System.Windows.Forms.RichTextBox();
            this.grpDossier.SuspendLayout();
            this.grpProgramme.SuspendLayout();
            this.grpChoix.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Crimson;
            this.lblStatus.Location = new System.Drawing.Point(8, 68);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "lblStatus";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(10, 312);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(375, 23);
            this.btnRefresh.TabIndex = 82;
            this.btnRefresh.Text = "Lister vos dossiers de ";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // grpDossier
            // 
            this.grpDossier.Controls.Add(this.lblDossierCourant);
            this.grpDossier.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDossier.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpDossier.Location = new System.Drawing.Point(12, 29);
            this.grpDossier.Name = "grpDossier";
            this.grpDossier.Size = new System.Drawing.Size(251, 81);
            this.grpDossier.TabIndex = 85;
            this.grpDossier.TabStop = false;
            this.grpDossier.Text = "Dossier en cours";
            // 
            // lblDossierCourant
            // 
            this.lblDossierCourant.AutoSize = true;
            this.lblDossierCourant.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDossierCourant.ForeColor = System.Drawing.Color.Brown;
            this.lblDossierCourant.Location = new System.Drawing.Point(6, 34);
            this.lblDossierCourant.Name = "lblDossierCourant";
            this.lblDossierCourant.Size = new System.Drawing.Size(80, 23);
            this.lblDossierCourant.TabIndex = 86;
            this.lblDossierCourant.Text = "----------";
            // 
            // grpProgramme
            // 
            this.grpProgramme.Controls.Add(this.lblProgramme);
            this.grpProgramme.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.grpProgramme.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grpProgramme.Location = new System.Drawing.Point(12, 116);
            this.grpProgramme.Name = "grpProgramme";
            this.grpProgramme.Size = new System.Drawing.Size(251, 82);
            this.grpProgramme.TabIndex = 87;
            this.grpProgramme.TabStop = false;
            this.grpProgramme.Text = "Programme pièce";
            // 
            // lblProgramme
            // 
            this.lblProgramme.AutoSize = true;
            this.lblProgramme.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgramme.ForeColor = System.Drawing.Color.Green;
            this.lblProgramme.Location = new System.Drawing.Point(6, 38);
            this.lblProgramme.Name = "lblProgramme";
            this.lblProgramme.Size = new System.Drawing.Size(80, 23);
            this.lblProgramme.TabIndex = 86;
            this.lblProgramme.Text = "----------";
            // 
            // treeView
            // 
            this.treeView.BackColor = System.Drawing.Color.Ivory;
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView.ImageIndex = 1;
            this.treeView.ImageList = this.imageList1;
            this.treeView.Location = new System.Drawing.Point(24, 87);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(344, 219);
            this.treeView.TabIndex = 1;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "fichier.gif");
            this.imageList1.Images.SetKeyName(1, "doc_dossier.jpg");
            // 
            // grpChoix
            // 
            this.grpChoix.BackColor = System.Drawing.Color.Ivory;
            this.grpChoix.Controls.Add(this.richTextBox2);
            this.grpChoix.Controls.Add(this.richTextBox1);
            this.grpChoix.Controls.Add(this.treeView);
            this.grpChoix.Controls.Add(this.btnRefresh);
            this.grpChoix.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpChoix.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpChoix.Location = new System.Drawing.Point(271, 29);
            this.grpChoix.Name = "grpChoix";
            this.grpChoix.Size = new System.Drawing.Size(391, 425);
            this.grpChoix.TabIndex = 89;
            this.grpChoix.TabStop = false;
            this.grpChoix.Text = "Choix du dossier de fabrication";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.Ivory;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(10, 357);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(375, 52);
            this.richTextBox2.TabIndex = 84;
            this.richTextBox2.Text = "En cas de problème, vous pouvez relancer la préparation en cliquant sur le progra" +
                "mme pièce de l\'arborescence.";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Ivory;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(24, 29);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(225, 52);
            this.richTextBox1.TabIndex = 83;
            this.richTextBox1.Text = "Copiez vos dossiers dans le répertoire ";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 560);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(783, 22);
            this.statusStrip1.TabIndex = 90;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.CausesValidation = false;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(200, 16);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Ambitour.Properties.Resources.logo_620x620_Ivory_2;
            this.pictureBox1.Location = new System.Drawing.Point(12, 204);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 250);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblProgression
            // 
            this.lblProgression.BackColor = System.Drawing.Color.Ivory;
            this.lblProgression.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblProgression.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgression.Location = new System.Drawing.Point(12, 473);
            this.lblProgression.Name = "lblProgression";
            this.lblProgression.Size = new System.Drawing.Size(650, 52);
            this.lblProgression.TabIndex = 91;
            this.lblProgression.Text = "";
            // 
            // frmPreparation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(783, 582);
            this.ControlBox = false;
            this.Controls.Add(this.lblProgression);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpChoix);
            this.Controls.Add(this.grpProgramme);
            this.Controls.Add(this.grpDossier);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(0, 85);
            this.Name = "frmPreparation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.frmPreparation_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPreparation_FormClosed);
            this.grpDossier.ResumeLayout(false);
            this.grpDossier.PerformLayout();
            this.grpProgramme.ResumeLayout(false);
            this.grpProgramme.PerformLayout();
            this.grpChoix.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox grpDossier;
        private System.Windows.Forms.Label lblDossierCourant;
        private System.Windows.Forms.GroupBox grpProgramme;
        private System.Windows.Forms.Label lblProgramme;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.GroupBox grpChoix;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox lblProgression;
        

    }
}