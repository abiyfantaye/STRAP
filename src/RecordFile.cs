using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Collections;

namespace StracturalControls
{
    [Serializable]
    class RecordFile
    {
        private MainForm.Stracture stracture;
        private ArrayList points = new ArrayList();
        private ArrayList endl = new ArrayList();
        private ArrayList jointload = new ArrayList();
        private ArrayList supportgraphics = new ArrayList();
        private ArrayList loadgraphics = new ArrayList();
        private LableGraphics lg = new LableGraphics();
        private PointF reference;
        private Member[] members;
        private Member[] adjMember;
        private Joint[] joints;
        private Point[] pt;
        private double[,] displacements;
        private double[] constants;
        public RecordFile()
        {
        }
        public RecordFile(MainForm.Stracture str, ArrayList Points, ArrayList Endl, ArrayList JointLoad, ArrayList SupportGraphics,ArrayList LoadGraphics)
        {
            str = stracture;
            points = Points;
            endl = Endl;
            jointload = JointLoad;
            supportgraphics = SupportGraphics;
            loadgraphics = LoadGraphics;
        }
        public RecordFile(Member[] Members, Member[] AdjMembers, Joint[] Joints, Point[] Pt, double[,] Displacements, double[] Constants, LableGraphics LG)
        {
            members = Members;
            adjMember = AdjMembers;
            joints = Joints;
            pt = Pt;
            displacements = Displacements;
            constants = Constants;
            lg = LG;
        }
        public MainForm.Stracture Stracture
        {
            get { return stracture; }
            set { stracture = value; }
        }
        public ArrayList Points
        {
            get { return points; }
            set { points = value; }
        }
        public ArrayList Endl
        {
            get { return endl; }
            set { endl = value; }
        }
        public ArrayList JointLoad
        {
            get { return jointload; }
            set { jointload = value; }
        }
        public ArrayList SupportGrapics
        {
            get { return supportgraphics; }
            set { supportgraphics = value; }
        }
        public ArrayList LoadGraphics
        {
            get { return loadgraphics; }
            set { loadgraphics = value; }
        }
        public Member[] Members
        {
            get { return members; }
            set { members = value; }
        }
        public Member[] AdjMembers
        {
            get { return adjMember; }
            set { adjMember = value; }
        }
        public Joint[] Joints
        {
            get { return joints; }
            set { joints = value; }
        }
        public Point[] Pt
        {
            get { return pt; }
            set { pt = value; }
        }
        public double[,] Displacements
        {
            get { return displacements; }
            set { displacements = value; }
        }
        public double[] Constants
        {
            get { return constants; }
            set { constants = value; }
        }
        public LableGraphics LG
        {
            get { return lg; }
            set { lg = value; }
        }
        public PointF Refference
        {
            get { return reference; }
            set { reference = value; }
        }
    }
}
