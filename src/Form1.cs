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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace StracturalControls
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public enum Stracture{Truss,Beam,Frame};
        public Stracture SelectedStr;
        public int k = 0, x = 0, y = 0,Xvalue,Yvalue;
        public int t = 0, mt = 0, _t = 0, s = 0;
        public Point EqualPoint;       
        public PointF Reff_Point = new PointF();
        public const float scale = (float)(0.125*0.25);// drawing scale for the dimensions 
        public double[] Constants =  {0,0,200};
        public double[,] Displacements = new double [0,3];
        public Rectangle[] Prect;//public variable which represents point rectangles
        public Region[] Mregion;// public variable which represents member regions
        public Rectangle[] Row;
        public Rectangle[] Column;
        public bool pselect = false, contain = false, insert = false,equalpoint_x = false;
        public bool mselect = false, member = false, endline = false,equalpoint_y = false;
        public bool Add_Refferece = false,IsSI = true;
        public Point[] pt = new Point[0];// point array of all joint points
        public ArrayList points = new ArrayList();// arraylist to handle point 
        public ArrayList EndL = new ArrayList();// arraylist to handle end line points
        public Rectangle tempr;// temporary rectable to hold red points
        public Pen white = new Pen(Color.White, 2.0f);
        public Pen erase = new Pen(Color.Black, 2.0f);
        public SolidBrush brsh = new SolidBrush(Color.Black);
        public Joint[] Joints = new Joint[0];
        maths math = new maths();
        public Member[] Members = new Member[0];
        mMatrixs[] Mmatrix; 
        public Member[] AdjMember = new Member[1];
        private sMatrix SolutionMatrix;
        public Member M = new Member();
        SupportDialog SD  = new SupportDialog ();
        LoadDialog LD = new LoadDialog();
        LoadSummary LS = new LoadSummary();
        LableGraphics LG = new LableGraphics();
        public ArrayList jointload = new ArrayList();
        public ArrayList SupportGraphis = new ArrayList();
        private void ArrowDraw()
        {
            Graphics g = drawpanel.CreateGraphics();
            Pen p = new Pen(Color.White);
            SolidBrush Brush = new SolidBrush(Color.White);
            Point[] p1 = new Point[3];
            Point[] p2 = new Point[3];
            g.DrawLine(p, 24, drawpanel.Height - 24, 24, drawpanel.Height - 96);
            g.DrawLine(p, 24, drawpanel.Height - 24, 96, drawpanel.Height - 24);
            p1[0] = new Point(20, drawpanel.Height - 96);
            p1[1] = new Point(24, drawpanel.Height - 104);
            p1[2] = new Point(28, drawpanel.Height - 96);
            g.FillPolygon(Brush, p1);
            p2[0] = new Point(96, drawpanel.Height - 28);
            p2[1] = new Point(96, drawpanel.Height - 20);
            p2[2] = new Point(104, drawpanel.Height - 24);
            g.FillPolygon(Brush, p2);
            FontStyle style = FontStyle.Regular;
            Font timesNewRoman = new Font("Times New Roman", 10, style);
            g.DrawString("Y", timesNewRoman, Brush, 32, drawpanel.Height - 96);
            g.DrawString("X", timesNewRoman, Brush, 86, drawpanel.Height - 45);
        }
        public void DrawGrid()
        {  
            Row = new Rectangle[drawpanel.Height / 8 - 1];
            Column = new Rectangle[drawpanel.Width / 8 - 1];
            for (int i = 0; i < Row.Length; i++)
            {
                Row[i] = new Rectangle(0, i * 8 + 4, drawpanel.Width, 8);
            }
            for (int i = 0; i <Column.Length; i++)
            {
                Column[i] = new Rectangle(i * 8 + 4, 0, 8, drawpanel.Height);
            }
        }
        private void Grid_Point(ref int ex,ref int ey)
        {

            for(int i = 0;i< Row.Length;i++)
            {
               for(int j = 0;j<Column.Length ;j++)
               {
                if(Column[j].Contains(ex,ey)&&Row[i].Contains(ex,ey))
                {
                       ex = j*8+8;
                       ey = i*8+8;
                break;
                }
               }
            }           
        }
        private void AddPoint(int ex, int ey)
        {
            // This method is used to add a new point when ever the mousedown event is trigered 
            bool added = false;
            for (int i = 0; i < k; i++)
            {
                if (Prect[i].Contains(ex, ey))
                {
                    points.Add(pt[i]);//the newly added point is modified by already existing one
                    added = true;
                    break;// breaks if any found
                }
            }
            if (!added)
                points.Add(new Point(ex, ey)); // if no rectagle is found then the point will be add as a new point                                            
            k++;// adds one for the added point
            pt = (Point[])points.ToArray(points[0].GetType());//copy the arraylist to array
        }
        private void rectCreator()
        {
            //This method is used to add a new rectagles for clicked points
            Prect = new Rectangle[k];
            for (int i = 0; i < k; i++)
            {
                Prect[i] = new Rectangle(pt[i].X - 5, pt[i].Y - 5, 10, 10);
            }
        }
        private bool IsMbrCld(int ex, int ey)
        {
            // this method checks if a member is clicked using its global region
            bool clicked = false;
            for (int i = 0; i < k - 1; i++)
            {
                if (!EndL.Contains(i))
                {
                    if (Mregion[i].IsVisible(new Point(ex, ey)))
                    {
                        clicked = true;
                        break;
                    }
                }
            }
            return clicked;
        }
        private int FIndx(int ex, int ey)
        {
            // this method returns the index of the first occurace of the points
            int Clicled_Point = -1;
            for (int i = 0; i < k; i++)
            {
                if (Prect[i].Contains(ex, ey))
                {
                    Clicled_Point = i;
                    break;
                }
            }
            return Clicled_Point;
        }
        private int FIndx(Point p)
        {
            // this method returns the index of the first occurace of the points
            int Clicled_Point = -1;
            for (int i = 0; i < k; i++)
            {
                if (Prect[i].Contains(p))
                {
                    Clicled_Point = i;
                    break;
                }
            }
            return Clicled_Point;
        }
        private int LstClkdPt(int ex, int ey)
        {
            // this method returns the index of the last occurace of the points
            int Clicled_Point = -1;
            for (int i = 0; i < k; i++)
            {
                if (Prect[i].Contains(ex, ey))
                {
                    Clicled_Point = i;
                }
            }
            return Clicled_Point;
        }
        private bool IsPointClkd(int ex, int ey)
        {
            // this method is used to check if a point is clicked or not clicked
            bool Is_Clicked = false;
            for (int i = 0; i < k; i++)
            {
                if (Prect[i].Contains(ex, ey))
                {
                    Is_Clicked = true;
                    break;
                }
            }
            return Is_Clicked;
        }
        private int ClkdMbr(int ex, int ey)
        {
            // this method returns the index of the clicked member if not clicked it returnes -1
            int Clicked_Member = -1;
            for (int i = 0; i < k - 1; i++)
            {
                if (!EndL.Contains(i))
                {
                    if (Mregion[i].IsVisible(new Point(ex, ey)))
                    {
                        Clicked_Member = i;
                        break;
                    }
                }
            }
            return Clicked_Member;
        }
        private bool IsInsAlwdMbr(int m)
        {
            // this method checkes if insert is allowed in the clicked member m
            bool Is_Allowed = false;
            ArrayList B = new ArrayList();// is used to store all the insert not allowed members
            for (int i = 0; i < k; i++)
            {
                if (pt[i] == pt[k - 1])
                {
                    if (i == (k - 1))
                    {
                        B.Add(i - 1); //if i is the index of the last point clicked the only member is the last one
                    }
                    else if (i == 0)
                    {
                        B.Add(i);// if i is the index of the first point the the member will be itself 
                    }
                    else
                    {
                        B.Add(i - 1); // if it is between firs and last
                        B.Add(i);
                    }
                }
            }
            if (!B.Contains(m))// cheches if m is insert allowed member
                Is_Allowed = true;
            return Is_Allowed;
        }
        private bool IsMbrRptd(Point p)
        {
            Random rad = new Random();// creation of radom object rad
            // This method checks if a member is repeated when ever the point p is clicked
            bool Is_Repeated = false;// set initialy reapeated to false
            ArrayList CR = new ArrayList();// temporary arraylist used to store inserted points with the point array
            int a = 0;// incriminate variable if insertion is done
            CR = (ArrayList)points.Clone();// copies the original point array to the temporary array
            Point[] pnt = new Point[pt.Length + EndL.Count];// array of point for checking
            for (int i = 0; i < k; i++)
            {
                if (EndL.Contains(i))
                {
                    Point insrt = new Point(rad.Next(-952312356, 158946472), rad.Next(-952312356, 158946472));// rademization for inserted seodoPoints
                    CR.Insert(i + 1 + a, insrt);// if aready inseted it will add icrimination
                    a++;// incriminate if inseted
                }
            }

            /* I have taken other point array insted of using the original temporary array because  
              it is not posible to compare an object instance and a point */

            pnt = (Point[])CR.ToArray(CR[0].GetType());// copy the temporary array list to point array for comparison
            // this loop check if a point is repeated taking two couple points at a time
            for (int i = 0; i < CR.Count - 1; i++)
            {
                if ((pnt[i] == pnt[pnt.Length - 1]) | (pnt[i + 1] == pnt[pnt.Length - 1]))
                {
                    if ((pnt[i] == p) | (pnt[i + 1] == p))
                    {
                        Is_Repeated = true;
                        break;
                    }
                }
            }
            return Is_Repeated;
        }
        private void InsertPoint(int ex, int ey)
        {
            Graphics g = drawpanel.CreateGraphics();
            int i = ClkdMbr(ex, ey);
            int Yinsert = 0;
            k += 2;// if a member is inserted we add one for inserted and one for added point
            try
            {
                // here we calculate the y for given x
                Yinsert = (int)(((pt[i + 1].Y - pt[i].Y) * (ex - pt[i].X)) / (pt[i + 1].X - pt[i].X) + pt[i].Y);
            }
            catch (DivideByZeroException)
            {
                Yinsert = ey;
            }
            points.Insert(i + 1, new Point(ex, Yinsert));// inserts the point
            points.Add(new Point(ex, Yinsert));// adds the point 
            pt = (Point[])points.ToArray(points[0].GetType());// converts array list to Point array
            g.DrawLine(new Pen(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray)), pt[k - 2], new Point(ex, ey));// removes the line do to the moving mouse       
            g.Dispose();
        }
        private void CMemberClicked(int ex, int ey)
        {
            /*this method is used to select the a member 
             which is clicked in the region of a member while the user is not drawing */
            for (int i = 0; i < k - 1; i++)
            {
                if (!EndL.Contains(i))
                {
                    if (Mregion[i].IsVisible(new Point(ex, ey)))
                    {
                        maths mth = new maths();
                        Joint J = new Joint();
                        double angle = 0;
                        angle = Math.Round(mth.angle(pt[i], pt[i + 1]), 3);
                        Members[i].Angle = mth.angle(pt[i], pt[i + 1]);
                        txtAngleFromHorz.Text = angle.ToString();
                        txtMname.Text = Members[i].Name;
                        txtMarea.Text = Members[i].Area.ToString();
                        txtMMI.Text = Members[i].MI.ToString();
                        txtMME.Text = Members[i].ME.ToString();
                        txtNearEndCoordinate.Text = J.CleintToRefference(Members[i].NECDNT, Reff_Point).X.ToString()+ "," + J.CleintToRefference(Members[i].NECDNT, Reff_Point).Y.ToString();
                        txtFarEndCoordinate.Text = J.CleintToRefference(Members[i].FECDNT, Reff_Point).X.ToString() + "," + J.CleintToRefference(Members[i].FECDNT, Reff_Point).Y.ToString();                        
                        txtMlength.Text = Members[i].Length.ToString();// print the length
                        gbxMember.Enabled = true;// enable the goup box
                        mselect = true;// the member is selected
                        mt = i;// send the selected number to the public variable
                        break;
                    }
                }
            }
        }
        private void CPointClicked(int ex, int ey)
        {
            // this Method is used to enable the point information group box to accept data when a point is clicked
            Graphics g = drawpanel.CreateGraphics();
            for (int i = 0; i < k; i++)
            {
                if (Prect[i].Contains(ex, ey))
                {
                    _t = i;// passes the point to public vaiabls which allow display
                    s = i;// pasess the point to public variable which allow editing
                    Joint J = new Joint();
                    pselect = true;
                    gbxPoint.Enabled = true;
                    int index =  LG.IndexOf(LG.RemoveRepeatedPoints(pt), pt[i]);
                    txtPoint.Text = Joints[index].Coordinate.X.ToString() + "," + Joints[index].Coordinate.Y.ToString();// displays the coordinate    
                    txtJointName.Text = Joints[index].Name;
                    txtSuportType.Text = Joints[index].SupportType;
                    if (SelectedStr == Stracture.Truss)
                    {
                        txtSSMIndexs.Text = "( " +  Convert.ToInt16(Joints[index].SSMatrixIndexs[0]+1) + " , " + Convert.ToInt16(Joints[index].SSMatrixIndexs[1]+1) + " )";
                    }
                    else if (SelectedStr == Stracture.Beam)
                    {
                        txtSSMIndexs.Text = "( " + Convert.ToInt16(Joints[index].SSMatrixIndexs[1]+1) + " , " + Convert.ToInt16(Joints[index].SSMatrixIndexs[2] + 1)+" )";
                    }
                    else
                        txtSSMIndexs.Text = "( " + Convert.ToInt16(Joints[index].SSMatrixIndexs[0]+1) + " , " + Convert.ToInt16(Joints[index].SSMatrixIndexs[1]+1) + " , " +Convert.ToInt16(Joints[index].SSMatrixIndexs[2]+1)+ " )";
                    break;
                } 
            }
        }
        private void Message(int condition)
        {
            // This method is used to prepare all the nessary message boxs for each end every exeption
            switch (condition)
            {
                case 1:
                    MessageBox.Show("It is not allowed for two member to overlap each other. Pleas select other point ", "Members Overlaped", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 2:
                    MessageBox.Show("Sory you have repeated a member.Pleas select other Point.", "Member Repeated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case 3:
                    MessageBox.Show("You have drawn extrimly small member! And the program cosidered\n it as a point ignoring the  stractural acuracy achived", "Verry Small Member ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    MessageBox.Show("Sory some erors has ocured.Pleas try other option");
                    break;
            }
        }
        private void AdjstEndL()
        {
            ArrayList NEndL = new ArrayList();// declear temporary arrayList used for adjestemt
            NEndL = (ArrayList)EndL.Clone();// copy the elements form the first aray list
            NEndL.Insert(0, -10000000);// add -infinity at the begining
            int[] TEndL = (int[])NEndL.ToArray(NEndL[0].GetType());
            // this loop is used to add a icrimination for each non extistin point 
            for (int i = 0; i < EndL.Count; i++)
            {
                if ((TEndL[i] < FIndx(pt[k - 1])) & (FIndx(pt[k - 1]) <= TEndL[i + 1]))
                {
                    for (int j = i; j < EndL.Count; j++)
                    {
                        EndL[j] = Convert.ToInt32(EndL[j]) + 1;
                    }
                }
            }
        }
        private double length(Point p1, Point p2)
        {
            // this method is used to retrun the length of the line P1-P2
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
        private void RegCreator()
        {
            // this method is used to create a member region for each members 
            if (k > 1)
            {
                int r = 1;
                GraphicsPath gp = new GraphicsPath();
                Member[] tempmember;
                Mregion = new Region[k - 1];
                try
                {
                    tempmember = new Member[Members.Length];

                }
                catch (Exception)
                {
                    Members = new Member[0];
                    tempmember = new Member[0];
                }
                for (int i = 0; i < Members.Length; i++)
                {
                    tempmember[i] = Members[i];
                }
                Members = new Member[k - 1];
                for (int i = 0; i < k - 1; i++)
                {
                    if (!EndL.Contains(i))
                    {
                        // here we create the coordinates of eache parallelogram for the members
                        int Dx, Dy, dx, dy;
                        Dx = (int)(10 * ((pt[i + 1].X - pt[i].X) / length(pt[i], pt[i + 1])));
                        Dy = (int)(10 * ((pt[i].Y - pt[i + 1].Y) / length(pt[i], pt[i + 1])));
                        dy = (int)(7 * ((pt[i + 1].X - pt[i].X) / length(pt[i], pt[i + 1])));
                        dx = (int)(7 * ((pt[i].Y - pt[i + 1].Y) / length(pt[i], pt[i + 1])));
                        Point[] ptreg = { new Point((pt[i].X+Dx-dx ),(pt[i].Y-Dy-dy)),new Point((pt[i].X+Dx+dx ),(pt[i].Y-Dy+dy))  
                                  ,new Point((pt[i+1].X-Dx+dx ),(pt[i+1].Y+Dy+dy)),new Point((pt[i+1].X-Dx-dx ),(pt[i+1].Y+Dy-dy))};
                        gp.AddPolygon(ptreg);
                        Mregion[i] = new Region(gp);
                        Members[i] = new Member(r.ToString(), scale*Math.Round(length(pt[i], pt[i+1]), 3), math.angle(pt[i], pt[i+1]),Constants[0], Constants[1], Constants[2],0, 0, pt[i], pt[i+1]);
                        r++;
                        gp = new GraphicsPath();
                    }
                }
                for (int i = 0; i < tempmember.Length; i++)
                {
                    if (!EndL.Contains(i + 1))
                        Members[i] = tempmember[i];// used to create new member when ever a regiion is created
                }
            }
                   
        }
        private void mebrerBlue(Point focus)
        {
            // this method is used to meke a member blue when ever clicked or the mouse aproaches its region
            for (int i = 0; i < k - 1; i++)
            {
                if (!EndL.Contains(i))
                {
                    try
                    {
                        if (Mregion[i].IsVisible(focus))
                        {
                            member = true;// make the member  selected
                            t = i;
                            break;
                        }
                        if (!(Mregion[t].IsVisible(focus)) & (member))
                        {
                            member = false;// make the member not selected
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        private void PointRed(Point p)
        {
            foreach (Rectangle m in Prect)
            {
                if (m.Contains(p))
                {
                    tempr = m;// temper stands for rectangles where the mouse is in
                    contain = true;// the point contain the mouse will be true
                    break;// breaks if only one rectangle is found
                }
            }
            // this if is used to make the points to be unselected if the mouse moves away
            if (!tempr.Contains(p))
            {
                contain = false;// if the point is out of the rectangle make it is containded to false
            }
        }
        private void EXregion()
        {
            // this method is used to exclude the region of one member from the other 
            for (int i = 0; i < k - 1; i++)
            {
                if (!EndL.Contains(i))
                {
                    for (int j = i + 1; j < k - 1; j++)
                    {
                        if (!EndL.Contains(j))
                            Mregion[i].Exclude(Mregion[j]);
                    }
                }
            }
        }
        private bool CheckContinuity()
        {
      // This method is used to check the continuity criteria for the stracture
            bool Continious = true;
            if (EndL.Contains(k - 1))
            {
                EndL.Remove(k - 1);
                endline = false;
            }               
            for (int i = 0; i < EndL.Count; i++)
            {
                bool IsContinious = false;
                int[] Indexes;
                int Bound = 0;
                try
                {
                    Bound = Convert.ToInt16(EndL[i + 1]) + 1;
                }
                catch (Exception)
                {
                    Bound = k;
                }
                Indexes =PointAllIndexes(Convert.ToInt16(EndL[i]) + 1, Bound - 1);
                for (int j = Convert.ToInt16(EndL[i]) + 1; j < Bound; j++)
                {                    
                    if (Indexes[j - Convert.ToInt16(EndL[i]) - 1] <= Convert.ToInt16(EndL[i]))
                    {
                        IsContinious = true;
                        break;
                    }
                }
                if (!IsContinious)
                {
                    MessageBox.Show("Your stracture fails to satisfay stactural continuity criteria.Pleas check and finalyse you drawing. ", "Discontinuity Failer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Continious = false;
                    break;
                }
            }
            return Continious;
         }
        private int[] PointIndexes(Point p)
        {
            ArrayList Indexes = new ArrayList();
            int[] indxs;
            for (int i = 0; i < k; i++)
            {
                if (pt[i] == p)
                {
                    Indexes.Add(i);
                }
            }
            indxs = (int[])Indexes.ToArray(Indexes[0].GetType());
            return indxs;
        }
        private int[] PointAllIndexes(int idx1,int idx2)
        {
            ArrayList Indexes = new ArrayList();
            int[] indxs;
            for (int j = 0; j < k; j++)
            {
                for (int i = 0; i <= idx2 - idx1; i++)
                {
                    if (pt[j] == pt[idx1 + i])
                    {
                        Indexes.Add(j);
                        break;
                    }
                }
            }
            try
            {
                indxs = (int[])Indexes.ToArray(Indexes[0].GetType());
            }
            catch (Exception)
            {
                indxs = new int[0];
            }
            return indxs;
        }
        private void btnInput_Click(object sender, EventArgs e)
        {
            Graphics g = drawpanel.CreateGraphics();
            PaintEventArgs d = new PaintEventArgs(g, drawpanel.ClientRectangle);
            if (gbxPoint.Enabled)
            {
                bool notstring = true;
                char[] sep = { ',',' '};
                string[] part = new string [2];
                //here if the elemet modified is the last coordinat no need to change other points           
                try
                {
                    part = txtPoint.Text.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    float chec = float.Parse(part[0]) + float.Parse(part[1]);
                }
                catch (Exception)
                {
                    MessageBox.Show("Pleas input numerical values for the coordinate separated by comma","Invalid Input",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    notstring = false;
                }
                // this loop will move all  the joints to the required place
                Point[] newPoint = pt; // this copy will prevent the aray not to be passed by value
                for (int i = 0; (notstring) & (i < k); i++)
                {
                    if (newPoint[i] == newPoint[s])
                    {
                        int Xvalue, Yvalue;
                        Xvalue = (int)((1 / scale )* (float.Parse(part[0])+Reff_Point.X));
                        Yvalue = (int)((1 / scale)* (Reff_Point.Y- float.Parse(part[1])));
                        points[i] = new Point(Xvalue,Yvalue);
                        Joints[IndexOf(pt[s])].Coordinate  = new PointF(float.Parse(part[0]),float.Parse(part[1]));
                        Joints[IndexOf(pt[s])].CleintCoordinate = new Point(Xvalue,Yvalue);
                        AdjestMembers(pt[s], new Point(Xvalue, Yvalue));
                        pt = (Point[])points.ToArray(points[0].GetType());
                    }
                }
                for (int i = 0; i < Members.Length; i++)
                {
                    if (!EndL.Contains(i))
                        Members[i].Length = scale * length(Members[i].NECDNT, Members[i].FECDNT);
                }
                rectCreator();// used to creat a new rectagle when ever you modify a point     
                RegCreator(); //creates a region for each members modified member   
                EXregion();//excludes the region of one member from others for the modified memebers 
                drawpanel.Invalidate();
                drawpanel_Paint_1(sender, d);
            }
            if (gbxMember.Enabled)
            {
                if (txtMarea.Text != "" & txtMME.Text != "" & txtMMI.Text != "" & txtMname.Text != "" & txtMarea.Text != "")
                {                     
                    if (CheckForTheSameName(txtMname.Text))
                    {
                        for (int i = 0; i < LD.ld._LoadGraph.Count / 7; i++)
                        {
                            if (LD.ld._LoadGraph.IndexOf(Members[mt].Name) >= 0)
                                LD.ld._LoadGraph[LD.ld._LoadGraph.IndexOf(Members[mt].Name)] = txtMname.Text;
                        }
                        Members[mt] = new Member(txtMname.Text, double.Parse(txtMlength.Text), float.Parse(txtAngleFromHorz.Text), double.Parse(txtMarea.Text), double.Parse(txtMMI.Text), double.Parse(txtMME.Text), Members[mt].NFEM, Members[mt].FFEM,pt[mt],pt[mt+1]);
                        if (k > 1)
                            AdjMember = M.AdjestMembers(Members);
                    }
                    drawpanel.Invalidate();
                }
                else
                {
                    MessageBox.Show("Pleas fill all the fields first!", "All Fields Required", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
        }
        private bool CheckForTheSameName(string str)
        {
            bool check = true;
            for (int i = 0; i < Members.Length; i++)
            {     
                if(!EndL.Contains(i))
                if ((str!=Members[mt].Name)&&(Members[i].Name == str))
                {
                    check = false;
                    MessageBox.Show("Two members canot have the same name! Pleas change the name.", "The Same Member Exist", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtMname.Text = Members[mt].Name;
                    break;
                }
            }
            return check;
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            txtCoordinate.Visible = false;
            if (DrawMember.Selected & (k > 1))
            {
                if (!EndL.Contains (k-1)&(!EndL.Contains(k-2)))
                {
                    EndL.Add(k - 1);// stores the point from where drawing is not posible
                    endline = true;// make the moving line to stop untile the next click
                    btnInsert.FlatStyle = FlatStyle.Flat;// changes the insert button to black boreded
                }
                else if (EndL.Contains(k - 1) & (!EndL.Contains(k - 2)))
                {
                    EndL.RemoveAt(EndL.Count - 1);
                    endline = false;// if inserte button is touched twice one after the other we allow the moving line
                    btnAddRefferece.FlatStyle = FlatStyle.Popup;
                }
                else
                    endline = false;// if inserte button is touched twice one after the other we allow the moving line
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            TmrMove.Start();
            DrawGrid();
            this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            this.Location = new Point(0, 0);  
        }
        private void DrawMember_Click(object sender, EventArgs e)
        {
            if (DrawMember.Selected)
            {
                drawpanel.Invalidate();
                txtCoordinate.Visible = false;
            }
            if (endline)
            {
                if(EndL.Count!=0)
                EndL.RemoveAt(EndL.Count - 1);
               endline = false;
            }
            if (!DrawMember.Selected)
            {
                DrawMember.Selected = true;
                drawpanel.Cursor = Cursors.Cross;
                drawpanel.Invalidate();
                DataTimer.Start();
            }
            else
            {
                DrawMember.Selected = false;
                drawpanel.Cursor = Cursors.Hand;
            }
            mselect = false;
            member = false;
            pselect = false;
            TmrMove.Start();
        }
        private void drawpanel_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            ArrowDraw();
            SolidBrush brush = new SolidBrush(Color.White);
            Pen blue = new Pen(Color.Blue, 2);
            if (k > 0)
            {
                white.LineJoin = LineJoin.Round;
                white.StartCap = LineCap.SquareAnchor;
                white.EndCap = LineCap.SquareAnchor;
                DrawMembers(e); 
                if (pselect)
                {
                    g.FillEllipse(new SolidBrush(Color.Red), Prect[_t]);
                }
                if (contain)
                {
                    g.FillEllipse(new SolidBrush(Color.Red), tempr);
                }
                else
                {
                    g.FillEllipse(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray), tempr);
                    tempr = new Rectangle(0, 0, 0, 0);// makes the temporarly seleted rectagel to zero dimension
                }
                if (member & k > 1)
                {
                    g.DrawLine(blue, pt[t], pt[t + 1]);
                }
                if (mselect & k > 1)
                {
                    g.DrawLine(blue, pt[mt], pt[mt + 1]);
                }       
                SD.DrawSupport(e);
                LD.DrawLoad(e);
                if (chbShowLable.Checked)
                    LG.DrawLables(e);
            }
        }
        private void drawpanel_MouseDown_1(object sender, MouseEventArgs e)
        {  
            panel1.Visible = false;
            LD.ld = LD;
            Graphics g = drawpanel.CreateGraphics();
            PaintEventArgs d = new PaintEventArgs(g, drawpanel.ClientRectangle);// declears a paint event            
            bool Point_Added = false; // no ensert is trigered at initial                                                                           
            bool nexinsrt = endline;
            int ex, ey;
            ex = e.X;
            ey = e.Y;
            Grid_Point(ref ex, ref ey);
            x = ex;
            y = ey;
            if(k>1)g.FillEllipse(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray), Prect[_t]);
            if (DrawMember.Selected&(e.Button == MouseButtons.Left)) // goes only if drawing is on
            {
                btnInsert.FlatStyle = FlatStyle.Popup;
                if (IsMbrCld(ex, ey))
                {
                    MessageBox.Show("Canot insert a member once drawn.Pleas try to input your drawing memberwisly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (IsPointClkd(ex, ey))
                {
                    if (pt[FIndx(ex, ey)] == pt[k - 1])// 
                    {
                        if (nexinsrt) // if insert was imideately touched before this point is clickd
                            EndL.RemoveAt(EndL.Count - 1);// remove the last element of the End Line array list
                        else
                            Message(3); // Displays a message if point is clicked in its own region                       
                    }
                    else if (IsMbrRptd(pt[FIndx(ex, ey)]))
                        Message(2);// dispalays a message if point is clicked where the member is repeated
                    else
                    {
                        AddPoint(ex, ey);// addes new point if it is out of the region of any member 
                        Point_Added = true;
                    }
                }
                else
                {
                    AddPoint(ex, ey);// addes new point if it is out of the region of any member 
                    Point_Added = true;
                }
                if (Point_Added)
                {
                    rectCreator();// used to creat a new rectagle when ever you click a point     
                    RegCreator(); //creates a region for each members    
                    EXregion();//excludes the region of one member from others 
                    Refferece_Request(new Point(ex, ey)); 
                    AdjestJointAndMembers();
                }
                endline = false;
            }
            if (!DrawMember.Selected & (k > 1))
            {
                CMemberClicked(ex, ey); // selects the clicked member (see the clicked member)                                               
                if ((!Mregion[mt].IsVisible(new Point(ex, ey))) & (mselect))
                {
                    // here we shoud have to set the member unselected if the point is clicked out of the region
                    gbxMember.Enabled = false;// disable the member group box
                    mselect = false;
                    txtMname.Text =txtMlength.Text = txtMarea.Text = txtMMI.Text = txtMME.Text = txtAngleFromHorz.Text = "";
                }
                CPointClicked(ex, ey);// selects the clikced point
                if (!(Prect[_t].Contains(ex, ey)))
                {
                    // here we make the point selected unselected if the other place is clicked
                    gbxPoint.Enabled = false;
                    txtJointName.Text = "";
                    txtPoint.Text = "";
                    txtSuportType.Text = "";
                    txtSSMIndexs.Text = "";
                    _t = 0;
                    pselect = false;
                }
            }
            drawpanel_Paint_1(sender, d);
            DataTimer.Start();
        }
        private int  MemberIndexForCoordinate(int ex,int ey)
        {
            int index = -1;
            for (int i = 0; i < Members.Length; i++)
            {
                if (!EndL.Contains(i))
                {
                    if (Members[i].FECDNT == new Point(ex, ey))
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }
        private void AdjestMembers(Point PrevPoint, Point NewPoint)
        {
            for (int i = 0; i < Members.Length; i++)
            {
                if (!EndL.Contains(i))
                {
                    if (Members[i].NECDNT == PrevPoint)
                        Members[i].NECDNT = NewPoint;
                    if (Members[i].FECDNT == PrevPoint)
                        Members[i].FECDNT = NewPoint;
                }
            }
            drawpanel.Invalidate();
        }
        private void drawpanel_MouseLeave_1(object sender, EventArgs e)
        {
            // this method is used to cancel the moving line when the pointer leaves the drawing region
            Graphics g = drawpanel.CreateGraphics();
            PaintEventArgs d = new PaintEventArgs(g, drawpanel.ClientRectangle);
            if ((k > 0)&DrawMember.Selected)
            {
                if (equalpoint_x|equalpoint_y )
                    drawpanel.Invalidate();
                g.DrawLine(erase, pt[k - 1], new Point(Xvalue, Yvalue));
                g.DrawLine(new Pen(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray), 2.0f), pt[k - 1], new Point(Xvalue, Yvalue));
                txtCoordinate.Visible = false;
            }  
        }
        private void DrawMembers(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // this loop is used to draw all the lines when ever the mouse is moving
            for (int i = 0; i < k - 1; i++)
            {
                if (!EndL.Contains(i))
                {
                    if ((member & (k > 1) & i == t) | (mselect & k > 1 & i == mt))
                        continue;
                    g.DrawLine(white, pt[i], pt[i + 1]);
                }
            }
        }
        private void drawpanel_MouseMove(object sender, MouseEventArgs e)
        {  
            if(!chbStopDrawing.Checked)
            TmrMove.Start();
            Graphics g = drawpanel.CreateGraphics();
            int ex, ey;
            ex =x = e.X;
            ey =y =  e.Y;   
            Grid_Point(ref ex, ref ey);
            txtRefCoord.Text =  ((scale * ex) - Reff_Point.X).ToString() + "," + (Reff_Point.Y - (scale * ey)).ToString() ;
            if (DrawMember.Selected & (k > 0))
            {
                MakeJointLine(ex, ey);
                gbxPoint.Enabled = false;// if the user is drawing disable the group boxs              
                gbxMember.Enabled = false;// while drawing no imput is allowed
                //her we creat a moving line in the pannel 
                if (!endline)
                {
                    txtCoordinate.Visible = true;
                    txtCoordinate.Text = "From NE:" + "(" + (scale * (ex - pt[k - 1].X)).ToString() + "," + (scale * (pt[k - 1].Y - ey)).ToString() + ")";
                    txtCoordinate.Location = new Point(ex + 20, ey - 20);
                    g.DrawLine(new Pen(Color.Black), pt[k - 1].X, pt[k - 1].Y, Xvalue, Yvalue);
                    g.DrawLine(new Pen(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray)), pt[k - 1].X, pt[k - 1].Y, Xvalue, Yvalue);
                    Xvalue = ex;
                    Yvalue = ey; 
                    
                    g.DrawLine(new Pen(Color.White), pt[k - 1].X, pt[k - 1].Y, ex, ey);               
                }
            }                
    
        }
        private void MakeJointLine(int ex, int ey)
        {
            Graphics g = drawpanel.CreateGraphics();
            Pen pn = new Pen(Color.Yellow);
            pn.DashStyle = DashStyle.Dash;
            for (int j = 0; (j < k); j++)
            {
                if ((pt[j].Y == ey))
                {
                    if (ey == EqualPoint.Y - 8)
                        g.DrawLine(new Pen(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray)), 0, EqualPoint.Y, drawpanel.Width, EqualPoint.Y);
                    else if (ey == EqualPoint.Y + 8)
                        g.DrawLine(new Pen(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray)), 0, EqualPoint.Y, drawpanel.Width, EqualPoint.Y);
                    g.DrawLine(pn, 0, pt[j].Y, drawpanel.Width, pt[j].Y);
                    EqualPoint.Y = pt[j].Y;
                    equalpoint_x = true;
                    break;
                }
                if (equalpoint_x & (ey != EqualPoint.Y))
                {
                    g.DrawLine(new Pen(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray)), 0, EqualPoint.Y, drawpanel.Width, EqualPoint.Y);
                    equalpoint_x = false;
                }
            }
            for (int j = 0; j < k; j++)
            {
                if ((pt[j].X == ex))
                {
                    if (ex == EqualPoint.X - 8)
                        g.DrawLine(new Pen(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray)), 0, EqualPoint.X, drawpanel.Width, EqualPoint.X);
                    else if (ey == EqualPoint.Y + 8)
                        g.DrawLine(new Pen(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray)), 0, EqualPoint.X, drawpanel.Width, EqualPoint.X);
                    g.DrawLine(pn, pt[j].X, 0, pt[j].X, drawpanel.Height);
                    EqualPoint.X = pt[j].X;
                    equalpoint_y = true;
                    break;
                }
                else if (equalpoint_y & (ex != EqualPoint.X))
                {
                    g.DrawLine(new Pen(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray)), EqualPoint.X, 0, EqualPoint.X, drawpanel.Height);
                    equalpoint_y = false;
                }
            }
          
          
        }
        private void Refferece_Request(Point p)
        {
            if (!Add_Refferece&k==1)
            {
                string str = " Do you want to make this point as you refferece point?\n" +
                    "If you click 'No' the program meke the Left top corner of this drawing pannel as a refference. \n" +
                    "Else If you want to change the refferce later you can click the 'Add Reffece' Button. ";
                if (MessageBox.Show(str, "Refference Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Reff_Point.X =scale*p.X; 
                    Reff_Point.Y =scale*p.Y;                   
                }
                Add_Refferece = true;
            }
        }
        private void drawpanel_Load(object sender, EventArgs e)
        {
            txtCoordinate.Visible = false;
            panel1.Visible = false;
            SD.sd = new SupportDialog();
        }
        private void btnAddRefferece_Click(object sender, EventArgs e)
        {
            try
            {
                if (pselect)
                {
                    Reff_Point.X = scale * pt[s].X;
                    Reff_Point.Y = scale * pt[s].Y;
                    MessageBox.Show(("Point (" + Reff_Point.X.ToString() + "," + (-Reff_Point.Y).ToString() + ") is made your refference point with respect to top left corner (Absolute Refference)"), "Refference Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPoint.Text = "0,0";
                    for (int i = 0; i < Joints.Length; i++)
                    {
                        Joints[i].Coordinate = Global(Joints[i].CleintCoordinate.X, Joints[i].CleintCoordinate.Y);
                    }
                }
                else
                {
                    MessageBox.Show("The point selected as a refference should be a defined point in the stractural drawing", "Point Is Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (NullReferenceException)
            {
              // do nothing but stop program interaption
            }
        }
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            // here when ever the clean button is clicked it will meke all varabls to default 
            if (MessageBox.Show(" Are you sure you want to clear all the drawing ", "Confirm Clearing", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                drawpanel.Invalidate();
                points = new ArrayList();
                pt = new Point[0];
                Mregion = new Region[0];
                Prect = new Rectangle[0];
                k = t = _t = mt = s = 0;
                EndL = new ArrayList();
                txtMlength.Text = txtPoint.Text = "";
                mselect = false;
                Add_Refferece = false;
                pselect = false;
                member = false;
                Members = new Member[0];
                Reff_Point = new Point();
                SD = new SupportDialog();
                SD.sd = SD;
                LG = new LableGraphics() ;
                Joints = new Joint[1];
                LD = new LoadDialog();
                AdjMember = new Member[1];
                jointload = new ArrayList();
                dgvSSMatrix.Columns.Clear();
                dgvJoint.Rows.Clear();
                dgvMember.Rows.Clear();
            }
        }
        private void txtMarea_KeyPress(object sender, KeyPressEventArgs e)
        {
            LoadDialog lD = new LoadDialog();
            e.Handled = lD.checkIfONum(e, txtMarea.Text);
        }
        private void txtMMI_KeyPress(object sender, KeyPressEventArgs e)
        {
            LoadDialog lD = new LoadDialog();
            e.Handled = lD.checkIfONum(e, txtMMI.Text);
        }
        private void txtMME_KeyPress(object sender, KeyPressEventArgs e)
        {
            LoadDialog lD = new LoadDialog();
            e.Handled = lD.checkIfONum(e, txtMME.Text);
        }
        private PointF Global(int ex, int ey)
        {
            return new PointF((ex * scale - Reff_Point.X), (Reff_Point.Y - ey * scale));
        }
        private void drawpanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                panel1.Visible = true;
                panel1.Location = new Point(e.X, e.Y);
                for (int i = 0; i < k; i++)
                {
                    if (Prect[i].Contains(e.X, e.Y))
                    {
                        SD.sd = SD;// re assing the support dialog object each time it is modified
                        SD.sd.pt = pt;// we add the point array to the support dialog class for checking
                        break;
                    }
                }
            }
        }
        private void AdjestJointAndMembers()
        {
            if (k > 0)
            {
                // here we form the joints 
                Joint J = new Joint();
                if (k == 1)
                {
                    Joints = new Joint[1];
                    Joints[0] = new Joint(J.CleintToRefference(pt[k - 1], Reff_Point), Joints.Length.ToString(), pt[k - 1], "None", 0.00, 0.00, 0.00, 0.00, 0.00, 0.00);               
                }
                if (LG.RemoveRepeatedPoints(pt).Length > Joints.Length)
                {                   
                    Joint[] tempjoint = Joints;
                    Joints = new Joint[LG.RemoveRepeatedPoints(pt).Length];
                    for (int i = 0; i < tempjoint.Length; i++)
                    {
                        Joints[i] = tempjoint[i];
                    }
                    Joints[LG.RemoveRepeatedPoints(pt).Length - 1] = new Joint(J.CleintToRefference(pt[k - 1], Reff_Point), Joints.Length.ToString(), pt[k - 1], "None", 0.00, 0.00, 0.00, 0.00, 0.00, 0.00);
                }
                    if (k > 1)
                    AdjMember = M.AdjestMembers(Members);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            if (pselect)
            {
                SD.RefferencePoint = Reff_Point;
                SD.Jnt = Joints[IndexOf(pt[s])];
                SD.Joints = Joints;
                SD.stracture = SelectedStr;
                SupportGraphis = SD._SupGraph;
                SD.ShowDialog();                 
                drawpanel.Invalidate();
            }
        }
        private void btnAddThisReff_Click(object sender, EventArgs e)
        {
            btnAddRefferece_Click(sender, e);
            panel1.Visible = false;
            drawpanel.Invalidate();
        }
        private void btnRemovSupport_Click(object sender, EventArgs e)
        {
           panel1.Visible = false;
           try
           {
               if (SD.sd._SupGraph.Contains(pt[s]))
               {
                   int indx = SD.sd._SupGraph.IndexOf(pt[s]);
                   Joints[IndexOf(pt[s])].SupportType = "None";
                   SD.sd._SupGraph.RemoveRange(indx - 1, 3);
                   SD.sd = SD;                  
                   drawpanel.Invalidate();
               }
           }
           catch (Exception)
           {
              //Do nothing but stop interaption
           }
        }
        private int IndexOf(Point p)
        {
            int indxe = -1;
            for (int i = 0; i < Joints.Length; i++)
            {
                if (Joints[i].CleintCoordinate == p)
                {
                    indxe = i;
                    break;
                }
            }
            return indxe;
        }
        private bool CheckFixedTruss()
        { 
            bool check = false;
            if (SelectedStr == Stracture.Truss)
            {              
                for (int i = 0; i < Joints.Length; i++)
                {
                    try
                    {
                        if (Joints[i].SupportType == "Fixed")
                        {
                            check = true;
                            MessageBox.Show(" There should be no fixed support in truss stractures.Pleas remove the fixed support and try again.", "Inconsistant Support Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        check = true;
                        break;
                    }
                 
                }
            }
            return check;
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            if (!DrawMember.Selected && pselect)
            {
                JointLoad JL = new JointLoad();
                JL.JointName = (IndexOf(pt[s]) + 1).ToString();
                JL.Joints = Joints;
                JL.jointload = jointload;
                JL.stracture = SelectedStr;
                LD.ld.Joints = Joints;
                LD.ld.JointLoad = jointload;
                JL.ShowDialog();                  
                drawpanel.Invalidate();
            }
            if (!DrawMember.Selected && mselect && SelectedStr != Stracture.Truss)
            {
                try
                {
                    panel1.Visible = false;
                    LD.ld = LD;
                    LD.ld.mbr = Members;
                    LD.ld.Joints = Joints;
                    LD.ld.pt = pt;
                    LD.ld.Endl = EndL;
                    LD.ld.SelectedMember = Members[mt].Name;
                    LD.RefferencePoint = Reff_Point;
                    LS.LoadingInfo = LD.ld._LoadGraph;
                    LS.JointLoad = jointload;
                    LD.ShowDialog();
                    try
                    {
                        for (int i = 0; i < Joints.Length; i++)
                        {
                            Joints[i].LoadX = 0;
                            Joints[i].LoadY = 0;
                        }
                        for (int i = 0; i < jointload.Count; i += 4)
                        {
                            Joints[Convert.ToInt16(jointload[i])].LoadX += Math.Cos(math.DR(Convert.ToDouble(jointload[i + 3]))) * Convert.ToDouble(jointload[i + 2]);
                            Joints[Convert.ToInt16(jointload[i])].LoadY += Math.Sin(math.DR(Convert.ToDouble(jointload[i + 3]))) * Convert.ToDouble(jointload[i + 2]);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    drawpanel.Invalidate();
                }
                catch (Exception)
                {
                    MessageBox.Show("No stractural drawing is found.Pleas first input the drawing!", "Drawing Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            if (CheckContinuity())
            {
                for (int i = 0; i < AdjMember.Length; i++)
                {
                    AdjMember[i].NFEM = 0;
                    AdjMember[i].FFEM = 0;
                }
                for (int i = 0; i < AdjMember.Length; i++)
                {
                    for (int j = 0; j < LD.ld._LoadGraph.Count; j += 7)
                    {
                        if (LD.ld._LoadGraph[j].ToString() == AdjMember[i].Name)
                        {
                            AdjMember[i].NFEM += M.NearEndMoment(Convert.ToDouble(LD.ld._LoadGraph[j + 2]), Convert.ToDouble(LD.ld._LoadGraph[j + 3]), Convert.ToDouble(LD.ld._LoadGraph[j + 4]), Convert.ToDouble(LD.Length(AdjMember[i].Name)), Convert.ToDouble(LD.ld._LoadGraph[j + 5]), (string)LD.ld._LoadGraph[j + 1], Convert.ToBoolean(LD.ld._LoadGraph[j + 6]));
                            AdjMember[i].FFEM += M.FarEndMoment(Convert.ToDouble(LD.ld._LoadGraph[j + 2]), Convert.ToDouble(LD.ld._LoadGraph[j + 3]), Convert.ToDouble(LD.ld._LoadGraph[j + 4]), Convert.ToDouble(LD.Length(AdjMember[i].Name)), Convert.ToDouble(LD.ld._LoadGraph[j + 5]), (string)LD.ld._LoadGraph[j + 1], Convert.ToBoolean(LD.ld._LoadGraph[j + 6]));
                        }
                    }
                }
            }
        }
        private void drawpanel_KeyDown(object sender, KeyEventArgs e)
        {

            if (DrawMember.Selected&drawpanel.ClientRectangle.Contains(MousePosition.X,MousePosition.Y))
            {
                Graphics g = drawpanel.CreateGraphics();
                if (e.KeyValue == Keys.Escape.GetHashCode())
                {
                    PaintEventArgs p = new PaintEventArgs(g, drawpanel.ClientRectangle);
                    btnInsert_Click(sender, e);
                    drawpanel.Invalidate();
                    drawpanel_Paint_1(sender, p);
                }
            }
            if (e.KeyValue == Keys.X.GetHashCode())
                DrawMember_Click(sender, new EventArgs());
        }
        private void checkContinuityToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if(CheckContinuity())
               MessageBox.Show("Your stracture is continious! ", "Discontinuity Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void TmrMove_Tick(object sender, EventArgs e)
        {
            if (insert)
            {
                drawpanel.Invalidate();
                insert = false;
            }
            Graphics g = drawpanel.CreateGraphics();
            PaintEventArgs d = new PaintEventArgs(g, drawpanel.ClientRectangle);
            if (k > 1) mebrerBlue(new Point(x,y));//used to make a member blue when a mouse aproaches the reagion
            if (k > 0) PointRed(new Point(x,y));// used to make the points red when ever the mouse aproaches
            if (LS.SaveChanges)
            {
                LD.ld._LoadGraph = LS.LoadingInfo;
                LD.ld.JointLoad = LS.JointLoad;
                jointload = LS.JointLoad;
                drawpanel.Invalidate();
                LS.SaveChanges = false;
            }
            if (!panel1.Visible)
                panel1.Location = new Point(x, y);
            drawpanel_Paint_1(sender, d);// calls the paint event to repaint the panell when ever the mouse moves to avoid over lap 
            TmrMove.Stop();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TmrMove.Stop();
            DataTimer.Stop();
            if (MessageBox.Show("Do you want to save this Project?", "Save File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                saveAsToolStripMenuItem_Click(sender, e);
        }
        private void drawpanel_MouseEnter(object sender, EventArgs e)
        {
            drawpanel.Focus();
        }
        private void drawpanel_MouseHover(object sender, EventArgs e)
        {
            TmrMove.Stop();
        }
        private void DataTimer_Tick(object sender, EventArgs e)
        {
            if (AdjMember[AdjMember.Length-1] != null)
            {
                for (int i = 0; (i < AdjMember.Length) & (k > 1); i++)
                {
                    AdjMember[i].NFEM = 0;
                    AdjMember[i].FFEM = 0;
                    AdjMember[i].NForce = 0;
                    AdjMember[i].FForce = 0;
                }
                for (int i = 0; i < Joints.Length; i++)
                {
                    Joints[i].LoadX = Joints[i].LoadY = Joints[i].LoadZ = 0;
                    Joints[i].DispX = Joints[i].DispY = Joints[i].DispZ = 0;
                    if (i < Displacements.GetLength(0))
                    {
                        Joints[i].DispX = Displacements[i, 0];
                        Joints[i].DispY = Displacements[i, 1];
                        Joints[i].DispZ = Displacements[i, 2];
                    }
                }
                for (int i = 0; i < jointload.Count; i += 4)
                {
                    if (jointload[i].ToString() == "Force")
                    {
                        Joints[int.Parse(jointload[i + 1].ToString()) - 1].LoadX -= double.Parse(jointload[i + 2].ToString()) * Math.Sin(math.DR(double.Parse(jointload[i + 3].ToString())));
                        Joints[int.Parse(jointload[i + 1].ToString()) - 1].LoadY -= double.Parse(jointload[i + 2].ToString()) * Math.Cos(math.DR(double.Parse(jointload[i + 3].ToString())));
                    }
                    else
                    {
                        Joints[int.Parse(jointload[i + 1].ToString()) - 1].LoadZ += double.Parse(jointload[i + 2].ToString());
                    }
                }
                for (int i = 0; i < AdjMember.Length & (k > 1); i++)
                {
                    for (int j = 0; j < LD.ld._LoadGraph.Count; j += 7)
                    {
                        if (LD.ld._LoadGraph[j].ToString() == AdjMember[i].Name)
                        {
                            double NEM = M.NearEndMoment(Convert.ToDouble(LD.ld._LoadGraph[j + 2]), Convert.ToDouble(LD.ld._LoadGraph[j + 3]), Convert.ToDouble(LD.ld._LoadGraph[j + 4]), Convert.ToDouble(LD.Length(AdjMember[i].Name)), Convert.ToDouble(LD.ld._LoadGraph[j + 5]), (string)LD.ld._LoadGraph[j + 1], Convert.ToBoolean(LD.ld._LoadGraph[j + 6]));
                            double FEM = M.FarEndMoment(Convert.ToDouble(LD.ld._LoadGraph[j + 2]), Convert.ToDouble(LD.ld._LoadGraph[j + 3]), Convert.ToDouble(LD.ld._LoadGraph[j + 4]), Convert.ToDouble(LD.Length(AdjMember[i].Name)), Convert.ToDouble(LD.ld._LoadGraph[j + 5]), (string)LD.ld._LoadGraph[j + 1], Convert.ToBoolean(LD.ld._LoadGraph[j + 6]));
                            double JL1 = M.MemberEndForce((string)LD.ld._LoadGraph[j + 1], Convert.ToDouble(LD.ld._LoadGraph[j + 2]), Convert.ToDouble(LD.ld._LoadGraph[j + 3]), Convert.ToDouble(LD.ld._LoadGraph[j + 4]), Convert.ToDouble(LD.Length(AdjMember[i].Name)), NEM, FEM, Convert.ToDouble(LD.ld._LoadGraph[j + 5]), Convert.ToBoolean(LD.ld._LoadGraph[j + 6]))[0];
                            double JL2 = M.MemberEndForce((string)LD.ld._LoadGraph[j + 1], Convert.ToDouble(LD.ld._LoadGraph[j + 2]), Convert.ToDouble(LD.ld._LoadGraph[j + 3]), Convert.ToDouble(LD.ld._LoadGraph[j + 4]), Convert.ToDouble(LD.Length(AdjMember[i].Name)), NEM, FEM, Convert.ToDouble(LD.ld._LoadGraph[j + 5]), Convert.ToBoolean(LD.ld._LoadGraph[j + 6]))[1];
                            AdjMember[i].NFEM += NEM;
                            AdjMember[i].FFEM += FEM;
                            AdjMember[i].NForce += JL1;
                            AdjMember[i].FForce += JL2;
                            Joints[M.NearEndJointIndex(Joints, AdjMember[i])].LoadX += JL1 * Math.Sin(math.DR(AdjMember[i].Angle));
                            Joints[M.NearEndJointIndex(Joints, AdjMember[i])].LoadY -= JL1 * Math.Cos(math.DR(AdjMember[i].Angle));
                            Joints[M.FarEndJointIndex(Joints, AdjMember[i])].LoadX += JL2 * Math.Sin(math.DR(AdjMember[i].Angle));
                            Joints[M.FarEndJointIndex(Joints, AdjMember[i])].LoadY -= JL2 * Math.Cos(math.DR(AdjMember[i].Angle));
                            Joints[M.NearEndJointIndex(Joints, AdjMember[i])].LoadZ += NEM;
                            Joints[M.FarEndJointIndex(Joints, AdjMember[i])].LoadZ += FEM;
                        }
                    }
                }
                if (SelectedStr == Stracture.Truss)
                {
                    dgvSSMatrix.ColumnCount = dgvSSMatrix.RowCount = 2 * Joints.Length;
                    txtMMI.Enabled = false;
                    txtMarea.Enabled = true;
                }
                else if (SelectedStr == Stracture.Beam)
                {
                    dgvSSMatrix.ColumnCount = dgvSSMatrix.RowCount = 2 * Joints.Length;
                    txtMMI.Enabled = true;
                    txtMarea.Enabled = false;
                }
                else if (SelectedStr == Stracture.Frame)
                {
                    dgvSSMatrix.ColumnCount = dgvSSMatrix.RowCount = 3 * Joints.Length;
                    txtMMI.Enabled = true;
                    txtMarea.Enabled = true;
                }                
                Mmatrix = new mMatrixs[AdjMember.Length];
                for (int i = 0; i < Mmatrix.Length; i++)
                {
                    Mmatrix[i] = new mMatrixs(AdjMember[i], Joints,SelectedStr,SupportGraphis);                   
                }
                 SolutionMatrix = new sMatrix(Mmatrix, Joints,AdjMember,SelectedStr);
            }
            LG = new LableGraphics(Members, pt, EndL);
            M.ReffePoitn = Reff_Point;
            DataTimer.Stop();
        }
        private void tbDataGenerated_Enter(object sender, EventArgs e)
        {
            if (k > 0)
            {
                dgvJoint.RowCount = Joints.Length;
                dgvMember.RowCount = AdjMember.Length;
                for (int i = 0; i < Joints.Length; i++)
                {
                    int[] SSmatrix = new int[3];
                    for (int n = 0; n < 3; n++)
                    {
                        SSmatrix[n] = Joints[i].SSMatrixIndexs[n];
                        SSmatrix[n]++;
                    }
                    dgvJoint[0, i].Value = Joints[i].Name.ToString();
                    dgvJoint[1, i].Value = Joints[i].Coordinate.X.ToString() + " ̡ " + Joints[i].Coordinate.Y.ToString();
                    dgvJoint[2, i].Value = Joints[i].SupportType;
                    dgvJoint[3, i].Value = "(" + SSmatrix[0] + " ̡ " + SSmatrix[1] + " ̡ " + SSmatrix[2] + ")";
                    dgvJoint[4, i].Value = Math.Round(Joints[i].LoadX, 10);
                    dgvJoint[5, i].Value = Math.Round(Joints[i].LoadY, 10);
                    dgvJoint[6, i].Value = Math.Round(Joints[i].LoadZ, 10);
                    dgvJoint[7, i].Value = Math.Round(Joints[i].DispX, 10);
                    dgvJoint[8, i].Value = Math.Round(Joints[i].DispY, 10);
                    dgvJoint[9, i].Value = Math.Round(Joints[i].DispZ, 10);
                }
                for (int i = 0; i < AdjMember.Length&&k>1; i++)
                {
                    dgvMember[0, i].Value = AdjMember[i].Name;
                    dgvMember[1, i].Value = AdjMember[i].Length;
                    dgvMember[2, i].Value = AdjMember[i].Angle;
                    dgvMember[3, i].Value = AdjMember[i].Area;
                    dgvMember[4, i].Value = AdjMember[i].MI;
                    dgvMember[5, i].Value = AdjMember[i].ME;
                    dgvMember[6, i].Value = AdjMember[i].NFEM;
                    dgvMember[7, i].Value = AdjMember[i].FFEM;
                }
            }
        }
        private void tbSSMatrix_Enter_1(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvSSMatrix.ColumnCount; i++)
            {
                dgvSSMatrix.Columns[i].HeaderText = (i + 1).ToString();
                dgvSSMatrix.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                for (int j = 0; j < dgvSSMatrix.RowCount; j++)
                {
                    dgvSSMatrix[i, j].Value = Math.Round(SolutionMatrix.SSMatrix[j, i], 10);
                }
                SolutionMatrix.MakeNNForceAndDisp();
                dgvNNDisp.RowCount = dgbNNForce.RowCount = SolutionMatrix.SSMatrix.GetLength(0);
                if (i < SolutionMatrix.NNForce.Length)
                {  
                    dgbNNForce[0,i].Value = SolutionMatrix.NNForce[i, 0];
                    if (SolutionMatrix.UNDisp== null)
                        dgvNNDisp[0, i].Value = "D" + (i + 1).ToString();
                    else
                        dgvNNDisp[0, i].Value = SolutionMatrix.UNDisp[i, 0];
                } 
                else 
                {
                    if (SolutionMatrix.UNForce == null)
                        dgbNNForce[0, i].Value = "F" + (1 + i).ToString();
                    else
                        dgbNNForce[0, i].Value = SolutionMatrix.UNForce[ i - SolutionMatrix.NNForce.Length,0];
                }

                if (i < SolutionMatrix.NNDisp.Length)
                {                         
                    dgvNNDisp[0,SolutionMatrix.NNForce.Length + i].Value = SolutionMatrix.NNDisp[i,0]; 
                }
            }
        }
        private void showSolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckContinuity()&!CheckFixedTruss())
            {
              
                if (SD.sd._SupGraph.Count != 0)
                {
                    if (SelectedStr == Stracture.Beam)
                    {
                        if (M.CheckIfBeam(Joints))
                            ShowSolutionDialog();
                        else
                            MessageBox.Show("Such types of stractures canot be analysed as continious beam.", "Invalid Stracture Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (SelectedStr == Stracture.Frame && M.CheckIfBeam(Joints))
                    {
                        if (MessageBox.Show("Such type of stracture should be analysed as a continious beam for effeciency and accuracy. Do you want to continue? ", "Stractural Miss Match", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                         ShowSolutionDialog(); 
                    }
                    else ShowSolutionDialog();
                }
                else if (k > 1)
                    MessageBox.Show("No support is provided for this stracture.Plaes provided and try again", "Support Missing");                  
            }
        }
        private void ShowSolutionDialog()
        {
            SolutionMatrix.MakeSSMatrix();
            SolutionMatrix.MakeNNForceAndDisp();
            SolutionMatrix.FindUNDisp();
            SolutionMatrix.FindUNForces();
            SolutionMatrix.MakeMemberDisp();
            SolutionMatrix.MakeMemberForces();
            Solution sol = new Solution();
            sol.mMatrix = SolutionMatrix.MMatrix;
            sol.Members = AdjMember;
            sol.stracture = SelectedStr;
            sol.IsSI = IsSI;
            sol.ShowDialog();
        }
        private void addConstantForToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Members.Length > 0)
            {
                AddConstant AddConst = new AddConstant();
                AddConst.Members = Members;
                AddConst.EndL = EndL;
                AddConst.Const = Constants;
                AddConst.stracture = SelectedStr;
                AddConst.IsSI = IsSI;
                AddConst.ShowDialog();
                if (AdjMember[0] != null)
                    AdjMember = M.AdjestMembers(Members);
            }
        }
        private void btnShowSolution_Click(object sender, EventArgs e)
        {
           showSolutionToolStripMenuItem_Click(sender, e);
        }
        private void addDisplacemetnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (k > 0)
            {
                Displacements = new double[Joints.Length, 3];
                AddDisplacement AddDisp = new AddDisplacement();
                AddDisp.Joints = Joints;
                AddDisp.Displacement = Displacements;
                AddDisp.stracture = SelectedStr;
                AddDisp.IsSI = IsSI;
                AddDisp.ShowDialog();
                DataTimer.Start();
            }
        }
        private void showLoadSummaryToolStripMenuItem2_Click(object sender, EventArgs e)
        {
               
                LS.JointLoad = jointload;
                try
                {
                    LS.LoadInfo = LD.ld._LoadGraph;
                    LS.IsSI = IsSI;
                }
                catch (NullReferenceException)
                {
                    LS.LoadInfo = new ArrayList();
                }
                LS.ShowDialog();
           
        }
        private void tbStartPage_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (rbtnTruss.Checked)
            {
                SelectedStr = Stracture.Truss;
                lblStracturalCathagory.Text = "Plane Truss";
            }
            else if (rbtBeam.Checked)
            {
                SelectedStr = Stracture.Beam;
                lblStracturalCathagory.Text = "Continious Beam";
            }
            else if (rbtFrame.Checked)
            {
                SelectedStr = Stracture.Frame;
                lblStracturalCathagory.Text = "Plane Frame ";
            }
            DataTimer.Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            drawpanel.Invalidate();
            DataTimer.Start();
            panel1.Visible = false;
        }
        private void chbStopDrawing_CheckedChanged(object sender, EventArgs e)
        {
            if (chbStopDrawing.Checked)
            {
                TmrMove.Stop();
            }
            else DataTimer.Start();
        }
        private void chbShowLable_CheckedChanged(object sender, EventArgs e)
        {
            if (!chbShowLable.Checked)
                drawpanel.Invalidate();              
            TmrMove.Start();
        }
        private void showLoadSummaryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LS.IsJointLoadSummary = true;
            showLoadSummaryToolStripMenuItem2_Click(sender, e);
        }
        private void addLoadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                JointLoad JL = new JointLoad();
                JL.jointload = jointload;
                JL.Joints = Joints;
                LD.ld.JointLoad = jointload;
                LD.ld.Joints = Joints;
                if (k == 0)
                    MessageBox.Show(" No stractural drawing is found. Pleas Draw and try angain!", " No Drawing Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
               else JL.ShowDialog();
            }
            catch (Exception)
            {   
                if(k==0)
                MessageBox.Show(" No stractural drawing is found. Pleas Draw and try angain!", " No Drawing Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void addSupportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SD._SupGraph = SD.sd._SupGraph;
            SD.RefferencePoint = Reff_Point;
            SD.Joints = Joints;
            SD.ShowDialog();
            drawpanel.Invalidate();
        }
        private void tlsAddMemberLoad_Click(object sender, EventArgs e)
        {
            if (SelectedStr != Stracture.Truss)
            {
                LD.ld = LD;
                LD.ld.mbr = Members;
                LD.ld.Joints = Joints;
                LD.ld.pt = pt;
                LD.ld.Endl = EndL;
                LD.RefferencePoint = Reff_Point;
                LD.ShowDialog();
            }
        }
        private void GetRecordFileValues(RecordFile record)
        {
            tbStartPage.SelectedIndex = 1;
            pt = record.Pt;
            Reff_Point = record.Refference;
            points = record.Points;
            Joints = record.Joints;
            jointload = record.JointLoad;
            EndL = record.Endl;
            SD.stracture = record.Stracture;
            SD.sd = SD;
            SD._SupGraph = record.SupportGrapics;
            SD.Joints = record.Joints;
            SD.pt = record.Pt;
            SD.ReffPoin = record.Refference;
            SD.stracture = record.Stracture;
            LD.ld = LD;
            LD.ld._LoadGraph = record.LoadGraphics;
            LD.ld.JointLoad = record.JointLoad;
            LD.ld.Joints = record.Joints;
            LD.mbr = record.Members;
            LD.Endl = record.Endl;
            LD.pt = record.Pt;
            LG = record.LG;
            if (record.Stracture == Stracture.Truss)
                rbtnTruss.Checked = true;
            else if (record.Stracture == Stracture.Beam)
                rbtBeam.Checked = true;
            else if (record.Stracture == Stracture.Frame)
                rbtFrame.Checked = true;
            SelectedStr = record.Stracture;
            Constants = record.Constants;
            Displacements = record.Displacements;
            AdjMember = record.AdjMembers;
            Members = record.Members;
            SupportGraphis = record.SupportGrapics;
            k = record.Pt.Length;
            RegCreator();
            rectCreator();
            DataTimer.Start();
        }
        private RecordFile SetRecordFileValues()
        {
            RecordFile record = new RecordFile();
            record.Refference = Reff_Point;
            record.Pt = pt  ;
            record.Points = points ;
            record.Joints = Joints ;
            record.JointLoad = jointload;
            record.Endl = EndL;
            record.SupportGrapics =SD.sd._SupGraph;
            record.LoadGraphics = LD.ld._LoadGraph;
            record.LG = LG;
            record.Stracture = SelectedStr;
            record.Constants = Constants;
            record.Displacements = Displacements;
            record.AdjMembers = AdjMember;
            record.Members = Members;
            record.SupportGrapics = SupportGraphis;
            return record;
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {             
            string filename;
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.CheckFileExists = false;
            if (OpenFile.ShowDialog() == DialogResult.Cancel)
                return;
            filename = OpenFile.FileName;
            if (filename == "" || filename == null)
            {
                MessageBox.Show("Invalid File Name ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    RecordFile record = new RecordFile();
                    FileStream output = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    BinaryFormatter reader = new BinaryFormatter();
                    record = (RecordFile)reader.Deserialize(output);
                    GetRecordFileValues(record);
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show(" The File Does Not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (SerializationException)
                {
                    MessageBox.Show("Sory the STRAP V1 Could't open this file due to some limitations ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename;
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.CheckFileExists = false;
            if (SaveFile.ShowDialog() == DialogResult.Cancel)
                return;
            filename = SaveFile.FileName;
            if(!filename.Contains(".STRAP"))
            filename+=".STRAP";
            if (filename == "" || filename == null)
            {
                MessageBox.Show("Invalid File Name ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    RecordFile record = SetRecordFileValues();
                    FileStream input = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                    BinaryFormatter writer = new BinaryFormatter();
                    writer.Serialize(input,record);
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show(" The File Does Not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(sender, e);
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm MF = new MainForm();
            MF.Show();
        }
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem_Click(sender, e);
        }
    }
    [Serializable]
    public class Member
    {
        public PointF ReffePoitn;
        public string Name; // stands for the name of the member
        public double Length;// stands for the length of the member
        public double Angle;// stands for angle of the member with the horizontal
        public double Area;// stands for area of the member
        public double MI;// stands for Moment of inertia
        public double ME;  // stands for Modulus of elasticity
        public double NFEM;// stands for near end fixed end moment
        public double FFEM;// stands for far end fixed end moment
        public Point NECDNT; // stands for the coordinate of the near end
        public Point FECDNT; // stands for the coordinate of the far end
        public double NForce;// stands for the near end fixed end force
        public double FForce;// stands for the far end fixed end force
        public Member(string _Name, double _Lenght,double _Angle ,double _Area, double _MI, double _ME,double _NFEM,double _FFEM,Point _NECDNT,Point _FECDNT)
        {
            Name = _Name;
            Length = _Lenght;
            Angle  = _Angle;
            Area = _Area;
            MI = _MI;
            ME = _ME;
            NFEM = _NFEM;
            FFEM = _FFEM;
            NECDNT = _NECDNT;
            FECDNT = _FECDNT;
        }
        public Member()
        {
        }
        public double NearEndMoment(double q,double a,double b,double L,double Angle,string LoadType,bool Direction)
        {  
            maths math  = new maths();
            if(LoadType== "Concentrated" )
           return -q*a*b*b*Math.Cos(math.DR(Angle))/(L*L);
           else if(LoadType=="Distributed")
                return -(q /(L*L))*(Math.Pow(L-b,2)*(L*L/12+L*b/6+(b*b)/4)-(a*a)*(L*L/2-2*L*a/3+(a*a)/4));
           else if(LoadType == "Triangular"&Direction)
            {
                return -(q / (L * L*(L - a - b))) * (TriangularLoadFunctionLR(a, b, L, L - b) - TriangularLoadFunctionLR(a, b, L, a));
            }
            else if ((LoadType == "Triangular") & (!Direction))
            {
                return -(q / (L * L * (L - a - b))) * (TriangularLoadFunctionRL(a, b, L, L - b) - TriangularLoadFunctionRL(a, b, L, a));
            }
            else
            {
                MessageBox.Show("Such Type of load is not yet defined");
                return 0;
            }
        } 
        public double FarEndMoment(double q,double a,double b,double L,double Angle,string LoadType,bool Direction)
        {
            maths math  = new maths();
           if(LoadType== "Concentrated" )
           return q*Math.Pow(a,2)*b*Math.Cos(math.DR(Angle))/(L*L);
           else if(LoadType=="Distributed")
           return (q /(L*L))*(Math.Pow(L-a,2)*(L*L/12+L*a/6+(a*a)/4)-b*b*(L*L/2-2*b/3+(b*b)/4));
            else if(LoadType == "Triangular"&Direction)
            {
                return (q / (L * L*(L - a - b))) * (TriangularLoadFunctionRL(a, b, L, L - b) - TriangularLoadFunctionRL(a, b, L, a));  
            }
           else if ((LoadType == "Triangular" )&(!Direction))
           {
               return (q / (L * L*(L - a - b))) * (TriangularLoadFunctionLR(a, b, L, L - a) - TriangularLoadFunctionRL(a, b, L, b));
           }
           else
           {
               MessageBox.Show("Such Type of load is not yet defined");
               return 0;
           }
        }
        public double TriangularLoadFunctionLR(double a ,double b,double L,double x)
        {
            return (Math.Pow(x, 5) / 5 - (2 * L + a) * Math.Pow(x, 4) / 4 + L / 3 * (L + 2 * a) * Math.Pow(x, 3) - a * L * L * x * x);
        }
        public double TriangularLoadFunctionRL(double a, double b, double L, double x)
        {
            return -Math.Pow(x, 5) / 5 + (L + a)* Math.Pow(x, 4)/ 4 - a * L * Math.Pow(x, 3) / 3;
        }
        public Member[] AdjestMembers(Member[] members)
        {
            ArrayList Adjestedmembers = new ArrayList();
            Member M = new Member();
            Member[] Adjmemmber;
            for (int i = 0; i < members.Length; i++)
            {
                try
                {
                    if (!Adjestedmembers.Contains(members[i]) | members[i].Angle == 234424266788878)
                    {
                        Adjestedmembers.Add(members[i]);
                    }
                }
                catch (NullReferenceException)
                {
                    // does nothing but stop the program from interupting
                }
            }
            Adjmemmber = (Member[])Adjestedmembers.ToArray(Adjestedmembers[0].GetType());
            return Adjmemmber;
        }
        public int FindMember(Member[] mbrs,Point p1, PointF p2)
        {
            int index = -1;
            for (int i = 0; i < mbrs.Length; i++)
            {
                if (((mbrs[i].NECDNT == p1) & (mbrs[i].FECDNT == p2)) | ((mbrs[i].FECDNT == p1) & (mbrs[i].NECDNT == p2)))
                {
                    index = -1;
                    break;
                }
            }
            return index;
        }
        public int[] MemmbersAtJoint(Member[] mbrs, Point p)
        {
            ArrayList member = new ArrayList();
            for (int i = 0; i < mbrs.Length; i++)
            {
                if ((mbrs[i].NECDNT == p)| (mbrs[i].FECDNT == p))
                {
                   member.Add(i);
                }
            }
            return (int[])member.ToArray(member[0].GetType());
        }
        public int NearEndJointIndex(Joint []Joints,Member member)
        {
            int Index = -1;
            for (int i = 0; i < Joints.Length; i++)
            {
                if (Joints[i].CleintCoordinate == member.NECDNT)
                {
                    Index = i;
                    break;
                }
            }
            return Index;
        }
        public int FarEndJointIndex(Joint []Joints, Member member)
        {
            int Index = -1;
            for (int i = 0; i < Joints.Length; i++)
            {
                if (Joints[i].CleintCoordinate == member.FECDNT)
                {
                    Index = i;
                    break;
                }
            }
            return Index;
        }
        public bool CheckIfBeam(Joint[] Joints)
        {
            bool check = true;
            for (int i = 0; i < Joints.Length; i++)
            {
                if (Joints[0].Coordinate.Y != Joints[i].Coordinate.Y)
                {
                    check = false;
                    break;
                }
            }
            return check;
        }
        public double[] MemberEndForce(string loadtype, double q, double a, double b, double L, double NearEndMoment, double FarEndMoment, double angle, bool Direction)
        {
            double[] result = new double[2];
            switch (loadtype)
            {
                case "Concentrated":
                    result[1] = (Math.Cos(Math.PI / 180 * angle) * a * q + NearEndMoment + FarEndMoment) / L;
                    result[0] = Math.Cos(Math.PI / 180 * angle) * q - result[1];
                    break;
                case "Distributed":
                    result[1] = ((L - a - b) * (a + 0.5 * (L - a - b)) * q + NearEndMoment + FarEndMoment) / L;
                    result[0] = (L - a - b) * q - result[1];
                    break;
                case "Triangular":
                    if (Direction)
                    {
                        result[1] = (0.5 * (L - a - b) * q * (a + 2 * (L - a - b)/ 3) + NearEndMoment + FarEndMoment) / L;
                        result[0] = 0.5 * (L - a - b) * q - result[1];
                    }
                    else
                    {
                        result[0] = (0.5 * (L - a - b) * q * (a + 2 * (L - a - b) / 3) + NearEndMoment + FarEndMoment) / L;
                        result[1] = 0.5 * (L - a - b) * q - result[0];
                    }
                    break;
            }
            return result;
        }
    }
    public class maths
    {
        public double DR(double angle)
        {
            return Math.PI * angle / 180.0;
        }
        public double RD(double angle)
        {
            return angle * (180.0 / Math.PI);
        }
        public float DRF(double angle)
        {
            return (float)(Math.PI * angle / 180.0);
        }
        public float RDF(double angle)
        {
            return (float)(angle * (180.0 / Math.PI));
        }
        public double angle(Point p1, Point p2)
        {
            // this method returns the radian of the angle btween two point p1 and p2
           
            double angle = 0;
            double dx, dy;
            dx = MainForm.scale*(p2.X - p1.X);
            dy = MainForm.scale*(p2.Y - p1.Y);
            if ((dx > 0) & (dy < 0))
                angle = ((180 / Math.PI) * (Math.Atan(-dy / dx)));
            else if ((dx > 0) & (dy > 0))
                angle = (360 - (180 / Math.PI) * (Math.Atan(dy / dx)));
            else if ((dx < 0) & (dy <= 0))
                angle = (180 - (180 / Math.PI) * (Math.Atan(dy / dx)));
            else if ((dx < 0) & (dy > 0))
                angle = (180 + (180 / Math.PI) * (Math.Atan(-dy / dx)));
            else if ((dx == 0) & (dy > 0))
                angle = 270;
            else if ((dx == 0) & (dy < 0))
                angle = 90;
            return angle;
        }
        public double[,] copy(double[,] matx)
        {
            double[,] result = new double[matx.GetLength(0), matx.GetLength(1)];
            for (int i = 0; i < matx.GetLength(0); i++)
            {
                for (int j = 0; j < matx.GetLength(1); j++)
                {
                    result[i, j] = matx[i, j];
                }
            }
            return result;
        }
        public double[,] Transpose(double[,] matx)
        {
            double[,] result = new double[matx.GetLength(1), matx.GetLength(0)];
            for (int i = 0; i < matx.GetLength(0); i++)
            {
                for (int j = 0; j < matx.GetLength(1); j++)
                {
                    result[j, i] = matx[i, j];
                }
            }
            return result;
        }
        public double[,] Multiply(double[,] matx1, double[,] matx2)
        {
            try
            {
                if (matx1.GetLength(1) != matx2.GetLength(0))
                {
                    double[,] res = new double[0, 0];
                    return res;
                }

                else
                {
                    double[,] result = new double[matx1.GetLength(0), matx2.GetLength(1)];
                    for (int i = 0; i < matx1.GetLength(0); i++)
                    {
                        for (int j = 0; j < matx2.GetLength(1); j++)
                        {
                            double temp = 0;
                            for (int k = 0; k < matx1.GetLength(1); k++)
                            {
                                temp = temp + matx1[i, k] * matx2[k, j];
                            }
                            result[i, j] = temp;
                        }

                    }
                    return result;
                }
            }
            catch (Exception)
            {
                return new double[0, 0];
            }
        }
        public double[,] Solution(double[,] mtx)
        {
            int n = mtx.GetLength(0);
            double[,] matx = new double[n, n + 1];
            matx = copy(mtx);
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (matx[i, i] == 0)
                    {
                        for (int s = i + 1; s < n; s++)
                        {
                            if (!(matx[s, i] == 0))
                            // here rows with first elemet equal to 0 are swaped with other rows 
                            {
                                for (int t = i; t < n + 1; t++)
                                {
                                    double temp;
                                    temp = matx[i, t];
                                    matx[i, t] = matx[s, t];
                                    matx[s, t] = temp;
                                }
                                break;
                            }
                        }
                    }
                    double factor;
                    factor = matx[j, i] / matx[i, i];
                    for (int k = i; k < n + 1; k++)
                    {
                        matx[j, k] = matx[j, k] - factor * matx[i, k];
                    }
                }
            }
            //Here the back substitution is started
            double[,] ans = new double[n,1];
            for (int i = n - 1; i >= 0; i--)
            {
                double temp = 0;
                for (int j = 0; j < n; j++)
                {
                    temp += matx[i, j] * ans[j,0];
                }
                ans[i,0] = (matx[i, n] - temp) / matx[i, i];
            }
            return ans;
        }
        public double[,] Ogument(double[,] matx1, double[,] matx2)
        {
            double[,] Result = new double[matx1.GetLength(0), matx1.GetLength(1) + matx2.GetLength(1)];
            for (int i = 0; i < Result.GetLength(0); i++)
            {
                for (int j = 0; j < matx1.GetLength(1);j++)
                {
                    Result[i, j] = matx1[i, j];
                }
            }
            for (int i = 0; i < Result.GetLength(0); i++)
            {
                Result[i, matx1.GetLength(1)] = matx2[i, 0];
            }
            return Result;
        }
        public double[,] Partation(double[,] matx, int Row, int Columen, int XPartIndex,int YPartIndex)
        {
            if (XPartIndex < 2 & YPartIndex < 2)
            {
                double[,] Result = new double[Math.Abs(matx.GetLength(0) * XPartIndex - Row),Math.Abs(matx.GetLength(0) * YPartIndex - Columen)];
                for (int i = 0; i < Result.GetLength(0); i++)
                {
                    for (int j = 0; j < Result.GetLength(1); j++)
                    {
                        Result[i, j] = matx[ Row * XPartIndex + i, Columen * YPartIndex + j];
                    }
                }
                return Result;
            }
            else
            {
                MessageBox.Show("Inconsistant input: Index out of range");
                return new double[0, 0];
            }
        }
        public double[,] superpose(double[,] matx1, double[,] matx2, string ch)
        {
            if (matx1.GetLength(0) != matx2.GetLength(0) || matx1.GetLength(1) != matx2.GetLength(1))
            {
                Console.WriteLine(" The matrixs are not in compatable format pleas check your input");
                return null;
            }
            else
            {
                double[,] result = new double[matx1.GetLength(0), matx1.GetLength(1)];
                switch (ch)
                {
                    case "a":
                        {
                            for (int i = 0; i < matx1.GetLength(0); i++)
                            {
                                for (int j = 0; j < matx1.GetLength(1); j++)
                                {
                                    result[i, j] = matx1[i, j] + matx2[i, j];
                                }
                            }
                            break;
                        }
                    case "s":
                        {
                            for (int i = 0; i < matx1.GetLength(0); i++)
                            {
                                for (int j = 0; j < matx1.GetLength(1); j++)
                                {
                                    result[i, j] = matx1[i, j] - matx2[i, j];
                                }
                            }
                        }
                        break;
                }
                return result;

            }
        }
    }
}

