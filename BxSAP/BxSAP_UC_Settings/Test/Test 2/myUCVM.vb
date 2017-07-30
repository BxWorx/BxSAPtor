Imports BxSAP_UC_Settings.Helpers
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace myUC

	Public Class myUCVM

		Private co_SaveCmd	As RelayCommand


		Public ReadOnly Property SaveCommand()	As RelayCommand
			Get
				Return	New RelayCommand(AddressOf Save)
			End Get
		End Property


		Public Property Autosave()	As Boolean




		Public Sub New()
		End Sub



		Private Sub Save()
			Dim x = 1
		End Sub


	End Class

End Namespace
