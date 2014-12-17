namespace Ambitour
{
    partial class frmExecution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExecution));
            this.groupBoxVerifs = new System.Windows.Forms.GroupBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.groupBoxPrechauffage = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.checkedListBoxPrechauffage = new System.Windows.Forms.CheckedListBox();
            this.BtnPrechauffage = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBoxVerifs.SuspendLayout();
            this.groupBoxPrechauffage.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxVerifs
            // 
            this.groupBoxVerifs.BackColor = System.Drawing.Color.Ivory;
            this.groupBoxVerifs.Controls.Add(this.checkedListBox1);
            this.groupBoxVerifs.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.groupBoxVerifs.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBoxVerifs.Location = new System.Drawing.Point(12, 342);
            this.groupBoxVerifs.Name = "groupBoxVerifs";
            this.groupBoxVerifs.Size = new System.Drawing.Size(452, 202);
            this.groupBoxVerifs.TabIndex = 11;
            this.groupBoxVerifs.TabStop = false;
            this.groupBoxVerifs.Text = "Vérifications avant usinage";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.Color.Ivory;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Vérifier les DECS (avec une jauge de profondeur)",
            "Vérifier l\'Origine  Programme (Origine FAO=DECS)",
            "Vérifier la correspondance Outils tourelle/Outils FAO",
            "Vérifier les jauges outils",
            "Vérifier le serrage de la pièce et la concentricité",
            "Mettre le potentiomètre des avances à 0"});
            this.checkedListBox1.Location = new System.Drawing.Point(10, 46);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(421, 132);
            this.checkedListBox1.TabIndex = 0;
            // 
            // groupBoxPrechauffage
            // 
            this.groupBoxPrechauffage.BackColor = System.Drawing.Color.Ivory;
            this.groupBoxPrechauffage.Controls.Add(this.richTextBox1);
            this.groupBoxPrechauffage.Controls.Add(this.checkedListBoxPrechauffage);
            this.groupBoxPrechauffage.Controls.Add(this.BtnPrechauffage);
            this.groupBoxPrechauffage.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.groupBoxPrechauffage.ForeColor = System.Drawing.SystemColors.InfoText;
            this.groupBoxPrechauffage.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPrechauffage.Name = "groupBoxPrechauffage";
            this.groupBoxPrechauffage.Size = new System.Drawing.Size(452, 297);
            this.groupBoxPrechauffage.TabIndex = 10;
            this.groupBoxPrechauffage.TabStop = false;
            this.groupBoxPrechauffage.Text = "Préchauffage de la broche";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Ivory;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.Maroon;
            this.richTextBox1.Location = new System.Drawing.Point(11, 37);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(424, 132);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // checkedListBoxPrechauffage
            // 
            this.checkedListBoxPrechauffage.BackColor = System.Drawing.Color.Ivory;
            this.checkedListBoxPrechauffage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBoxPrechauffage.CheckOnClick = true;
            this.checkedListBoxPrechauffage.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxPrechauffage.FormattingEnabled = true;
            this.checkedListBoxPrechauffage.Items.AddRange(new object[] {
            "Reculer la tourelle",
            "Vérifier la présence d\'une pièce dans le mandrin"});
            this.checkedListBoxPrechauffage.Location = new System.Drawing.Point(6, 182);
            this.checkedListBoxPrechauffage.Name = "checkedListBoxPrechauffage";
            this.checkedListBoxPrechauffage.Size = new System.Drawing.Size(425, 44);
            this.checkedListBoxPrechauffage.TabIndex = 2;
            this.checkedListBoxPrechauffage.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxPrechauffage_SelectedIndexChanged);
            // 
            // BtnPrechauffage
            // 
            this.BtnPrechauffage.Enabled = false;
            this.BtnPrechauffage.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrechauffage.Location = new System.Drawing.Point(10, 252);
            this.BtnPrechauffage.Name = "BtnPrechauffage";
            this.BtnPrechauffage.Size = new System.Drawing.Size(425, 27);
            this.BtnPrechauffage.TabIndex = 1;
            this.BtnPrechauffage.Text = "Exécuter le programme de préchauffage";
            this.BtnPrechauffage.UseVisualStyleBackColor = true;
            this.BtnPrechauffage.Click+=new System.EventHandler(BtnPrechauffage_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 575);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(775, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // frmExecution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(775, 597);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxVerifs);
            this.Controls.Add(this.groupBoxPrechauffage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 85);
            this.Name = "frmExecution";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.groupBoxVerifs.ResumeLayout(false);
            this.groupBoxPrechauffage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxVerifs;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.GroupBox groupBoxPrechauffage;
        private System.Windows.Forms.CheckedListBox checkedListBoxPrechauffage;
        private System.Windows.Forms.Button BtnPrechauffage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.RichTextBox richTextBox1;

    }
}