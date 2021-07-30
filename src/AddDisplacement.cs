using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StracturalControls
{
    public partial class AddDisplacement : Form
    {
        public AddDisplacement()
        {
            InitializeComponent();
        }
        public Joint[] Joints;
        public double[,] Displacement;
        public MainForm.Stracture stracture;
        public bool IsSI;
        private void AddDisplacement_Load(object sender, EventArgs e)
        {
            ndnJoint_ValueChanged(sender, e);
            ndnJoint.Minimum = 1;
            ndnJoint.Maximum = Joints.Length;
        }
        private void ndnJoint_ValueChanged(object sender, EventArgs e)
        {
            txtSupportType.Text = Joints[(int)ndnJoint.Value-1].SupportType;
            switch (Joints[(int)ndnJoint.Value - 1].SupportType)
            {
                case "Fixed":
                    txtXDisp.Enabled = true;
                    txtYDisp.Enabled = true;
                    txtZDisp.Enabled = true;
                    break;
                case "Pin":
                    if (stracture == MainForm.Stracture.Beam)
                    {
                        txtXDisp.Enabled = false;
                    }
                    else
                    txtXDisp.Enabled = true;
                    txtYDisp.Enabled = true;
                    txtZDisp.Enabled = false;
                    break;
                case "Roler":

                    txtXDisp.Enabled = false;
                    txtYDisp.Enabled = true;
                    txtZDisp.Enabled = false;
                    break;
                default:
                    txtXDisp.Enabled = false;
                    txtYDisp.Enabled = false;
                    txtZDisp.Enabled = false;
                    break;
            }
            txtXDisp.Text = Joints[(int)ndnJoint.Value - 1].DispX.ToString();
            txtYDisp.Text = Joints[(int)ndnJoint.Value - 1].DispY.ToString();
            txtZDisp.Text = Joints[(int)ndnJoint.Value - 1].DispZ.ToString();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtXDisp.Text != "" && txtYDisp.Text != "" && txtZDisp.Text != "")
            {
                try
                {
                    Displacement[(int)ndnJoint.Value - 1, 0] = double.Parse(txtXDisp.Text);
                    Displacement[(int)ndnJoint.Value - 1, 1] = double.Parse(txtYDisp.Text);
                    Displacement[(int)ndnJoint.Value - 1, 2] = double.Parse(txtZDisp.Text);
                    if(ndnJoint.Value<ndnJoint.Maximum)
                    ndnJoint.Value++;
                    ndnJoint_ValueChanged(sender, e);
                }
                catch (Exception)
                {
                    MessageBox.Show(" Your inputs are not valid pleas chech and try agian.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(" Pleas first fill all the fields and try agian.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
