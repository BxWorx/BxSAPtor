'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Sapgui.Xml

	'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
	Friend Class SapGuiXmlRepos

		#Region "Properties"

			Friend Property	Services			As Dictionary(Of String, MsgServiceDTO)
			Friend Property	MsgServers		As Dictionary(Of String, MsgServerDTO)
			Friend Property	WorkSpaces		As Dictionary(Of String, WorkspaceDTO)

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Constructor"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub New()

				Me.Services			= New Dictionary(Of String, MsgServiceDTO)
				Me.MsgServers		= New Dictionary(Of String, MsgServerDTO)
				Me.WorkSpaces		= New Dictionary(Of String, WorkspaceDTO)

			End Sub

		#End Region

	End Class
	'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
	Friend Class WorkspaceDTO

		Friend Property uuid					As String
		Friend Property name					As String
		Friend Property Nodes					As Dictionary(Of String, WSNodeDTO)
		Friend Property Items					As Dictionary(Of String, WSNodeItemDTO)

	End Class
	'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
	Friend Class WSNodeDTO

		Friend Property uuid					As String
		Friend Property name					As String
		Friend Property Items					As Dictionary(Of String, WSNodeItemDTO)

	End Class
	'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
	Friend Class WSNodeItemDTO

		Friend Property uuid					As String
		Friend Property serviceid			As String

	End Class
	'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
	Friend Class MsgServerDTO

		Friend Property uuid					As String
		Friend Property name					As String
		Friend Property host					As String
		Friend Property port					As String
		Friend Property description		As String

	End Class
	'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
	Friend Class MsgServiceDTO

		Friend Property uuid					As String
		Friend Property name					As String
		Friend Property systemid			As String
		Friend Property msid					As String
		Friend Property type					As String
		Friend Property server				As String
		Friend Property sncname				As String
		Friend Property sapcpg				As String
		Friend Property dcpg					As String
		Friend Property sncop      		As String
		Friend Property description		As String
		Friend Property mode					As String

	End Class

End Namespace
