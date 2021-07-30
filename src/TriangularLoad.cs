using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace StracturalControls
{
    public partial class TriangularLoad : UserControl
    {
        public TriangularLoad()
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
        public PointF[] triagles(PointF pt)
        {
            PointF[] result = new PointF[3];
            result[0] = new PointF(pt.X - 4, pt.Y);
            result[1] = new PointF(pt.X + 4, pt.Y);
            result[2] = new PointF(pt.X, pt.Y + 8);
            return result;
        }
        private void TriangularLoad_Paint(object sender, PaintEventArgs e)
        {  
            float k = (float) this.Height / this.Width;
            int y = this.Height-10;
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);
            g.DrawLine(pen, this.Width-1, 0, this.Width-1, this.Height);
            g.DrawLine(pen, 0, this.Height-1, this.Width, this.Height-1);
            g.DrawLine(pen, this.Width, 0, 0, this.Height);
            SolidBrush brush = new SolidBrush(Color.Black);
            int nuarow = this.Width / 15;
            int spacing = 15;
            for (int i = 2; i <=nuarow; i++)
            {
                g.DrawLine(pen, (spacing * i),this.Height - k*spacing*i ,(spacing * i), (this.Height - 8));
                Point p = new Point(i * spacing, this.Height - 8);
                g.FillPolygon(brush, triagles(p));
            }
        }
    }
}
