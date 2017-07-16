Imports System.Windows.Forms

Imports BxSAP_Config.Model.Logon.Connections
Imports BxSAP_Config.Model.Logon.ConnectionSetup
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Sapgui.Xml

	Public Class SAPGuiXmlModel
								Implements	iSapGuiXmlModel

		#Region "Definitions"

			Private co_Repos					As	SAPGuiXmlLoader
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

				'Return	Me.CompileConnectionData(_id)

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetConnections()	As List(Of iLogonConnectionDTO) _
												Implements	iSapGuiXmlModel.GetConnections

				'Return	Me.CompileConnectionList()

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetConnectionTree()	As List(Of TreeNode) _
												Implements	iSapGuiXmlModel.GetConnectionTree

				'Return	Me.CompileTree()

			End Function

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Constructor"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Public Sub New(	ByVal _connoptions	As	iLogonConnSetupDTO)

				Me.co_ConnOptions	= _connoptions
				'..................................................
				Me.co_Repos	= New	SAPGuiXmlLoader(_connoptions)

			End Sub

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Private: Connections"

			''¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			'Private Function CompileConnectionData(ByVal _id	As String)	As iLogonConnectionDTO

			'	Dim lo_ConnDTO		As	New LogonConnectionDTO
			'	'..................................................
			'	If Me.ct_ItemNodes.ContainsKey(_id)

			'		Dim lo_ItemDTO	= Me.ct_ItemNodes.Item(_id)

			'		If Me.co_Repos.Services.ContainsKey(lo_ItemDTO.serviceid)

			'			Dim lo_SrvDTO	= Me.co_Repos.Services.Item(lo_ItemDTO.serviceid)

			'			If Not IsNothing(lo_SrvDTO)	Then	lo_ConnDTO	= Me.ConvertConnectionData(lo_SrvDTO)

			'		End If
			'	End If
			'	'..................................................
			'	Return	lo_ConnDTO

			'End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function CompileConnectionList()	As List(Of iLogonConnectionDTO)

				Dim lt_List		As New List(Of iLogonConnectionDTO)
				'..................................................
			'	For Each	lo	In	Me.ct_ItemNodes

			'		If Me.co_Repos.Services.ContainsKey(lo.Value.serviceid)

			'			Dim lo_SrvDTO	= Me.co_Repos.Services.Item(lo.Value.serviceid)

			'			If Not IsNothing(lo_SrvDTO)
							
			'				Dim	lo_ConnDTO	= Me.ConvertConnectionData(lo_SrvDTO)

			'				If Not lo_ConnDTO.ID.Length.Equals(0)
			'					lt_List.Add(lo_ConnDTO)
			'				End If

			'			End If
			'		End If
					
			'	Next
				'..................................................
				Return	lt_List

			End Function
			''¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			'Private Function ConvertConnectionData(ByVal _msgsvrdto As MsgServiceDTO)	As LogonConnectionDTO

			'	Dim lo_ConnDTO	As	New LogonConnectionDTO
			'	'..................................................
			'	lo_ConnDTO.IsNew						= False
			'	lo_ConnDTO.CanEdit					= False

			'	lo_ConnDTO.ID								= _msgsvrdto.uuid
			'	lo_ConnDTO.Name							= _msgsvrdto.name
			'	lo_ConnDTO.SNC_PartnerName	= _msgsvrdto.sncname
			'	lo_ConnDTO.SystemID					= _msgsvrdto.systemid
			'	lo_ConnDTO.SNC_Active				= CBool( IIf(_msgsvrdto.sncname.Length.Equals(0), False, True) )
			'	lo_ConnDTO.SNC_QOP					= 3	' CInt( lo_SrvDTO.sncop )

			'	If _msgsvrdto.mode.Equals("1")
			'		lo_ConnDTO.AppServer	= _msgsvrdto.server
			'	Else
			'		If Not _msgsvrdto.msid.Length.Equals(0)

			'			Dim lo_MsgDTO					= Me.co_Repos.MsgServers.Item(_msgsvrdto.msid)
			'			lo_ConnDTO.AppServer	= lo_MsgDTO.host

			'		End If

			'	End If

			'	lo_ConnDTO.InstanceNo				= 0
			'	lo_ConnDTO.LowSpeed					= False
			'	lo_ConnDTO.RouterPath				= ""
			'	'..................................................
			'	Return	lo_ConnDTO

			'End Function

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Private: Tree Compiler"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function CompileTree()	As List(Of TreeNode)

			'	Dim lt_List		As New List(Of TreeNode)
			'	'..................................................
			'	For Each lo_WS In Me.co_Repos.WorkSpaces

			'		Dim lo_NodeWS	As New TreeNode()

			'		lo_NodeWS.Text	= lo_WS.Value.name
			'		lo_NodeWS.Name	= lo_WS.Value.uuid

			'		For Each lo_Obj In lo_WS.Value.Nodes

			'			If lo_WS.Value.NodeIsItem

			'				Dim lo_NodeI	= CType(lo_Obj.Value, WSNodeItemDTO)
			'				Dim lo_NodeIt	= Me.CreateItemNode(lo_NodeI.serviceid, Me.co_ConnOptions.XML_OnlyLoadGUI)

			'				If Not IsNothing(lo_NodeIt)

			'					lo_NodeIt.Name	= lo_NodeI.uuid
			'					lo_NodeIt.Tag		= "X"

			'					lo_NodeWS.Nodes.Add(lo_NodeIt)

			'				End If

			'			Else

			'				Dim lo_NodeN	= CType(lo_Obj.Value, WSNodeDTO)
			'				Dim lo_NodeNd	= New TreeNode

			'				lo_NodeNd.Text	= lo_NodeN.name
			'				lo_NodeNd.Name	= lo_NodeN.uuid

			'				For Each lo_Item In lo_NodeN.Items

			'					Dim lo_NodeIt	= Me.CreateItemNode(lo_Item.Value.serviceid, Me.co_ConnOptions.XML_OnlyLoadGUI)

			'					If Not IsNothing(lo_NodeIt)

			'						lo_NodeIt.Name	= lo_Item.Value.uuid

			'						lo_NodeNd.Nodes.Add(lo_NodeIt)

			'					End If

			'				Next

			'				If lo_NodeNd.Nodes.Count > 0	Then	lo_NodeWS.Nodes.Add(lo_NodeNd)

			'			End If

			'		Next
			'		'................................................
			'		If lo_NodeWS.Nodes.Count > 0

			'			lo_NodeWS.Collapse()
			'			lt_List.Add(lo_NodeWS)

			'		End If

			'	Next
			'	'..................................................
			'	Return	lt_List

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function CreateItemNode(ByVal	_id				As String		,
																			ByVal _onlygui	As Boolean		)		As TreeNode

				'Dim lb_Ok		As Boolean				= False
				'Dim lo_Node	As TreeNode				= Nothing
				'Dim lo_Srv	As MsgServiceDTO	= Nothing
				''..................................................
				'If Me.co_Repos.Services.ContainsKey(_id)

				'	lo_Srv	=	Me.co_Repos.Services.Item(_id)

				'	If _onlygui
				'		If lo_Srv.type.Equals("SAPGUI")
				'			lb_Ok	= True
				'		End If
				'	Else
				'		lb_Ok	= True
				'	End If

				'End If
				''..................................................
				'If lb_Ok

				'	lo_Node	= New TreeNode

				'	If lo_Srv.systemid.Length.Equals(0)
				'		lo_Node.Text	= String.Format("{0}", lo_Srv.name)
				'	Else
				'		lo_Node.Text	= String.Format("[{0}] - {1}", lo_Srv.systemid, lo_Srv.name)
				'	End If

				'End If				
				''..................................................
				'Return	lo_Node

			End Function

		#End Region

	End Class

End Namespace
