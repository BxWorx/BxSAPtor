'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Sapgui.Xml

	Public Class SAPGuiXmlMsgServiceDTO

		#Region "Properties"

				Public Property		Uuid					As String
				Public Property		Name					As String
				Public Property		SystemID			As String
				Public Property		MsID					As String
				Public Property		Type					As String
				Public Property		Server				As String
				Public Property		SncName				As String
				Public Property		SapCpg				As String
				Public Property		Dcpg					As String
				Public Property		SncOP      		As String
				Public Property		Mode					As String
				Public Property		Description		As String

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Constructor"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Public Sub New()

				Me.Uuid						= String.Empty
				Me.Name						= String.Empty
				Me.SystemID				= String.Empty
				Me.MsID						= String.Empty
				Me.Type						= String.Empty
				Me.Server					= String.Empty
				Me.SncName				= String.Empty
				Me.SapCpg					= String.Empty
				Me.Dcpg						= String.Empty
				Me.SncOP					= String.Empty
				Me.Mode						= String.Empty
				Me.Description		= String.Empty

			End Sub

		#End Region

	End Class

End Namespace
