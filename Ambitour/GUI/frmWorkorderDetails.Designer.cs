namespace Ambitour.GUI
{
    partial class frmWorkorderDetails
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblActualStartDate = new System.Windows.Forms.Label();
            this.lblActualEndDate = new System.Windows.Forms.Label();
            this.txtWorkorderRoutingNo = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblScheduledStartDate = new System.Windows.Forms.Label();
            this.lblScheduledEndDate = new System.Windows.Forms.Label();
            this.lblWorkorderRoutingNo = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "StartOF";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(233, 136);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "StopOF";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lblActualStartDate
            // 
            this.lblActualStartDate.AutoSize = true;
            this.lblActualStartDate.Location = new System.Drawing.Point(14, 162);
            this.lblActualStartDate.Name = "lblActualStartDate";
            this.lblActualStartDate.Size = new System.Drawing.Size(13, 13);
            this.lblActualStartDate.TabIndex = 16;
            this.lblActualStartDate.Text = "--";
            // 
            // lblActualEndDate
            // 
            this.lblActualEndDate.AutoSize = true;
            this.lblActualEndDate.Location = new System.Drawing.Point(238, 162);
            this.lblActualEndDate.Name = "lblActualEndDate";
            this.lblActualEndDate.Size = new System.Drawing.Size(13, 13);
            this.lblActualEndDate.TabIndex = 17;
            this.lblActualEndDate.Text = "--";
            // 
            // txtWorkorderRoutingNo
            // 
            this.txtWorkorderRoutingNo.Location = new System.Drawing.Point(94, 21);
            this.txtWorkorderRoutingNo.Name = "txtWorkorderRoutingNo";
            this.txtWorkorderRoutingNo.Size = new System.Drawing.Size(214, 20);
            this.txtWorkorderRoutingNo.TabIndex = 18;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(94, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(214, 20);
            this.textBox1.TabIndex = 19;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(94, 73);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(214, 20);
            this.textBox2.TabIndex = 20;
            // 
            // lblScheduledStartDate
            // 
            this.lblScheduledStartDate.AutoSize = true;
            this.lblScheduledStartDate.Location = new System.Drawing.Point(14, 54);
            this.lblScheduledStartDate.Name = "lblScheduledStartDate";
            this.lblScheduledStartDate.Size = new System.Drawing.Size(75, 13);
            this.lblScheduledStartDate.TabIndex = 21;
            this.lblScheduledStartDate.Text = "Début planifié ";
            // 
            // lblScheduledEndDate
            // 
            this.lblScheduledEndDate.AutoSize = true;
            this.lblScheduledEndDate.Location = new System.Drawing.Point(14, 80);
            this.lblScheduledEndDate.Name = "lblScheduledEndDate";
            this.lblScheduledEndDate.Size = new System.Drawing.Size(63, 13);
            this.lblScheduledEndDate.TabIndex = 22;
            this.lblScheduledEndDate.Text = "Fin planifiée";
            // 
            // lblWorkorderRoutingNo
            // 
            this.lblWorkorderRoutingNo.AutoSize = true;
            this.lblWorkorderRoutingNo.Location = new System.Drawing.Point(13, 24);
            this.lblWorkorderRoutingNo.Name = "lblWorkorderRoutingNo";
            this.lblWorkorderRoutingNo.Size = new System.Drawing.Size(34, 13);
            this.lblWorkorderRoutingNo.TabIndex = 23;
            this.lblWorkorderRoutingNo.Text = "WOR";
            // 
            // frmWorkorderDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(484, 262);
            this.ControlBox = false;
            this.Controls.Add(this.lblWorkorderRoutingNo);
            this.Controls.Add(this.lblScheduledEndDate);
            this.Controls.Add(this.lblScheduledStartDate);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txtWorkorderRoutingNo);
            this.Controls.Add(this.lblActualEndDate);
            this.Controls.Add(this.lblActualStartDate);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "frmWorkorderDetails";
            this.Text = "frmWorkorderDetails";
            this.Load += new System.EventHandler(this.frmWorkorderDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblActualStartDate;
        private System.Windows.Forms.Label lblActualEndDate;
        private System.Windows.Forms.TextBox txtWorkorderRoutingNo;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblScheduledStartDate;
        private System.Windows.Forms.Label lblScheduledEndDate;
        private System.Windows.Forms.Label lblWorkorderRoutingNo;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}