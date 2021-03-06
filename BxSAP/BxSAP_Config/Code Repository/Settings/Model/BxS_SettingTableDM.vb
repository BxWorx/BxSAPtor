﻿'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Settings.Model

	Friend Class SettingTableDM(Of T	As DataTable)

		#Region "Definitions"

			Private Const cz_NmeTime		As String	= "TimeStamp"
			'....................................................
			Private co_DataTable		As T
			Private cn_MaxRows			As UShort

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Exposed"

			Friend Event ev_DataChanged()

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub ResetHistory()

				If Me.EnsureLimit(1)

					Me.co_DataTable.AcceptChanges()
					RaiseEvent	ev_DataChanged()
					
				End If

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetHistoryList(Optional ByVal IncludeCurrent As Boolean = False)	As Dictionary(Of UShort, DateTime)

				Dim lt_Ret	= New Dictionary(Of UShort, Date)
				'..................................................
				For Each	lo_Row As DataRow		In Me.co_DataTable.Rows()

					Dim ln_Index	As UShort		=	CUShort( Me.co_DataTable.Rows().IndexOf(lo_Row) )
					Dim ld_Date		As DateTime	= CDate( lo_Row.Item(cz_NmeTime) )

					lt_Ret.Add(ln_Index, ld_Date)

				Next
				'..................................................
				Return	lt_Ret

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub SetHistoryLimit(ByVal Maximum As UShort)
				
				Me.cn_MaxRows	= Maximum
				'..................................................
				If Me.EnsureLimit(Me.cn_MaxRows)

					Me.co_DataTable.AcceptChanges()
					RaiseEvent	ev_DataChanged()
					
				End If

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function CommitSettings(ByVal Settings As DataRow)	As Boolean

				Dim lb_Ret	As Boolean	= True
				Dim lo_Row	As DataRow	= Me.CreateCopy(Settings)
				'..................................................
				lo_Row.Item(cz_NmeTime)	= Now()
				Me.co_DataTable.Rows.InsertAt(lo_Row, 0)
				Me.EnsureLimit(Me.cn_MaxRows)
				Me.co_DataTable.AcceptChanges()
				RaiseEvent	ev_DataChanged()
				'..................................................
				Return	lb_Ret

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetSettings(Optional ByVal VersionNo	As UShort	= 0)	As DataRow

				Dim lo_Row	As DataRow
				'..................................................
				If Me.co_DataTable.Rows.Count > VersionNo
					lo_Row	= Me.CreateCopy(Me.co_DataTable.Rows(VersionNo))
				Else
					lo_Row	= Me.GetDefaulRow()
					lo_Row.Item(cz_NmeTime)	= Now()
				End If
				'..................................................
				Return	lo_Row

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Function GetDefaulRow()	As DataRow

				Return	Me.co_DataTable.NewRow()

			End Function

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Methods: Private"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function CreateCopy(ByVal _src As DataRow)	As DataRow

				Dim lo_Row	= Me.GetDefaulRow()
				'..................................................
				For Each	lo_Col As DataColumn	In Me.co_DataTable.Columns
					If Not lo_Col.ReadOnly
						lo_Row.SetField(lo_Col, _src.Item(lo_Col)	)
					End If
				Next
				'..................................................
				Return	lo_Row

			End Function
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Private Function EnsureLimit(ByVal _limit As UShort)	As Boolean

				Dim lb_Chg	As Boolean	= False
				'..................................................
				If Me.co_DataTable.Rows.Count > _limit

					For	ln_I As Integer		= (Me.co_DataTable.Rows.Count - 1)	To	_limit	Step -1

						Me.co_DataTable.Rows.RemoveAt(ln_I)
						lb_Chg	= True

					Next

				End If
				'..................................................
				Return	lb_Chg

			End Function

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
		#Region "Constructor"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub New(						ByVal _table		As T						,
											Optional	ByVal _maxrows	As UShort = 10		)

				Me.co_DataTable	= _table
				Me.cn_MaxRows		= _maxrows
				'..................................................
				If Me.EnsureLimit(Me.cn_MaxRows)

					Me.co_DataTable.AcceptChanges()
					RaiseEvent	ev_DataChanged()
					
				End If

			End Sub

		#End Region

	End Class

End Namespace