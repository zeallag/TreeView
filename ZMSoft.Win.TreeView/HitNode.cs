using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMSoft.Win.TreeView
{
   public class HitNode
    {
        public TreeNode Node { set; get; }

        public bool IsPlusButton { set; get; }
        public HitNode(TreeNode node,bool plus)
        {
            Node = node;
            IsPlusButton = plus;
        }
    }
}
