namespace Ambitour
{
    partial class frm3D
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm3D));
            this.axVIA3DXMLPlugin1 = new AxVIA3DXMLPluginLib.AxVIA3DXMLPlugin();
            ((System.ComponentModel.ISupportInitialize)(this.axVIA3DXMLPlugin1)).BeginInit();
            this.SuspendLayout();
            // 
            // axVIA3DXMLPlugin1
            // 
            this.axVIA3DXMLPlugin1.Enabled = true;
            this.axVIA3DXMLPlugin1.Location = new System.Drawing.Point(0, 0);
            this.axVIA3DXMLPlugin1.Name = "axVIA3DXMLPlugin1";
            this.axVIA3DXMLPlugin1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVIA3DXMLPlugin1.OcxState")));
            this.axVIA3DXMLPlugin1.Size = new System.Drawing.Size(912, 722);
            this.axVIA3DXMLPlugin1.TabIndex = 0;
            // 
            // frm3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 725);
            this.Controls.Add(this.axVIA3DXMLPlugin1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Location = new System.Drawing.Point(700, 50);
            this.Name = "frm3D";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "3DXml Viewer";
            this.Load += new System.EventHandler(this.frm3D_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axVIA3DXMLPlugin1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxVIA3DXMLPluginLib.AxVIA3DXMLPlugin axVIA3DXMLPlugin1;




    }
}