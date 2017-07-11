Imports System.Threading.Tasks

Imports BxSAP_Excel.UI
Imports BxSAP_Excel.Main.About
'Imports BxSAP_Excel.Main.Config
Imports BxSAP_Excel.Main.Notification.Icon
Imports BxSAP_Excel.Main.SAPLogon
Imports BxSAP_Excel.Main.Session
Imports BxSAP_Excel.Main.Services
Imports BxSAP_Excel.Services.Excel
Imports BxSAP_Excel.Services.Utilities.Generic
Imports BxSAP_Excel.Main.Process.Controller

Imports BxSAP_NCO.API.Main
Imports BxSAP_NCO.API.Destination
Imports BxSAP_NCO.API.About
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Main

	Friend Interface iBxSMainController

		#Region "Methods: Get Controllers"

			Function GetAboutController()					As ixSAPAboutController
			Function GetSAPLogonController()			As ixSAPLogonController
			Function GetSessionController()				As ixSessionController
			Function GetServicesController()			As ixServicesController
			'Function GetConfigController()				As ixSAPConfigController
			Function GetNCOController()						As ixNCOController
			Function GetNotificationController()	As iNotificationIconVM
			Function GetProcessController()				As iBxSProcessController

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: General"

			Function GetSAPFavouritesVM()		As iSAPFavoritesVM
			Function GetServicesGeneric()		As iServicesGeneric
			Function GetExcelHelper()				As iExcelHelper

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Events"
		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Properties"

			ReadOnly	Property	NCOAboutInfo()						As iBxSNCOAboutInfo
			ReadOnly	Property	ActiveDestination()				As iBxSDestination
			ReadOnly	Property	ActiveDestinationID()			As String
			ReadOnly	Property	ActiveUserID()						As String
			ReadOnly	Property	DestinationSelected()			As Boolean

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Destination"

			Function	IsConnectedAsync()																					As Task(of Boolean)
			Function	SetActiveDestination(ByRef logonConfig As iLogonSystemDTO)	As Boolean
			Function	GetDestMonitorDataAsync()																		As Task(Of List(Of iBxSDestMonitorDTO))

		#End Region

	End Interface

End Namespace