Imports BxSAP_Config.Controllers
Imports	BxSAPtor_V02.Helpers
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Public Class AppVM
							Inherits	Helpers.ViewModelBase


		Public ReadOnly Property ChangePageCommand() As ICommand
			Get
				If Me.co_ChangeVWCmd Is Nothing Then

					'Me.co_ChangeVWCmd	= New RelayCommand(Of iUCViewModel)(Function(param) AddressOf ChangeVM)

					Me.co_ChangeVWCmd	= New	RelayCommand(Of iUCViewModel)(AddressOf ChangeVM, Function(p)
																																											Dim x = True
																																											Dim y = TypeOf p Is iUCViewModel
																																											Return x
					                 	     	                                                      'Return TypeOf p Is iUCViewModel
					                 	     	                                                  End Function)
					'Me.co_ChangeVWCmd	= New RelayCommand(Function(p) AddressOf ChangeVM()), Function(p) TypeOf p Is iUCViewModel)
				End If

				Return Me.co_ChangeVWCmd
			End Get
		End Property



	'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
	#Region "Constructors"

		Private co_Config	As iController

		Private co_ChangeVWCmd	As ICommand
		Private ct_UCVMList			As List(Of iUCViewModel)
		Private co_CurrUCVM			As iUCViewModel

		'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		Public Sub New()

			Me.cc_Name	= "AppVM"

			'Me.co_Config	= 

		End Sub

	#End Region
	'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
	#Region "Methods: Private"

		Private Sub ChangeVM(ByVal _vm	As iUCViewModel)

			If Not Me.ct_UCVMList.Contains(_vm) Then
				Me.ct_UCVMList.Add(_vm)
			End If

			Me.co_CurrUCVM = Me.ct_UCVMList.FirstOrDefault(Function(vm) vm Is _vm)

		End Sub

	#End Region

End Class
