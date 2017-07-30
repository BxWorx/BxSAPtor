'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace	myUC

	Public Class myUC

			Protected ReadOnly Property Model() As myUCVM
				Get
					Return DirectCast(Resources("VM"), myUCVM)
				End Get
			End Property


	End Class

End Namespace
