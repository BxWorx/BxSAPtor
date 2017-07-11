'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Main.Notification.Log

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NotificationLogView
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
		Me.xtss_Main = New System.Windows.Forms.ToolStrip()
		Me.xbtn_ts_Reset = New System.Windows.Forms.ToolStripButton()
		Me.xdgv_Log = New System.Windows.Forms.DataGridView()
		Me.xtss_Main.SuspendLayout
		CType(Me.xdgv_Log,System.ComponentModel.ISupportInitialize).BeginInit
		Me.SuspendLayout
		'
		'xtss_Main
		'
		Me.xtss_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.xtss_Main.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.xbtn_ts_Reset})
		Me.xtss_Main.Location = New System.Drawing.Point(0, 0)
		Me.xtss_Main.Name = "xtss_Main"
		Me.xtss_Main.Padding = New System.Windows.Forms.Padding(2)
		Me.xtss_Main.Size = New System.Drawing.Size(841, 27)
		Me.xtss_Main.TabIndex = 0
		Me.xtss_Main.Text = "ToolStrip1"
		'
		'xbtn_ts_Reset
		'
		Me.xbtn_ts_Reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.xbtn_ts_Reset.Image = Global.BxSAP_Excel.My.Resources.Resources._Erase
		Me.xbtn_ts_Reset.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.xbtn_ts_Reset.Name = "xbtn_ts_Reset"
		Me.xbtn_ts_Reset.Size = New System.Drawing.Size(23, 20)
		Me.xbtn_ts_Reset.Tag = "xtag_ts_Reset"
		Me.xbtn_ts_Reset.Text = "Reset Log"
		'
		'xdgv_Log
		'
		Me.xdgv_Log.AllowUserToAddRows = false
		Me.xdgv_Log.AllowUserToDeleteRows = false
		Me.xdgv_Log.AllowUserToOrderColumns = true
		Me.xdgv_Log.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.xdgv_Log.Dock = System.Windows.Forms.DockStyle.Fill
		Me.xdgv_Log.Location = New System.Drawing.Point(0, 27)
		Me.xdgv_Log.Name = "xdgv_Log"
		Me.xdgv_Log.ReadOnly = true
		Me.xdgv_Log.Size = New System.Drawing.Size(841, 270)
		Me.xdgv_Log.TabIndex = 1
		'
		'NotificationLogView
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(841, 297)
		Me.Controls.Add(Me.xdgv_Log)
		Me.Controls.Add(Me.xtss_Main)
		Me.Name = "NotificationLogView"
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.Text = "Notifcation Log..."
		Me.TopMost = true
		Me.xtss_Main.ResumeLayout(false)
		Me.xtss_Main.PerformLayout
		CType(Me.xdgv_Log,System.ComponentModel.ISupportInitialize).EndInit
		Me.ResumeLayout(false)
		Me.PerformLayout

End Sub

	Friend WithEvents xtss_Main As Windows.Forms.ToolStrip
	Friend WithEvents xdgv_Log	As Windows.Forms.DataGridView
		Friend WithEvents xbtn_ts_Reset As Windows.Forms.ToolStripButton
	End Class

End Namespace