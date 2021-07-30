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
    public partial class Pin : UserControl
    {
        public Pin()
        {
            InitializeComponent();
        }
        public bool _selected;
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
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black,2.0f);
            Point[] p = new Point[3];
            p[0] = new Point(this.Width / 2,10);
            p[1] = new Point(this.Width-10,this.Height-10);
            p[2] = new Point(10,this.Height-10);
            g.DrawPolygon(pen, p);
            g.DrawEllipse(pen, this.Width / 2 - 5, 0, 10, 10);
            for (int i = 0; i <=this.Width / 10; i++)
            {
                g.DrawLine(pen, i * 10, this.Height,(i + 1) * 10, this.Height - 10);
            }
            g.DrawLine(pen,0,this.Height-10,this.Width,this.Height-10);
        }
    }
}
