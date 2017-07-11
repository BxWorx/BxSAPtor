Imports System.Runtime.Serialization
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Logon.Systems

	<DataContract([Namespace]:="")> _
	Friend	Class SysReposDTO
									Implements	iSysReposDTO

		#Region "Definitions"

			<DataMember>	Private	ct_Systems		As	Dictionary(Of String, SysReposSystemDTO)

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Properties"

			Friend Property	Systems		As Dictionary(Of String, iSysReposSystemDTO)		Implements	iSysReposDTO.Systems

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Constructor"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub New()

				Me.Systems			=	New	Dictionary(Of String, iSysReposSystemDTO)
				Me.ct_Systems		=	New	Dictionary(Of String, SysReposSystemDTO)

			End Sub

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Exposed"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub	PrepareBeforeSave() _
										Implements	iSysReposDTO.PrepareBeforeSave

				Me.ct_Systems		= New Dictionary(Of String, SysReposSystemDTO)

				Me.ct_Systems		= (	From lo In Me.Systems ).ToDictionary(
															Function(lo) lo.Key, 
															Function(lo) CType( lo.Value, SysReposSystemDTO ) )
				
			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub	PrepareAfterLoad() _
										Implements	iSysReposDTO.PrepareAfterLoad

				Me.Systems		= New Dictionary(Of String, iSysReposSystemDTO)

				Me.Systems		= (	From lo In Me.ct_Systems ).ToDictionary(
													Function(lo) lo.Key, 
													Function(lo) CType( lo.Value, iSysReposSystemDTO )	)
				
			End Sub

		#End Region

	End Class

End Namespace