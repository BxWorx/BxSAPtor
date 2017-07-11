Imports SAPNCO = SAP.Middleware.Connector

Imports BxSAP_NCO.API.Destination
Imports BxSAP_NCO.API.SAPFunctions.ZDTON
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace SAPFunctions.ZDTON

	Friend Interface iBxS_ZDTON_Profile

							Property	NoofConsumers			As Integer
		'ReadOnly  Property	GetDTO						As iBxS_ZDTON_DTO
		ReadOnly	Property	SAPrfcDestination	As SAPNCO.RfcCustomDestination
		ReadOnly	Property	rfcDestination		As iBxSDestination

	End Interface

End Namespace