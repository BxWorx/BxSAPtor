Imports BxSAP_UC_Settings.Helpers
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Friend Class ucvm
							Inherits ViewModelBase

	Private _canuse	As Boolean
	Public Property canuse()	As Boolean
		Get
			Return	Me._canuse
		End Get
	    Set(value As Boolean)
				Me._canuse	= value
				Me.canexec	= Me._canuse
	    End Set
	End Property


	Private _canexec	As Boolean
	Public Property canexec()	As Boolean
		Get
			Return	Me._canexec
		End Get
	    Set(value As Boolean)
				Me.SetProperty(_canexec,value)
	    End Set
	End Property

	Public Property mytext()	As String



	'Friend Sub New(ByVal _dto	As BaseDTO)



	'End Sub

End Class
