Imports BxSAP_UC_Settings.BaseSettings
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Public Class BaseSettingsView

	Public Shared ReadOnly SaveCommandProperty As DependencyProperty	= DependencyProperty.Register("SaveClickCommand", GetType(ICommand), GetType(BaseSettingsView))

	Public Property SaveClickCommand() As ICommand
		Get
			Return DirectCast(GetValue(SaveCommandProperty), ICommand)
		End Get
		Set
			SetValue(SaveCommandProperty, value)
		End Set
	End Property



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
