Imports BxSAP_Config.Settings.DTO
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Public Class BaseSettings

	Private Property co_DTO	As BaseDTO


	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.

		Me.co_DTO	= New BaseDTO

		Me.DataContext			= Me.co_DTO
		Me.co_DTO.AutoSave	= True


	End Sub


End Class
