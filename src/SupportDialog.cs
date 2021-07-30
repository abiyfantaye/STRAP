using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Collections;
using System.Drawing.Drawing2D;
namespace StracturalControls
{
    public partial class SupportDialog :Form
    {
        public MainForm.Stracture stracture;
        public Point[] pt;
        public Joint Jnt;
        public Joint[] Joints;
        public SupportDialog()
        {
            InitializeComponent();
        }
        public PointF ReffPoin;// used to store the refference for using only in this class
        public PointF RefferencePoint
        {
            // is used to strore the refference point passed by the main form
            get
            {
                return ReffPoin;
            }
            set
            {
                ReffPoin = value;
            }
        }
        public SupportDialog sd;//object of this class used as a varialble for the main Form
        public ArrayList _SupGraph = new ArrayList();//array list used to store all point which are provided with support
        public SupportDialog(ArrayList SuportGraphics)
        {
            _SupGraph = SuportGraphics;
        }
        public void DrawSupport(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            try
            {
                for (int i = 0; i < sd._SupGraph.Count; i += 3)
                {
                    // this loop is used to draw all the supports from the support array list
                    string ch = Convert.ToString(_SupGraph[i]);// extract the caracter of the support type from the array list
                    Point pt = (Point)_SupGraph[i + 1];// extract the location fo the point from the array list
                    PointF p = new PointF(pt.X, pt.Y);
                    float angle = (float)Convert.ToDouble(_SupGraph[i + 2]);
                    switch (ch)
                    {
                        case "Pin":// case when the support is pin
                            DrawPin(g, p, angle);
                            break;
                        case "Fixed":// case when the support is fixed
                            DrawFixed(g, p, angle);
                            break;
                        case "Roler":// case when the support is roller
                            DrawRoller(g, p, angle);
                            break;
                        case "Hindge":// case when the support is hidge
                            DrawHidge(g, p);
                            break;
                    }
                }
            }
            catch (NullReferenceException)
            {
                // if ther is exception jump with out any interaption and action
            }
        }
        private void DrawPin(Graphics g,PointF p,float angle)
        {
            // this method is used to draw the pin for specifed point and angle           
            Pen pen = new Pen(Color.HotPink, 2.0f);
            int Width = 30, Height = 25;
            Matrix m = new Matrix();
            m.Translate(p.X - Width / 2, p.Y);
            m.RotateAt(angle,p , MatrixOrder.Append);
            g.Transform = m;
            Point[] pt = new Point[3];
            pt[0] = new Point(Width / 2, 0);
            pt[1] = new Point(Width - 5, Height - 5);
            pt[2] = new Point(5, Height - 5);
            g.DrawPolygon(pen, pt);
            for (int j = 0; j <= Width / 5; j++)
            {
                g.DrawLine(pen, (j - 1) * 5, Height, j * 5, Height - 5);
            }
            g.DrawLine(pen, 0, Height - 5, Width + 2, Height - 5);
        }
        private void DrawFixed(Graphics g,PointF p,float angle)
        {
            Matrix m = new Matrix();
            Pen pen = new Pen(Color.HotPink, 2.0f);
            int Width, Height;
            Width = 30;
            Height = 5;
            m.Translate(p.X - Width / 2, p.Y);
            m.RotateAt(angle, p, MatrixOrder.Append);
            g.Transform = m;
            g.DrawLine(pen, 0,1,Width, 1);
            for(int i = 0; i <= Width / 5; i++)
            {
                g.DrawLine(pen, (i - 1) * 5, Height, i * 5, Height - 4);
            }
        }
        private void DrawRoller(Graphics g,PointF p,float angle)
        {
            Matrix m = new Matrix();
            int Width, Height;
            Width = 30;
            Height = 25;
            m.Translate(p.X - Width / 2, p.Y);
            m.RotateAt(angle, p, MatrixOrder.Append);
            g.Transform = m;
            Pen pen = new Pen(Color.HotPink, 2.0f);
            Point[] pt = new Point[3];
            pt[0] = new Point(Width / 2, 0);
            pt[1] = new Point(Width - 5, Height - 10);
            pt[2] = new Point(5, Height - 10);
            g.DrawPolygon(pen, pt);
            g.DrawLine(pen,0,Height-5,Width,Height-5);
            for (int i = 0; i<= Width / 5; i++)
            {
                g.DrawLine(pen, (i - 1) * 5, Height, i * 5, Height - 5);
            }
            g.DrawEllipse(pen, 3, Height - 10, 5, 5);
            g.DrawEllipse(pen, 12, Height - 10, 5, 5);
            g.DrawEllipse(pen, 21, Height - 10, 5, 5);

        }
        private void DrawHidge(Graphics g, PointF p)
        {
            g.FillEllipse(new SolidBrush(Color.HotPink), p.X - 5, p.Y - 5, 10, 10);
            g.FillEllipse(new SolidBrush(Color.Black), p.X - 2, p.Y - 2, 4, 4);
        }
        private void pin1_Click(object sender, EventArgs e)
        {
            if (!pin1.Selected)
            {
                hindge1.Selected = roler1.Selected = fixed1.Selected = false;
                pin1.Selected = true;
                txtSupAngle.Enabled = true;
            }
            else
                pin1.Selected = false;
        }
        private void hindge1_Click(object sender, EventArgs e)
        {
            if (!hindge1.Selected)
            {
                pin1.Selected = roler1.Selected = fixed1.Selected = false;
                hindge1.Selected = true;
                txtSupAngle.Enabled = false;
                txtSupAngle.Text = "0";
            }
            else
            {
                hindge1.Selected = false;
                txtSupAngle.Enabled = true;
            }
        }
        private void roler1_Click(object sender, EventArgs e)
        {
            if (!roler1.Selected)
            {
                fixed1.Selected = hindge1.Selected = pin1.Selected = false;
                roler1.Selected = true;
                txtSupAngle.Enabled = true;
            }
            else
                roler1.Selected = false;
        }
        private void fixed1_Click(object sender, EventArgs e)
        {
            if (!fixed1.Selected)
            {
                fixed1.Selected = true;
                roler1.Selected = pin1.Selected = hindge1.Selected = false;
                txtSupAngle.Enabled = true;
            }
            else
                fixed1.Selected = false;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {  
            pin1.Selected = roler1.Selected = fixed1.Selected = hindge1.Selected = false;
            txtSupLocation.Text = txtSupAngle.Text = "";
        }
        private void SupportDialog_Click(object sender, EventArgs e)
        {
            pin1.Selected = roler1.Selected = fixed1.Selected = hindge1.Selected = false;
        }
        private void txtSupAngle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar )&!char.IsControl(e.KeyChar)&e.KeyChar != '.'&& e.KeyChar!='-')
                e.Handled = true;
            if(e.KeyChar == '.'& txtSupAngle.Text.Contains('.'))
                e.Handled = true;
            if (e.KeyChar == '-' && txtSupAngle.Text.Contains('-'))
                e.Handled = true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (pin1.Selected | roler1.Selected | fixed1.Selected | hindge1.Selected)
            {
                if (txtSupAngle.Text != "" && txtSupLocation.Text != "")
                {
                    float angle = 0;
                    bool check = false;
                    try
                    {
                        angle = float.Parse(txtSupAngle.Text);
                        foreach (Joint joint in Joints)
                        {
                            if (joint.Name == txtSupLocation.Text)
                            {
                                check = true;
                                break;
                            }
                        }
                        if(!check)
                        MessageBox.Show("The point selected  should be a defined point in the stractural drawing", "Point Is Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(" Your inputs are not correct! Pleas check and try again");
                        check = false;
                    }
                    if (pin1.Selected & check)
                    {
                        AddSupport("Pin", Joints[int.Parse(txtSupLocation.Text)-1].CleintCoordinate, angle);
                        Jnt.SupportType = "Pin";
                    }
                    else if (fixed1.Selected && check)
                    {
                        AddSupport("Fixed", Joints[int.Parse(txtSupLocation.Text)-1].CleintCoordinate, angle);
                        Jnt.SupportType = "Fixed";
                    }
                    else if (roler1.Selected && check)
                    {
                        AddSupport("Roler", Joints[int.Parse(txtSupLocation.Text)-1].CleintCoordinate, angle);
                        Jnt.SupportType = "Roler";
                    }
                    else if (hindge1.Selected & check)
                    {
                        AddSupport("Hindge", Joints[int.Parse(txtSupLocation.Text) - 1].CleintCoordinate, angle);
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Pleas fill all the fields first and try again");
                }
            }
            else
            {
                MessageBox.Show("Pleas select support type first ", "Support Type Required", MessageBoxButtons.OK);
            }
        }
        private void AddSupport(string ch, Point p, float angle)
        {
           // this method is used to add support type, angle and location for the structure
            if (sd._SupGraph.Contains(p))// if the point contain a support we replace with the new support
            {
                int indx = sd._SupGraph.IndexOf(Jnt.CleintCoordinate);
                sd._SupGraph[indx - 1] = ch;
                sd._SupGraph[indx + 1] = angle;
            }
           // else if there is no suppot at the point we add new support
            else 
            {  
                sd._SupGraph.Add(ch);
                sd._SupGraph.Add(p);
                sd._SupGraph.Add(angle);
            }
        }
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtSupLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) & !char.IsControl(e.KeyChar) & e.KeyChar != '.'& e.KeyChar!=','&e.KeyChar!='-')
                e.Handled = true;
            if (e.KeyChar == ',' & txtSupLocation.Text.Contains(','))
                e.Handled = true;
        }
        private void SupportDialog_Load(object sender, EventArgs e)
        {
            hindge1.Enabled = false;
            if (stracture == MainForm.Stracture.Truss)
            {
                fixed1.Selected = false;
                fixed1.Enabled = false;
            }
            else
            {
                fixed1.Enabled = true;
            }
            txtSupAngle.Text = "0";
            if (Jnt != null)
                txtSupLocation.Text = Jnt.Name;
            else txtSupLocation.Text = "1";
        }
    }
}
