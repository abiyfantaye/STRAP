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
    public partial class LineLoad : UserControl
    {
        public LineLoad()
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

        private void LineLoad_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.Black);
            g.DrawRectangle(pen, 0, 0, this.Width-1, this.Height-1);
            int nuarow = this.Width / 15;
            int spacing = 15;
            for (int i = 1; i <nuarow; i++)
            {    
                g.DrawLine(pen, spacing * i,0, spacing * i, this.Height - 8);
                Point p = new Point(i * spacing, this.Height - 8);
                g.FillPolygon(brush, triagles(p));
            }
        }


    }
}
