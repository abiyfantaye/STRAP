using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace StracturalControls
{
    public class sMatrix
    {
        public MainForm.Stracture stracture;
        public double[,] SSMatrix;
        public double[,] NNForce;
        public double[,] NNDisp;
        public double[,] UNForce;
        public double[,] UNDisp;
        public Joint [] Joints;
        public Member[] Members;
        public mMatrixs[] MMatrix;
        public int MatrixDim;
        public maths m = new maths();
        private Member M = new Member();
        public sMatrix()
        {
        }
        public sMatrix(mMatrixs[] MSMatrixs,Joint [] joints,Member [] members ,MainForm.Stracture str)
        {
            stracture = str;
            Joints = joints;
            MakeDimension();
            MMatrix = MSMatrixs;           
            MakeSSMatrix();
            Members = members;
        }
        private void MakeDimension()
        {
            if (stracture == MainForm.Stracture.Truss | stracture == MainForm.Stracture.Beam)
            {
                MatrixDim = 2 * Joints.Length;
            }
            else if (stracture == MainForm.Stracture.Frame)
                MatrixDim = 3 * Joints.Length;
        }
        public void MakeSSMatrix()
        {
            SSMatrix = new double[MatrixDim, MatrixDim];
            foreach (mMatrixs memb in MMatrix)
            {
                for (int i = 0; i < memb.SSMIndex.Length;i++)
                {
                    for (int j = 0; j <memb.SSMIndex.Length ; j++)
                    {
                        try
                        {
                            if (stracture == MainForm.Stracture.Beam)
                                SSMatrix[memb.SSMIndex[i], memb.SSMIndex[j]] += memb.LMSMatrix[i, j];
                            else
                                SSMatrix[memb.SSMIndex[i], memb.SSMIndex[j]] += memb.GMSMatrix[i, j];
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
            }
        }
        public void MakeNNForceAndDisp()
        {
            switch (stracture)
            {
                case MainForm.Stracture.Truss:
                    NNForce = new double[2 * SupporyCounter("None") + SupporyCounter("Roler"), 1];
                    NNDisp = new double[MatrixDim - NNForce.Length, 1];
                    AddTrussNNForce();
                    break;
                case MainForm.Stracture.Beam:
                    NNForce = new double[2 * SupporyCounter("None") +  SupporyCounter("Roler") + SupporyCounter("Pin"), 1];
                    NNDisp = new double[MatrixDim - NNForce.Length, 1];
                    AddBeamNNForce();
                    break;
                case MainForm.Stracture.Frame:
                    NNForce = new double[3 * SupporyCounter("None") + 2 * SupporyCounter("Roler") + SupporyCounter("Pin"), 1];
                    NNDisp = new double[MatrixDim - NNForce.Length, 1];
                    AddFrameNNForce();
                    break;
            }         
        }
        private void AddTrussNNForce()
        {
            for (int i = 0; i < Joints.Length; i++)
            {
                switch (Joints[i].SupportType)
                {
                    case "None":
                        NNForce[Joints[i].SSMatrixIndexs[0], 0] = Joints[i].LoadX;
                        NNForce[Joints[i].SSMatrixIndexs[1], 0] = Joints[i].LoadY;
                        break;
                    case "Roler":
                        NNForce[Joints[i].SSMatrixIndexs[0], 0] = Joints[i].LoadX;
                        NNDisp[Joints[i].SSMatrixIndexs[1] - NNForce.Length, 0] = Joints[i].DispY;
                        break;
                    case "Pin":
                        NNDisp[Joints[i].SSMatrixIndexs[0] - NNForce.Length, 0] = Joints[i].DispX;
                        NNDisp[Joints[i].SSMatrixIndexs[1] - NNForce.Length, 0] = Joints[i].DispY;
                        break;
                }
            }     
        }
        private void AddBeamNNForce()
        {
            for (int i = 0; i < Joints.Length; i++)
            {
                switch (Joints[i].SupportType)
                {
                    case "None":
                        NNForce[Joints[i].SSMatrixIndexs[1], 0] = Joints[i].LoadY;
                        NNForce[Joints[i].SSMatrixIndexs[2], 0] = Joints[i].LoadZ;
                        break;
                    case "Roler":
                        NNForce[Joints[i].SSMatrixIndexs[2], 0] = Joints[i].LoadZ;
                        NNDisp[Joints[i].SSMatrixIndexs[1] - NNForce.Length, 0] = Joints[i].DispY;
                        break;
                    case "Pin":
                        NNForce[Joints[i].SSMatrixIndexs[2], 0] = Joints[i].LoadZ;
                        NNDisp[Joints[i].SSMatrixIndexs[1] - NNForce.Length, 0] = Joints[i].DispY;
                        break;
                    case "Fixed":
                        NNDisp[Joints[i].SSMatrixIndexs[1] - NNForce.Length, 0] = Joints[i].DispY;
                        NNDisp[Joints[i].SSMatrixIndexs[2] - NNForce.Length, 0] = Joints[i].DispZ;
                        break;
                }
            }     
        }
        private void AddFrameNNForce()
        {
            for (int i = 0; i < Joints.Length; i++)
            {
                switch (Joints[i].SupportType)
                {
                    case "None":
                        NNForce[Joints[i].SSMatrixIndexs[0], 0] = Joints[i].LoadX;
                        NNForce[Joints[i].SSMatrixIndexs[1], 0] = Joints[i].LoadY;
                        NNForce[Joints[i].SSMatrixIndexs[2], 0] = Joints[i].LoadZ;
                        break;
                    case "Roler":
                        NNForce[Joints[i].SSMatrixIndexs[0], 0] = Joints[i].LoadX;
                        NNForce[Joints[i].SSMatrixIndexs[2], 0] = Joints[i].LoadZ;
                        NNDisp[Joints[i].SSMatrixIndexs[1] - NNForce.Length, 0] = Joints[i].DispY;

                        break;
                    case "Pin":
                        NNForce[Joints[i].SSMatrixIndexs[2], 0] = Joints[i].LoadZ;
                        NNDisp[Joints[i].SSMatrixIndexs[0] - NNForce.Length, 0] = Joints[i].DispX;
                        NNDisp[Joints[i].SSMatrixIndexs[1] - NNForce.Length, 0] = Joints[i].DispY;
                        break;
                    case "Fixed":
                        NNDisp[Joints[i].SSMatrixIndexs[0] - NNForce.Length, 0] = Joints[i].DispX;
                        NNDisp[Joints[i].SSMatrixIndexs[1] - NNForce.Length, 0] = Joints[i].DispY;
                        NNDisp[Joints[i].SSMatrixIndexs[2] - NNForce.Length, 0] = Joints[i].DispZ;
                        break;
                }
            }     
        }
        public int SupporyCounter(string support_type)
        {
            int counter = 0;
            for (int i = 0; i < Joints.Length; i++)
            {
                if (Joints[i].SupportType == support_type)
                    counter++;
            }
            return counter;
        }
        public void FindUNForces()
        {
            double[,] K21 = m.Partation(SSMatrix, NNForce.Length, NNForce.GetLength(0), 1, 0);
            double[,] K22 = m.Partation(SSMatrix, NNForce.Length, NNForce.GetLength(0),1,1);
            UNForce = m.superpose(m.Multiply(K21, UNDisp), m.Multiply(K22,NNDisp),"a");         
        }
        public void FindUNDisp()
        {   
            double [,] K11 = m.Partation(SSMatrix,NNForce.Length,NNForce.Length, 0, 0);
            double [,] K12 = m.Partation(SSMatrix, NNForce.Length, NNForce.Length, 0, 1);
            UNDisp = m.Solution(m.Ogument(K11,m.superpose(NNForce,m.Multiply(K12,NNDisp),"s")));
        }
        public void MakeMemberDisp()
        {
            double[] Displacement = new double[SSMatrix.GetLength(0)];
            for (int i = 0; i < Displacement.Length; i++)
            {
                if (i < UNDisp.GetLength(0))
                    Displacement[i] = UNDisp[i, 0];
                else
                    Displacement[i] = NNDisp[i - UNDisp.GetLength(0), 0];
            }
            for (int i = 0; i < Members.Length; i++)
            {
                if (stracture == MainForm.Stracture.Truss)
                {
                    MMatrix[i].GMDisp = new double[4, 1];
                    for (int j = 0; j < 2; j++)
                    {
                        MMatrix[i].GMDisp[j, 0] = Displacement[Joints[M.NearEndJointIndex(Joints, Members[i])].SSMatrixIndexs[j]];
                        MMatrix[i].GMDisp[j + 2, 0] = Displacement[Joints[M.FarEndJointIndex(Joints, Members[i])].SSMatrixIndexs[j]];
                    }
                    MMatrix[i].LMDisp = m.Multiply(MMatrix[i].DTMatrix, MMatrix[i].GMDisp);
                }
                else if (stracture == MainForm.Stracture.Beam)
                {
                    MMatrix[i].GMDisp = new double[4, 1];
                    for (int j = 0; j < 2; j++)
                    {
                        MMatrix[i].GMDisp[j, 0] = Displacement[Joints[M.NearEndJointIndex(Joints, Members[i])].SSMatrixIndexs[j + 1]];
                        MMatrix[i].GMDisp[j + 2, 0] = Displacement[Joints[M.FarEndJointIndex(Joints, Members[i])].SSMatrixIndexs[j + 1]];
                    }
                }
                else if (stracture == MainForm.Stracture.Frame)
                {
                    MMatrix[i].GMDisp = new double[6, 1];
                    for (int j = 0; j < 3; j++)
                    {
                        MMatrix[i].GMDisp[j, 0] = Displacement[Joints[M.NearEndJointIndex(Joints, Members[i])].SSMatrixIndexs[j]];
                        MMatrix[i].GMDisp[j + 3, 0] = Displacement[Joints[M.FarEndJointIndex(Joints, Members[i])].SSMatrixIndexs[j]];
                    }
                    MMatrix[i].LMDisp = m.Multiply(MMatrix[i].DTMatrix, MMatrix[i].GMDisp);
                }
            }
        }
        public void MakeMemberForces()
        {
            for (int i = 0; i < Members.Length; i++)
            {
                if (stracture == MainForm.Stracture.Truss)
                {
                    MMatrix[i].LMForce = m.Multiply(MMatrix[i].LMSMatrix, MMatrix[i].LMDisp);
                    MMatrix[i].GMForce = m.Multiply(MMatrix[i].FTMatrix, MMatrix[i].LMForce);
                }
                else if (stracture == MainForm.Stracture.Beam)
                {
                    MMatrix[i].LMForce = m.Multiply(MMatrix[i].LMSMatrix, MMatrix[i].GMDisp);
                    MMatrix[i].LMForce[0, 0] += Members[i].NForce;
                    MMatrix[i].LMForce[1, 0] -= Members[i].NFEM;
                    MMatrix[i].LMForce[2, 0] += Members[i].FForce;
                    MMatrix[i].LMForce[3, 0] -= Members[i].FFEM;
                }
                else if (stracture == MainForm.Stracture.Frame)
                {
                    MMatrix[i].LMForce = m.Multiply(MMatrix[i].LMSMatrix, MMatrix[i].LMDisp);
                    MMatrix[i].LMForce[1, 0] += Members[i].NForce;
                    MMatrix[i].LMForce[2, 0] -= Members[i].NFEM;
                    MMatrix[i].LMForce[4, 0] += Members[i].FForce;
                    MMatrix[i].LMForce[5, 0] -= Members[i].FFEM;
                    MMatrix[i].GMForce = m.Multiply(MMatrix[i].FTMatrix, MMatrix[i].LMForce);
                }
            }
        }
    }
}
