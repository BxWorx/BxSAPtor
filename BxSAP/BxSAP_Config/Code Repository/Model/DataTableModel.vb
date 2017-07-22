	'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Settings

	Friend Class SettingDataModel(Of T	As DataTable)

		#Region "Definitions"

			Private co_DataTable		As T

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Exposed"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function DeleteVersion(Optional ByVal VersionNo	As Short		= 1	,
																		Optional ByVal Commit			As Boolean	= True)	As Boolean

				'If VersionNo = 0	Then	Return False
				''..................................................
				'Dim lo_Row	As BxSAPConfig_Settings.LogonSettingsRow	=	Me.co_DataTable.FindByVersion(VersionNo)
				'If IsNothing(lo_Row)	Then	Return False
				''..................................................
				'Me.co_DataTable.RemoveLogonSettingsRow(lo_Row)
				'If Commit		Then Me.co_DataTable.AcceptChanges

				Return	True

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function UpdateOptions(ByVal Settings	As BxSAPConfig_Settings.LogonSettingsRow)	As Boolean

				'If Not Settings.Version = 0		Then	Return False
				''..................................................
				'Dim lo_Row	As BxSAPConfig_Settings.LogonSettingsRow	= Me.CreateNewRow()

				'For Each	lo_Col As DataColumn	In Me.co_DataTable.Columns
				'	If Not lo_Col.ReadOnly
				'		lo_Row.SetField(lo_Col, Settings.Item(lo_Col)	)
				'	End If
				'Next
				''..................................................
				'Me.co_DataTable.AcceptChanges()
				''Settings.AcceptChanges()
				Return	True

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetOptions(Optional ByVal VersionNo As Short = 0)	As DataRow

				Dim lo_Row	As DataRow

			
				'lo_Row	=	Me.co_DataTable.FindByVersion(VersionNo)
				''..................................................
				'If IsNothing(lo_Row)	AndAlso	VersionNo = 0
				'	lo_Row	=	Me.CreateNewRow()
				'End If
				'..................................................
				Return	lo_Row

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetOptionsVersions()	As Dictionary(Of Short, DateTime)

				Dim lt_Ret	As Dictionary(Of Short, DateTime)		= New Dictionary(Of Short, Date)
				'..................................................
				For Each	lo_Row As DataRow		In Me.co_DataTable.Rows()

					Dim ln_VerNo	As Short			= CShort(	lo_Row.Item("Version")		)
					Dim ld_Date		As DateTime		= CDate(	lo_Row.Item("TimeStamp")	)

					lt_Ret.Add(ln_VerNo, ld_Date)

				Next
				'..................................................
				Return	lt_Ret

			End Function

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Private"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function CreateNewRow()	As DataRow

				Dim lo_Row	As DataRow	=	Me.co_DataTable.NewRow()
				'..................................................
				lo_Row.Item("TimeStamp")	= Now()
				Me.co_DataTable.Rows.Add(lo_Row)
				'..................................................
				Return	lo_Row

				'lo_Row.Field(Of Date)("TimeStamp")	= Now()

			End Function

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Constructor"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub New(ByVal _table As T)

				Me.co_DataTable	= _table

			End Sub

		#End Region

	End Class

End Namespace