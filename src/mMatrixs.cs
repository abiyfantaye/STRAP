using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Media;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;


namespace StracturalControls
{
    public class mMatrixs
    {
        public MainForm.Stracture stracture;
        public double[,] LMSMatrix;// stands for local member stiffeness matrix
        public double[,] GMSMatrix;// stands for global member stiffness matrix
        public double[,] DTMatrix;// stands for displacement transform matrix from globa to  local
        public double[,] FTMatrix;// stands for force transform matrix from local to global
        public double[,] GMForce;// stands for Global Member Force
        public double[,] LMForce;// stands for Loacal Member Force
        public double[,] LMDisp; // stands for Local Member Displacement
        public double[,] GMDisp;// stands for Global Member Displacement
        public int[] SSMIndex ;// stands for stractural stiffness matrix indexs
        public Joint[] Joints;
        public Member _member;
        public ArrayList SupportList = new ArrayList();
        maths m = new maths();
        public mMatrixs()
        {
        }
        public mMatrixs(Member member,Joint [] joints,MainForm.Stracture str,ArrayList supportgraphics)
        {
            stracture = str;
            Joints = joints; 
            _member = member;
            SupportList = supportgraphics;
            MakeLMSMatrix(member.Length,member.Area,member.MI,member.ME);
            MakeDTMatrix(member.Angle);
            MakeFTMatrix(member.Angle);
            MakeGMSMatrix();               
            MakeSSMIndex();
        }
        public void MakeLMSMatrix(double L, double A, double I, double E)
        {
            A = 0.001*A;
            I = 0.001*I;
            if (stracture == MainForm.Stracture.Truss)
            {
                LMSMatrix = new double[2, 2] { { A * E / L, -A * E / L }, { -A * E / L, A * E / L } };
            }
            else if (stracture == MainForm.Stracture.Beam)
            {
                LMSMatrix = new double[4, 4] { { 12 * E * I / (L * L * L), 6 * E * I / (L * L) ,-12 * E * I / (L * L * L),6 * E * I / (L * L)} ,
                                       { 6 * E * I / (L * L), 4 * E * I / L  ,- 6 * E * I / (L * L), 2 * E * I / L },
                                       { - 12 * E * I / (L * L * L), - 6 * E * I / (L * L),12 * E * I / (L * L * L),-6 * E * I / (L * L)},
                                       {6 * E * I / (L * L), 2 * E * I / L , - 6 * E * I / (L * L) ,4 * E * I / L } };
            }
            else if (stracture == MainForm.Stracture.Frame)
            {
                LMSMatrix = new double[6, 6] {{A*E/L,0,0,-A*E/L,0,0},
                                    {0,12*E*I/(L*L*L),6*E*I/(L*L),0,-12*E*I/(L*L*L),6*E*I/(L*L)},
                                    {0,6*E*I/(L*L),4*E*I/L,0,-6*E*I/(L*L),2*E*I/L},
                                    {-A*E/L,0,0,A*E/L,0,0},
                                    {0,-12*E*I/(L*L*L),-6*E*I/(L*L),0,12*E*I/(L*L*L),-6*E*I/(L*L)},
                                    {0,6*E*I/(L*L),2*E*I/L,0,-6*E*I/(L*L),4*E*I/L}};
            }
        }
        public void MakeGMSMatrix()
        {
            if (stracture != MainForm.Stracture.Beam)
            GMSMatrix =  m.Multiply(m.Multiply(FTMatrix, LMSMatrix), DTMatrix);
        }
        public void MakeDTMatrix(double θ)
        {
            double lx  = Math.Cos(m.DR(θ));
            double ly =  Math.Sin(m.DR(θ));
            if (stracture == MainForm.Stracture.Truss)
            {
                if (Joints[NearEndJointIndex()].SupportType == "Roler")
                {
                    double lxP = Math.Cos(m.DR(θ+ Convert.ToDouble(SupportList[SupportList.IndexOf(Joints[NearEndJointIndex()].CleintCoordinate)+1])));
                    double lyP = Math.Sin(m.DR(θ+ Convert.ToDouble(SupportList[SupportList.IndexOf(Joints[NearEndJointIndex()].CleintCoordinate)+1])));
                    DTMatrix = new double[2, 4] { { lxP, lyP,0, 0 }, {0, 0 ,lx, ly  } };
                    return;
                }
                else if (Joints[FarEndJointIndex()].SupportType == "Roler")
                {
                    double lxP = Math.Cos(m.DR(θ + Convert.ToDouble(SupportList[SupportList.IndexOf(Joints[FarEndJointIndex()].CleintCoordinate) + 1])));
                    double lyP = Math.Sin(m.DR(θ + Convert.ToDouble(SupportList[SupportList.IndexOf(Joints[FarEndJointIndex()].CleintCoordinate) + 1])));
                    DTMatrix = new double[2, 4] { { lx, ly, 0, 0 }, { 0, 0, lxP, lyP } };
                    return;
                }
                else
                {
                    DTMatrix = new double[2, 4] { { lx, ly, 0, 0 }, { 0, 0, lx, ly } };
                    return;
                }
            }
            else if (stracture == MainForm.Stracture.Beam)
            {
                return; // no transformation matix is needed for continious beams
            }
            else if (stracture == MainForm.Stracture.Frame)
            {
                if (Joints[NearEndJointIndex()].SupportType == "Roler")
                {
                    double lxP = Math.Cos(m.DR(θ + Convert.ToDouble(SupportList[SupportList.IndexOf(Joints[NearEndJointIndex()].CleintCoordinate) + 1])));
                    double lyP = Math.Sin(m.DR(θ + Convert.ToDouble(SupportList[SupportList.IndexOf(Joints[NearEndJointIndex()].CleintCoordinate) + 1])));
                    DTMatrix = new double[6, 6] { { lxP, lyP, 0, 0, 0, 0 }, { -lyP, lxP, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0, 0 }, { 0, 0, 0, lx, ly, 0 }, { 0, 0, 0, -ly, lx, 0 }, { 0, 0, 0, 0, 0, 1 } };
                    return;
                }
                else if (Joints[FarEndJointIndex()].SupportType == "Roler")
                {
                    double lxP = Math.Cos(m.DR(θ + Convert.ToDouble(SupportList[SupportList.IndexOf(Joints[FarEndJointIndex()].CleintCoordinate) + 1])));
                    double lyP = Math.Sin(m.DR(θ + Convert.ToDouble(SupportList[SupportList.IndexOf(Joints[FarEndJointIndex()].CleintCoordinate) + 1])));
                    DTMatrix = new double[6, 6] { { lx, ly, 0, 0, 0, 0 }, { -ly, lx, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0, 0 }, { 0, 0, 0, lxP, lyP, 0 }, { 0, 0, 0, -lyP, lxP, 0 }, { 0, 0, 0, 0, 0, 1 } };
                    return;
                }
                else
                {
                    DTMatrix = new double[6, 6] { { lx, ly, 0, 0, 0, 0 }, { -ly, lx, 0, 0, 0, 0 }, { 0, 0, 1, 0, 0, 0 }, { 0, 0, 0, lx, ly, 0 }, { 0, 0, 0, -ly, lx, 0 }, { 0, 0, 0, 0, 0, 1 } };
                    return;
                }
            }         
        }
        public void MakeFTMatrix(double θ)
        {
            if (stracture != MainForm.Stracture.Beam)
                FTMatrix = m.Transpose(DTMatrix);
        }
        public void MakeSSMIndex()
        {
            JointOrder();
            try
            {
                int[] NEIndx = Joints[NearEndJointIndex()].SSMatrixIndexs;
                int[] FEIndx = Joints[FarEndJointIndex()].SSMatrixIndexs;
                switch (stracture)
                {
                    case MainForm.Stracture.Truss:
                        SSMIndex[0] = NEIndx[0];
                        SSMIndex[1] = NEIndx[1];
                        SSMIndex[2] = FEIndx[0];
                        SSMIndex[3] = FEIndx[1];
                        break;
                    case MainForm.Stracture.Beam:
                        SSMIndex[0] = NEIndx[1];
                        SSMIndex[1] = NEIndx[2];
                        SSMIndex[2] = FEIndx[1];
                        SSMIndex[3] = FEIndx[2];
                        break;
                    case MainForm.Stracture.Frame:
                        for (int i = 0; i < 3; i++)
                        {
                            SSMIndex[i] = NEIndx[i];
                            SSMIndex[i + 3] = FEIndx[i];
                        }
                        break;
                }
            }
            catch (Exception)
            {
            }
        }
        public void JointOrder()
        {   
            ArrayList None = new ArrayList();
            ArrayList Rollers = new ArrayList();
            ArrayList Pins = new ArrayList();
            ArrayList Fixed = new ArrayList();
            for (int i = 0; i < Joints.Length; i++)
            {
                switch(Joints[i].SupportType)
                {
                    case "None":
                     None.Add(i);
                     break;               
                    case "Roler":
                     Rollers.Add(i);
                        break;
                    case "Pin":
                        Pins.Add(i);
                        break;
                    case "Fixed":
                        Fixed.Add(i);
                      break;
                }
            } 
            switch (stracture)
            {
                case MainForm.Stracture.Truss:
                    TrussJointOrder(None,Rollers,Pins);
                    SSMIndex = new int[4];
                    break;
                case MainForm.Stracture.Beam:
                    BeamJointOrder(None, Rollers, Pins, Fixed);
                    SSMIndex = new int[4];
                    break;
                case MainForm.Stracture.Frame:
                    FrameJointOrder(None, Rollers, Pins, Fixed);
                    SSMIndex = new int[6];
                    break;
            }
        }
        private void TrussJointOrder(ArrayList None,ArrayList Rollers,ArrayList Pins)
        {
            int PinCounter = 2 * (None.Count + Rollers.Count);
            for (int i = 0; i < Joints.Length; i++)
            {
                if (i < None.Count)
                {
                    Joints[(int)None[i]].SSMatrixIndexs = new int[3] { 2 * i, 2 * i + 1,-1};
                }
                if (i < Rollers.Count)
                {
                    Joints[(int)Rollers[i]].SSMatrixIndexs[0] =  2 * None.Count+i;
                    Joints[(int)Rollers[i]].SSMatrixIndexs[1] = 2 * None.Count +  Rollers.Count + i;
                    Joints[(int)Rollers[i]].SSMatrixIndexs[2] = -1;
                }
                if (i < Pins.Count)
                {
                    Joints[(int)Pins[i]].SSMatrixIndexs[0] = PinCounter;
                    Joints[(int)Pins[i]].SSMatrixIndexs[1] = PinCounter + 1;
                    Joints[(int)Pins[i]].SSMatrixIndexs[2] = -1;
                    PinCounter += 2;
                }
            }
        }
        private void BeamJointOrder(ArrayList None,ArrayList Rollers,ArrayList Pins ,ArrayList Fixed)
        {
            for (int i = 0; i < Joints.Length; i++)
            {
                if (i < None.Count)
                {
                    Joints[(int)None[i]].SSMatrixIndexs = new int[3] { -1, 2 * i + 1, 2 * i };
                }
                if (i < Rollers.Count)
                {
                    Joints[(int)Rollers[i]].SSMatrixIndexs[0] = -1;
                    Joints[(int)Rollers[i]].SSMatrixIndexs[1] = 2 * None.Count + Rollers.Count+Pins.Count+i;
                    Joints[(int)Rollers[i]].SSMatrixIndexs[2] = 2 * None.Count + i;
                }
                if (i < Pins.Count)
                {
                    Joints[(int)Pins[i]].SSMatrixIndexs[0] = -1;
                    Joints[(int)Pins[i]].SSMatrixIndexs[1] = 2 * (None.Count + Rollers.Count)+Pins.Count+i;
                    Joints[(int)Pins[i]].SSMatrixIndexs[2] = 2 * None.Count + Rollers.Count+i;
                }
                if (i < Fixed.Count)
                {
                    int index = 2* (Joints.Length - Fixed.Count + i);
                    Joints[(int)Fixed[i]].SSMatrixIndexs = new int[3] { -1,index, index + 1};
                }
            }
        }
        private void FrameJointOrder(ArrayList None, ArrayList Rollers, ArrayList Pins, ArrayList Fixed)
        {
            int RolCounter = 3 * None.Count;
            int PinCounter = 3 * (None.Count + Rollers.Count) + Pins.Count;
            for (int i = 0; i < Joints.Length; i++)
            {
                if (i < None.Count)
                {
                    Joints[(int)None[i]].SSMatrixIndexs = new int[3] { 3 * i, 3 * i + 1, 3 * i + 2 };
                }
                if (i < Rollers.Count)
                {
                    Joints[(int)Rollers[i]].SSMatrixIndexs[0] = RolCounter;
                    Joints[(int)Rollers[i]].SSMatrixIndexs[1] = 3 * None.Count + 2 * Rollers.Count + Pins.Count + i;
                    Joints[(int)Rollers[i]].SSMatrixIndexs[2] = RolCounter + 1;
                    RolCounter += 2;
                }
                if (i < Pins.Count)
                {
                    Joints[(int)Pins[i]].SSMatrixIndexs[0] = PinCounter;
                    Joints[(int)Pins[i]].SSMatrixIndexs[1] = PinCounter + 1;
                    Joints[(int)Pins[i]].SSMatrixIndexs[2] = 3 * None.Count + 2 * Rollers.Count + i;
                    PinCounter += 2;
                }
                if (i < Fixed.Count)
                {
                    int index = 3 * (Joints.Length - Fixed.Count + i);
                    Joints[(int)Fixed[i]].SSMatrixIndexs = new int[3] { index, index + 1, index + 2 };
                }
            }
        }
        public int NearEndJointIndex()
        { 
            int Index = -1;
            for (int i = 0; i < Joints.Length; i++)
            {
                if (Joints[i].CleintCoordinate == _member.NECDNT)
                {
                    Index = i;
                    break;
                }       
            }
            return Index;
        }
        public int FarEndJointIndex()
        {
            int Index = -1;
            for (int i = 0; i < Joints.Length; i++)
            {
                if (Joints[i].CleintCoordinate == _member.FECDNT)
                {
                    Index = i;
                    break;
                }
            }
            return Index;
        }
    }
}
