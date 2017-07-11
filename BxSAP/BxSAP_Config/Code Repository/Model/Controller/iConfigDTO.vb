Imports BxSAP_Config.Model.Logon.Options
Imports BxSAP_Config.Model.Logon.ConnectionSetup
Imports BxSAP_Config.Model.Logon.Connections
Imports BxSAP_Config.Model.Logon.Systems
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Controller.Config

	Public Interface iConfigDTO

		#Region "Properties"

			Property LogonOptionsDTO()									As iLogonOptionsDTO
			Property LogonConnectionSetupDTO()					As iLogonConnSetupDTO
			Property LogonConnectionsRepositoryDTO()		As iLogonConnReposDTO
			'....................................................
			Property SystemLanguagesDTO()					As iSystemLanguagesDTO
			Property SystemRepositoryDTO()				As iSysReposDTO
			Property SystemLogonRepositoryDTO()		As iSysLogonRepositoryDTO

		#End Region

	End Interface

End Namespace