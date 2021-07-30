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
    public partial class Hindge : UserControl
    {
        public Hindge()
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
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Black, 2.0f);
            g.DrawEllipse(p, this.Width / 2 - 5, this.Height/2-5, 10, 10);
            g.DrawLine(p, 5, this.Height/2, this.Width / 2 - 5, this.Height/2);
            g.DrawLine(p, this.Width / 2 + 5, this.Height/2, this.Width - 5, this.Height/2);
        }
    }
}
