'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Settings

	Friend Class SettingReposDataModel

		#Region "Definitions"

			Private cc_Name							As	String
			Private cb_Open							As	Boolean
			Private	cb_Dirty						As  Boolean
			Private	cb_DirtyBase				As  Boolean
			'....................................................
			Private co_Repos						As	BxSAPConfig_Settings
			'....................................................
			Private	co_LogonSettings		As	SettingTableDataModel(Of BxSAPConfig_Settings.LogonSettingsDataTable)
			Private	co_ConnSettings			As	SettingTableDataModel(Of BxSAPConfig_Settings.ConnectionSetupDataTable)
			Private	co_BaseSettings			As	SettingTableDataModel(Of BxSAPConfig_Settings.BaseSetupDataTable)
			'....................................................
			Private	cs_BaseSettings			As	BxSAPConfig_Settings.BaseSetupRow

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Properties"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Property	AutoSave()	As Boolean
				Get
					Return	Me.cs_BaseSettings.AutoSaveOnClose
				End Get
			  Set(value As Boolean)
					If Me.cs_BaseSettings.AutoSaveOnClose <> value

						Me.cs_BaseSettings.AutoSaveOnClose	= value
						Me.cb_DirtyBase											= True

					End If
			  End Set
			End Property
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend ReadOnly Property	IsDirty()	As Boolean
				Get
					Return	Me.cb_Dirty
				End Get
			End Property

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Exposed"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub SetHistoryLimit(						ByVal Maximum As UShort												,
																	Optional	ByVal	LogonSettings				As Boolean	= True	,
																	Optional	ByVal	ConnectionSettings	As Boolean	= True		)

				If Not Me.cb_Open		Then	Exit Sub
				'..................................................
				If LogonSettings
					If Me.cs_BaseSettings.LogonLimit <> Maximum

						Me.co_LogonSettings.SetHistoryLimit(Maximum)
						Me.cs_BaseSettings.LogonLimit	= Maximum
						Me.cb_DirtyBase								= True

					End If
				End If
				'................................................
				If ConnectionSettings
					If Me.cs_BaseSettings.ConnLimit	<> Maximum

						Me.co_ConnSettings.SetHistoryLimit(Maximum)
						Me.cs_BaseSettings.ConnLimit	= Maximum
						Me.cb_DirtyBase								= True

					End If
				End If

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub ResetHistory(Optional	ByVal	LogonSettings				As Boolean	= True	,
															Optional	ByVal	ConnectionSettings	As Boolean	= True		)
			
				If Not Me.cb_Open			Then	Exit Sub
				'..................................................
				If LogonSettings			Then	Me.co_LogonSettings.ResetHistory()
				If ConnectionSettings	Then	Me.co_ConnSettings.ResetHistory()

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetConnectionHistory(Optional ByVal IncludeCurrent As Boolean = False)	As Dictionary(Of UShort, DateTime)

				Dim lt_Ret	= New Dictionary(Of UShort, Date)

				If Me.cb_Open
					lt_Ret	=	Me.co_ConnSettings.GetHistoryList(IncludeCurrent)
				End If
				'..................................................
				Return	lt_Ret

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function SaveConnectionSettings(ByVal Settings	As BxSAPConfig_Settings.ConnectionSetupRow)	As Boolean

				If Me.cb_Open
					Return	Me.co_ConnSettings.CommitSettings(Settings)
				Else
					Return	Nothing
				End If

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetConnectionSettings()	As BxSAPConfig_Settings.ConnectionSetupRow

				If Me.cb_Open
					Return	CType(Me.co_ConnSettings.GetSettings(), BxSAPConfig_Settings.ConnectionSetupRow)
				Else
					Return	Nothing
				End If

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetLogonHistory(Optional ByVal IncludeCurrent As Boolean = False)	As Dictionary(Of UShort, DateTime)

				Dim lt_Ret	= New Dictionary(Of UShort, Date)
				'..................................................
				If Me.cb_Open
					lt_Ret	=	Me.co_LogonSettings.GetHistoryList(IncludeCurrent)
				End If
				'..................................................
				Return	lt_Ret

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function SaveLogonSettings(ByVal Settings	As BxSAPConfig_Settings.LogonSettingsRow)	As Boolean

				If Me.cb_Open
					Return	Me.co_LogonSettings.CommitSettings(Settings)
				Else
					Return	Nothing
				End If

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetLogonSettings()	As BxSAPConfig_Settings.LogonSettingsRow

				If Me.cb_Open
					Return	CType(Me.co_LogonSettings.GetSettings(), BxSAPConfig_Settings.LogonSettingsRow)
				Else
					Return	Nothing
				End If

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub Save()

				If Me.cb_Open

					If Me.cb_DirtyBase

						Me.co_BaseSettings.CommitSettings(Me.cs_BaseSettings)
						Me.cb_DirtyBase	= False
						Me.cb_Dirty			= True

					End If
					'................................................
					If Me.cb_Dirty

						Me.co_Repos.WriteXml(Me.cc_Name)
						Me.cb_Dirty	= False

					End If
				End If

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub Close()
			
				RemoveHandler		Me.co_LogonSettings.ev_DataChanged	,	AddressOf SetDirtyFlag
				RemoveHandler		Me.co_ConnSettings.ev_DataChanged		, AddressOf SetDirtyFlag
				RemoveHandler		Me.co_BaseSettings.ev_DataChanged		, AddressOf SetDirtyFlag
				'..................................................
				If Me.cs_BaseSettings.AutoSaveOnClose
					Me.Save()
				End If
				'..................................................
				Me.cb_Open	= False

			End Sub

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Private"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Sub SetDirtyFlag

				Me.cb_Dirty	= True

			End Sub

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Constructor"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub New(ByVal _name	As String)

				Me.co_Repos		=	New	BxSAPConfig_Settings
				'..................................................
				Me.co_LogonSettings		= New SettingTableDataModel(Of BxSAPConfig_Settings.LogonSettingsDataTable)		(Me.co_Repos.LogonSettings)
				Me.co_ConnSettings		= New SettingTableDataModel(Of BxSAPConfig_Settings.ConnectionSetupDataTable)	(Me.co_Repos.ConnectionSetup)
				Me.co_BaseSettings		= New SettingTableDataModel(Of BxSAPConfig_Settings.BaseSetupDataTable)				(Me.co_Repos.BaseSetup)
				'..................................................
				Me.cc_Name				= _name

				Me.cb_Dirty				= False
				Me.cb_DirtyBase		= False
				Me.cb_Open				= False
				'..................................................
				If IO.File.Exists(Me.cc_Name)
					Me.co_Repos.ReadXml(Me.cc_Name)
				End If
				'..................................................
				Me.cb_Open						= True
				Me.cs_BaseSettings		= CType(Me.co_BaseSettings.GetSettings(), BxSAPConfig_Settings.BaseSetupRow)
				'..................................................
				AddHandler	Me.co_LogonSettings.ev_DataChanged	,	AddressOf SetDirtyFlag
				AddHandler	Me.co_ConnSettings.ev_DataChanged		, AddressOf SetDirtyFlag
				AddHandler	Me.co_BaseSettings.ev_DataChanged		, AddressOf SetDirtyFlag

			End Sub

		#End Region

	End Class

End Namespace