Imports System.Xml

Imports BxSAP_Config.Model.Logon.ConnectionSetup
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Sapgui.Xml

	Friend Class	SAPGuiXmlLoader

		#Region "Definitions"

			Private co_Repos						As	SapGuiXmlRepos
			Private	co_ConnOptions			As	iLogonConnSetupDTO
			'....................................................
			Private	ct_UnUsedSrvList		As	New	List(Of String)

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Properties"
			
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend ReadOnly Property	Repository()	As SapGuiXmlRepos
				Get
					Return	Me.co_Repos
				End Get
			End Property
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend ReadOnly Property	Services			As Dictionary(Of String, MsgServiceDTO)
				Get
					Return	Me.co_Repos.Services
				End Get
			End Property
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend ReadOnly Property	MsgServers()	As Dictionary(Of String, MsgServerDTO)
				Get
					Return	Me.co_Repos.MsgServers
				End Get
			End Property
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend ReadOnly Property	WorkSpaces		As Dictionary(Of String, WorkspaceDTO)
				Get
					Return	Me.co_Repos.WorkSpaces
				End Get
			End Property
			
		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Constructor"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub New(	ByVal _connoptions	As	iLogonConnSetupDTO)

				Me.co_ConnOptions	= _connoptions
				'..................................................
				Me.LoadSapGuiXML()				

			End Sub

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Private: XML Sections"

			'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
			#Region "Methods: Private: XML: Header Section"

				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Sub LoadSapGuiXML()

					Me.co_Repos		= New SapGuiXmlRepos
					'......................................................
					Dim lo_XMLDoc		As XmlDocument		= Me.LoadXMLDoc()

					Me.Load_XML_Services(lo_XMLDoc)
					Me.Load_XML_MsgServers(lo_XMLDoc)
					Me.Load_XML_WorkSpaces(lo_XMLDoc)
					Me.Load_XML_Cleanup()

				End Sub
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Function LoadXMLDoc()	As XmlDocument

					Dim	lo_XMLDoc		As XmlDocument		= New XmlDocument
					'..................................................
					Try

							lo_XMLDoc.Load(Me.co_ConnOptions.XML_FullName)

						Catch ex As Exception

					End Try
					'..................................................
					Return	lo_XMLDoc

				End Function
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private	Sub Load_XML_Cleanup()

					For Each	lc As String	In Me.ct_UnUsedSrvList
						Me.co_Repos.Services.Remove(lc)
					Next

					Me.ct_UnUsedSrvList	= Nothing
					'................................................

				End Sub

			#End Region
			'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
			#Region "Methods: Private: XML: Workspace Section"

				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Sub Load_XML_WorkSpaces(_xmldoc As XmlDocument)

					For Each	lo_WrkSpace As XmlElement		In _xmldoc.GetElementsByTagName("Workspace")

						If Not Me.co_ConnOptions.XML_FromWorkspace.Length.Equals(0)
							If Not lo_WrkSpace.GetAttribute("name").Equals(Me.co_ConnOptions.XML_FromWorkspace)
								Continue For
							End If
						End If
						'..............................................
						Dim lo_WsDTO	As WorkspaceDTO		= Me.LoadWSAttributtes(lo_WrkSpace)
						'..............................................
						For Each	lo_Node As XmlElement		In lo_WrkSpace.GetElementsByTagName("Node")

							If Not Me.co_ConnOptions.XML_FromNode.Length.Equals(0)
								If Not lo_Node.GetAttribute("name").Equals(Me.co_ConnOptions.XML_FromNode)
									Continue For
								End If
							End If
							'............................................
							Dim lo_WSNode	As WSNodeDTO	= Me.LoadWSNodeAttributtes(lo_Node)

							For Each	lo	In Me.GetItemList(lo_Node)
								lo_WSNode.Items.Add(lo.uuid, lo)
							Next
							'............................................
							If lo_WSNode.Items.Count > 0
								lo_WsDTO.Nodes.Add(lo_WSNode.uuid, lo_WSNode)
							End If

						Next
						''..............................................
						'For Each	lo	In Me.GetItemList(lo_WrkSpace)
						'	lo_WsDTO.Items.Add(lo.uuid, lo)
						'Next
						'..............................................
						If	lo_WsDTO.Nodes.Count > 0	OrElse
								lo_WsDTO.Items.Count > 0
							Me.co_Repos.WorkSpaces.Add(lo_WsDTO.uuid, lo_WsDTO)
						End If

					Next

				End Sub
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Function LoadWSAttributtes(ByVal _xmlelement	As XmlElement)	As WorkspaceDTO

					Dim lo_DTO	As New WorkspaceDTO
					'..............................................
					lo_DTO.uuid		= _xmlelement.GetAttribute("uuid")
					lo_DTO.name		= _xmlelement.GetAttribute("name")
					'..............................................
					lo_DTO.Nodes	= New Dictionary(Of String, WSNodeDTO)
					lo_DTO.Items	= New Dictionary(Of String, WSNodeItemDTO)
					'..............................................
					Return	lo_DTO

				End Function
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Function LoadWSNodeAttributtes(ByVal _xmlelement	As XmlElement)	As WSNodeDTO

					Dim lo_DTO	As New WSNodeDTO

					lo_DTO.uuid	= _xmlelement.GetAttribute("uuid")
					lo_DTO.name	= _xmlelement.GetAttribute("name")

					lo_DTO.Items	= New Dictionary(Of String, WSNodeItemDTO)

					Return	lo_DTO

				End Function
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Function GetItemList(ByVal _xmlelement	As XmlElement)	As List(Of WSNodeItemDTO)

					Dim lt_List			As	New List(Of WSNodeItemDTO)
					Dim lc_SrvID		As	String
					'..............................................
					For Each	lo_XMLItem As XmlElement	In _xmlelement.GetElementsByTagName("Item")

						lc_SrvID	= lo_XMLItem.GetAttribute("serviceid")

						If Me.co_Repos.Services.ContainsKey(lc_SrvID)

							lt_List.Add(Me.LoadItemAttributtes(lo_XMLItem))
							Me.ct_UnUsedSrvList.Remove(lc_SrvID)

						End If

					Next
					'..............................................
					Return	lt_List

				End Function
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Function LoadItemAttributtes(ByVal _xmlelement	As XmlElement)	As WSNodeItemDTO

					Dim lo_DTO	As New WSNodeItemDTO

					lo_DTO.uuid				= _xmlelement.GetAttribute("uuid")
					lo_DTO.serviceid	= _xmlelement.GetAttribute("serviceid")

					Return	lo_DTO

				End Function

			#End Region
			'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
			#Region "Methods: Private: XML: Message Server Section"

				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Sub Load_XML_MsgServers(ByRef _xmldoc As XmlDocument)

					For Each lo_MsgSvr As XmlElement	In _xmldoc.GetElementsByTagName("Messageserver")
						Me.LoadMsgServer(lo_MsgSvr)
					Next

				End Sub
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Sub LoadMsgServer(ByVal _xmlelement	As XmlElement)

					Dim lo_DTO	As New MsgServerDTO
					'................................................
					lo_DTO.uuid					= _xmlelement.GetAttribute("uuid")
					lo_DTO.name					= _xmlelement.GetAttribute("name")
					lo_DTO.host					= _xmlelement.GetAttribute("host")
					lo_DTO.port					= _xmlelement.GetAttribute("port")
					lo_DTO.description	= _xmlelement.GetAttribute("description")
					'................................................
					Me.co_Repos.MsgServers.Add(lo_DTO.uuid, lo_DTO)

				End Sub

			#End Region
			'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
			#Region "Methods: Private: XML: Services Section"

				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Sub Load_XML_Services(ByRef _xmldoc As XmlDocument)

					For Each	lo_ServiceElement As XmlElement		In _xmldoc.GetElementsByTagName("Service")
						Me.LoadService(lo_ServiceElement)
					Next

				End Sub
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Sub LoadService(ByVal _xmlelement	As XmlElement)

					Dim lc_Type		As String	=	_xmlelement.GetAttribute("type")
					'................................................
					If Me.co_ConnOptions.XML_OnlyLoadGUI
						If Not lc_Type.Equals("SAPGUI")
							Return
						End If
					End If
					'................................................
					Dim lo_DTO	As	New MsgServiceDTO

					lo_DTO.type					= lc_Type
					lo_DTO.uuid					= _xmlelement.GetAttribute("uuid")
					lo_DTO.name					= _xmlelement.GetAttribute("name")
					lo_DTO.dcpg					= _xmlelement.GetAttribute("dcpg")
					lo_DTO.msid					= _xmlelement.GetAttribute("msid")
					lo_DTO.sapcpg				= _xmlelement.GetAttribute("sapcpg")
					lo_DTO.server				= _xmlelement.GetAttribute("server")
					lo_DTO.sncname			= _xmlelement.GetAttribute("sncname")
					lo_DTO.sncop				= _xmlelement.GetAttribute("sncop")
					lo_DTO.systemid			= _xmlelement.GetAttribute("systemid")
					lo_DTO.mode					= _xmlelement.GetAttribute("mode")
					lo_DTO.description	= _xmlelement.GetAttribute("description")
					'................................................
					Me.co_Repos.Services.Add(lo_DTO.uuid, lo_DTO)
					Me.ct_UnUsedSrvList.Add(lo_DTO.uuid)

				End Sub

			#End Region

		#End Region

	End Class

End Namespace
