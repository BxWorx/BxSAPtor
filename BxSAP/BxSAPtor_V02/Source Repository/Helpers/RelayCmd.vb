
Imports System.Diagnostics
Imports System.Windows.Input


Namespace Helpers

	Public Class RelayCommand(Of T)
		Implements ICommand

		#Region "Declarations"

		ReadOnly _canExecute As Predicate(Of T)
		ReadOnly _execute As Action(Of T)

		#End Region

		#Region "Constructors"

		Public Sub New(execute As Action(Of T))
			Me.New(execute, Nothing)
		End Sub

		Public Sub New(execute As Action(Of T), canExecute As Predicate(Of T))

			If execute Is Nothing Then
				Throw New ArgumentNullException("execute")
			End If
			_execute = execute
			_canExecute = canExecute
		End Sub

		#End Region

		#Region "ICommand Members"

		Public Custom Event CanExecuteChanged As EventHandler	Implements ICommand.CanExecuteChanged
			AddHandler(ByVal value As EventHandler)

				If _canExecute IsNot Nothing Then
					AddHandler CommandManager.RequerySuggested, value
				End If
			End AddHandler

			RemoveHandler(ByVal value As EventHandler)

				If _canExecute IsNot Nothing Then
					RemoveHandler	CommandManager.RequerySuggested, value
				End If
			End RemoveHandler

				RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
					CommandManager.InvalidateRequerySuggested()
				End RaiseEvent

		End Event

		<DebuggerStepThrough> _
		Public Function CanExecute(parameter As [Object]) As [Boolean] Implements ICommand.CanExecute
			Return If(_canExecute Is Nothing, True, _canExecute(DirectCast(parameter, T)))
		End Function

		Public Sub Execute(parameter As [Object]) Implements ICommand.Execute

			_execute(DirectCast(parameter, T))
		End Sub

		#End Region
	End Class

	'================================================================================================

	Public Class RelayCommand
		Implements ICommand

		#Region "Declarations"

		ReadOnly _canExecute As Func(Of [Boolean])
		ReadOnly _execute As Action

		#End Region

		#Region "Constructors"

		Public Sub New(execute As Action)
			Me.New(execute, Nothing)
		End Sub

		Public Sub New(execute As Action, canExecute As Func(Of [Boolean]))

			If execute Is Nothing Then
				Throw New ArgumentNullException("execute")
			End If
			_execute = execute
			_canExecute = canExecute
		End Sub

		#End Region

		#Region "ICommand Members"

		Public Custom Event CanExecuteChanged As EventHandler	Implements ICommand.CanExecuteChanged
			AddHandler(ByVal value As EventHandler)

				If _canExecute IsNot Nothing Then
					AddHandler CommandManager.RequerySuggested, value
				End If
			End AddHandler

			RemoveHandler(ByVal value As EventHandler)

				If _canExecute IsNot Nothing Then
					RemoveHandler	CommandManager.RequerySuggested, value
				End If
			End RemoveHandler

				RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
					CommandManager.InvalidateRequerySuggested()
				End RaiseEvent

		End Event

		<DebuggerStepThrough> _
		Public Function CanExecute(parameter As [Object]) As [Boolean] Implements ICommand.CanExecute
			Return If(_canExecute Is Nothing, True, _canExecute())
		End Function

		Public Sub Execute(parameter As [Object]) Implements ICommand.Execute

			_execute()
		End Sub

		#End Region
	End Class
End Namespace
