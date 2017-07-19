﻿Imports System.Runtime.Serialization
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Model.Logon.ConnectionSetup

	<DataContract([Namespace]:="")> _
	Public	Class	LogonConnSetupDTO
									Implements iLogonConnSetupDTO

		#Region "Properties"

			<DataMember>	Friend	Property  DestinationID						As	String		Implements	iLogonConnSetupDTO.DestinationID
			<DataMember>	Friend	Property	PeakConnectionLimit			As	Integer		Implements	iLogonConnSetupDTO.PeakConnectionLimit
			<DataMember>	Friend	Property	PoolSize								As	Integer		Implements	iLogonConnSetupDTO.PoolSize
			<DataMember>	Friend	Property	IdleCheckTime						As	Integer		Implements	iLogonConnSetupDTO.IdleCheckTime
			<DataMember>	Friend	Property	ConnectionIdleTimeout		As	Integer		Implements	iLogonConnSetupDTO.ConnectionIdleTimeout
			<DataMember>	Friend	Property  UseManual								As	Boolean		Implements	iLogonConnSetupDTO.UseManual
			'....................................................
			<DataMember>	Friend	Property  SNC_LibPath   					As	String		Implements	iLogonConnSetupDTO.SNC_LibPath
			<DataMember>	Friend	Property  SNC_LibName32						As	String		Implements	iLogonConnSetupDTO.SNC_LibName32
			<DataMember>	Friend	Property  SNC_LibName64						As	String		Implements	iLogonConnSetupDTO.SNC_LibName64
			'....................................................
			<DataMember>	Friend	Property	XML_UseSAPGUI						As	Boolean		Implements	iLogonConnSetupDTO.XML_UseSAPGUI
			<DataMember>	Friend	Property  XML_OnlyLoadGUI					As	Boolean		Implements	iLogonConnSetupDTO.XML_OnlyLoadGUI
			<DataMember>	Friend	Property	XML_FromWorkspace				As	String		Implements	iLogonConnSetupDTO.XML_FromWorkspace
			<DataMember>	Friend	Property	XML_FromNode						As	String		Implements	iLogonConnSetupDTO.XML_FromNode
			<DataMember>	Friend	Property	XML_Path								As	String		Implements	iLogonConnSetupDTO.XML_Path
			<DataMember>	Friend	Property	XML_FileName						As	String		Implements	iLogonConnSetupDTO.XML_FileName

			ReadOnly			Friend	Property	XML_FullName						As	String		Implements	iLogonConnSetupDTO.XML_FullName
				Get
					Return	IO.Path.Combine(Me.XML_Path, Me.XML_FileName)
				End Get
			End Property

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Constructor"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub New()

				Me.DestinationID					= ""
				'..................................................
				Me.PeakConnectionLimit		= 10
				Me.PoolSize								= 10
				Me.IdleCheckTime					= 60
				Me.ConnectionIdleTimeout	= 60
				'..................................................
				Me.SNC_LibPath		= AppDomain.CurrentDomain.BaseDirectory
				Me.SNC_LibName32	= "gsskrb5.dll"
				Me.SNC_LibName64	= "gx64krb5.dll"
				'..................................................
				Me.XML_Path						=	IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "SAP")
				Me.XML_OnlyLoadGUI		= True

				Me.XML_FileName				= "SAPUILandscapeS2A.xml"
				'Me.XML_FromWorkspace	= ""
				Me.XML_FromNode				= ""
				Me.XML_FromWorkspace	= "LEGACY SYSTEMS"
				'Me.XML_FromNode				= "ZA"

			End Sub
		
		#End Region

	End Class

End Namespace