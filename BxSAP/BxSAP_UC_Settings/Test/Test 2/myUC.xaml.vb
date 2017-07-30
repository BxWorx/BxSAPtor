'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace	myUC

	Public Class myUC

		Public Shared LoadDataProperty As DependencyProperty _
			= DependencyProperty.Register(name					:=	"LoadDTOCommand"	,
																		propertyType	:=	GetType(ICommand)	,
																		ownerType			:=	GetType(myUC)				)


		Public Property LoadDTOCommand() As ICommand
			Get
				Return DirectCast(GetValue(LoadDataProperty), ICommand)
			End Get

			Set
				SetValue(LoadDataProperty, value)
			End Set
		End Property




			Protected ReadOnly Property Model() As myUCVM
				Get
					Return DirectCast(Resources("VM"), myUCVM)
				End Get
			End Property







	End Class

End Namespace
