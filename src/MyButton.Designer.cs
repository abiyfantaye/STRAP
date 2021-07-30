namespace StracturalControls
{
    partial class MyButton
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MyButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MyButton";
            this.Size = new System.Drawing.Size(210, 102);
            this.MouseLeave += new System.EventHandler(this.MyButton_MouseLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyButton_Paint);
            this.MouseEnter += new System.EventHandler(this.MyButton_MouseEnter);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
