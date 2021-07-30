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
    public partial class MyButton : UserControl
    {
        public MyButton()
        {
            InitializeComponent();
        }
        private string text;
        private bool analsed = false;
        public string MyText
        {
            get { return text; }
            set { text = value;}
        }
        public bool Analysed
        {
            get { return analsed; }
            set { analsed = value; }
        }
        private void MyButton_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            if(!analsed)
            DrawButton(g, Color.DarkRed, Color.Gold);
            else
                DrawButton(g, Color.Red, Color.Yellow);
        }

        private void MyButton_MouseEnter(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            DrawButton(g, Color.Red, Color.Yellow);
        }

        private void MyButton_MouseLeave(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            DrawButton(g, Color.DarkRed, Color.Gold);
        }

        private void DrawButton(Graphics g, Color color1, Color color2)
        {
            SolidBrush brush = new SolidBrush(Color.White);
            FontStyle style = FontStyle.Regular;
            Font areal = new Font(new FontFamily("Arial"), 10, style);
            Rectangle drawarea = this.ClientRectangle;
            LinearGradientBrush lineargridbrush = new LinearGradientBrush(drawarea, color1,color2, LinearGradientMode.Vertical);
            g.FillEllipse(lineargridbrush, drawarea);
            g.DrawString(text, areal, brush, this.Width / 2 - 8 * text.Length / 2, this.Height / 2 - 8);
        }
    }
}
