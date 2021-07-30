using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StracturalControls
{
    public partial class AddConstant : Form
    {
        public AddConstant()
        {
            InitializeComponent();
        }
        public MainForm.Stracture stracture;
        public double[] Const = new double[3];
        public Member[] Members;
        public ArrayList EndL = new ArrayList();
        public bool IsSI;
        private void txtArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = checkIfONum(e, txtArea.Text);
        }
        private void txtI_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = checkIfONum(e, txtI.Text);
        }
        private void txtE_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = checkIfONum(e, txtE.Text);
        }
        public bool checkIfONum(KeyPressEventArgs e, string str)
        {
            bool notnumber = false;
            if ((!char.IsControl(e.KeyChar)) & (!char.IsDigit(e.KeyChar)) & (e.KeyChar != '.'))
            {
                notnumber = true;
            }
            if (e.KeyChar == '.' && str.Contains('.'))
                notnumber = true;
            return notnumber;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtE.Text != "" & txtI.Text != "" & txtArea.Text != "")
            {
                Const[0] =  double.Parse(txtArea.Text);
                Const[1] =  double.Parse(txtI.Text);
                Const[2] =  double.Parse(txtE.Text);
                for (int i = 0; i < Members.Length; i++)
                {
                    if (!EndL.Contains(i))
                    {
                        Members[i].Area = Const[0];
                        Members[i].MI = Const[1];
                        Members[i].ME = Const[2];
                    }
                }
                this.Close();
            }
            else
                MessageBox.Show("Plaes fill the required feilds first.", "All Feilds Required");

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddConstant_Load(object sender, EventArgs e)
        {
            if (stracture == MainForm.Stracture.Truss)
            {
                txtArea.Enabled = txtE.Enabled = true;
                txtI.Enabled = false;
            }
            else if (stracture == MainForm.Stracture.Beam)
            {
                txtArea.Enabled = false;
                txtE.Enabled = txtI.Enabled = true;
            }
            else if (stracture == MainForm.Stracture.Frame)
            {
                txtArea.Enabled = txtE.Enabled = txtI.Enabled = true;

            }
            txtArea.Text = Members[0].Area.ToString();
            txtE.Text = Members[0].ME.ToString();
            txtI.Text = Members[0].MI.ToString();
        }
    }
}
