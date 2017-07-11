Imports	BxSAP_Utilities.Serialization
Imports	BxSAP_Utilities.Convertors
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Controllers
	Public Interface iController

		#Region "Section: Factory"

			Function CreateCloner()				As iClone
			Function CreateSerialiser()		As iSerialiser

		#End Region

	End Interface

End Namespace