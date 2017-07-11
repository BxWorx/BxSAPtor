Imports System.Threading.Tasks

Imports BxSAP_Excel.Services.Excel
Imports BxSAP_Excel.Services.UI
Imports BxSAP_Excel.WorksheetDomain

Imports BxSAP_NCO.API.BDC
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Main.Session

	Friend Interface ixSessionController

		#Region "Properties"

			ReadOnly	Property	IsBusy()						As Boolean
			ReadOnly	Property	IsDestinationSet()	As	Boolean

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods"

			Function ConfigToXMLString(ByVal i_BDCConfig	As iBDCConfiguration)					As String
			Function WriteBDCConfigToWSheet(ByVal i_Address    As String,
																			ByVal i_BDCConfig  As iBDCConfiguration)		As Boolean
			Function GetWorkSheetProfile(Optional ByVal WorkBookName  As String = "",
																	 Optional ByVal WorkSheetName As String = "")   As iExcelWSProfileDTO
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Function CreateWSEmpty()																																As Boolean
			Function CreateWSFromSessionAsync(ByVal i_PB	As IProgress(Of iPBarData) )							As Task(Of Integer)
			Function GetSessionListAsync(	Optional	ByVal	i_UserId      As String  = "*"c,
																		Optional	ByVal i_SessionName As String  = "*"c,
																		Optional	ByVal i_DateFrom    As Date    = #1999-01-01#,
																		Optional	ByVal i_DateTo      As Date    = #2999-12-31# )	As Task(Of List(Of iBxSBDCSession_Header))
			Function FetchSessionSelection()																												As iSessionSelectionDTO
			Function SaveSessionSelection(ByVal i_Selection	As iSessionSelectionDTO)								As Boolean
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Function FetchSessionOptions()																				As iSessionOptionsDTO
			Function SaveSessionOptions(ByVal i_Options As iSessionOptionsDTO)		As Boolean
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Sub RibbonEventHandler(ByVal i_Tag As String)

		#End Region

	End Interface

End Namespace