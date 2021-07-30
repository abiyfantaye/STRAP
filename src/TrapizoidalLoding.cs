using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StracturalControls
{
    public partial class TrapizoidalLoding : UserControl
    {
        public TrapizoidalLoding()
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
        public Point[] triagles(Point pt)
        {
            Point[] result = new Point[3];
            result[0] = new Point(pt.X - 4, pt.Y);
            result[1] = new Point(pt.X + 4, pt.Y);
            result[2] = new Point(pt.X, pt.Y + 8);
            return result;
        }
        private void TrapizoidalLoding_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            float k = (float)4*this.Height / this.Width;
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.Black);
            int nuarow = this.Width / 15;
            int  spacing = 15;
            for (int i = 1; i < nuarow; i++)
            {
                if ((i <= nuarow / 4) )
                {
                    g.DrawLine(pen, (spacing * i), this.Height - k * spacing * i, (spacing * i), (this.Height - 8));
                    Point p = new Point(i * spacing, this.Height - 8);
                    g.FillPolygon(brush, triagles(p));
                }
               else if (i >(0.75* nuarow))
                {
                    int j = i- (int)(0.75 * nuarow);
                    g.DrawLine(pen, (spacing * i),k * spacing * j, (spacing * i), (this.Height - 8));
                    Point p = new Point(i * spacing, this.Height - 8);
                    g.FillPolygon(brush, triagles(p));
                }
                else
                {
                    g.DrawLine(pen, spacing * i, 0, spacing * i, this.Height - 8);
                    Point p = new Point(i * spacing, this.Height - 8);
                    g.FillPolygon(brush, triagles(p));
                }
            }
            g.DrawLine(pen,(int) (this.Width / 4), 0, (int)(0.75* this.Width), 0);
            g.DrawLine(pen, 0, this.Height - 1, this.Width, this.Height - 1);
            g.DrawLine(pen, this.Width / 4, 0, 0, this.Height - 1);
            g.DrawLine(pen, 3 * this.Width / 4, 0, this.Width, this.Height);
        }
    }
}
