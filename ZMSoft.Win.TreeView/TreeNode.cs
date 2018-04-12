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
    public partial class TreeNode : UserControl
    {
        public TreeView OwnerView { set; get; }
        public override string Text
        {
            set
            {
                button1.Text = value;
            }
            get
            {
                return button1.Text;
            }
        }
        private bool isExpanded;
        [DefaultValue(true)]
        public bool IsExpanded
        {
            set
            {
                isExpanded = value;
                foreach (var item in Nodes)
                {
                    item.IsExpanded = value;
                    if (isExpanded)
                        item.Visible = true;
                    else
                        item.Visible = false;
                }
            }
            get
            {
                return isExpanded;
            }
        }
        public RootNodePosition RootNodePosition
        {
            get
            {
                return OwnerView.RootNodePosition;
            }
        }
        private List<TreeNode> Nodes;
       public TreeNode ParentNode { set; get; }
       
        public TreeNode()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            Nodes = new List<TreeNode>();
            IsExpanded = true;
        }
         
        public void AddNode(TreeNode childNode)
        {
            if (Nodes == null)
                Nodes = new List<TreeNode>();
          
            childNode.OwnerView = this.OwnerView;
            childNode.ParentNode = this;
            childNode.Width = OwnerView.NodeWidth;
            childNode.Height = OwnerView.NodeHeigth;
            childNode.BackColor= button1.BackColor= OwnerView.NodeColor;
            childNode.ForeColor= button1.ForeColor = OwnerView.NodeForeColor;
            button1.FlatAppearance.BorderColor= OwnerView.NodeColor;
            childNode.button1.FlatAppearance.BorderColor = OwnerView.NodeColor;
            Nodes.Add(childNode);
        }
        public List<TreeNode> GetNodes()
        {
            if (Nodes == null)
                Nodes = new List<TreeNode>();
            return Nodes;
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            //IsExpanded = !IsExpanded;
            //this.Invalidate();
            //OwnerView.Invalidate();

        }
        public void PaintLine(Graphics g)
        {
            if (ParentNode != null&&ParentNode.IsExpanded  )
            {
                Pen pen = new Pen(Color.Black, 1);
                bool isH = (RootNodePosition == RootNodePosition.Top || RootNodePosition == RootNodePosition.Bottom);
                DrawLine(g, pen, ParentNode.Bounds, this.Bounds, isH);

            }
            if (Visible&& GetNodes().Count > 0)
            {
                DrawPlusButton(g);
            }
        }
       
        protected override void OnPaint(PaintEventArgs e)
        {
           
            PaintLine(e.Graphics);

        }
        int BUTTONSIZE = 12;
        protected void DrawLine(Graphics grf, Pen penLine, Rectangle box1, Rectangle box2, bool isHorizontal)
        {
            Point[] pts = new Point[4];
            Rectangle boxStart = box1;
            Rectangle boxEnd = box2;
            System.Drawing.Drawing2D.AdjustableArrowCap lineCap =
                new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5, true);

            if (isHorizontal)
            {
                if (boxStart.Top > boxEnd.Top)
                {
                    boxStart = box2;
                    boxEnd = box1;
                    boxEnd.Y -= BUTTONSIZE;// / 2;
                                           //  penLine.EndCap = LineCap.ArrowAnchor;
                    penLine.CustomEndCap = lineCap;
                }
                else
                {
                    boxStart.Height += BUTTONSIZE;// / 2;
                                                  // penLine.StartCap = LineCap.ArrowAnchor;
                    penLine.CustomStartCap = lineCap;
                }

                pts[0] = new Point(boxStart.X + boxStart.Width / 2, boxStart.Bottom);
                pts[1] = new Point(pts[0].X, boxStart.Bottom + (boxEnd.Top - boxStart.Bottom) / 2);
                pts[2] = new Point(boxEnd.X + boxEnd.Width / 2, pts[1].Y);
                pts[3] = new Point(pts[2].X, boxEnd.Top);
            }
            else
            {
                if (boxStart.Left > boxEnd.Left)
                {
                    boxStart = box2;
                    boxEnd = box1;
                    boxEnd.X -= BUTTONSIZE;/// 2;
                    // penLine.EndCap = LineCap.ArrowAnchor;
                    penLine.CustomEndCap = lineCap;
                }
                else
                {
                    boxStart.Width += BUTTONSIZE;// / 2;
                    // penLine.StartCap = LineCap.ArrowAnchor;
                    penLine.CustomStartCap = lineCap;
                }

                pts[0] = new Point(boxStart.Right, boxStart.Top + boxStart.Height / 2);
                pts[1] = new Point(boxStart.Right + (boxEnd.Left - boxStart.Right) / 2, pts[0].Y);
                pts[2] = new Point(pts[1].X, boxEnd.Top + boxEnd.Height / 2);
                pts[3] = new Point(boxEnd.Left, pts[2].Y);
            }
            grf.SmoothingMode = SmoothingMode.HighQuality;
            grf.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grf.CompositingQuality = CompositingQuality.HighQuality;
            grf.DrawLines(penLine, pts);


        }

        protected virtual void DrawPlusButton( Graphics grf)
        {
            Image img = Properties.Resources.PlusButton;
            int x = 0;
            if (this.IsExpanded)
            {
                x += BUTTONSIZE;
            }
            grf.DrawImage(img, GetPlusButtonBounds()
                , new Rectangle(x, 0, BUTTONSIZE, BUTTONSIZE)
                , GraphicsUnit.Pixel);
        }
       
        private Rectangle GetPlusButtonBounds()
        {
            Rectangle rec = new Rectangle(0, 0, BUTTONSIZE, BUTTONSIZE);
            if (this != null)
            {
                switch (RootNodePosition)
                {
                    case RootNodePosition.Left:
                        rec.X = this.Bounds.Right;// - BUTTONSIZE / 2;
                        rec.Y = this.Bounds.Top + (this.Bounds.Height - BUTTONSIZE) / 2;
                        break;
                    case RootNodePosition.Top:
                        rec.X = this.Bounds.Left + (this.Bounds.Width - BUTTONSIZE) / 2;
                        rec.Y = this.Bounds.Bottom;// - BUTTONSIZE/ 2;
                        break;
                    case RootNodePosition.Right:
                        rec.X = this.Bounds.Left- BUTTONSIZE;// - BUTTONSIZE / 2;
                        rec.Y = this.Bounds.Top + (this.Bounds.Height - BUTTONSIZE) / 2;
                        break;
                    case RootNodePosition.Bottom:
                        rec.X = this.Bounds.Left + (this.Bounds.Width - BUTTONSIZE) / 2;
                        rec.Y = this.Bounds.Top- BUTTONSIZE;// - BUTTONSIZE / 2;
                        break;
                }
            }
            return rec;
        }
         Label progressLabel;
        [DefaultValue(typeof(Color), "Red")]
        public Color ProgressColor { set; get; }
        private decimal progress;
        public void SetProgress(decimal pro)
        {
            if (pro == 0)
                return;
            progress = pro;
            if (progressLabel == null)
            {
                progressLabel = new Label();
                progressLabel.AutoSize = false;
                progressLabel.Height = 4;
                progressLabel.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

                this.Controls.Add(progressLabel);
            }
            progressLabel.Width = (int)(this.Width * progress / 100);
            progressLabel.BackColor = ProgressColor;
            progressLabel.Location = new Point(0,  this.Height - progressLabel.Height);
            progressLabel.BringToFront();

           
        }
        public void SetProgress()
        {
            if (progress != 0)
                SetProgress(progress);
        }

        private void button1_SizeChanged(object sender, EventArgs e)
        {
            if ((int)(button1.Height * 0.38) == 0)
                return;

            Font font = new Font(button1.Font.FontFamily, (int)(button1.Height * 0.38));  
            button1.Font = font;


        }

       
        public HitNode HitTest(int x,int y)
        {

            if (this.Bounds.Contains(x, y))
                return new HitNode(this, false);
            if (GetPlusButtonBounds().Contains(x, y))
                return new HitNode(this, true);

            if(Nodes.Count>0&& IsExpanded)
            {
                foreach (var item in Nodes)
                {
                    var hi = item.HitTest(x, y);
                    if (hi != null)
                        return hi;
                }
            }
            return null;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }
    }
}
