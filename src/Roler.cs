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
    public partial class Roler : UserControl
    {
        public Roler()
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
        private void Roler_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black ,2.0f);
            g.DrawEllipse(pen, 10, 10, 40, 40);
            g.DrawLine(pen, 10, 10, this.Width - 10, 10);
            g.DrawLine(pen, 0, 50, this.Width - 35, 50);
            for (int i = 0; i <(this.Width -30)/ 10; i++)
            {
                g.DrawLine(pen, i * 10, this.Height, (i + 1) * 10, 50);
            }
        }

    }
}
