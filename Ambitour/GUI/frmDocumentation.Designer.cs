namespace Ambitour
{
    partial class frmDocumentation
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Dossier CATIA");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Programme");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("3DXML");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentation));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.richTextBoxProgramme = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpVisualiseur = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1.SuspendLayout();
            this.grpVisualiseur.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.Ivory;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(6, 33);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "DossierCATIA";
            treeNode1.Text = "Dossier CATIA";
            treeNode2.Name = "Noeud2";
            treeNode2.Text = "Programme";
            treeNode3.Name = "Noeud3Dxml";
            treeNode3.Text = "3DXML";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(251, 489);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "fichier.gif");
            this.imageList1.Images.SetKeyName(1, "doc_dossier.jpg");
            this.imageList1.Images.SetKeyName(2, "html.gif");
            this.imageList1.Images.SetKeyName(3, "3DXML.JPG");
            // 
            // richTextBoxProgramme
            // 
            this.richTextBoxProgramme.BackColor = System.Drawing.Color.White;
            this.richTextBoxProgramme.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxProgramme.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richTextBoxProgramme.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxProgramme.Location = new System.Drawing.Point(18, 33);
            this.richTextBoxProgramme.Name = "richTextBoxProgramme";
            this.richTextBoxProgramme.ReadOnly = true;
            this.richTextBoxProgramme.Size = new System.Drawing.Size(446, 489);
            this.richTextBoxProgramme.TabIndex = 1;
            this.richTextBoxProgramme.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 541);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Docs disponibles";
            // 
            // grpVisualiseur
            // 
            this.grpVisualiseur.Controls.Add(this.richTextBoxProgramme);
            this.grpVisualiseur.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.grpVisualiseur.ForeColor = System.Drawing.SystemColors.InfoText;
            this.grpVisualiseur.Location = new System.Drawing.Point(281, 12);
            this.grpVisualiseur.Name = "grpVisualiseur";
            this.grpVisualiseur.Size = new System.Drawing.Size(476, 541);
            this.grpVisualiseur.TabIndex = 4;
            this.grpVisualiseur.TabStop = false;
            this.grpVisualiseur.Text = "Programme ISO";
            this.grpVisualiseur.UseWaitCursor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 575);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(785, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmDocumentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(785, 597);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpVisualiseur);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(0, 85);
            this.Name = "frmDocumentation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.Documentation_Load);
            this.groupBox1.ResumeLayout(false);
            this.grpVisualiseur.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.RichTextBox richTextBoxProgramme;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpVisualiseur;
        private System.Windows.Forms.StatusStrip statusStrip1;

    }
}