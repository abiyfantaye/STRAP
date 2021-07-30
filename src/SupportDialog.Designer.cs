namespace StracturalControls
{
    partial class SupportDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.gbxSupportType = new System.Windows.Forms.GroupBox();
            this.txtSupAngle = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSupLocation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fixed1 = new StracturalControls.Fixed();
            this.hindge1 = new StracturalControls.Hindge();
            this.roler1 = new StracturalControls.Roler();
            this.pin1 = new StracturalControls.Pin();
            this.gbxSupportType.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(274, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fixed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hidge";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Pin";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Roller";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(83, 252);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(183, 252);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 10;
            this.btnCancle.Text = "Cancel";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // gbxSupportType
            // 
            this.gbxSupportType.Controls.Add(this.txtSupAngle);
            this.gbxSupportType.Controls.Add(this.label6);
            this.gbxSupportType.Controls.Add(this.txtSupLocation);
            this.gbxSupportType.Controls.Add(this.fixed1);
            this.gbxSupportType.Controls.Add(this.label2);
            this.gbxSupportType.Controls.Add(this.hindge1);
            this.gbxSupportType.Controls.Add(this.label5);
            this.gbxSupportType.Controls.Add(this.roler1);
            this.gbxSupportType.Controls.Add(this.pin1);
            this.gbxSupportType.Controls.Add(this.label1);
            this.gbxSupportType.Controls.Add(this.label4);
            this.gbxSupportType.Controls.Add(this.label3);
            this.gbxSupportType.Location = new System.Drawing.Point(5, 3);
            this.gbxSupportType.MaximumSize = new System.Drawing.Size(315, 222);
            this.gbxSupportType.MinimumSize = new System.Drawing.Size(315, 222);
            this.gbxSupportType.Name = "gbxSupportType";
            this.gbxSupportType.Size = new System.Drawing.Size(315, 222);
            this.gbxSupportType.TabIndex = 11;
            this.gbxSupportType.TabStop = false;
            this.gbxSupportType.Text = "Support Types";
            // 
            // txtSupAngle
            // 
            this.txtSupAngle.Location = new System.Drawing.Point(219, 174);
            this.txtSupAngle.Name = "txtSupAngle";
            this.txtSupAngle.Size = new System.Drawing.Size(87, 20);
            this.txtSupAngle.TabIndex = 14;
            this.txtSupAngle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSupAngle_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(97, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Angle From Vertical";
            // 
            // txtSupLocation
            // 
            this.txtSupLocation.Location = new System.Drawing.Point(219, 145);
            this.txtSupLocation.Name = "txtSupLocation";
            this.txtSupLocation.Size = new System.Drawing.Size(87, 20);
            this.txtSupLocation.TabIndex = 13;
            this.txtSupLocation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSupLocation_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Location (Joint)";
            // 
            // fixed1
            // 
            this.fixed1.BackColor = System.Drawing.SystemColors.Control;
            this.fixed1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.fixed1.Location = new System.Drawing.Point(219, 40);
            this.fixed1.Name = "fixed1";
            this.fixed1.Selected = false;
            this.fixed1.Size = new System.Drawing.Size(85, 58);
            this.fixed1.TabIndex = 6;
            this.fixed1.Click += new System.EventHandler(this.fixed1_Click);
            // 
            // hindge1
            // 
            this.hindge1.BackColor = System.Drawing.SystemColors.Control;
            this.hindge1.Location = new System.Drawing.Point(11, 40);
            this.hindge1.Name = "hindge1";
            this.hindge1.Selected = false;
            this.hindge1.Size = new System.Drawing.Size(74, 58);
            this.hindge1.TabIndex = 7;
            this.hindge1.Click += new System.EventHandler(this.hindge1_Click);
            // 
            // roler1
            // 
            this.roler1.BackColor = System.Drawing.SystemColors.Control;
            this.roler1.Location = new System.Drawing.Point(100, 40);
            this.roler1.Name = "roler1";
            this.roler1.Selected = false;
            this.roler1.Size = new System.Drawing.Size(96, 60);
            this.roler1.TabIndex = 5;
            this.roler1.Click += new System.EventHandler(this.roler1_Click);
            // 
            // pin1
            // 
            this.pin1.BackColor = System.Drawing.SystemColors.Control;
            this.pin1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pin1.Location = new System.Drawing.Point(7, 145);
            this.pin1.Name = "pin1";
            this.pin1.Selected = false;
            this.pin1.Size = new System.Drawing.Size(78, 49);
            this.pin1.TabIndex = 0;
            this.pin1.Click += new System.EventHandler(this.pin1_Click);
            // 
            // SupportDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 287);
            this.Controls.Add(this.gbxSupportType);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnAdd);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(341, 323);
            this.Name = "SupportDialog";
            this.Text = "Add Support";
            this.Load += new System.EventHandler(this.SupportDialog_Load);
            this.Click += new System.EventHandler(this.SupportDialog_Click);
            this.gbxSupportType.ResumeLayout(false);
            this.gbxSupportType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Pin pin1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Roler roler1;
        private Fixed fixed1;
        private Hindge hindge1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.GroupBox gbxSupportType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSupLocation;
        private System.Windows.Forms.TextBox txtSupAngle;
        private System.Windows.Forms.Label label6;

    }
}