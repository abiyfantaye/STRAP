using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StracturalControls
{
    public partial class ConcetratedLoad : UserControl
    {
        public ConcetratedLoad()
        {
            InitializeComponent();
        }
        public bool _selected = false;
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (value)
                {
                    this.BackColor = SystemColors.Window;
                    this.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    this.BackColor = SystemColors.Control;
                    this.BorderStyle = BorderStyle.None;
                }
                _selected = value;
                this.Invalidate();
            }
        }  
        private void ConcetratedLoad_Paint(object sender, PaintEventArgs e)
        {   
            maths ob = new maths();
            Graphics g = e.Graphics;
            Matrix m = new Matrix();
            m.RotateAt(45, new PointF(10, this.Height - 10), MatrixOrder.Append);           
            Pen pen = new Pen(Color.Black, 2.0f);
            SolidBrush brush = new SolidBrush(Color.Black);
            Point p1 = new Point(this.Width -5, 5);
            Point p2 = new Point(10 , this.Height-10);
            g.DrawLine(pen, p1, p2);
            g.Transform = m;
            Point[] p = new Point[3];
            p[0] = new Point (5,this.Height - 10);
            p[1] = new Point (15,this.Height - 10);
            p[2] = new Point (10,this.Height);
            g.FillPolygon(brush, p);
        }
   }
}
