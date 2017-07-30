Imports BxSAP_UC_Settings.Helpers
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Main

	Public Class MainVM

		Public ReadOnly Property SaveCommand()	As RelayCommand
			Get
				Return	New RelayCommand(AddressOf Save)
			End Get
		End Property

		Public Sub New()
		End Sub

		Public Sub Initialise(ByVal xx As String)
			Dim x = xx
		End Sub

		Private Sub Save()
			Dim x = 1
		End Sub

	End Class

End Namespace
