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
    public partial class MovingBox : UserControl
    {
        public MovingBox()
        {
            InitializeComponent();
        }
        private string _Text;
        public string Coordinate
        {
            get { return _Text; }
            set { _Text = value;this.Invalidate();}          
        }
        private void MovingBox_Paint(object sender, PaintEventArgs e)
        {   
            Graphics g = e.Graphics;
            g.FillRectangle(new HatchBrush(HatchStyle.Cross, Color.DarkSlateGray),this.ClientRectangle);
            g.DrawString(_Text, SystemFonts.DefaultFont, new SolidBrush(Color.White), new PointF(0, 0));
        }
    }
}
