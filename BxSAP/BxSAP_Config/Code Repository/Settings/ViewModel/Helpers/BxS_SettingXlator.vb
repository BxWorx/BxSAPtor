Imports	BxSDL	= BxSAP_Config.Settings.Model.BxS_SettingReposDL		' Data Layer
Imports BxSAP_Config.Settings.DTO
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace Settings.VM

	Friend Class BxS_Settings_Xlator

		#Region "Methods: Exposed"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub Logon(ByVal _srcdl		As BxSDL.LogonSettingsRow	,
											 ByVal _trgdto	As LogonDTO									)

				_trgdto.AutoConnect				=	_srcdl.AutoConnect
				_trgdto.AutoConnect				= _srcdl.AutoSave
				_trgdto.DefaultLanguage		= _srcdl.DefLang
				_trgdto.SavePassword			= _srcdl.SavePwrd
				_trgdto.ShowPassword			= _srcdl.ShowPwrd

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub Logon(ByVal _srcdto	As LogonDTO								,
											 ByVal _trgdl		As BxSDL.LogonSettingsRow		)

				_trgdl.AutoConnect	= _srcdto.AutoConnect
				_trgdl.AutoSave			= _srcdto.AutoSave
				_trgdl.DefLang			= _srcdto.DefaultLanguage
				_trgdl.SavePwrd			= _srcdto.SavePassword
				_trgdl.ShowPwrd			= _srcdto.ShowPassword

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub Connection(ByVal _srcdl	As BxSDL.ConnectionSetupRow	,
														ByVal _trgdto	As ConnectionDTO							)

				_trgdto.IdleCheckTime						=	_srcdl.IdleCheckTime
				_trgdto.ConnectionIdleTimeout		=	_srcdl.IdleTimeout
				_trgdto.PeakConnectionLimit			=	_srcdl.PeakConnectionLimit
				_trgdto.PoolSize								=	_srcdl.PoolSize

				_trgdto.SNC_LibName32						=	_srcdl.SNC_LibName32
				_trgdto.SNC_LibName64						=	_srcdl.SNC_LibName64
				_trgdto.SNC_LibPath							=	_srcdl.SNC_LibPath

				_trgdto.XML_FileName						=	_srcdl.XML_FileName
				_trgdto.XML_FromNode						=	_srcdl.XML_FromNode
				_trgdto.XML_FromWorkspace				=	_srcdl.XML_FromWorkspace
				_trgdto.XML_OnlyLoadGUI					=	_srcdl.XML_OnlyLoadGUI
				_trgdto.XML_Path								=	_srcdl.XML_Path
				_trgdto.XML_UseSAPGUI						=	_srcdl.XML_UseSAPGUI

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			Friend Sub Connection(ByVal _srcdto As ConnectionDTO						,
														ByVal _trgdl	As BxSDL.ConnectionSetupRow		)

				_trgdl.IdleCheckTime				=	CUShort( _srcdto.IdleCheckTime )
				_trgdl.IdleTimeout					= CUShort( _srcdto.ConnectionIdleTimeout )
				_trgdl.PeakConnectionLimit	=	CUShort( _srcdto.PeakConnectionLimit )
				_trgdl.PoolSize							=	CUShort( _srcdto.PoolSize )

				_trgdl.SNC_LibName32				=	_srcdto.SNC_LibName32
				_trgdl.SNC_LibName64				=	_srcdto.SNC_LibName64
				_trgdl.SNC_LibPath					=	_srcdto.SNC_LibPath

				_trgdl.XML_FileName					=	_srcdto.XML_FileName
				_trgdl.XML_FromNode					=	_srcdto.XML_FromNode
				_trgdl.XML_FromWorkspace		=	_srcdto.XML_FromWorkspace
				_trgdl.XML_OnlyLoadGUI			=	_srcdto.XML_OnlyLoadGUI
				_trgdl.XML_Path							=	_srcdto.XML_Path
				_trgdl.XML_UseSAPGUI				=	_srcdto.XML_UseSAPGUI

			End Sub

		#End Region

	End Class

End Namespace