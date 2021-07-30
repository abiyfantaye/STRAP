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
    public partial class DrawPanel : UserControl
    {
        public DrawPanel()
        {
            InitializeComponent();
        }
        public bool DoubleBuffer
        {
            get
            {
                return this.DoubleBuffered;
            }
            set
            {
                this.DoubleBuffered = value;
            }
        }
        private void DrawPanel_Paint(object sender, PaintEventArgs e)
        {            
            Graphics g = e.Graphics;
            g.FillRectangle(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray), this.ClientRectangle);
            Pen p = new Pen(Color.White);
            SolidBrush Brush = new SolidBrush(Color.White);
            Point[] p1 = new Point[3];
            Point[] p2 = new Point[3];
            g.DrawLine(p, 24, this.Height - 24, 24, this.Height - 96);
            g.DrawLine(p, 24, this.Height - 24, 96, this.Height - 24);
            p1[0] = new Point(20, this.Height - 96);
            p1[1] = new Point(24, this.Height - 104);
            p1[2] = new Point(28, this.Height - 96);
            g.FillPolygon(Brush, p1);
            p2[0] = new Point(96, this.Height - 28);
            p2[1] = new Point(96, this.Height - 20);
            p2[2] = new Point(104, this.Height - 24);
            g.FillPolygon(Brush, p2);
            FontStyle style = FontStyle.Regular;
            Font timesNewRoman = new Font("Times New Roman", 10, style);
            g.DrawString("Y", timesNewRoman, Brush, 32, this.Height - 96);
            g.DrawString("X", timesNewRoman, Brush, 86, this.Height - 45);
        }
    }
}
