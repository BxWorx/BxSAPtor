'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace	Model.Logon.Connections

	Public Class	LogonConnViewDTO
									Implements  iLogonConnViewDTO

		#Region "Properties"

			Property	ID()						As	String		Implements	iLogonConnViewDTO.ID
			Property	Group()					As	String		Implements	iLogonConnViewDTO.Group
			Property	SAPID()					As	String		Implements	iLogonConnViewDTO.SAPID
			Property  Description()		As	String		Implements	iLogonConnViewDTO.Description

		#End Region

	End Class

End Namespace
