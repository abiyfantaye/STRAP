using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StracturalControls
{
    public partial class Fixed : UserControl
    {
        public Fixed()
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
            Graphics g = CreateGraphics();
            Pen pen = new Pen(Color.Black,2.0f);
            g.DrawLine(pen,10,5,10,this.Height-5);
            g.DrawLine(pen ,10,this.Height/2,this.Width-5,this.Height/2);
            for(int i =0;i<this.Height/10;i++)
            {
                g.DrawLine(pen, 0, this.Height - (i * 10), 10, this.Height - (i + 1) * 10);
            }
        }
    }
}
