'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Logon.ConnectionSetup

	Public Interface iLogonConnSetupDTO

		#Region "Properties"

			Property  DestinationID					As	String
			Property	PeakConnectionLimit		As	Integer
			Property	PoolSize							As	Integer
			Property	IdleCheckTime					As	Integer
			Property  ConnectionIdleTimeout	As	Integer
			Property  UseManual							As	Boolean
			'....................................................
			Property	SNC_LibPath						As	String
			Property	SNC_LibName32					As	String
			Property	SNC_LibName64					As	String
			'....................................................
			Property	XML_UseSAPGUI					As	Boolean
			Property  XML_OnlyLoadGUI				As	Boolean
			Property	XML_FromNode					As	String
			Property	XML_FileName					As	String
			Property	XML_Path							As	String

		#End Region

	End Interface

End Namespace