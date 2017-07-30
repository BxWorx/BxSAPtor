Namespace Main

	Class MainWindow

		Private co_VM	As Main.MainVM

		Protected ReadOnly Property VM()	As MainVM
			Get
				Return	Me.co_VM
			End Get
		End Property



		Public Sub New()

			' This call is required by the designer.
			InitializeComponent()

			' Add any initialization after the InitializeComponent() call.

			Me.co_VM	= DirectCast(Resources("VM"), MainVM)

			Me.co_VM.Initialise("SS")

		End Sub




'Private _upJogRelayCommand As RelayCommand(Of Object)
'Public ReadOnly Property UpJogRelayCommand() As ICommand
'	Get
'		If _upJogRelayCommand Is Nothing Then
'			_upJogRelayCommand = New RelayCommand(Of Object)(AddressOf UpJogMove)
'		End If
'		Return _upJogRelayCommand
'	End Get
'End Property

'Private Sub UpJogMove(notUsed As Object)
'	Debug.Print("UpJogExcuted():")
'	'MoveToUpDirection()
'End Sub


	End Class

End Namespace
