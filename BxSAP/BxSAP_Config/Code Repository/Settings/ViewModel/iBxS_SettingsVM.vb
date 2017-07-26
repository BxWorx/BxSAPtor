Imports BxSAP_Config.Settings.DTO
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Settings.VM

	Friend Interface iBxS_SettingsVM

		#Region "Properties"
			Property ConnectionHistoryLimit()	As UShort
			Property LogonHistoryLimit()			As UShort
			Property AutoSaveReposOnClose()		As Boolean

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Exposed"

			Sub Open_Repository
			Sub Save_Repository()
			Sub Close_Repository()
			'....................................................
			Function GetLogonSettings()															As LogonDTO
			Function UpdateLogonSettings(ByVal DTO	As LogonDTO)		As Boolean
			'....................................................
			Function GetConnectionSettings()																As ConnectionDTO
			Function UpdateConnectionSettings(ByVal DTO	As ConnectionDTO)		As Boolean

		#End Region

	End Interface

End Namespace