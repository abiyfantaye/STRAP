//using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StracturalControls
{
    public partial class MemberControle : UserControl
    {
        public MemberControle()
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
                    this.BackColor = SystemColors.Window ;
                    this.BorderStyle = BorderStyle.FixedSingle  ;
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
        private void MemberControle_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLine(new Pen(Color.Black, 2.0f),3,this.Height-5, this.Width - 5, 3);
        }
    }
}
