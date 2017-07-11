Imports BxSAP_Excel.Main.Notification.Icon
Imports BxSAP_Excel.Services.Excel

Imports BxSAP_NCO.API.BDC

Imports BxSAP_Excel.Main.Process.Common
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Main.Process.Selection

	Friend Interface ixProcessSelectionVM

		#Region "Properties"
			ReadOnly	Property	IsViewDisposed()	As	Boolean

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Exposed"

			Sub	Show()
			'....................................................
			Function GetOpenWBWSHierarchy()																					As ixProcessSelectionDTO






			Function GetWorkSheetProfile(ByVal WorkBookName		As String,
																	 ByVal WorkSheetName	As String)						As iExcelWSProfileDTO
			Function SubmitTask(ByVal TaskRequest As iBxS_BDCTran_Tran)	As Boolean
		
		#End Region

	End Interface

End Namespace