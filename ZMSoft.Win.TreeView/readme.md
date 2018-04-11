# ZMSoft.Win.TreeView
> 树形控件,用来展示一个树状图形，数据结构类似TreeView。
## 使用方法

``` c#

            TreeView treeView1 = new TreeView();
            treeView1.Space = 100;
            treeView1.NodeWidth = 75;
            treeView1.NodeHeigth = 35;
            treeView1.NodeColor = Color.Gray;
            treeView1.NodeForeColor = Color.Red;
            TreeNode node = treeView1.InitRootNode("Root", RootNodePosition.Left);//创建根节点
           
            TreeNode A = new TreeNode();
            A.Text = "A";

            TreeNode B = new TreeNode();
            B.Text = "B";


            TreeNode C = new TreeNode();
            C.Text = "C";

            TreeNode B1 = new TreeNode();
            B1.Text = "B1";
           
            B1.ProgressColor = Color.Red;
            B1.SetProgress(30);
            node.AddNode(A);
            node.AddNode(B);
            node.AddNode(C);
            B.AddNode(B1);
           
            treeView1.BindNode();

```
```c#
            treeView1.RootNodePosition = RootNodePosition.Top;//根节点在上
            treeView1.BindNode();

            treeView1.RootNodePosition = RootNodePosition.Left;//根节点在左
            treeView1.BindNode();

            treeView1.RootNodePosition = RootNodePosition.Right;//根节点在右
            treeView1.BindNode();

            treeView1.RootNodePosition = RootNodePosition.Bottom;//根节点在下
            treeView1.BindNode();

             treeView1.ZoomIn();//放大
             treeView1.ZoomOut();//缩小

```
