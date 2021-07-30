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
    public partial class Solution : Form
    {
        
        public Solution()
        {
           InitializeComponent();
        }
        public MainForm.Stracture stracture;
        public Member[] Members;
        public mMatrixs[]mMatrix;
        private void Solution_Load(object sender, EventArgs e)
        {
            MakeJointDisp();
            dgvFrameLocForce.RowCount = mMatrix.Length;
            dgvFramGlobForce.RowCount = mMatrix.Length;
            dgvFrameJointDisp.RowCount = mMatrix[0].Joints.Length;
            if (stracture == MainForm.Stracture.Truss)
            {
                dgvFrameJointDisp.Visible = dgvFrameLocForce.Visible = dgvFramGlobForce.Visible = dgvFramMemberDisp.Visible = false;
                dgvBeamJointdisp.Visible = dgvBeamLocCoord.Visible = dgvBeamMemberDisp.Visible = false;
                dgvTrussGlobForce.Visible = dgvTrussJointdisp.Visible = dgvTrussLocForce.Visible = dgvTrussMemberDip.Visible = true;
                MakeTrussInviroment();
            }
            else if (stracture == MainForm.Stracture.Beam)
            {
                dgvBeamJointdisp.Visible = dgvBeamLocCoord.Visible = dgvBeamMemberDisp.Visible = true;
                dgvTrussGlobForce.Visible = dgvTrussJointdisp.Visible = dgvTrussLocForce.Visible = dgvTrussMemberDip.Visible = false;
                dgvFrameJointDisp.Visible = dgvFrameLocForce.Visible = dgvFramGlobForce.Visible = dgvFramMemberDisp.Visible = false;
                MakeBeamInviroment();
            }
            else if (stracture == MainForm.Stracture.Frame)
            {
                dgvTrussGlobForce.Visible = dgvTrussJointdisp.Visible = dgvTrussLocForce.Visible = dgvTrussMemberDip.Visible = false;
                dgvBeamJointdisp.Visible = dgvBeamLocCoord.Visible = dgvBeamMemberDisp.Visible = false;
                dgvFrameJointDisp.Visible = dgvFrameLocForce.Visible = dgvFramGlobForce.Visible = dgvFramMemberDisp.Visible = true;
                MakeFrameInviroment();
            }          
        }
        private void MakeJointDisp()
        {
            for (int i = 0; i < Members.Length; i++)
            {
                if (stracture == MainForm.Stracture.Truss)
                {
                    mMatrix[0].Joints[NearEndJointIndex(Members[i])].DispX = mMatrix[i].GMDisp[0, 0];
                    mMatrix[0].Joints[FarEndJointIndex(Members[i])].DispX = mMatrix[i].GMDisp[2, 0];
                    mMatrix[0].Joints[NearEndJointIndex(Members[i])].DispY = mMatrix[i].GMDisp[1, 0];
                    mMatrix[0].Joints[FarEndJointIndex(Members[i])].DispY = mMatrix[i].GMDisp[3, 0];
                }
                else if (stracture == MainForm.Stracture.Beam)
                {
                    mMatrix[0].Joints[NearEndJointIndex(Members[i])].DispY = mMatrix[i].GMDisp[0, 0];
                    mMatrix[0].Joints[FarEndJointIndex(Members[i])].DispY = mMatrix[i].GMDisp[2, 0];
                    mMatrix[0].Joints[FarEndJointIndex(Members[i])].DispZ = mMatrix[i].GMDisp[3, 0];
                    mMatrix[0].Joints[NearEndJointIndex(Members[i])].DispZ = mMatrix[i].GMDisp[1, 0];
                }
                else if (stracture == MainForm.Stracture.Frame)
                {
                    mMatrix[0].Joints[NearEndJointIndex(Members[i])].DispX = mMatrix[i].GMDisp[0, 0];
                    mMatrix[0].Joints[FarEndJointIndex(Members[i])].DispX = mMatrix[i].GMDisp[3, 0];
                    mMatrix[0].Joints[NearEndJointIndex(Members[i])].DispY = mMatrix[i].GMDisp[1, 0];
                    mMatrix[0].Joints[FarEndJointIndex(Members[i])].DispY = mMatrix[i].GMDisp[4, 0];
                    mMatrix[0].Joints[FarEndJointIndex(Members[i])].DispZ = mMatrix[i].GMDisp[5, 0];
                    mMatrix[0].Joints[NearEndJointIndex(Members[i])].DispZ = mMatrix[i].GMDisp[2, 0];
                }
            }
        }
        public bool IsSI;
        private void MakeTrussInviroment()
        {
            dgvTrussGlobForce.RowCount = dgvTrussLocForce.RowCount = dgvTrussMemberDip.RowCount =  Members.Length;
            dgvTrussJointdisp.RowCount = mMatrix[0].Joints.Length;
            for (int i = 0; i < Members.Length; i++)
            {
                dgvTrussGlobForce[0, i].Value = dgvTrussLocForce[0, i].Value = dgvTrussMemberDip[0, i].Value = Members[i].Name;
                dgvTrussLocForce[1, i].Value = Math.Round(mMatrix[i].LMForce[1, 0], 10);
                for (int j = 0; j < 4; j++)
                {
                    dgvTrussGlobForce[j + 1, i].Value = Math.Round(mMatrix[i].GMForce[j, 0], 10);
                    dgvTrussMemberDip[j + 1, i].Value = Math.Round(mMatrix[i].GMDisp[j, 0], 10);
                }
            }
            for (int i = 0; i < mMatrix[0].Joints.Length; i++)
            {
                dgvTrussJointdisp[0, i].Value = mMatrix[0].Joints[i].Name;
                dgvTrussJointdisp[1, i].Value = Math.Round(mMatrix[0].Joints[i].DispX,10);
                dgvTrussJointdisp[2, i].Value = Math.Round(mMatrix[0].Joints[i].DispY,10);
            }

        }
        private void MakeBeamInviroment()
        {
            dgvBeamLocCoord.RowCount = dgvBeamMemberDisp.RowCount =  Members.Length;
            dgvBeamJointdisp.RowCount = mMatrix[0].Joints.Length;
            for (int i = 0; i < Members.Length; i++)
            {
                dgvBeamLocCoord[0, i].Value = dgvBeamMemberDisp[0, i].Value =  Members[i].Name;               
                for (int j = 0; j < 4; j++)
                {
                    dgvBeamLocCoord[j + 1, i].Value = Math.Round(mMatrix[i].LMForce[j, 0],10);
                    dgvBeamMemberDisp[j + 1, i].Value = Math.Round(mMatrix[i].GMDisp[j, 0], 10);
                }
            }
            for (int i = 0; i < mMatrix[0].Joints.Length; i++)
            {
                dgvBeamJointdisp[0, i].Value = mMatrix[0].Joints[i].Name;
                dgvBeamJointdisp[1, i].Value = Math.Round(mMatrix[0].Joints[i].DispY,10);
                dgvBeamJointdisp[2, i].Value = Math.Round(mMatrix[0].Joints[i].DispZ,10);
            }
        }
        private void MakeFrameInviroment()
        {
            dgvFrameLocForce.RowCount = dgvFramGlobForce.RowCount = dgvFramMemberDisp.RowCount = Members.Length;
            dgvFrameJointDisp.RowCount = mMatrix[0].Joints.Length;
            for (int i = 0; i < Members.Length; i++)
            {
                dgvFrameLocForce[0, i].Value = dgvFramGlobForce[0, i].Value = dgvFramMemberDisp[0, i].Value = Members[i].Name;
                for (int j = 1; j < 7; j++)
                {
                    dgvFrameLocForce[j, i].Value = Math.Round(mMatrix[i].LMForce[j - 1, 0], 10);
                    dgvFramGlobForce[j, i].Value = Math.Round(mMatrix[i].GMForce[j - 1, 0], 10);
                    dgvFramMemberDisp[j, i].Value = Math.Round(mMatrix[i].GMDisp[j-1, 0], 10);
                }
            }
            for (int i = 0; i < mMatrix[0].Joints.Length; i++)
            {
                dgvFrameJointDisp[0, i].Value = mMatrix[0].Joints[i].Name;
                dgvFrameJointDisp[1, i].Value = Math.Round(mMatrix[0].Joints[i].DispX,10);
                dgvFrameJointDisp[2, i].Value = Math.Round(mMatrix[0].Joints[i].DispY,10);
                dgvFrameJointDisp[3, i].Value = Math.Round(mMatrix[0].Joints[i].DispZ,10);
            }
        }
        public int NearEndJointIndex(Member _member)
        {
            int Index = -1;
            for (int i = 0; i < mMatrix[0].Joints.Length; i++)
            {
                if (mMatrix[0].Joints[i].CleintCoordinate == _member.NECDNT)
                {
                    Index = i;
                    break;
                }
            }
            return Index;
        }
        public int FarEndJointIndex(Member _member)
        {
            int Index = -1;
            for (int i = 0; i < mMatrix[0].Joints.Length; i++)
            {
                if (mMatrix[0].Joints[i].CleintCoordinate == _member.FECDNT)
                {
                    Index = i;
                    break;
                }
            }
            return Index;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
