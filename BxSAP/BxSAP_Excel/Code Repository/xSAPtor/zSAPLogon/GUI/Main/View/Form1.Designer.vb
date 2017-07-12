<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Systems", System.Windows.Forms.HorizontalAlignment.Left)
		Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Languages", System.Windows.Forms.HorizontalAlignment.Left)
		Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left)
		Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"SAP DEV", "DEV 01", "DEV 02", "Dev 03"}, -1)
		Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("SAP PROD")
		Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("AF")
		Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("En")
		Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node1")
		Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node0", New System.Windows.Forms.TreeNode() {TreeNode1})
		Me.ListView1 = New System.Windows.Forms.ListView()
		Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
		Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
		Me.TreeView1 = New System.Windows.Forms.TreeView()
		Me.SuspendLayout
		'
		'ListView1
		'
		Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
		Me.ListView1.GridLines = true
		ListViewGroup1.Header = "Systems"
		ListViewGroup1.Name = "ListViewGroup1"
		ListViewGroup2.Header = "Languages"
		ListViewGroup2.Name = "ListViewGroup2"
		ListViewGroup3.Header = "ListViewGroup"
		ListViewGroup3.Name = "ListViewGroup3"
		Me.ListView1.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3})
		ListViewItem1.Group = ListViewGroup1
		ListViewItem1.IndentCount = 2
		ListViewItem2.Group = ListViewGroup1
		ListViewItem3.Group = ListViewGroup2
		ListViewItem4.Group = ListViewGroup2
		Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4})
		Me.ListView1.Location = New System.Drawing.Point(12, 12)
		Me.ListView1.Name = "ListView1"
		Me.ListView1.Size = New System.Drawing.Size(204, 174)
		Me.ListView1.TabIndex = 0
		Me.ListView1.UseCompatibleStateImageBehavior = false
		Me.ListView1.View = System.Windows.Forms.View.Tile
		'
		'TreeView1
		'
		Me.TreeView1.Location = New System.Drawing.Point(221, 240)
		Me.TreeView1.Name = "TreeView1"
		TreeNode1.Name = "Node1"
		TreeNode1.Text = "Node1"
		TreeNode2.Name = "Node0"
		TreeNode2.Text = "Node0"
		Me.TreeView1.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode2})
		Me.TreeView1.Size = New System.Drawing.Size(449, 252)
		Me.TreeView1.TabIndex = 1
		'
		'Form1
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(700, 530)
		Me.Controls.Add(Me.TreeView1)
		Me.Controls.Add(Me.ListView1)
		Me.Name = "Form1"
		Me.Text = "Form1"
		Me.ResumeLayout(false)

End Sub

	Friend WithEvents ListView1 As Windows.Forms.ListView
	Friend WithEvents ColumnHeader1 As Windows.Forms.ColumnHeader
	Friend WithEvents ColumnHeader2 As Windows.Forms.ColumnHeader
	Friend WithEvents TreeView1 As Windows.Forms.TreeView
End Class
