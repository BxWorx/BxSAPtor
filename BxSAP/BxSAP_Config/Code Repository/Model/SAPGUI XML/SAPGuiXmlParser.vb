'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace	Model.Sapgui.Xml

	Friend Class SAPGuiXmlParser

		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Definitions"

			Private	co_Repos			As SAPSysRepository
			Private co_XMLRepos		As SAPGuiXmlLoader

			Private	cn_Lev1				As Integer
			Private	cn_Lev2				As Integer
			Private	cn_Lev3				As Integer

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Exposed"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function Parse(ByVal	XMLRepos	As SAPGuiXmlLoader	,
														ByVal	SysRepos	As SAPSysRepository		)	As Boolean

				Dim	lb_Ret	= True
				'....................................................
				Me.co_XMLRepos	= XMLRepos
				Me.co_Repos			= SysRepos
				'....................................................
				Me.cn_Lev1			= 0
				Me.cn_Lev2			= 0
				Me.cn_Lev3			= 0
				'....................................................
				Me.ParseMsgServers()
				Me.ParseServices()
				Me.ParseWorkspaces()
				'....................................................
				Return	lb_Ret

			End Function

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Private"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Sub ParseWorkspaces()

				For Each	lo_WS		In Me.co_XMLRepos.WorkSpaces

					If Me.Parse(lo_WS.Value)

					End If

					'ln_HLev2	= 0
					'For Each	lo_Node		In lo_WS.Value.Nodes

					'	ln_HLev2	+= 1
					'	lc_HLev2	= String.Format("{0}.{1}",lc_HLev1, ln_HLev2.ToString("D2") )
					'	Me.co_Repos.Workspace.AddWorkspaceRow(Me.Parse(lo_Node.Value, lc_HLev2))

					'	ln_HLev3	= 0
					'	For Each	lo_Item		In lo_Node.Value.Items

					'		ln_HLev3	+= 1
					'		lc_HLev3	= String.Format("{0}.{1}",lc_HLev2, ln_HLev3.ToString("D2") )
					'		Me.co_Repos.Workspace.AddWorkspaceRow(Me.Parse(lo_Node.Value, lc_HLev3))

					'	Next

					'Next

				Next

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function Parse(ByVal _dto As WorkspaceDTO)	As Boolean

				Dim lb_Ret	As Boolean
				Dim lc_Lev	As String
				Dim lo_Row	As SAPSysRepository.WorkspaceRow
				'....................................................
				Me.cn_Lev1	+= 1

				lc_Lev			= Me.cn_Lev1.ToString("D2")
				lo_Row			= Me.CreateWSRow(	_dto.uuid, _dto.name, lc_Lev, "", "" )
				lb_Ret			= True
				'....................................................
				If IsNothing(lo_Row)
					lb_Ret			= False
					Me.cn_Lev1	-= 1
				Else
					Me.co_Repos.Workspace.AddWorkspaceRow(lo_Row)
				End If
				'....................................................
				Return	lb_Ret

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function Parse(ByVal _dto	As WSNodeDTO	, 
														 ByVal _hier		As String				)	As SAPSysRepository.WorkspaceRow

				'Return	Me.CreateWSRow(_dto.uuid, _dto.name, _hier, Nothing, Nothing)

				'Dim lo_Guid	As	Guid
				'Dim lo_Row	As	SAPSysRepository.WorkspaceRow		= Me.co_Repos.Workspace.NewWorkspaceRow()
				''....................................................
				'Guid.TryParse( _dto.uuid, lo_Guid)

				'lo_Row.UUID								= lo_Guid
				'lo_Row.Description				= _dto.name
				'lo_Row.Hierachy_Notation	= _hier
				''....................................................
				'Return	lo_Row

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function Parse(ByVal _dto	As WSNodeItemDTO	, 
														 ByVal _hier		As String						)	As SAPSysRepository.WorkspaceRow

				'Return	Me.CreateWSRow(_dto.uuid, _dto.name, _hier, Nothing, Nothing)

				'Dim lo_Guid	As	Guid
				'Dim lo_Row	As	SAPSysRepository.WorkspaceRow		= Me.co_Repos.Workspace.NewWorkspaceRow()
				''....................................................
				'Guid.TryParse( _dto.uuid, lo_Guid)

				'lo_Row.UUID								= lo_Guid
				'lo_Row.Hierachy_Notation	= _hier
				''....................................................
				'Return	lo_Row

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function CreateWSRow(	ByVal	_id		As String,
																		ByVal	_desc	As String	,
																		ByVal	_not	As String ,
																		ByVal	_par	As String	,
																		ByVal _ser	As String		)	As SAPSysRepository.WorkspaceRow

				Dim lo_uGuid	As Guid		= Nothing
				Dim lo_pGuid	As Guid		= Nothing
				Dim lo_sGuid	As Guid		= Nothing

				Dim lo_Row		As SAPSysRepository.WorkspaceRow
				'....................................................
				lo_Row	= Me.co_Repos.Workspace.NewWorkspaceRow()
				'....................................................
				If Not IsNothing(_id)
					If Not _id.Length.Equals(0)	Then	Guid.TryParse( _id, lo_uGuid)
				End If

				If IsNothing(lo_uGuid)		Then	Return	Nothing
				'....................................................
				If Not _par.Length.Equals(0)	Then	Guid.TryParse( _id, lo_pGuid)
				If Not _ser.Length.Equals(0)	Then	Guid.TryParse( _id, lo_sGuid)
				'....................................................
				lo_Row.UUID								= lo_uGuid
				lo_Row.Parent_uuid				= lo_pGuid
				lo_Row.Service_uuid				= lo_sGuid

				lo_Row.Hierachy_Notation	= _not
				'....................................................
				Return	lo_Row

			End Function

			'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Sub ParseServices()

				For Each	lo	In Me.co_XMLRepos.Services
					Me.co_Repos.Service.AddServiceRow(Me.Parse(lo.Value))
				Next

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function Parse(ByVal DTOObject As MsgServiceDTO)	As SAPSysRepository.ServiceRow

				Dim lc_Guid	As	Guid
				Dim lo_Row	As	SAPSysRepository.ServiceRow		= Me.co_Repos.Service.NewServiceRow()
				'....................................................
				Guid.TryParse( DTOObject.uuid, lc_Guid)
				lo_Row.UUID						= lc_Guid

				Guid.TryParse( DTOObject.msid, lc_Guid)
				lo_Row.MsgSvrID				= lc_Guid

				lo_Row.Name						= DTOObject.name
				lo_Row.Description		= DTOObject.description
				lo_Row.DownUpCodePage	= DTOObject.dcpg
				lo_Row.SAPCodePage		= DTOObject.sapcpg
				lo_Row.Server					= DTOObject.server
				lo_Row.SNCName				= DTOObject.sncname
				lo_Row.SNCOP					= CShort( DTOObject.sncop )
				lo_Row.SystemID				= DTOObject.systemid
				lo_Row.Type						= DTOObject.type
				'....................................................
				Return	lo_Row

			End Function

			'°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°°

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Sub ParseMsgServers()

				For Each	lo	In Me.co_XMLRepos.MsgServers
					Me.co_Repos.MsgServer.AddMsgServerRow(Me.Parse(lo.Value))
				Next

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function Parse(ByVal	DTOObject	As MsgServerDTO)	As SAPSysRepository.MsgServerRow

				Dim lo_Guid	As	Guid
				Dim lo_Row	As	SAPSysRepository.MsgServerRow		= Me.co_Repos.MsgServer.NewMsgServerRow()
				'....................................................
				Guid.TryParse( DTOObject.uuid, lo_Guid)

				lo_Row.UUID					= lo_Guid
				lo_Row.Name					= DTOObject.name
				lo_Row.Port					= DTOObject.port
				lo_Row.Description	= DTOObject.description
				'....................................................
				Return	lo_Row

			End Function

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Constructors"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub New()

				Me.co_Repos	= New	SAPSysRepository()

			End Sub

		#End Region

	End Class

End Namespace
