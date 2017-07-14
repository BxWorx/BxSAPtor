Imports System.Windows.Forms
Imports BxSAP_Config.Model.Logon.Connections
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Sapgui.Xml

	Friend Interface iSapGuiXmlModel

		#Region "Methods: Exposed"

			Function GetSapGuiXmlTree(					ByVal XMLFilePathName		As String	,
																Optional	ByVal	OnlySAPGui				As Boolean	= True	)		As List(Of TreeNode)
			Function GetSapGuiData(ByVal ID	As String)		As iLogonConnectionDTO


			Function	LoadSapGuiXmlConnections()	As List(Of	iLogonConnectionDTO)




			Property	Repository()			As	SapGuiXmlRepos

			Property	MessageServers		As	Dictionary(Of String, MsgServerDTO)
			Property	MessageServers		As	Dictionary(Of String, MsgServerDTO)
			Property	MessageServers		As	Dictionary(Of String, MsgServerDTO)



		#End Region

	End Interface

End Namespace