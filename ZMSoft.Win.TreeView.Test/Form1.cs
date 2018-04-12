using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZMSoft.Win.TreeView.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //
        }

        private void treeView1_Load(object sender, EventArgs e)
        {
            //TreeView treeView1 = new TreeView();
            treeView1.Space = 100;
            treeView1.NodeWidth = 75;
            treeView1.NodeHeigth = 35;
            treeView1.NodeColor = Color.FromArgb(0, 144, 255);
            treeView1.NodeForeColor = Color.Red;
            TreeNode node = treeView1.InitRootNode("Root", RootNodePosition.Left);//创建根节点
           
            TreeNode A = new TreeNode();
            A.Text = "A";

            TreeNode B = new TreeNode();
            B.Text = "B";


            TreeNode C = new TreeNode();
            C.Text = "C";

            TreeNode C1 = new TreeNode();
            C1.Text = "C1";
            TreeNode C2 = new TreeNode();
            C2.Text = "C2";

            TreeNode B1 = new TreeNode();
            B1.Text = "B1";
           
            B1.ProgressColor = Color.Red;
            B1.SetProgress(30);
            node.AddNode(A);
            node.AddNode(B);
            node.AddNode(C);
            B.AddNode(B1);
            C.AddNode(C1);
            C.AddNode(C2);
            treeView1.BindNode();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.RootNodePosition = RootNodePosition.Top;
            treeView1.BindNode();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.RootNodePosition = RootNodePosition.Left;
            treeView1.BindNode();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            treeView1.RootNodePosition = RootNodePosition.Right;
            treeView1.BindNode();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            treeView1.RootNodePosition = RootNodePosition.Bottom;
            treeView1.BindNode();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            treeView1.ZoomIn();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            treeView1.ZoomOut();
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            
                e.Effect = DragDropEffects.Move;
            
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            var obj =  e.Data.GetData(typeof(TreeNode));//将拖来的数据转化为数组存储 
        }
    }
}
