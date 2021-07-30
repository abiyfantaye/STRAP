using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace StracturalControls
{
    public partial class LoadSummary : Form
    {
        public bool IsJointLoadSummary = false;
        public LoadSummary()
        {
            InitializeComponent();
        }
        public ArrayList LoadInfo = new ArrayList();
        public ArrayList LoadingInfo
        {
            get { return LoadInfo; }
            set { LoadInfo = value; }
        }
        public ArrayList JointLoad = new ArrayList();
        public bool savechanges = false;
        public bool IsSI;
        public bool SaveChanges
        {
            get { return savechanges; }
            set { savechanges = value; }
        }
        LoadDialog LD = new LoadDialog();
        private void LoadSummary_Load(object sender, EventArgs e)
        {
            if (IsJointLoadSummary)
                tabControl1.SelectedIndex = 1;
            else tabControl1.SelectedIndex = 0;
            try
            {
                DgvLoadSummary.RowCount = (int)(LoadInfo.Count / 7);
            }
            catch (Exception)
            {
            }
            for (int i = 0; i < LoadInfo.Count; i += 7)
            {
                int k = i / 7;
                for (int j = 0; j < 7; j++)
                {
                    DgvLoadSummary[j + 1, k].Value = LoadInfo[i + j];
                    DgvLoadSummary[0, k].Value = k + 1;
                }
            }
            try
            {
                dgvJointLoad.RowCount = (int)(JointLoad.Count / 4);
            }
            catch (Exception)
            {
            }
            for (int i = 0; i < JointLoad.Count; i += 4)
            {
                int k = i / 4;
                dgvJointLoad[0, k].Value = k + 1;
                dgvJointLoad[1, k].Value = JointLoad[i].ToString();
                dgvJointLoad[2, k].Value = JointLoad[i + 1].ToString();
                dgvJointLoad[3, k].Value = JointLoad[i + 2].ToString();
                dgvJointLoad[4, k].Value = JointLoad[i + 3].ToString();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tabPage2_Enter(object sender, EventArgs e)
        {
            try
            {
                dgvJointLoad.RowCount = (int)(JointLoad.Count / 4);
            }
            catch (Exception)
            {
            }
            for (int i = 0; i < JointLoad.Count; i += 4)
            {
                int k = i / 4;
                dgvJointLoad[0, k].Value = k + 1;
                dgvJointLoad[1, k].Value = JointLoad[i].ToString();
                dgvJointLoad[2, k].Value = JointLoad[i + 1].ToString();
                dgvJointLoad[3, k].Value = JointLoad[i + 2].ToString();
                dgvJointLoad[4, k].Value = JointLoad[i + 3].ToString();
            }
        }
        private void dgvJointLoad_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (this.JointLoad.Count != 0)
                savechanges = true;
            this.JointLoad = new ArrayList();
            for (int j = 0; j < dgvJointLoad.RowCount; j++)
            {
                for (int i = 1; i < dgvJointLoad.ColumnCount; i++)
                {
                    this.JointLoad.Add(dgvJointLoad[i, j].Value);
                }
            }
        }
        private void DgvLoadSummary_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (this.LoadInfo.Count != 0)
                savechanges = true;
            this.LoadingInfo = new ArrayList();
            for (int j = 0; j < DgvLoadSummary.RowCount; j++)
            {
                for (int i = 1; i < DgvLoadSummary.ColumnCount; i++)
                {
                    this.LoadingInfo.Add(DgvLoadSummary[i, j].Value);
                }
            }
        }
    }
}
