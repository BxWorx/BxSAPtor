Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Collections.Concurrent

Imports BxSAP_Excel.Main.Notification.Icon
Imports BxSAP_Excel.Main.Notification.Options
Imports BxSAP_Excel.Utilities.MsgHub
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Main.Notification.Log

	Friend Class NotificationLogVM
								Implements iNotificationLogVM

		#Region "Process: Log View"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend ReadOnly Property LogList(ByVal Quantity	As Integer) _
																As List(Of iNotificationMessageDTO) _
																	Implements iNotificationLogVM.LogList
				Get
					Return Me.co_Model.ReadLog(Quantity).OrderByDescending(Function(Log) Log.Timestamp).ToList
				End Get
			End Property

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Exposed"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend	Sub Show()	_
										Implements iNotificationLogVM.Show
			
				If Me.co_View.IsDisposed
					Me.PrepareView()
				End If	
				'..................................................
				If Me.co_View.Visible
					If Me.co_View.WindowState = FormWindowState.Minimized
						Me.co_View.WindowState = FormWindowState.Normal
					Else
						Me.co_View.Hide()
					End If
				Else
					If Me.co_Parent Is Nothing
						Me.co_View.Show()
					Else
						Me.co_View.Show(Me.co_Parent)
					End If
				End If

			End Sub

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Constructors"

			Private	WithEvents	co_View		As	NotificationLogView
			'....................................................
			Private	co_Parent					As	IWin32Window
			Private co_Model					As	iNotificationLogModel
			Private	co_OptionsDTO			As	iNotifyOptionsDTO
			Private	co_Context				As	SynchronizationContext
			Private	co_BSLogList			As	BindingSource
			Private	ct_LogList				As	List(Of iNotificationMessageDTO)

			Private	cn_RowsToShow			As	UShort
			Private cb_Busy						As	Boolean
			Private	co_LockBusy				As	Object
			Private	ct_EventTracker		As	ConcurrentStack(Of Boolean)
			'....................................................
			Private	co_SubStartStop		As	iSubscription(Of sMsgStartupShutdown)
			Private	co_SubLogView			As	iSubscription(Of sMsgLogView)
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend	Sub New(Optional _parent	As	IWin32Window	= Nothing)

				Me.co_Parent			= _parent
				Me.co_Model				= NotificationLogModel.NotificationLogModel()
				Me.co_OptionsDTO	=	New NotifyOptionsModel().Fetch()
				'..................................................
				Me.PrepareView()
				Me.PrepareOps()

			End Sub

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Private"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private	Sub PrepareView()

				Me.co_View	= New NotificationLogView()
				'..................................................
				AddHandler	Me.co_View.Load	,	AddressOf	Me.eh_FormLoad

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private	Sub PrepareOps()

				Me.co_Context										= SynchronizationContext.Current

				Me.ct_LogList										= New List(Of iNotificationMessageDTO)
				Me.co_BSLogList									= New BindingSource
				Me.co_BSLogList.DataSource			= Me.ct_LogList
				'..................................................
				Me.cb_Busy					= False
				Me.co_LockBusy			=	New	Object
				Me.cn_RowsToShow		= 10
				Me.ct_EventTracker	= New ConcurrentStack(Of Boolean)
				'..................................................
				Me.co_SubStartStop	= so_MsgHub.Value.Subscribe(Of	sMsgStartupShutdown)	(AddressOf	Me.mh_StartupShutdown	, True)
				Me.co_SubLogView		= so_MsgHub.Value.Subscribe(Of sMsgLogView)						(AddressOf	Me.eh_LogUpdatedAsync	, True)

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Sub mh_StartupShutdown(ByVal _msg As sMsgStartupShutdown)

				If _msg.IsShutdown

					RemoveHandler	Me.co_View.Load	,	AddressOf	Me.eh_FormLoad

					Me.co_Context.Post(
						Sub()

						Me.co_View.Close()
						Me.co_View.Dispose()

					End Sub,	Nothing )
					'................................................
					so_MsgHub.Value.Unsubscribe(Me.co_SubLogView)
					so_MsgHub.Value.Unsubscribe(Me.co_SubStartStop)

				End If

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Async Sub eh_LogUpdatedAsync(ByVal _msg	As sMsgLogView)

				Me.ct_EventTracker.Push(True)
				If Me.cb_Busy	Then Return

				Await Task.Run(
					(	Sub()

							SyncLock	Me.co_LockBusy
								If Me.cb_Busy
									Exit Sub
								Else
									Me.cb_Busy	= True
								End If
							End SyncLock
							'............................................
							Do

								Me.ct_EventTracker.Clear()

								Thread.Sleep(500)

								If Me.ct_EventTracker.Count = 0
									Exit Do
								End If

							Loop
							'............................................
							SyncLock	Me.co_LockBusy
								Me.cb_Busy	= False
							End SyncLock

							Me.Refresh_DGV()

						End Sub)
					).ConfigureAwait(False)

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private	Sub eh_FormLoad(sender As Object,	e	As EventArgs)

				Me.Configure_DGVLayout()
				Me.Refresh_DGV()
				'..................................................
				AddHandler	Me.co_View.xbtn_ts_Reset.Click	,		AddressOf	Me.ResetLog

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Sub ResetLog(sender As Object,	e	As EventArgs)

				Me.co_Model.Reset()
				Me.Refresh_DGV()

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Sub Refresh_DGV()

				Me.co_Context.Post(
					Sub()

						Me.ct_LogList.Clear()
						Me.ct_LogList.AddRange( Me.LogList(Me.cn_RowsToShow) )
						'..................................................
						Me.co_View.xdgv_Log.SuspendLayout()
						Me.co_BSLogList.ResetBindings(False)
						Me.co_View.xdgv_Log.ResumeLayout(False)

					End Sub,	Nothing )

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Sub Configure_DGVLayout()

				Dim lo_ColTBx		As	DataGridViewTextBoxColumn	= Nothing
				'..................................................
				With Me.co_View.xdgv_Log

					.AutoGenerateColumns	= False
					.AutoSize							= False
					.RowHeadersWidth			= 20
					.DataSource						= Me.co_BSLogList

				End With
				'..................................................
				lo_ColTBx	=	New	DataGridViewTextBoxColumn

				With lo_ColTBx
					.Name							= "TimeStamp"
					.HeaderText				= "Date/Time"
					.DataPropertyName	= "Timestamp"
					.Width						= 110
				End With

				Me.co_View.xdgv_Log.Columns.Add(lo_ColTBx)
				'..................................................
				lo_ColTBx	=	New	DataGridViewTextBoxColumn

				With lo_ColTBx
					.Name							= "Type"
					.DataPropertyName	= "Type"
					.Width						= 50
				End With

				Me.co_View.xdgv_Log.Columns.Add(lo_ColTBx)
				'..................................................
				lo_ColTBx	=	New	DataGridViewTextBoxColumn

				With lo_ColTBx
					.Name							= "Title"
					.DataPropertyName	= "Title"
					.Width						= 150
				End With

				Me.co_View.xdgv_Log.Columns.Add(lo_ColTBx)
				'..................................................
				lo_ColTBx	=	New	DataGridViewTextBoxColumn

				With lo_ColTBx
					.Name							= "Text"
					.DataPropertyName	= "Text"
					.AutoSizeMode	= DataGridViewAutoSizeColumnMode.Fill
				End With

				Me.co_View.xdgv_Log.Columns.Add(lo_ColTBx)

			End Sub

		#End Region

	End Class

End Namespace