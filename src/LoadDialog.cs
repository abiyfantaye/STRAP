using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace StracturalControls
{  
    public partial class LoadDialog : Form
    {
        public Point [] pt;
        public bool Pselect = false;
        public Point SelectedPoint;
        public ArrayList Endl = new ArrayList();
        public string SelectedMember;
        public LoadDialog()
        {
            InitializeComponent();
        }
        public ArrayList _LoadGraph = new ArrayList();
        public ArrayList JointLoad = new ArrayList();
        public LoadDialog(ArrayList LoadGraph,ArrayList jointload)
        {
            _LoadGraph = LoadGraph;
            JointLoad = jointload;
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
        public LoadDialog ld;
        public Member[] mbr;
        public Joint[] Joints;
        public bool Lefttoright = true;
        public void DrawLoad(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            maths mts = new maths();
            for (int i = 0; i < ld.JointLoad.Count; i+=4)
            {
                float magnitude = float.Parse(ld.JointLoad[i + 2].ToString());
                float angle = float.Parse(ld.JointLoad[i + 3].ToString());
                if (ld.JointLoad[i].ToString() == "Force")
                {

                    DrawConcetrated(g, Joints[int.Parse(ld.JointLoad[i + 1].ToString())-1].CleintCoordinate,angle,magnitude);
                }
                else if (ld.JointLoad[i].ToString() == "Moment")
                {
                    DrawMomentSing(g, Joints[int.Parse(ld.JointLoad[i + 1].ToString()) - 1].CleintCoordinate, magnitude);
                }
            }
            for (int i = 0; i < ld._LoadGraph.Count; i += 7)
            {       
                    string  LoadType = "Concentrated" ;
                    PointF p = new PointF();
                    float angle = 0;
                try
                {
                    LoadType = ld._LoadGraph[i + 1].ToString();
                    p.X = (float)pt[MemberIndex(ld._LoadGraph[i].ToString())].X + (1/MainForm.scale)* (float)Math.Cos(mts.DR(ld.mbr[MemberIndex(ld._LoadGraph[i].ToString())].Angle))*(float)ld._LoadGraph[i+3];
                    p.Y = (float)pt[MemberIndex(ld._LoadGraph[i].ToString())].Y - (1 / MainForm.scale) * (float)Math.Sin(mts.DR(ld.mbr[MemberIndex(ld._LoadGraph[i].ToString())].Angle)) * (float)ld._LoadGraph[i + 3];
                    angle = -(float)ld.mbr[MemberIndex(Convert.ToString(ld._LoadGraph[i]))].Angle+(float)ld._LoadGraph[i + 5]; 
                }
                catch (Exception)
                {
                    MessageBox.Show("Error"); //does nothing but stop interuption
                }
                switch (LoadType)
                {
                    case "Concentrated":
                        DrawConcetrated(g, p, angle,(float)ld._LoadGraph[i + 2] );
                        break;
                    case "Distributed":
                        DrawDistributed(g, p, angle, (float)ld._LoadGraph[i + 2], (float)(MainForm.scale* length(pt[MemberIndex(ld._LoadGraph[i].ToString())], pt[MemberIndex(ld._LoadGraph[i].ToString()) + 1])) - (float)ld._LoadGraph[i + 3]-(float)ld._LoadGraph[i + 4]);
                        break;
                    case "Triangular":
                        DrawTriangular(g, p, angle, (float)ld._LoadGraph[i + 2], (float)(MainForm.scale * length(pt[MemberIndex(ld._LoadGraph[i].ToString())], pt[MemberIndex(ld._LoadGraph[i].ToString()) + 1])) - (float)ld._LoadGraph[i + 3] - (float)ld._LoadGraph[i + 4],(bool)ld._LoadGraph[i+6]);
                        break;
                }
            }
        }
        public Point[] triagles(Point pt)
        {
            Point[] result = new Point[3];
            result[0] = new Point(pt.X - 3, pt.Y);
            result[1] = new Point(pt.X + 3, pt.Y);
            result[2] = new Point(pt.X, pt.Y + 6);
            return result;
        }
        public void DrawMomentSing(Graphics g,Point pt, double magnitude)
        {
            Pen pen = new Pen(Color.Gold, 2.0f);
            SolidBrush brush = new SolidBrush(Color.Gold);
            SolidBrush StringBrush = new SolidBrush(Color.White);
            FontStyle style = FontStyle.Regular;
            Font areal = new Font(new FontFamily("Arial"), 8, style);
            Matrix m = new Matrix();
            m.Translate(pt.X, pt.Y); 
            Rectangle rect = new Rectangle(-15, -15, 30, 30);           
            g.Transform = m;
            g.DrawArc(pen, rect, -90, 180);
            Point[] p = new Point[3];
            p[1] = new Point(1, -19);
            p[0] = new Point(1, -11);
            p[2] = new Point(-8,-14);
            g.FillPolygon(brush, p);
            g.DrawString(magnitude.ToString()+" KN.m", areal, StringBrush, -15, -30);
            g.ResetTransform();
        }
        public void DrawConcetrated( Graphics g,PointF p ,float angle,float magnitude)
        { 
            Pen pen = new Pen(Color.Gold,2.0f);
            SolidBrush brush = new SolidBrush (Color.Gold);
            SolidBrush StringBrush = new SolidBrush(Color.White);
            FontStyle style = FontStyle.Regular;
            Font areal = new Font(new FontFamily("Arial"),8, style);
            Matrix m = new Matrix();
            int height;
            height = 10 + (int)Math.Pow(800 * Math.Abs(magnitude),0.35);
            m.Translate(p.X-4.0f, p.Y-height-8);
            m.RotateAt(angle, p,MatrixOrder.Append);           
            g.Transform = m;
            g.DrawLine(pen, 4, 0, 4, height);
            g.FillPolygon(brush, triagles(new Point(4, height)));
            g.DrawString(Math.Round ((double)magnitude,2).ToString()+" KN", areal, StringBrush, 6.0f, -5.0f);
            m.Reset();
            g.ResetTransform();
        }
        public void DrawDistributed(Graphics g, PointF p, float angle, float magnitude,float length)
        {
            Pen pen = new Pen(Color.Gold, 1.0f);
            SolidBrush brush = new SolidBrush(Color.Gold);
            SolidBrush StringBrush = new SolidBrush(Color.White);
            FontStyle style = FontStyle.Regular;
            Font areal = new Font(new FontFamily("Arial"), 8, style);
            Matrix m = new Matrix();        
            int Width, Height;
            Width = (int)((1/MainForm.scale)* length);
            Height = 10 + (int)Math.Pow(800 * Math.Abs( magnitude), 0.35);
            m.Translate(p.X , p.Y -Height);
            m.RotateAt(angle, p, MatrixOrder.Append);
            g.Transform = m;
            g.DrawString(Math.Round((double)magnitude,2).ToString() + " KN/m", areal,StringBrush, Width / 2 ,-12 );
            g.DrawLine(pen, 0, 0, Width, 0);
            g.DrawLine(pen, 0, 0, 0, Height);
            g.DrawLine(pen, Width , 0, Width, Height);
            int nuarow = Width / 15;
            int spacing = 15;
            for (int i = 0; (i <= nuarow) & ((i * spacing + 10)<=Width); i++)
            {
                g.DrawLine(pen, spacing * i, 0, spacing * i, Height - 6);
                g.FillPolygon(brush, triagles(new Point(i * spacing , Height - 7)));
            }
            g.FillPolygon(brush,triagles(new Point(Width,Height - 7)));
            m.Reset();
            g.ResetTransform();
        }
        public void DrawTriangular(Graphics g, PointF p, float angle, float magnitude, float length, bool direction)
        {
            Pen pen = new Pen(Color.Gold, 1.0f);
            SolidBrush brush = new SolidBrush(Color.Gold);
            SolidBrush StringBrush = new SolidBrush(Color.White);
            FontStyle style = FontStyle.Regular;
            Font areal = new Font(new FontFamily("Arial"), 8, style);
            Matrix m = new Matrix();
            int Width, Height;
            Width = (int)((1 / MainForm.scale) * length);
            Height = 10 + (int)Math.Pow(Math.Abs(800 * magnitude), 0.4);
            m.Translate(p.X, p.Y - Height);
            m.RotateAt(angle, p, MatrixOrder.Append);
            g.Transform = m;
            float k = (float)Height / Width;
            int nuarow =Width / 15;
            int spacing = 15;
            if (direction)
            {
                g.DrawLine(pen, Width, 0, 0, Height - 2);
                g.DrawLine(pen, Width, 0, Width, Height - 2);
                g.DrawString(magnitude.ToString() + "KN/m", areal, StringBrush, new PointF(Width - 2, -10));
                for (int i = (int)(0.6 * (1 / k)); i <= nuarow; i++)
                {
                    g.DrawLine(pen, (spacing * i), Height - k * spacing * i, (spacing * i), (Height - 6));
                    g.FillPolygon(brush, triagles(new Point(i * spacing, Height - 7)));
                }
            }
            else
            {
                g.DrawLine(pen, 0, 0, 0, Height - 2);
                g.DrawLine(pen, 0, 0, Width, Height - 2);
                g.DrawString(magnitude.ToString() + "KN/m", areal, StringBrush, new PointF(0, -12));
                for (int i = 0; i <= (nuarow -(int)(0.6 * (1 / k))) ; i++)
                {
                    g.DrawLine(pen, (15 * i), (Height - 6),(15 * i),k * 15 * i);
                    g.FillPolygon(brush, triagles(new Point(i * spacing, Height - 7)));
                }
            }
            g.ResetTransform();
            m.Reset();
        }
        public void DrawTrapizoidal(Graphics g, PointF p, float angle, float magnitude)
        {
        }
        private float length(Point p1, Point p2)
        {
            // this method is used to retrun the length of the line P1-P2
            return (float)Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
        public bool CheckIfNmbr(KeyPressEventArgs e, string str)
        {
            bool notnumber = false;
            if ((!char.IsControl(e.KeyChar)) &(!char.IsDigit(e.KeyChar)) &(e.KeyChar != '.' )& (e.KeyChar != '-' ))
            {   
                notnumber = true;
            }
            if ((e.KeyChar == '.' && str.Contains('.'))|(e.KeyChar == '-' && str.Contains('-')))       
            {
                notnumber  = true;
            }
            return notnumber;
        }
        public bool checkIfONum(KeyPressEventArgs e,string str)
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
        public bool IsZerAMember(string name)
        {
            bool found = false;
            try
            {
                for (int i = 0; i < ld.mbr.Length; i++)
                {

                    if ((!ld.Endl.Contains(i)) && (ld.mbr[i].Name == name))
                    {
                        found = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No stractural drawing is found.Pleas first input the drawing!", "Drawing Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return found;
        }
        public int IsZeSameLoad(string name,float angle, float DsNE,string LoadType)
        {
             int index  = -1;

                for (int i = 0; i < ld._LoadGraph.Count; i += 7)
                {
                    if ((ld._LoadGraph[i].ToString() == name) & (ld._LoadGraph[i + 1].ToString() == LoadType) & ((float)ld._LoadGraph[i + 3] == DsNE) & ((float)ld._LoadGraph[i + 5] == angle))
                        index = i;
                }
            return index;
        }
        private void txtMagnitude_KeyPress(object sender, KeyPressEventArgs e)
        {  
            e.Handled = CheckIfNmbr(e,txtMagnitude.Text);         
        }
        public float Length(string name)
        {
            float length = -1;
            for (int i = 0; i < ld.mbr.Length; i++)
            {
                if ((!ld.Endl.Contains(i))&&(ld.mbr[i].Name == name)  )
                {
                    length = (float)ld.mbr[i].Length;
                }
            }
            return length;
        }
        private int MemberIndex(string name)
        {
            int index = -1;
            for (int i = 0; i < ld.mbr.Length; i++)
            {
                if ((!ld.Endl.Contains(i)) && ld.mbr[i].Name == name)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        private void txtDsFFarEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = checkIfONum(e, txtDsFromNearEnd.Text); 
        }
        private void txtLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = checkIfONum(e, txtDsFromFarEnd.Text);  
        }
        private void txtAngle_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = CheckIfNmbr(e, txtAngle.Text);
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ConsLoad_Click(object sender, EventArgs e)
        {
            ConsLoad.Selected  = IsSelected(ConsLoad.Selected);
            if (ConsLoad.Selected)
            {
                gbxLoadingInfo.Enabled = true;
                txtDsFromFarEnd.Enabled = false;
                txtAngle.Enabled = true;
                triangularLoad1.Enabled = triangul1.Selected = false;
                triangul1.Enabled = triangul1.Selected = false;
            }
            else
            {
                gbxLoadingInfo.Enabled = false;
                
            }
            DistLoad.Selected = TriangLoad.Selected = TrapLoad.Selected = false;
        }
        private void DistLoad_Click(object sender, EventArgs e)
        {
            DistLoad.Selected = IsSelected(DistLoad.Selected);
            
            if (DistLoad.Selected)
            {
                txtDsFromFarEnd.Enabled = true;
                gbxLoadingInfo.Enabled = true;
                txtAngle.Enabled = false;
                triangul1.Enabled = triangul1.Selected = false;
                triangularLoad1.Enabled = triangularLoad1.Selected = false;
            }
            ConsLoad.Selected = TriangLoad.Selected  = TrapLoad.Selected = false;
        }
        private void TriangLoad_Click(object sender, EventArgs e)
        {
            TriangLoad.Selected = IsSelected(TriangLoad.Selected);
            if (TriangLoad.Selected)
            {
                gbxLoadingInfo.Enabled = true;
                txtDsFromFarEnd.Enabled = true;
                triangul1.Enabled = true;
                triangularLoad1.Enabled = true;
            }
            ConsLoad.Selected = DistLoad.Selected = TrapLoad.Selected = false;
        }
        private void TrapLoad_Click(object sender, EventArgs e)
        {
            TrapLoad.Selected = IsSelected(TrapLoad.Selected);
            if (TrapLoad.Selected)
            {
                gbxLoadingInfo.Enabled = true;
                txtDsFromFarEnd.Enabled = true;
                triangul1.Enabled = true;
                triangularLoad1.Enabled = true;
            }
            ConsLoad.Selected = DistLoad.Selected = TriangLoad.Selected = false;
        }
        private bool IsSelected(bool check)
        {
            if (check)
                check = false;
            else
                check = true;
            return check;
        }
        private int IndexOfPoint(Point p)
        {
            int index = 0;
            for (int i = 0; i < Joints.Length; i++)
            {
                if (Joints[i].CleintCoordinate == p)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            bool check = true ;
            float angle = 0, DsFromNearEnd = 0, DsFromFarEnd= 0, Magnitude = 0;
            try
            {
                angle = float.Parse(txtAngle.Text);
                Magnitude = float.Parse(txtMagnitude.Text);
                DsFromNearEnd = float.Parse(txtDsFromNearEnd.Text);
                DsFromFarEnd = float.Parse(txtDsFromFarEnd.Text);   
                           
            }
            catch (Exception)
            {
                MessageBox.Show("Your inputs are not correct.Pleas check and try agian.", "Improper Imput", MessageBoxButtons.OK, MessageBoxIcon.Error);
                check = false;
            }
            if (txtDsFromFarEnd.Text != "" & txtDsFromNearEnd.Text != "" & txtAngle.Text != "" & txtMagnitude.Text != "" & txtMbrName.Text != ""&!Pselect)
            {
                if (!IsZerAMember(txtMbrName.Text)&check)
                {
                    MessageBox.Show(" The member you input doesn't exist in the stractural drawing. Pleas check you inputs","Member Is Missing",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                else if (ConsLoad.Selected&check)
                {
                    if (DsFromNearEnd > Length(txtMbrName.Text))
                    {
                        MessageBox.Show(" You input for this load is inconsistant! 'Distance From Near End' should be less than the length of the member.", "Inconsistant Loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DsFromFarEnd  = Length(txtMbrName.Text) - DsFromNearEnd;
                        AddLoad(txtMbrName.Text, "Concentrated", Magnitude, DsFromNearEnd, (float)(ld.mbr[MemberIndex(txtMbrName.Text)].Length - DsFromNearEnd), angle, true);
                    }
                }
                else if (DistLoad.Selected&check)
                {
                 AddLoad(txtMbrName.Text, "Distributed", Magnitude, DsFromNearEnd, DsFromFarEnd, angle,true);                  
                }
                else if (TriangLoad.Selected&check)
                {   
                    AddLoad(txtMbrName.Text,"Triangular", Magnitude, DsFromNearEnd, DsFromFarEnd, angle,Lefttoright);
                }
                else if (TrapLoad.Selected&check)
                {
                    MessageBox.Show(" Sory for the inconsistancy this fueture of the program is still undder development.", "Under Development", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if(check)
                {
                    MessageBox.Show("Pleas select load type first.", "Load Type Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(" Pleas fill all the rquired fields first! And try again. ", "All Feilds Required");
            }
        }
        private void AddLoad(string name,string typ,float Magnitude, float DsFromNearEnd,float DsFromFarEnd,float angle,bool LeftToRight)
        {
           if (IsZeSameLoad(name,angle, DsFromNearEnd, typ)>=0)
            {
                if ((DsFromFarEnd + DsFromNearEnd) > Length(name))
                {
                    MessageBox.Show(" You input for this load is inconsistant!The sum of 'Distance From Near End' and 'Distance From Far End' should be less than the length of the member.", "Inconsistant Loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    int indx = IsZeSameLoad(name, angle, DsFromNearEnd, typ);
                    ld._LoadGraph[indx + 4] = DsFromFarEnd;
                    ld._LoadGraph[indx + 2] = Magnitude;
                    ld._LoadGraph[indx + 5] = angle;
                    ld._LoadGraph[indx + 6] = LeftToRight;
                    this.Close();
                }
            }
            else
            {
                if (Length(name) < (DsFromFarEnd + DsFromNearEnd))
                {
                    MessageBox.Show(" You input for this load is inconsistant!The sum of 'Distance From Near End' and 'Distance From Far End' should be less than the length of the member.", "Inconsistant Loading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ld._LoadGraph.Add(name);
                    ld._LoadGraph.Add(typ);
                    ld._LoadGraph.Add(Magnitude);
                    ld._LoadGraph.Add(DsFromNearEnd);
                    ld._LoadGraph.Add(DsFromFarEnd);
                    ld._LoadGraph.Add(angle);
                    ld._LoadGraph.Add(LeftToRight);
                    this.Close();
                }
            }
        }
        private void LoadDialog_Click(object sender, EventArgs e)
        {
            ConsLoad.Selected = TriangLoad.Selected = TrapLoad.Selected =  DistLoad.Selected = gbxLoadingInfo.Enabled =  false;
        }
        private void LoadDialog_Load(object sender, EventArgs e)
        {
            if (DistLoad.Selected | TriangLoad.Selected | TrapLoad.Selected)
                ConsLoad.Selected = false;
            else
            {
                ConsLoad.Selected = true;
                txtDsFromFarEnd.Enabled = false;
            }
            gbxLoadingInfo.Enabled = true;
            txtMbrName.Text = ld.SelectedMember;
        }
        private void triangularLoad1_Click(object sender, EventArgs e)
        {
            if (triangularLoad1.Selected)
            {
                triangularLoad1.Selected = false;
            }
            else
            {
                triangularLoad1.Selected = true;
                Lefttoright = true;
                triangul1.Selected = false;
            }

        }
        private void triangul1_Click(object sender, EventArgs e)
        {
            if (triangul1.Selected)
            {
                triangul1.Selected = false;
            }
            else
            {
                triangul1.Selected = true;
                Lefttoright = false;
                triangularLoad1.Selected = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Sory for the inconsistancy this fueture of the program is still undder development.", "Under Development", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private int NearEndJointIndex(Member mebr)
        {
            int Index = -1;
            for (int i = 0; i < Joints.Length; i++)
            {
                if (Joints[i].CleintCoordinate == mebr.NECDNT)
                {
                    Index = i;
                    break;
                }
            }
            return Index;
        }
        private int FarEndJointIndex(Member mebr)
        {
            int Index = -1;
            for (int i = 0; i < Joints.Length; i++)
            {
                if (Joints[i].CleintCoordinate == mebr.FECDNT)
                {
                    Index = i;
                    break;
                }
            }
            return Index;
        }
    }
}
