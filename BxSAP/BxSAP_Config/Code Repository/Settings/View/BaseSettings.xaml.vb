Imports BxSAP_Config.Settings.DTO
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Public Class BaseSettings

	Private Property co_DTO	As LogonDTO


	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.

		Me.co_DTO	= New LogonDTO

		Me.DataContext	= Me.co_DTO


	End Sub


End Class
