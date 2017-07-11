Imports System.Collections.Concurrent
Imports BxSAP_NCO.API.BDC
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace API.SAPFunctions.BDCRunner
	Friend Interface iBxS_BDCRun_Consumer

		ReadOnly	Property CompletedTasks()	As ConcurrentQueue(Of iBxS_BDCTran_Tran)

		Function StartAsync()	As Task(Of Integer)

	End Interface

End Namespace