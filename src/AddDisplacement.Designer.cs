namespace StracturalControls
{
    partial class AddDisplacement
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
            this.ndnJoint = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtXDisp = new System.Windows.Forms.TextBox();
            this.txtYDisp = new System.Windows.Forms.TextBox();
            this.txtZDisp = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSupportType = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ndnJoint)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ndnJoint
            // 
            this.ndnJoint.Location = new System.Drawing.Point(148, 21);
            this.ndnJoint.Name = "ndnJoint";
            this.ndnJoint.Size = new System.Drawing.Size(54, 20);
            this.ndnJoint.TabIndex = 0;
            this.ndnJoint.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ndnJoint.ValueChanged += new System.EventHandler(this.ndnJoint_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Joint Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Z-Displacement (E-3 Rad)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Y-Displacement (mm)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "X-Displacement (mm)";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(165, 182);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(72, 182);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtXDisp
            // 
            this.txtXDisp.Location = new System.Drawing.Point(149, 15);
            this.txtXDisp.Name = "txtXDisp";
            this.txtXDisp.Size = new System.Drawing.Size(73, 20);
            this.txtXDisp.TabIndex = 8;
            this.txtXDisp.Text = "0.000";
            // 
            // txtYDisp
            // 
            this.txtYDisp.Location = new System.Drawing.Point(149, 41);
            this.txtYDisp.Name = "txtYDisp";
            this.txtYDisp.Size = new System.Drawing.Size(73, 20);
            this.txtYDisp.TabIndex = 9;
            this.txtYDisp.Text = "0.000";
            // 
            // txtZDisp
            // 
            this.txtZDisp.Location = new System.Drawing.Point(149, 70);
            this.txtZDisp.Name = "txtZDisp";
            this.txtZDisp.Size = new System.Drawing.Size(73, 20);
            this.txtZDisp.TabIndex = 10;
            this.txtZDisp.Text = "0.000";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtXDisp);
            this.groupBox1.Controls.Add(this.txtZDisp);
            this.groupBox1.Controls.Add(this.txtYDisp);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Displacements";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Support Type";
            // 
            // txtSupportType
            // 
            this.txtSupportType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSupportType.Location = new System.Drawing.Point(148, 46);
            this.txtSupportType.Name = "txtSupportType";
            this.txtSupportType.ReadOnly = true;
            this.txtSupportType.Size = new System.Drawing.Size(54, 20);
            this.txtSupportType.TabIndex = 13;
            // 
            // AddDisplacement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 208);
            this.Controls.Add(this.txtSupportType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ndnJoint);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(258, 244);
            this.Name = "AddDisplacement";
            this.Text = "Add Displacement";
            this.Load += new System.EventHandler(this.AddDisplacement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ndnJoint)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown ndnJoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtXDisp;
        private System.Windows.Forms.TextBox txtYDisp;
        private System.Windows.Forms.TextBox txtZDisp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSupportType;
    }
}