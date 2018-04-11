using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ZMSoft.Win.TreeView
{
    public partial class TreeView : UserControl
    {
        public TreeNode RootNode { set; get; }
        public Color NodeColor { set; get; }
        public Color NodeForeColor { set; get; }
        private float zoom;


        public RootNodePosition RootNodePosition { get; set; }
        public int Space { set; get; }
        [DefaultValue(75)]
        public int NodeWidth { set; get; }
        [DefaultValue(35)]
        public int NodeHeigth { set; get; }
        public TreeView()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            AutoScroll = true;

        }
        public void BindNode()
        {
            if (RootNode != null)
            {
                this.Controls.Clear();
                if (zoom == 0)
                    zoom = 1;
                int zoomSpace = (int)(Space * zoom);
                RootNode.Width = (int)(NodeWidth * zoom);
                RootNode.Height = (int)(NodeHeigth * zoom);
                switch (RootNodePosition)
                {
                    case RootNodePosition.Left:
                        RootNode.Location = new Point(zoomSpace, this.Height / 2);
                        break;
                    case RootNodePosition.Top:
                        RootNode.Location = new Point(this.Width / 2, zoomSpace);
                        break;
                    case RootNodePosition.Right:
                        RootNode.Location = new Point(this.Width - zoomSpace, this.Height / 2);
                        break;
                    case RootNodePosition.Bottom:
                        RootNode.Location = new Point(this.Width / 2, this.Height - zoomSpace);
                        break;
                }

                AddNode(RootNode, 0);
                DrawLines(RootNode, this.CreateGraphics());
            }
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            if (RootNode != null)
            {
                DrawLines(RootNode, e.Graphics);
            }
        }
        private void DrawLines(TreeNode node, Graphics g)
        {

            node.PaintLine(g);
            int i = 0;
            foreach (TreeNode item in node.GetNodes())
            {
                DrawLines(item, g);
                i++;
            }

        }
        public TreeNode InitRootNode(string text, RootNodePosition position)
        {
            TreeNode node = new TreeNode();
            node.Text = text;
            RootNodePosition = position;
            node.OwnerView = this;
            RootNode = node;


            return node;
        }

        private void AddNode(TreeNode node, int seq)
        {
            if (node.ParentNode != null)
            {
                if (zoom == 0)
                    zoom = 1;
                int zoomSpace = (int)(Space * zoom);
                node.Width = (int)(NodeWidth * zoom);
                node.Height = (int)(NodeHeigth * zoom);
                Point point = new Point();
                int countBrother = node.ParentNode.GetNodes().Count;
                int mod = countBrother % 2;
                int k = countBrother / 2;
                switch (RootNodePosition)
                {
                    case RootNodePosition.Left:
                        point.X = node.ParentNode.Location.X + zoomSpace + NodeWidth;
                        if (mod == 0)
                            point.Y = (int)(-k * NodeHeigth + (0.5 - k) * zoomSpace + node.ParentNode.Location.Y + (zoomSpace + NodeHeigth) * seq);
                        if (mod == 1)
                            point.Y = (int)(0.5 - k * NodeHeigth + (-k) * zoomSpace + node.ParentNode.Location.Y + (zoomSpace + NodeHeigth) * seq);
                        break;
                    case RootNodePosition.Top:

                        if (mod == 0)
                            point.X = (int)(-k * NodeWidth + (0.5 - k) * zoomSpace + node.ParentNode.Location.X + (zoomSpace + NodeWidth) * seq);
                        if (mod == 1)
                            point.X = (int)(0.5 - k * NodeWidth + (-k) * zoomSpace + node.ParentNode.Location.X + (zoomSpace + NodeWidth) * seq);
                        point.Y = node.ParentNode.Location.Y + zoomSpace + NodeHeigth;
                        break;
                    case RootNodePosition.Right:
                        point.X = node.ParentNode.Location.X - zoomSpace - NodeWidth;
                        if (mod == 0)
                            point.Y = (int)(-k * NodeHeigth + (0.5 - k) * zoomSpace + node.ParentNode.Location.Y + (zoomSpace + NodeHeigth) * seq);
                        if (mod == 1)
                            point.Y = (int)(0.5 - k * NodeHeigth + (-k) * zoomSpace + node.ParentNode.Location.Y + (zoomSpace + NodeHeigth) * seq);
                        break;
                        break;
                    case RootNodePosition.Bottom:
                        if (mod == 0)
                            point.X = (int)(-k * NodeWidth + (0.5 - k) * zoomSpace + node.ParentNode.Location.X + (zoomSpace + NodeWidth) * seq);
                        if (mod == 1)
                            point.X = (int)(0.5 - k * NodeWidth + (-k) * zoomSpace + node.ParentNode.Location.X + (zoomSpace + NodeWidth) * seq);
                        point.Y = node.ParentNode.Location.Y - zoomSpace - NodeHeigth;
                        break;
                }
                node.Location = point;// new Point(node.ParentNode.Location.X + Sapce * ( seq), node.ParentNode.Location.Y+ Sapce);
            }
            node.SetProgress();
            node.MouseDown += Node_MouseDown;
            this.Controls.Add(node);

            node.Visible = node.IsExpanded;
            int i = 0;
            foreach (TreeNode item in node.GetNodes())
            {
                AddNode(item, i);
                i++;
            }

        }

        private void Node_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        /// <summary>
        /// 放大
        /// </summary>

        public void ZoomIn()
        {
            if (zoom == 0)
                zoom = 1;

            zoom = zoom * 1.1f;
            BindNode();
        }
        /// <summary>
        /// 放大
        /// </summary>

        public void ZoomOut()
        {
            if (zoom == 0)
                zoom = 1;

            zoom = zoom * 0.9f;
            BindNode();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            var hit = RootNode.HitTest(e.X, e.Y);
            if (hit != null && hit.IsPlusButton)
            {
                hit.Node.IsExpanded = !hit.Node.IsExpanded;
                hit.Node.Invalidate();
                this.Invalidate();
            }
        }
    }
}
