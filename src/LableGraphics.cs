using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Media;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
namespace StracturalControls
{
    [Serializable]
    public class LableGraphics
    {
        public Member[] Members = new Member[1];
        public Point[] Points  = new Point[1];
        public ArrayList EndL  = new ArrayList();
        public LableGraphics(Member[] members,Point[]points,ArrayList endl)
        {
             Members = members;
             Points = points;
             EndL = endl;
        }
        public LableGraphics()
        {
            Members = new Member[0];
            Points = new Point[0];
            EndL = new ArrayList();
        }
        public void DrawLables(PaintEventArgs e)
        { 
            Graphics g = e.Graphics;
            Point[] Adjested_Points = RemoveRepeatedPoints(Points); 
            for (int i = 0; i < Adjested_Points.Length; i++)
            {
                DrawJointLables(g, Adjested_Points[i],i+1);
            }
            for (int i = 0; i < Members.Length; i++)
            {
                if (!EndL.Contains(i))
                {
                    DrawMemberLables(g, Members[i]);
                }
            }
        }
        private void DrawJointLables(Graphics g, Point p,int n)
        {
            Matrix m = new Matrix();
            Pen pen = new Pen(Color.White);
            SolidBrush brush = new SolidBrush(Color.White);
            FontStyle style = FontStyle.Regular;
            Font areal = new Font(new FontFamily("Arial"), 8, style);
            PointF ReffPoint = new PointF();
            ReffPoint.X =p.X +5;
            ReffPoint.Y = p.Y+5 ;
            m.Translate(ReffPoint.X, ReffPoint.Y,MatrixOrder.Append);
            g.Transform = m;
            g.DrawEllipse(pen,0, 0, 20, 20);
            g.FillEllipse(new SolidBrush(Color.Transparent), 1, 2, 18, 18);
            g.DrawString(n.ToString(),areal,brush,2,3);
            g.ResetTransform();
        }
        private void DrawMemberLables(Graphics g,Member member)
        {

            Matrix m = new Matrix();
            Pen pen = new Pen(Color.White);
            SolidBrush brush = new SolidBrush(Color.White);
            FontStyle style = FontStyle.Regular;
            Font areal = new Font(new FontFamily("Arial"), 8, style);
            PointF ReffPoint = new PointF();
            try
            {
                maths mt = new maths();
                ReffPoint.X = (member.NECDNT.X + member.FECDNT.X) / 2;
                ReffPoint.Y = (member.NECDNT.Y + member.FECDNT.Y) / 2;
                m.Translate(ReffPoint.X + (float)(7.5*Math.Sin(mt.DR(member.Angle))), ReffPoint.Y + (float)(7.5*Math.Cos(mt.DR(member.Angle))));               
                g.Transform = m;
                g.DrawString(member.Name, areal, brush,-7,-6 );
                g.ResetTransform();
                m.Reset();
                m.Translate(ReffPoint.X, ReffPoint.Y,MatrixOrder.Append);
                m.RotateAt(-(float)member.Angle, ReffPoint, MatrixOrder.Append);
                g.Transform = m;
                g.DrawLine(pen, -10, 15, 10, 15);
                g.DrawLine(pen, 10, 1, 10, 15);
                g.DrawLine(pen, -10, 1, -10, 15);
                g.DrawLine(pen, -12, 20, 12, 20);
                g.FillPolygon(brush, triagles(new Point (12, 20)));                
                g.ResetTransform();
            }
            catch (Exception)
            {
            }
        }
        public Point[] triagles(Point pt)
        {
            Point[] result = new Point[3];
            result[0] = new Point(pt.X, pt.Y-3);
            result[1] = new Point(pt.X, pt.Y+3);
            result[2] = new Point(pt.X + 6, pt.Y );
            return result;
        }
        private int[] MembersForPoint(Point p)
        {
            ArrayList FoundMemember = new ArrayList();
            for (int i = 0; i < Members.Length; i++)
            {
                if (!EndL.Contains(i))
                {
                    if ((Members[i].NECDNT == p) || (Members[i].FECDNT == p))
                    {
                        FoundMemember.Add(i);
                    }
                } 
            }
            return (int [])FoundMemember.ToArray(FoundMemember[0].GetType());
        }
        private int XAgregate(Point p)
        {
            int Xagregate = 0;
            int[] Members_Point = MembersForPoint(p);
            for (int i = 0; i < Members_Point.Length; i++)
            {
                Point TempCoordinate = Members[Members_Point[i]].NECDNT;
                if (Members[Members_Point[i]].NECDNT == p)
                    TempCoordinate = Members[Members_Point[i]].FECDNT;
                Xagregate += p.X - TempCoordinate.X;
            }
            return Xagregate;
        }
        private int YAgregate(Point p)
        {
            int Yagregate = 0;
            int[] Members_Point = MembersForPoint(p);
            for (int i = 0; i < Members_Point.Length; i++)
            {
                Point TempCoordinate = Members[Members_Point[i]].NECDNT;
                if (Members[Members_Point[i]].NECDNT == p)
                    TempCoordinate = Members[Members_Point[i]].FECDNT;
                Yagregate += p.Y- TempCoordinate.Y;
            }
            return Yagregate;
        }
        public Point [] RemoveRepeatedPoints(Point [] pnts)
        {  
            ArrayList AdjestedPoints   = new ArrayList();
            Point[] pt = new Point[0];
            //MessageBox.Show(Points.Length.ToString());

            for (int i = 0; i < pnts.Length; i++)
                {
                    if (!AdjestedPoints.Contains(pnts[i]))
                    {
                        AdjestedPoints.Add(pnts[i]);
                    }
                }
            try
            {
            pt = (Point[])AdjestedPoints.ToArray(AdjestedPoints[0].GetType());
            }
            catch(Exception)
            {
            }
            return pt;
        }
        public int IndexOf(Point[] pts, Point p)
        {
            int index = -1;
            for (int i = 0; i < pts.Length; i++)
            {
                if (pts[i] == p)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
    }
}
