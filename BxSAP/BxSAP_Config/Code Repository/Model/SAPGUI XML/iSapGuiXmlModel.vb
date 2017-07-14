Imports System.Windows.Forms
Imports BxSAP_Config.Model.Logon.Connections
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Sapgui.Xml

	Public Interface iSapGuiXmlModel

		#Region "Methods: Exposed"

			Function	GetConnectionTree()												As	List(Of TreeNode)
			Function	GetConnectionData(ByVal ID	As String)		As	iLogonConnectionDTO
			Function	GetConnections()													As List(Of	iLogonConnectionDTO)



			'Property	Repository()			As	SapGuiXmlRepos

			'Property	MessageServers		As	Dictionary(Of String, MsgServerDTO)
			'Property	MessageServers		As	Dictionary(Of String, MsgServerDTO)
			'Property	MessageServers		As	Dictionary(Of String, MsgServerDTO)



		#End Region

	End Interface

End Namespace