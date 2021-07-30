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
    public class Joint
    {
        public PointF Coordinate;
        public string Name;
        public Point CleintCoordinate;
        public string SupportType;
        public double LoadX;
        public double LoadY;
        public double LoadZ;
        public double DispX;
        public double DispY;
        public double DispZ;
        public int[] SSMatrixIndexs = new int [3];
        public Joint()
        {
            Coordinate = new Point();
            Name = "1";
            CleintCoordinate = new Point();
            SupportType  = "None";
            LoadX = 0.00;
            LoadY = 0.00;
        }
        public Joint(PointF coordinate, string name, Point cleintcoordinate, string supporttype,double loadx,double loady,double loadZ,double dispX,double dispY,double dispZ)
        {
            Coordinate = coordinate;
            Name = name;
            CleintCoordinate = cleintcoordinate;
            SupportType = supporttype;
            LoadX = loadx;
            LoadY = loady;
            LoadZ = loadZ;
            DispX = dispX;
            DispY = dispY;
            DispZ = dispZ;
        }
        public Point RefferenceToCleint(PointF p ,PointF Reffp)
        {
            Point Changed = new Point();
            Changed.X = (int)(p.X + Reffp.X*(1/MainForm.scale) );
            Changed.Y = (int)(Reffp.Y* (1 / MainForm.scale) - p.Y );
            return Changed;
        }
        public PointF CleintToRefference(Point p, PointF Reffp)
        {
            PointF changed = new Point();
            changed.X = (MainForm.scale * p.X)- Reffp.X;
            changed.Y = Reffp.Y - (p.Y * MainForm.scale);
            return changed;
        }
        public void AddDisplacement(double Xdisp, double Ydisp, double Zdisp)
        {  
            DispX = Xdisp;
            DispY = Ydisp;
            DispZ = Zdisp;
        }
        public void AddLoad(double Xload, double Yload, double Zlaod)
        {
            LoadX = Xload;
            LoadY = Yload;
            LoadZ = Zlaod;
        }
    }
}
