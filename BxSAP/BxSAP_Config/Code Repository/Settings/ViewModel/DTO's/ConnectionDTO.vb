'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace	Settings.DTO

	Friend	Class	ConnectionDTO

		#Region "Properties"

			Friend	Property	PeakConnectionLimit			As	Integer
			Friend	Property	PoolSize								As	Integer
			Friend	Property	IdleCheckTime						As	Integer
			Friend	Property	ConnectionIdleTimeout		As	Integer
			Friend	Property  UseManual								As	Boolean
			'....................................................
			Friend	Property  SNC_LibPath   					As	String
			Friend	Property  SNC_LibName32						As	String
			Friend	Property  SNC_LibName64						As	String
			'....................................................
			Friend	Property	XML_UseSAPGUI						As	Boolean
			Friend	Property  XML_OnlyLoadGUI					As	Boolean
			Friend	Property	XML_FromWorkspace				As	String
			Friend	Property	XML_FromNode						As	String
			Friend	Property	XML_Path								As	String
			Friend	Property	XML_FileName						As	String

		#End Region

	End Class

End Namespace