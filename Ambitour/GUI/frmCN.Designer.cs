namespace Ambitour
{
    partial class frmCN
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
            this.lblNumeroProgrammeCourant = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.grpCN = new System.Windows.Forms.GroupBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblEnCycle = new System.Windows.Forms.Label();
            this.lbllblEncycle = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCN = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpCN.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNumeroProgrammeCourant
            // 
            this.lblNumeroProgrammeCourant.AutoSize = true;
            this.lblNumeroProgrammeCourant.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroProgrammeCourant.ForeColor = System.Drawing.Color.Brown;
            this.lblNumeroProgrammeCourant.Location = new System.Drawing.Point(59, 43);
            this.lblNumeroProgrammeCourant.Name = "lblNumeroProgrammeCourant";
            this.lblNumeroProgrammeCourant.Size = new System.Drawing.Size(165, 16);
            this.lblNumeroProgrammeCourant.TabIndex = 0;
            this.lblNumeroProgrammeCourant.Text = "NumeroProgrammeCourant";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 73;
            this.label1.Text = "Prog. %";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 75;
            this.label3.Text = "Mode ";
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.Brown;
            this.lblMode.Location = new System.Drawing.Point(59, 69);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(40, 16);
            this.lblMode.TabIndex = 76;
            this.lblMode.Text = "Mode";
            // 
            // grpCN
            // 
            this.grpCN.Controls.Add(this.lblMessage);
            this.grpCN.Controls.Add(this.lblEnCycle);
            this.grpCN.Controls.Add(this.lbllblEncycle);
            this.grpCN.Controls.Add(this.lblMode);
            this.grpCN.Controls.Add(this.lblNumeroProgrammeCourant);
            this.grpCN.Controls.Add(this.label3);
            this.grpCN.Controls.Add(this.label1);
            this.grpCN.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCN.ForeColor = System.Drawing.SystemColors.InfoText;
            this.grpCN.Location = new System.Drawing.Point(12, 12);
            this.grpCN.Name = "grpCN";
            this.grpCN.Size = new System.Drawing.Size(191, 161);
            this.grpCN.TabIndex = 78;
            this.grpCN.TabStop = false;
            this.grpCN.Text = "NUM1050";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(6, 117);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(49, 16);
            this.lblMessage.TabIndex = 81;
            this.lblMessage.Text = "Defaut";
            // 
            // lblEnCycle
            // 
            this.lblEnCycle.AutoSize = true;
            this.lblEnCycle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnCycle.ForeColor = System.Drawing.Color.Brown;
            this.lblEnCycle.Location = new System.Drawing.Point(59, 92);
            this.lblEnCycle.Name = "lblEnCycle";
            this.lblEnCycle.Size = new System.Drawing.Size(12, 16);
            this.lblEnCycle.TabIndex = 80;
            this.lblEnCycle.Text = "-";
            // 
            // lbllblEncycle
            // 
            this.lbllblEncycle.AutoSize = true;
            this.lbllblEncycle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllblEncycle.Location = new System.Drawing.Point(5, 92);
            this.lbllblEncycle.Name = "lbllblEncycle";
            this.lbllblEncycle.Size = new System.Drawing.Size(43, 16);
            this.lbllblEncycle.TabIndex = 79;
            this.lbllblEncycle.Text = "Cycle";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCN});
            this.statusStrip1.Location = new System.Drawing.Point(0, 478);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(228, 22);
            this.statusStrip1.TabIndex = 80;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCN
            // 
            this.toolStripStatusLabelCN.Name = "toolStripStatusLabelCN";
            this.toolStripStatusLabelCN.Size = new System.Drawing.Size(27, 17);
            this.toolStripStatusLabelCN.Text = "Prêt";
            // 
            // frmCN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(228, 500);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpCN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(775, 85);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCN";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.frmCN_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCN_FormClosed);
            this.grpCN.ResumeLayout(false);
            this.grpCN.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNumeroProgrammeCourant;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.GroupBox grpCN;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCN;
        private System.Windows.Forms.Label lblEnCycle;
        private System.Windows.Forms.Label lbllblEncycle;
        private System.Windows.Forms.Label lblMessage;
    }
}