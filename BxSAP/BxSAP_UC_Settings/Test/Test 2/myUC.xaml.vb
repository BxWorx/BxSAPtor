'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace	myUC

	Public Class myUC

		Public Shared LoadDataProperty As DependencyProperty _
			= DependencyProperty.Register(name					:=	"LoadData"	,
																		propertyType	:=	GetType(ICommand)	,
																		ownerType			:=	GetType(myUC)				)


		Public Property LoadData() As ICommand
			Get
				Return DirectCast(GetValue(LoadDataProperty), ICommand)
			End Get
			Set
				SetValue(LoadDataProperty, value)
			End Set
		End Property


		Public Shared ViewModelProperty	As DependencyPropertyKey _
										= DependencyProperty.RegisterReadOnly("ViewModel"			,
																													GetType(myUCVM)	,
																													GetType(myUC)		,
																													New PropertyMetadata(GetType(myUCVM)) )

		Public ReadOnly Property ViewModel()	As myUCVM
			Get
				Return DirectCast(Resources("VM"), myUCVM)
			End Get
		End Property



			Protected ReadOnly Property Model() As myUCVM
				Get
					Return DirectCast(Resources("VM"), myUCVM)
				End Get
			End Property

	End Class

End Namespace
