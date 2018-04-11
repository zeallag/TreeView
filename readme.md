 
# ZMSoft.Win.TreeView
> 树状图控件,用来展示一个树状图形，数据结构类似TreeView。在企业信息系统里经常会用到，比如公司组织架构，产品BOM"。</br>
代码托管在:[github]https://github.com/zeallag/TreeView.</br>
在Visual Studio可以通过nuget直接引用到项目里，nuget里查找名称为ZMSoft.Win.TreeView，[包地址]https://www.nuget.org/packages/ZMSoft.Win.TreeView/</br>
[mes.guru]https://www.mes.guru/2018/04/11/treeview/

# 特性
> 根节点位置可调整，整体大小可缩放，节点大小、颜色可修改、可设置进度
![avatar](https://www.mes.guru/images/treeview.png)

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
  
