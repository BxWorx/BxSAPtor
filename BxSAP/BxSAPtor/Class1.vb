
''' <summary>
''' A command whose sole purpose is to 
''' relay its functionality to other
''' objects by invoking delegates. The
''' default return value for the CanExecute
''' method is 'true'.
''' </summary>
Public Class RelayCommand
	Implements ICommand
	#Region "Fields"

	ReadOnly _execute As Action(Of Object)
	ReadOnly _canExecute As Predicate(Of Object)
	Private _displayText As String

	#End Region

	#Region "Constructors"

	''' <summary>
	''' Creates a new command that can always execute.
	''' </summary>
	''' <param name="execute">The execution logic.</param>
	Public Sub New(execute As Action(Of Object))
		Me.New(execute, Nothing)
	End Sub

	''' <summary>
	''' Creates a new command.
	''' </summary>
	''' <param name="execute">The execution logic.</param>
	''' <param name="canExecute">The execution status logic.</param>
	Public Sub New(execute As Action(Of Object), canExecute As Predicate(Of Object))
		If execute Is Nothing Then
			Throw New ArgumentNullException("execute")
		End If

		_execute = execute
		_canExecute = canExecute
	End Sub

	Public Property DisplayText() As String
		Get
			Return _displayText
		End Get
		Set
			_displayText = value
		End Set
	End Property

	#End Region

	#Region "ICommand Members"

	Public Sub Execute(parameter As Object) Implements ICommand.Execute
		_execute(parameter)
	End Sub

	<DebuggerStepThrough> _
	Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
		Return If(_canExecute Is Nothing, True, _canExecute(parameter))
	End Function

	Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

		AddHandler(ByVal value As EventHandler)
			AddHandler CommandManager.RequerySuggested, value
		End AddHandler

		RemoveHandler(ByVal value As EventHandler)
			RemoveHandler	CommandManager.RequerySuggested, value
		End RemoveHandler

		 RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
				CommandManager.InvalidateRequerySuggested()
		End RaiseEvent

	End Event


	#End Region
End Class
