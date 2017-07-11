Imports System.Threading
Imports	System.Threading.Tasks

Imports	BxSAP_Excel.Main.Process.Selection
Imports	BxSAP_Excel.Services.UI
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Main.Process.Runner.ViaZDTON

	Friend Interface iBxSRunnerViaZDTON

		Sub	Reset()
		'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		Function	StartPostAsync(	ByVal TaskList				As	List(Of ProcessViewTaskDTO)	,
															ByVal ProgressTask		As	IProgress(Of iPBarData)			,
															ByVal ProgressTran		As	IProgress(Of iPBarData)			,
															ByVal Cancel					As	CancellationToken							)		As Task(Of Boolean)

		Function	FetchMessagesAsync(ByVal TaskList				As	List(Of ProcessViewTaskDTO)	,
																 ByVal ProgressTask		As	IProgress(Of iPBarData)			,
																 ByVal Cancel					As	CancellationToken							)		As Task(Of Boolean)

	End Interface

End Namespace