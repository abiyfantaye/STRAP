namespace StracturalControls
{
    partial class JointLoad
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
            this.txtMagnitude = new System.Windows.Forms.TextBox();
            this.txtJoint = new System.Windows.Forms.TextBox();
            this.txtAngle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnConsMoment = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.gbxLaodType = new System.Windows.Forms.GroupBox();
            this.ConsForce = new StracturalControls.ConcetratedLoad();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.gbxLaodType.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMagnitude
            // 
            this.txtMagnitude.Location = new System.Drawing.Point(163, 47);
            this.txtMagnitude.Name = "txtMagnitude";
            this.txtMagnitude.Size = new System.Drawing.Size(75, 20);
            this.txtMagnitude.TabIndex = 3;
            this.txtMagnitude.Text = "10.00";
            // 
            // txtJoint
            // 
            this.txtJoint.Location = new System.Drawing.Point(163, 19);
            this.txtJoint.Name = "txtJoint";
            this.txtJoint.Size = new System.Drawing.Size(75, 20);
            this.txtJoint.TabIndex = 4;
            this.txtJoint.Text = "1";
            // 
            // txtAngle
            // 
            this.txtAngle.Location = new System.Drawing.Point(163, 73);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(75, 20);
            this.txtAngle.TabIndex = 5;
            this.txtAngle.Text = "0.00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Angle From The Vertical";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Magnitude ( KN )";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Joint";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMagnitude);
            this.groupBox2.Controls.Add(this.txtJoint);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtAngle);
            this.groupBox2.Location = new System.Drawing.Point(12, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 101);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Loading Information";
            // 
            // btnConsMoment
            // 
            this.btnConsMoment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConsMoment.Location = new System.Drawing.Point(117, 32);
            this.btnConsMoment.Name = "btnConsMoment";
            this.btnConsMoment.Size = new System.Drawing.Size(61, 48);
            this.btnConsMoment.TabIndex = 11;
            this.btnConsMoment.UseVisualStyleBackColor = true;
            this.btnConsMoment.Paint += new System.Windows.Forms.PaintEventHandler(this.btnConsMoment_Paint);
            this.btnConsMoment.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnConsMoment_MouseMove);
            this.btnConsMoment.Click += new System.EventHandler(this.btnConsMoment_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Moment";
            // 
            // gbxLaodType
            // 
            this.gbxLaodType.Controls.Add(this.ConsForce);
            this.gbxLaodType.Controls.Add(this.label5);
            this.gbxLaodType.Controls.Add(this.label4);
            this.gbxLaodType.Controls.Add(this.btnConsMoment);
            this.gbxLaodType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbxLaodType.Location = new System.Drawing.Point(12, 12);
            this.gbxLaodType.Name = "gbxLaodType";
            this.gbxLaodType.Size = new System.Drawing.Size(254, 89);
            this.gbxLaodType.TabIndex = 10;
            this.gbxLaodType.TabStop = false;
            this.gbxLaodType.Text = "Load Type";
            // 
            // ConsForce
            // 
            this.ConsForce.BackColor = System.Drawing.SystemColors.Control;
            this.ConsForce.Location = new System.Drawing.Point(21, 32);
            this.ConsForce.Name = "ConsForce";
            this.ConsForce.Selected = false;
            this.ConsForce.Size = new System.Drawing.Size(48, 48);
            this.ConsForce.TabIndex = 14;
            this.ConsForce.Click += new System.EventHandler(this.ConsForce_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Force";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(175, 214);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(84, 214);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // JointLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(271, 240);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbxLaodType);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(287, 276);
            this.MinimumSize = new System.Drawing.Size(287, 276);
            this.Name = "JointLoad";
            this.Text = "Add Joint Load";
            this.Load += new System.EventHandler(this.JointLoad_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbxLaodType.ResumeLayout(false);
            this.gbxLaodType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtMagnitude;
        private System.Windows.Forms.TextBox txtJoint;
        private System.Windows.Forms.TextBox txtAngle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnConsMoment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbxLaodType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private ConcetratedLoad ConsForce;
    }
}