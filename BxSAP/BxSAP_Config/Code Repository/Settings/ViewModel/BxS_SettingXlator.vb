Imports	BxSDL	= BxSAP_Config.Settings.Model.BxS_SettingReposDL		' Data Layer
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Settings.VM

	Friend Class BxS_Settings_Xlator

		#Region "Methods: Exposed"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub Logon(ByVal _src As LogonDTO, ByVal _trg As BxSDL.LogonSettingsRow)

				_src.AutoConnect				=	_trg.AutoConnect
				_src.AutoConnect				= _trg.AutoSave
				_src.DefaultLanguage		= _trg.DefLang
				_src.SavePassword				= _trg.SavePwrd
				_src.ShowPassword				= _trg.ShowPwrd

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub Logon(ByVal _src As BxSDL.LogonSettingsRow, ByVal _trg As LogonDTO)

				_src.AutoConnect		= _trg.AutoConnect
				_src.AutoSave				= _trg.AutoSave
				_src.DefLang				= _trg.DefaultLanguage
				_src.SavePwrd				= _trg.SavePassword
				_src.ShowPwrd				= _trg.ShowPassword

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub Connection(ByVal _src As ConnectionDTO, ByVal _trg As BxSDL.ConnectionSetupRow)

				_src.IdleCheckTime					=	_trg.IdleCheckTime
				_src.ConnectionIdleTimeout	=	_trg.IdleTimeout
				_src.PeakConnectionLimit		=	_trg.PeakConnectionLimit
				_src.PoolSize								=	_trg.PoolSize
				_src.SNC_LibName32					=	_trg.SNC_LibName32
				_src.SNC_LibName64					=	_trg.SNC_LibName64
				_src.SNC_LibPath						=	_trg.SNC_LibPath
				_src.XML_FileName						=	_trg.XML_FileName
				_src.XML_FromNode						=	_trg.XML_FromNode
				_src.XML_FromWorkspace			=	_trg.XML_FromWorkspace
				_src.XML_OnlyLoadGUI				=	_trg.XML_OnlyLoadGUI
				_src.XML_Path								=	_trg.XML_Path
				_src.XML_UseSAPGUI					=	_trg.XML_UseSAPGUI

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub Connection(ByVal _src As BxSDL.ConnectionSetupRow, ByVal _trg As ConnectionDTO)

				_src.IdleCheckTime				=	CUShort( _trg.IdleCheckTime )
				_src.IdleTimeout					= CUShort( _trg.ConnectionIdleTimeout )
				_src.PeakConnectionLimit	=	CUShort( _trg.PeakConnectionLimit )
				_src.PoolSize							=	CUShort( _trg.PoolSize )

				_src.SNC_LibName32				=	_trg.SNC_LibName32
				_src.SNC_LibName64				=	_trg.SNC_LibName64
				_src.SNC_LibPath					=	_trg.SNC_LibPath

				_src.XML_FileName					=	_trg.XML_FileName
				_src.XML_FromNode					=	_trg.XML_FromNode
				_src.XML_FromWorkspace		=	_trg.XML_FromWorkspace
				_src.XML_OnlyLoadGUI			=	_trg.XML_OnlyLoadGUI
				_src.XML_Path							=	_trg.XML_Path
				_src.XML_UseSAPGUI				=	_trg.XML_UseSAPGUI

			End Sub

		#End Region
		'¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯

	End Class

End Namespace