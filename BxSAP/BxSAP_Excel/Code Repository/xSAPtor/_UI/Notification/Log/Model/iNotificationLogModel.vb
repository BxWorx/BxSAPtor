Imports BxSAP_Excel.Main.Notification.Icon
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Main.Notification.Log

	Friend Interface iNotificationLogModel

		#Region "Properties"

			ReadOnly	Property Count()			As Integer
			WriteOnly	Property QueueSize()	As Integer

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods"

			Sub Reset()
			Function ReadLog(ByVal Quantity As Integer)	As List(Of iNotificationMessageDTO)
			Function Log(ByVal LogEntry As iNotificationMessageDTO)	As Boolean

		#End Region

	End Interface

End Namespace