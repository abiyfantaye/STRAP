using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace StracturalControls
{
    public partial class JointLoad : Form
    {
        public JointLoad()
        {
            InitializeComponent();
        }
        public MainForm.Stracture stracture;
        public Joint[] Joints;
        public ArrayList jointload;
        public string JointName = "1";
        private maths m = new maths();
        private bool MomentSelected = false;
        private void btnConsMoment_Paint(object sender, PaintEventArgs e)
        {
             Graphics g = e.Graphics;
             if (MomentSelected)
             {
                 g.FillRectangle(new SolidBrush(Color.White), btnConsMoment.ClientRectangle);
                 DrawMementSign(g);
             }
             else
             {
                 btnConsMoment.Invalidate();
                 DrawMementSign(g);
             }
        }
        private void DrawMementSign(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2.0f);
            SolidBrush Sbrush = new SolidBrush(Color.Black);
            Rectangle rect = new Rectangle(10, 10, btnConsMoment.Width - 20, btnConsMoment.Height - 20);
            g.DrawArc(pen, rect, -120, 180);
            Point[] p = new Point[3];
            g.DrawLine(pen, btnConsMoment.Width / 2 - 5, btnConsMoment.Height / 2, btnConsMoment.Width / 2 + 5, btnConsMoment.Height / 2);
            g.DrawLine(pen, btnConsMoment.Width / 2, btnConsMoment.Height / 2 - 5, btnConsMoment.Width / 2, btnConsMoment.Height / 2 + 5);
            p[0] = new Point(btnConsMoment.Width / 2, 6);
            p[1] = new Point(btnConsMoment.Width / 2, 14);
            p[2] = new Point(btnConsMoment.Width / 2 - 8, 10);
            g.FillPolygon(Sbrush, p);
            g.DrawRectangle(pen, btnConsMoment.ClientRectangle);
        }
        private void btnConsMoment_Click(object sender, EventArgs e)
        {
            if (MomentSelected)
            {
                ConsForce.Selected = false;
                MomentSelected = false;
                Graphics g = btnConsMoment.CreateGraphics();
                PaintEventArgs p = new PaintEventArgs(g, btnConsMoment.ClientRectangle);
                btnConsMoment_Paint(sender, p);
            }
            else
            {
                txtAngle.Enabled = false;
                MomentSelected = true;
                ConsForce.Selected = false;
                Graphics g = btnConsMoment.CreateGraphics();
                PaintEventArgs p = new PaintEventArgs(g, btnConsMoment.ClientRectangle);
                btnConsMoment_Paint(sender, p);
            }
        }
        private void btnConsMoment_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = btnConsMoment.CreateGraphics();
            PaintEventArgs p = new PaintEventArgs(g, btnConsMoment.ClientRectangle);
            btnConsMoment_Paint(sender, p);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckIfJointExist())
                {
                    if (ConsForce.Selected & txtAngle.Text != "" & txtJoint.Text != "" & txtMagnitude.Text != "")
                    {
                        jointload.Add("Force");
                        jointload.Add(txtJoint.Text);
                        jointload.Add(double.Parse(txtMagnitude.Text));
                        jointload.Add(txtAngle.Text);
                        this.Close();
                    }
                    else if (MomentSelected & txtAngle.Text != "" & txtJoint.Text != "" & txtMagnitude.Text != "")
                    {
                        jointload.Add("Moment");
                        jointload.Add(txtJoint.Text);
                        jointload.Add(double.Parse(txtMagnitude.Text));
                        jointload.Add(0);
                        this.Close();
                    }
                    else if (txtAngle.Text == "" || txtJoint.Text == "" || txtMagnitude.Text == "")
                        MessageBox.Show("Pleas fill all the required filds first and try again");
                }
                else
                    MessageBox.Show("The specified Joint does not exist. Plaes check and try again.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Input! Plaes check and try again.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!MomentSelected & !ConsForce.Selected)
                MessageBox.Show("Plaes select load type first!.", "Load Type Required");                
        }
        private bool CheckIfJointExist()
        {
            bool check = false;
            for (int i = 0; i < Joints.Length; i++)
            {
                if (Joints[i].Name == txtJoint.Text)
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ConsForce_Click_1(object sender, EventArgs e)
        {
            if (ConsForce.Selected)
            {
                ConsForce.Selected = false;
                MomentSelected = false;
                Graphics g = btnConsMoment.CreateGraphics();
                PaintEventArgs p = new PaintEventArgs(g, btnConsMoment.ClientRectangle);
                btnConsMoment_Paint(sender, p);
            }
            else
            {
                ConsForce.Selected = true;
                MomentSelected = false;
                txtAngle.Enabled = true;
                Graphics g = btnConsMoment.CreateGraphics();
                PaintEventArgs p = new PaintEventArgs(g, btnConsMoment.ClientRectangle);
                btnConsMoment_Paint(sender, p);
            }
        }
        private void JointLoad_Load(object sender, EventArgs e)
        {
            txtJoint.Text = JointName;
            ConsForce.Selected = true;
            if (stracture == MainForm.Stracture.Truss)
            {
                btnConsMoment.Enabled = false;
                MomentSelected = false;
            }
            else
                btnConsMoment.Enabled = true;
        }
    }
}
