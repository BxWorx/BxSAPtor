Imports System.Windows.Forms
Imports System.Xml

Imports BxSAP_Config.Model.Logon.Connections
Imports BxSAP_Config.Model.Logon.ConnectionSetup
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Sapgui.Xml

	Public Class SAPGuiXmlModelxx
								Implements	iSapGuiXmlModel

		#Region "Definitions"

			Private co_Repos					As	SapGuiXmlRepos
			Private	co_ConnOptions		As	iLogonConnSetupDTO
			'....................................................
			Private	ct_ItemNodes			As	Dictionary(Of String,	WSNodeItemDTO)
			Private ct_Srvid					As	Dictionary(Of String, String)

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Exposed"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetConnectionData(ByVal _id	As String)	As iLogonConnectionDTO _
												Implements	iSapGuiXmlModel.GetConnectionData

				Return	Me.CompileConnectionData(_id)

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetConnections()	As List(Of iLogonConnectionDTO) _
												Implements	iSapGuiXmlModel.GetConnections

				Return	Me.CompileConnectionList()

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetConnectionTree()	As List(Of TreeNode) _
												Implements	iSapGuiXmlModel.GetConnectionTree

				Return	Me.CompileTree()

			End Function

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Constructor"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Public Sub New(	ByVal _connoptions	As	iLogonConnSetupDTO)

				Me.co_ConnOptions	= _connoptions
				'..................................................
				Me.ct_ItemNodes		= New	Dictionary(Of String, WSNodeItemDTO)
				Me.ct_Srvid				= New	Dictionary(Of String, String)
				'..................................................
				Me.LoadSapGuiXMLData()				

			End Sub

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Private: Connections"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function CompileConnectionData(ByVal _id	As String)	As iLogonConnectionDTO

				Dim lo_ConnDTO		As	New LogonConnectionDTO
				'..................................................
				If Me.ct_ItemNodes.ContainsKey(_id)

					Dim lo_ItemDTO	= Me.ct_ItemNodes.Item(_id)

					If Me.co_Repos.Services.ContainsKey(lo_ItemDTO.serviceid)

						Dim lo_SrvDTO	= Me.co_Repos.Services.Item(lo_ItemDTO.serviceid)

						If Not IsNothing(lo_SrvDTO)	Then	lo_ConnDTO	= Me.ConvertConnectionData(lo_SrvDTO)

					End If
				End If
				'..................................................
				Return	lo_ConnDTO

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function CompileConnectionList()	As List(Of iLogonConnectionDTO)

				Dim lt_List		As New List(Of iLogonConnectionDTO)
				'..................................................
				For Each	lo	In	Me.ct_ItemNodes

					If Me.co_Repos.Services.ContainsKey(lo.Value.serviceid)

						Dim lo_SrvDTO	= Me.co_Repos.Services.Item(lo.Value.serviceid)

						If Not IsNothing(lo_SrvDTO)
							
							Dim	lo_ConnDTO	= Me.ConvertConnectionData(lo_SrvDTO)

							If Not lo_ConnDTO.ID.Length.Equals(0)
								lt_List.Add(lo_ConnDTO)
							End If

						End If
					End If
					
				Next
				'..................................................
				Return	lt_List

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function ConvertConnectionData(ByVal _msgsvrdto As MsgServiceDTO)	As LogonConnectionDTO

				Dim lo_ConnDTO	As	New LogonConnectionDTO
				'..................................................
				lo_ConnDTO.IsNew						= False
				lo_ConnDTO.CanEdit					= False

				lo_ConnDTO.ID								= _msgsvrdto.uuid
				lo_ConnDTO.Name							= _msgsvrdto.name
				lo_ConnDTO.SNC_PartnerName	= _msgsvrdto.sncname
				lo_ConnDTO.SystemID					= _msgsvrdto.systemid
				lo_ConnDTO.SNC_Active				= CBool( IIf(_msgsvrdto.sncname.Length.Equals(0), False, True) )
				lo_ConnDTO.SNC_QOP					= 3	' CInt( lo_SrvDTO.sncop )

				If _msgsvrdto.mode.Equals("1")
					lo_ConnDTO.AppServer	= _msgsvrdto.server
				Else
					If Not _msgsvrdto.msid.Length.Equals(0)

						Dim lo_MsgDTO					= Me.co_Repos.MsgServers.Item(_msgsvrdto.msid)
						lo_ConnDTO.AppServer	= lo_MsgDTO.host

					End If

				End If

				lo_ConnDTO.InstanceNo				= 0
				lo_ConnDTO.LowSpeed					= False
				lo_ConnDTO.RouterPath				= ""
				'..................................................
				Return	lo_ConnDTO

			End Function

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Private: Tree Compiler"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function CompileTree()	As List(Of TreeNode)

				Dim lt_List		As New List(Of TreeNode)
				'..................................................
				For Each lo_WS In Me.co_Repos.WorkSpaces

					Dim lo_NodeWS	As New TreeNode()

					lo_NodeWS.Text	= lo_WS.Value.name
					lo_NodeWS.Name	= lo_WS.Value.uuid

					For Each lo_Obj In lo_WS.Value.Nodes

						If lo_WS.Value.NodeIsItem

							Dim lo_NodeI	= CType(lo_Obj.Value, WSNodeItemDTO)
							Dim lo_NodeIt	= Me.CreateItemNode(lo_NodeI.serviceid, Me.co_ConnOptions.XML_OnlyLoadGUI)

							If Not IsNothing(lo_NodeIt)

								lo_NodeIt.Name	= lo_NodeI.uuid
								lo_NodeIt.Tag		= "X"

								lo_NodeWS.Nodes.Add(lo_NodeIt)

							End If

						Else

							Dim lo_NodeN	= CType(lo_Obj.Value, WSNodeDTO)
							Dim lo_NodeNd	= New TreeNode

							lo_NodeNd.Text	= lo_NodeN.name
							lo_NodeNd.Name	= lo_NodeN.uuid

							For Each lo_Item In lo_NodeN.Items

								Dim lo_NodeIt	= Me.CreateItemNode(lo_Item.Value.serviceid, Me.co_ConnOptions.XML_OnlyLoadGUI)

								If Not IsNothing(lo_NodeIt)

									lo_NodeIt.Name	= lo_Item.Value.uuid

									lo_NodeNd.Nodes.Add(lo_NodeIt)

								End If

							Next

							If lo_NodeNd.Nodes.Count > 0	Then	lo_NodeWS.Nodes.Add(lo_NodeNd)

						End If

					Next
					'................................................
					If lo_NodeWS.Nodes.Count > 0

						lo_NodeWS.Collapse()
						lt_List.Add(lo_NodeWS)

					End If

				Next
				'..................................................
				Return	lt_List

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function CreateItemNode(ByVal	_id				As String		,
																			ByVal _onlygui	As Boolean		)		As TreeNode

				Dim lb_Ok		As Boolean				= False
				Dim lo_Node	As TreeNode				= Nothing
				Dim lo_Srv	As MsgServiceDTO	= Nothing
				'..................................................
				If Me.co_Repos.Services.ContainsKey(_id)

					lo_Srv	=	Me.co_Repos.Services.Item(_id)

					If _onlygui
						If lo_Srv.type.Equals("SAPGUI")
							lb_Ok	= True
						End If
					Else
						lb_Ok	= True
					End If

				End If
				'..................................................
				If lb_Ok

					lo_Node	= New TreeNode

					If lo_Srv.systemid.Length.Equals(0)
						lo_Node.Text	= String.Format("{0}", lo_Srv.name)
					Else
						lo_Node.Text	= String.Format("[{0}] - {1}", lo_Srv.systemid, lo_Srv.name)
					End If

				End If				
				'..................................................
				Return	lo_Node

			End Function

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Private: XML Sections"

			'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
			#Region "Methods: Private: XML: Header Section"

				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Sub LoadSapGuiXMLData()

					Me.co_Repos		= New SapGuiXmlRepos
					'......................................................
					Dim lo_XMLDoc		As XmlDocument		= Me.LoadXMLDoc()

					Me.Load_XML_WorkSpaces(lo_XMLDoc)
					Me.Load_XML_Services(lo_XMLDoc)
					Me.Load_XML_MsgServers(lo_XMLDoc)

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
				Private Function GetAttrValue(ByVal _attr	As XmlNode)	As String

					If IsNothing(_attr)
						Return	String.Empty
					Else
						Return	_attr.Value
					End If

				End Function
				'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
				Private Class SapGuiXmlRepos

					#Region "Properties"

						Friend Property	MsgServers		As Dictionary(Of String, MsgServerDTO)
						Friend Property	WorkSpaces		As Dictionary(Of String, WorkspaceDTO)
						Friend Property	Services			As Dictionary(Of String, MsgServiceDTO)

					#End Region
					'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
					#Region "Constructor"

						'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
						Friend Sub New()

							Me.MsgServers		= New Dictionary(Of String, MsgServerDTO)
							Me.WorkSpaces		= New Dictionary(Of String, WorkspaceDTO)
							Me.Services			= New Dictionary(Of String, MsgServiceDTO)

						End Sub

					#End Region

				End Class

			#End Region
			'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
			#Region "Methods: Private: XML: Workspace Section"



				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Sub Load_XML_WorkSpaces(_xmldoc As XmlDocument)

					For Each	lo_WrkSpace As XmlElement		In _xmldoc.GetElementsByTagName("Workspace")

						Dim lo_WSdto	= Me.LoadWSAttributtes(lo_WrkSpace.Attributes())
						'..............................................
						If Not Me.co_ConnOptions.XML_FromNode.Equals(0)
							If Not lo_WSdto.name.Equals(Me.co_ConnOptions.XML_FromNode)
								Continue For
							End If
						End If
						'..............................................
						Dim lo_Nodes	= lo_WrkSpace.GetElementsByTagName("Node")

						If lo_Nodes.Count.Equals(0)

							lo_WSdto.NodeIsItem	= True

							For Each lo_WSNItem As XmlElement In lo_WrkSpace.GetElementsByTagName("Item")
							
								Dim lo_WSNodeItemdto	= Me.LoadWSNodeItemAttributtes(lo_WSNItem.Attributes())

								lo_WSdto.Nodes.Add(lo_WSNodeItemdto.uuid, lo_WSNodeItemdto)

								Me.ct_ItemNodes.Add(lo_WSNodeItemdto.uuid, lo_WSNodeItemdto)
								Me.ct_Srvid.Add(lo_WSNodeItemdto.serviceid,Nothing)

							Next

						Else

							For Each	lo_WSNode As XmlElement		In lo_Nodes

								Dim lo_WSNodedto	= Me.LoadWSNodeAttributtes(lo_WSNode.Attributes())

								For Each	lo_WSNItem As XmlElement	In lo_WSNode.GetElementsByTagName("Item")

									Dim lo_WSNodeItemdto	= Me.LoadWSNodeItemAttributtes(lo_WSNItem.Attributes())
									lo_WSNodedto.Items.Add(lo_WSNodeItemdto.uuid, lo_WSNodeItemdto)

									Me.ct_ItemNodes.Add(lo_WSNodeItemdto.uuid, lo_WSNodeItemdto)
									Me.ct_Srvid.Add(lo_WSNodeItemdto.serviceid,Nothing)

								Next

								lo_WSdto.Nodes.Add(lo_WSNodedto.uuid, lo_WSNodedto)

							Next

						End If

						Me.co_Repos.WorkSpaces.Add(lo_WSdto.uuid, lo_WSdto)

					Next

				End Sub
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Function LoadWSAttributtes(ByVal _attributes As XmlAttributeCollection)	As WorkspaceDTO

					Dim lo_DTO	As New WorkspaceDTO

					lo_DTO.uuid	= Me.GetAttrValue(_attributes.GetNamedItem("uuid"))
					lo_DTO.name	= Me.GetAttrValue(_attributes.GetNamedItem("name"))

					lo_DTO.Nodes	= New Dictionary(Of String, Object)

					Return	lo_DTO

				End Function
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Function LoadWSNodeAttributtes(ByVal _attributes As XmlAttributeCollection)	As WSNodeDTO

					Dim lo_DTO	As New WSNodeDTO

					lo_DTO.uuid	= Me.GetAttrValue(_attributes.GetNamedItem("uuid"))
					lo_DTO.name	= Me.GetAttrValue(_attributes.GetNamedItem("name"))

					lo_DTO.Items	= New Dictionary(Of String, WSNodeItemDTO)

					Return	lo_DTO

				End Function
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Function LoadWSNodeItemAttributtes(ByVal _attributes As XmlAttributeCollection)	As WSNodeItemDTO

					Dim lo_DTO	As New WSNodeItemDTO

					lo_DTO.uuid				= Me.GetAttrValue(_attributes.GetNamedItem("uuid"))
					lo_DTO.serviceid	= Me.GetAttrValue(_attributes.GetNamedItem("serviceid"))

					Return	lo_DTO

				End Function
				'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
				Private Class WorkspaceDTO

					Friend Property uuid					As String
					Friend Property name					As String
					Friend Property Nodes					As Dictionary(Of String, Object)
					Friend Property NodeIsItem		As Boolean

				End Class
				'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
				Private Class WSNodeDTO

					Friend Property uuid					As String
					Friend Property name					As String
					Friend Property Items					As Dictionary(Of String, WSNodeItemDTO)

				End Class
				'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
				Private Class WSNodeItemDTO

					Friend Property uuid					As String
					Friend Property serviceid			As String

				End Class

			#End Region
			'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
			#Region "Methods: Private: XML: Message Server Section"

				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Sub Load_XML_MsgServers(ByRef _xmldoc As XmlDocument)

					For Each lo_MsgSvr As XmlElement	In _xmldoc.GetElementsByTagName("Messageserver")

						Dim lo_MSdto	= Me.LoadMsgAttributtes(lo_MsgSvr.Attributes())
						Me.co_Repos.MsgServers.Add(lo_MSdto.uuid, lo_MSdto)

					Next

				End Sub
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Function LoadMsgAttributtes(ByVal _attributes As XmlAttributeCollection)	As MsgServerDTO

					Dim lo_DTO	As New MsgServerDTO

					lo_DTO.uuid					= Me.GetAttrValue(_attributes.GetNamedItem("uuid"))
					lo_DTO.name					= Me.GetAttrValue(_attributes.GetNamedItem("name"))
					lo_DTO.host					= Me.GetAttrValue(_attributes.GetNamedItem("host"))
					lo_DTO.port					= Me.GetAttrValue(_attributes.GetNamedItem("port"))
					lo_DTO.description	= Me.GetAttrValue(_attributes.GetNamedItem("description"))

					Return	lo_DTO

				End Function
				'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
				Private Class MsgServerDTO

					Friend Property uuid					As String
					Friend Property name					As String
					Friend Property host					As String
					Friend Property port					As String
					Friend Property description		As String

				End Class

			#End Region
			'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
			#Region "Methods: Private: XML: Services Section"

				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Sub Load_XML_Services(ByRef _xmldoc As XmlDocument)

					For Each	lo_Service As XmlElement	In _xmldoc.GetElementsByTagName("Service")

						Dim lo_Srvdto	= Me.xLoadSrvAttributtes(lo_Service.Attributes())

						If Not IsNothing(lo_Srvdto)
							Me.co_Repos.Services.Add(lo_Srvdto.uuid, lo_Srvdto)
						End If

					Next

				End Sub
				'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				Private Function xLoadSrvAttributtes(ByVal _attributes As XmlAttributeCollection)	As MsgServiceDTO

					Dim lc_Type		As String		= Me.GetAttrValue(_attributes.GetNamedItem("type"))
					'................................................
					If Me.co_ConnOptions.XML_OnlyLoadGUI
						If Not lc_Type.Equals("SAPGUI")
							Return	Nothing
						End If
					End If
					'................................................
					Dim lo_DTO	As	New MsgServiceDTO

					lo_DTO.type					= lc_Type
					lo_DTO.uuid					= Me.GetAttrValue(_attributes.GetNamedItem("uuid"))
					lo_DTO.name					= Me.GetAttrValue(_attributes.GetNamedItem("name"))
					lo_DTO.dcpg					= Me.GetAttrValue(_attributes.GetNamedItem("dcpg"))
					lo_DTO.msid					= Me.GetAttrValue(_attributes.GetNamedItem("msid"))
					lo_DTO.sapcpg				= Me.GetAttrValue(_attributes.GetNamedItem("sapcpg"))
					lo_DTO.server				= Me.GetAttrValue(_attributes.GetNamedItem("server"))
					lo_DTO.sncname			= Me.GetAttrValue(_attributes.GetNamedItem("sncname"))
					lo_DTO.sncop				= Me.GetAttrValue(_attributes.GetNamedItem("sncop"))
					lo_DTO.systemid			= Me.GetAttrValue(_attributes.GetNamedItem("systemid"))
					lo_DTO.mode					= Me.GetAttrValue(_attributes.GetNamedItem("mode"))
					lo_DTO.description	= Me.GetAttrValue(_attributes.GetNamedItem("description"))

					Return	lo_DTO

				End Function
				'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°
				Private Class MsgServiceDTO

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

			#End Region

		#End Region

	End Class

End Namespace
