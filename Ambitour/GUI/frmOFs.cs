using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ambitour.CoucheMetier.ObjetsMetier;


namespace Ambitour
{
    public partial class frmOFs : Form
    {
        public delegate void UpdateDoorStatusLabel(string text, Color color);


        OFAgent agent;

        public frmOFs()
        {
            InitializeComponent();
        }

        private void frmOFs_Load(object sender, EventArgs e)
        {
            agent = new OFAgent(this);
            agent.RaiseNewOF += new OFAgent.NewOF(agent_RaiseNewOF);
        }

        void agent_RaiseNewOF()
        {
            string msg = "New OF received...";
            Console.WriteLine(msg);
            object[] p = GetInvokerParameters(msg, Color.Orange);
            BeginInvoke(new UpdateDoorStatusLabel(UpdateLabelText), p);
        }

        private object[] GetInvokerParameters(string labelMessage,
                                                Color labelColor)
        {
            // We have to create an object array as this is the only way our
            // UpdateLabelText method can receive the parameters
            object[] delegateParameter = new object[2];

            delegateParameter[0] = labelMessage;
            delegateParameter[1] = labelColor;

            return delegateParameter;
        }

        private void UpdateLabelText(string text, Color color)
        {
            this.textBox1.Text = text;
            this.textBox1.ForeColor = color;
        }


    }
}
