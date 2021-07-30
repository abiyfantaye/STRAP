namespace StracturalControls
{
    partial class LoadDialog
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
            this.Concetrated = new System.Windows.Forms.Label();
            this.GbxLoadTypes = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMagnitude = new System.Windows.Forms.TextBox();
            this.txtAngle = new System.Windows.Forms.TextBox();
            this.txtDsFromNearEnd = new System.Windows.Forms.TextBox();
            this.txtMbrName = new System.Windows.Forms.TextBox();
            this.gbxLoadingInfo = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDsFromFarEnd = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.triangul1 = new StracturalControls.triangul();
            this.triangularLoad1 = new StracturalControls.TriangularLoad();
            this.TrapLoad = new StracturalControls.TrapizoidalLoding();
            this.TriangLoad = new StracturalControls.TriangularLoad();
            this.DistLoad = new StracturalControls.LineLoad();
            this.ConsLoad = new StracturalControls.ConcetratedLoad();
            this.GbxLoadTypes.SuspendLayout();
            this.gbxLoadingInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // Concetrated
            // 
            this.Concetrated.AutoSize = true;
            this.Concetrated.Location = new System.Drawing.Point(9, 16);
            this.Concetrated.Name = "Concetrated";
            this.Concetrated.Size = new System.Drawing.Size(65, 13);
            this.Concetrated.TabIndex = 1;
            this.Concetrated.Text = "Concetrated";
            // 
            // GbxLoadTypes
            // 
            this.GbxLoadTypes.Controls.Add(this.label1);
            this.GbxLoadTypes.Controls.Add(this.TrapLoad);
            this.GbxLoadTypes.Controls.Add(this.button1);
            this.GbxLoadTypes.Controls.Add(this.label3);
            this.GbxLoadTypes.Controls.Add(this.label2);
            this.GbxLoadTypes.Controls.Add(this.TriangLoad);
            this.GbxLoadTypes.Controls.Add(this.DistLoad);
            this.GbxLoadTypes.Controls.Add(this.ConsLoad);
            this.GbxLoadTypes.Controls.Add(this.Concetrated);
            this.GbxLoadTypes.Location = new System.Drawing.Point(3, 6);
            this.GbxLoadTypes.Name = "GbxLoadTypes";
            this.GbxLoadTypes.Size = new System.Drawing.Size(367, 175);
            this.GbxLoadTypes.TabIndex = 2;
            this.GbxLoadTypes.TabStop = false;
            this.GbxLoadTypes.Text = "Load Type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Trapizoidal";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(223, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Other Types Of Loading";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Uniformly Distributed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Triangular";
            // 
            // txtMagnitude
            // 
            this.txtMagnitude.Location = new System.Drawing.Point(213, 45);
            this.txtMagnitude.Name = "txtMagnitude";
            this.txtMagnitude.Size = new System.Drawing.Size(72, 20);
            this.txtMagnitude.TabIndex = 3;
            this.txtMagnitude.Text = "10.00";
            this.txtMagnitude.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMagnitude_KeyPress);
            // 
            // txtAngle
            // 
            this.txtAngle.Location = new System.Drawing.Point(213, 122);
            this.txtAngle.Name = "txtAngle";
            this.txtAngle.Size = new System.Drawing.Size(72, 20);
            this.txtAngle.TabIndex = 4;
            this.txtAngle.Text = "0.00";
            this.txtAngle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAngle_KeyPress);
            // 
            // txtDsFromNearEnd
            // 
            this.txtDsFromNearEnd.Location = new System.Drawing.Point(213, 71);
            this.txtDsFromNearEnd.Name = "txtDsFromNearEnd";
            this.txtDsFromNearEnd.Size = new System.Drawing.Size(72, 20);
            this.txtDsFromNearEnd.TabIndex = 5;
            this.txtDsFromNearEnd.Text = "0.00";
            this.txtDsFromNearEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDsFFarEnd_KeyPress);
            // 
            // txtMbrName
            // 
            this.txtMbrName.Location = new System.Drawing.Point(213, 19);
            this.txtMbrName.Name = "txtMbrName";
            this.txtMbrName.Size = new System.Drawing.Size(72, 20);
            this.txtMbrName.TabIndex = 6;
            this.txtMbrName.Text = "1";
            // 
            // gbxLoadingInfo
            // 
            this.gbxLoadingInfo.Controls.Add(this.triangul1);
            this.gbxLoadingInfo.Controls.Add(this.triangularLoad1);
            this.gbxLoadingInfo.Controls.Add(this.label9);
            this.gbxLoadingInfo.Controls.Add(this.txtDsFromFarEnd);
            this.gbxLoadingInfo.Controls.Add(this.label8);
            this.gbxLoadingInfo.Controls.Add(this.label7);
            this.gbxLoadingInfo.Controls.Add(this.label6);
            this.gbxLoadingInfo.Controls.Add(this.label5);
            this.gbxLoadingInfo.Controls.Add(this.label4);
            this.gbxLoadingInfo.Controls.Add(this.txtMagnitude);
            this.gbxLoadingInfo.Controls.Add(this.txtMbrName);
            this.gbxLoadingInfo.Controls.Add(this.txtDsFromNearEnd);
            this.gbxLoadingInfo.Controls.Add(this.txtAngle);
            this.gbxLoadingInfo.Enabled = false;
            this.gbxLoadingInfo.Location = new System.Drawing.Point(3, 187);
            this.gbxLoadingInfo.Name = "gbxLoadingInfo";
            this.gbxLoadingInfo.Size = new System.Drawing.Size(367, 180);
            this.gbxLoadingInfo.TabIndex = 7;
            this.gbxLoadingInfo.TabStop = false;
            this.gbxLoadingInfo.Text = "Loading Information";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 158);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Direction";
            // 
            // txtDsFromFarEnd
            // 
            this.txtDsFromFarEnd.Location = new System.Drawing.Point(213, 97);
            this.txtDsFromFarEnd.Name = "txtDsFromFarEnd";
            this.txtDsFromFarEnd.Size = new System.Drawing.Size(72, 20);
            this.txtDsFromFarEnd.TabIndex = 12;
            this.txtDsFromFarEnd.Text = "0.00";
            this.txtDsFromFarEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLength_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Distance From Far End (m)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Angle of Inclination ( Degree)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Magnitude (KN/m or KN)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Member Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Distace From Near End (m)";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(102, 373);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(295, 373);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancle";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(197, 373);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // triangul1
            // 
            this.triangul1.BackColor = System.Drawing.SystemColors.Control;
            this.triangul1.LeftToRight = false;
            this.triangul1.Location = new System.Drawing.Point(263, 148);
            this.triangul1.Name = "triangul1";
            this.triangul1.Selected = false;
            this.triangul1.Size = new System.Drawing.Size(79, 23);
            this.triangul1.TabIndex = 17;
            this.triangul1.Click += new System.EventHandler(this.triangul1_Click);
            // 
            // triangularLoad1
            // 
            this.triangularLoad1.BackColor = System.Drawing.SystemColors.Control;
            this.triangularLoad1.Location = new System.Drawing.Point(159, 148);
            this.triangularLoad1.Name = "triangularLoad1";
            this.triangularLoad1.Selected = false;
            this.triangularLoad1.Size = new System.Drawing.Size(72, 23);
            this.triangularLoad1.TabIndex = 16;
            this.triangularLoad1.Click += new System.EventHandler(this.triangularLoad1_Click);
            // 
            // TrapLoad
            // 
            this.TrapLoad.BackColor = System.Drawing.SystemColors.Control;
            this.TrapLoad.Location = new System.Drawing.Point(12, 123);
            this.TrapLoad.Name = "TrapLoad";
            this.TrapLoad.Selected = false;
            this.TrapLoad.Size = new System.Drawing.Size(120, 46);
            this.TrapLoad.TabIndex = 8;
            this.TrapLoad.Click += new System.EventHandler(this.TrapLoad_Click);
            // 
            // TriangLoad
            // 
            this.TriangLoad.BackColor = System.Drawing.SystemColors.Control;
            this.TriangLoad.Location = new System.Drawing.Point(236, 41);
            this.TriangLoad.Name = "TriangLoad";
            this.TriangLoad.Selected = false;
            this.TriangLoad.Size = new System.Drawing.Size(116, 46);
            this.TriangLoad.TabIndex = 3;
            this.TriangLoad.Click += new System.EventHandler(this.TriangLoad_Click);
            // 
            // DistLoad
            // 
            this.DistLoad.BackColor = System.Drawing.SystemColors.Control;
            this.DistLoad.Location = new System.Drawing.Point(107, 41);
            this.DistLoad.Name = "DistLoad";
            this.DistLoad.Selected = false;
            this.DistLoad.Size = new System.Drawing.Size(106, 46);
            this.DistLoad.TabIndex = 2;
            this.DistLoad.Click += new System.EventHandler(this.DistLoad_Click);
            // 
            // ConsLoad
            // 
            this.ConsLoad.BackColor = System.Drawing.SystemColors.Control;
            this.ConsLoad.Location = new System.Drawing.Point(10, 41);
            this.ConsLoad.Name = "ConsLoad";
            this.ConsLoad.Selected = false;
            this.ConsLoad.Size = new System.Drawing.Size(46, 46);
            this.ConsLoad.TabIndex = 0;
            this.ConsLoad.Click += new System.EventHandler(this.ConsLoad_Click);
            // 
            // LoadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 408);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.gbxLoadingInfo);
            this.Controls.Add(this.GbxLoadTypes);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(393, 444);
            this.Name = "LoadDialog";
            this.Text = "Add Load";
            this.Load += new System.EventHandler(this.LoadDialog_Load);
            this.Click += new System.EventHandler(this.LoadDialog_Click);
            this.GbxLoadTypes.ResumeLayout(false);
            this.GbxLoadTypes.PerformLayout();
            this.gbxLoadingInfo.ResumeLayout(false);
            this.gbxLoadingInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ConcetratedLoad ConsLoad;
        private System.Windows.Forms.Label Concetrated;
        private System.Windows.Forms.GroupBox GbxLoadTypes;
        private LineLoad DistLoad;
        private TriangularLoad TriangLoad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMagnitude;
        private System.Windows.Forms.TextBox txtAngle;
        private System.Windows.Forms.TextBox txtDsFromNearEnd;
        private System.Windows.Forms.TextBox txtMbrName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gbxLoadingInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private TrapizoidalLoding TrapLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDsFromFarEnd;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label9;
        private TriangularLoad triangularLoad1;
        private triangul triangul1;







    }
}