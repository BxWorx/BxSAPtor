Imports	xSAPUtl	=	BxSAP_Utilities.Controllers
Imports xSAPCnf	=	BxSAP_Config.Controllers
Imports xSAPLog = BxSAP_Config.Model.Logon.Options
Imports xSAPCon	= BxSAP_Config.Model.Logon.Connections
Imports xSAPSys	= BxSAP_Config.Model.Logon.Systems
Imports xSAPCCf	= BxSAP_Config.Model.Logon.ConnectionSetup
Imports xSAPXML	= BxSAP_Config.Model.Sapgui.Xml
Imports xSAPSet	= BxSAP_Config.Model.Settings
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
Namespace UT_Config

	<TestClass()>
	Public Class UT_Config

		Private testContextInstance As TestContext

		'''<summary>
		'''Gets or sets the test context which provides
		'''information about and functionality for the current test run.
		'''</summary>
		Public Property TestContext() As TestContext
			Get
				Return testContextInstance
			End Get
			Set(ByVal value As TestContext)
				testContextInstance = Value
			End Set
		End Property

			Private Shared	cc_Path					As String
			Private Shared	cc_FullName			As String
			Private Shared	co_CntlrUtil		As xSAPUtl.iController

		#Region "Additional test attributes"
			'
			' You can use the following additional attributes as you write your tests:
			'
			' Use ClassInitialize to run code before running the first test in the class
			<ClassInitialize()>
			Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)

				cc_Path				= "c:\temp\xSAPtor"
				cc_FullName		= cc_Path & "\Config.xml"
				co_CntlrUtil	= xSAPUtl.Controller.Controller
				
			End Sub
			'
			' Use ClassCleanup to run code after all tests in a class have run
			' <ClassCleanup()> Public Shared Sub MyClassCleanup()
			' End Sub
			'
			' Use TestInitialize to run code before running each test
			' <TestInitialize()> Public Sub MyTestInitialize()
			' End Sub
			'
			' Use TestCleanup to run code after each test has run
			' <TestCleanup()> Public Sub MyTestCleanup()
			' End Sub
			'
		#End Region

		#Region "Test Units"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			<TestMethod()>
			Public Sub UT_Config_Settings_Repos()

				'Dim lo_SetRepDM		As xSAPSet.SettingReposDM	= New xSAPSet.SettingReposDM(cc_FullName)

				'Dim x =	lo_SetRepDM.GetLogonSettings()
				'lo_SetRepDM.SaveLogonSettings(x)
				'x.DefLang	= "XX"
				'lo_SetRepDM.SaveLogonSettings(x)
				'x.DefLang	= "YY"
				'lo_SetRepDM.SaveLogonSettings(x)
				'lo_SetRepDM.SetHistoryLimit(7)
				'lo_SetRepDM.AutoSave	= True
				'lo_SetRepDM.Close()

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			<TestMethod()>
			Public Sub UT_Config_Settings_DataModel()

				'Dim lo_SysSet		As xSAPSet.BxSAPConfig_Settings	= New xSAPSet.BxSAPConfig_Settings
				'Dim lo_SetMdl		As xSAPSet.SettingTableDM(Of xSAPSet.BxSAPConfig_Settings.LogonSettingsDataTable)
				'Dim lo_Row0			As xSAPSet.BxSAPConfig_Settings.LogonSettingsRow
				'Dim lo_Row1			As xSAPSet.BxSAPConfig_Settings.LogonSettingsRow
			
				'lo_SetMdl	= New xSAPSet.SettingTableDM(Of xSAPSet.BxSAPConfig_Settings.LogonSettingsDataTable)(lo_SysSet.LogonSettings)
				''..................................................
				'lo_Row0					= lo_SetMdl.GetSettings()
				'lo_Row0.DefLang	= "XX"
				'lo_SetMdl.CommitSettings(lo_Row0)
				'lo_Row1					= lo_SetMdl.GetSettings

				'Assert.AreEqual( lo_Row0.DefLang,	lo_Row1.DefLang, "Settings: Fail: Save/Load: Systems" )
				''..................................................
				'lo_Row1.DefLang	= "YY"
				'lo_SetMdl.CommitSettings(lo_Row1)
				'lo_Row0					= lo_SetMdl.GetSettings

				'Assert.AreEqual( lo_Row0.DefLang,	lo_Row1.DefLang, "Settings: Fail: Save/Load: Systems" )
				''..................................................
				'lo_Row1	= lo_SetMdl.GetDefaulRow()	:	lo_Row1.DefLang = "01"	:	lo_SetMdl.CommitSettings(lo_Row1)
				'lo_Row1	= lo_SetMdl.GetDefaulRow()	:	lo_Row1.DefLang = "02"	:	lo_SetMdl.CommitSettings(lo_Row1)
				'lo_Row1	= lo_SetMdl.GetDefaulRow()	:	lo_Row1.DefLang = "03"	:	lo_SetMdl.CommitSettings(lo_Row1)
				'lo_Row1	= lo_SetMdl.GetDefaulRow()	:	lo_Row1.DefLang = "04"	:	lo_SetMdl.CommitSettings(lo_Row1)
				'lo_Row1	= lo_SetMdl.GetDefaulRow()	:	lo_Row1.DefLang = "05"	:	lo_SetMdl.CommitSettings(lo_Row1)
				'lo_Row1	= lo_SetMdl.GetDefaulRow()	:	lo_Row1.DefLang = "06"	:	lo_SetMdl.CommitSettings(lo_Row1)
				'lo_Row1	= lo_SetMdl.GetDefaulRow()	:	lo_Row1.DefLang = "07"	:	lo_SetMdl.CommitSettings(lo_Row1)
				'lo_Row1	= lo_SetMdl.GetDefaulRow()	:	lo_Row1.DefLang = "08"	:	lo_SetMdl.CommitSettings(lo_Row1)
				'lo_Row1	= lo_SetMdl.GetDefaulRow()	:	lo_Row1.DefLang = "09"	:	lo_SetMdl.CommitSettings(lo_Row1)
				'lo_Row1	= lo_SetMdl.GetDefaulRow()	:	lo_Row1.DefLang = "10"	:	lo_SetMdl.CommitSettings(lo_Row1)
				'lo_Row1	= lo_SetMdl.GetDefaulRow()	:	lo_Row1.DefLang = "11"	:	lo_SetMdl.CommitSettings(lo_Row1)
				'lo_Row1	= lo_SetMdl.GetDefaulRow()	:	lo_Row1.DefLang = "12"	:	lo_SetMdl.CommitSettings(lo_Row1)
				''..................................................
				'Dim l0	= lo_SetMdl.GetHistoryList(True)

				'Assert.AreEqual( l0.Count,	10, "Settings: Fail: Save/Load: Systems")
				''..................................................
				'lo_SetMdl.SetHistoryLimit(5)
				'Dim l1	= lo_SetMdl.GetHistoryList(True)

				'Assert.AreEqual( l1.Count,	5, "Settings: Fail: Save/Load: Systems")
				''..................................................
				'Dim l2	= lo_SetMdl.GetHistoryList(True)

				'lo_Row1	= lo_SetMdl.GetSettings(l2.Count-1)

				'Assert.AreEqual( lo_Row1.DefLang,	"08", "Settings: Fail: Save/Load: Systems")
				''..................................................
				'lo_SetMdl.ResetHistory()
				'Dim l3	= lo_SetMdl.GetHistoryList(True)

				'Assert.AreEqual( l3.Count,	1, "Settings: Fail: Save/Load: Systems")

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			<TestMethod()>
			Public Sub UT_Config_SAPGUI_XML_Parser()

				Dim	lo_CntlrConf		As xSAPCnf.iController
				Dim lo_ConnSetup		As xSAPCCf.iLogonConnSetupDTO
				Dim lo_XMLLoader		As xSAPXML.SAPGuiXmlLoader
				Dim lo_XMLParser		As xSAPXML.SAPGuiXmlParser
				Dim lo_SysRepos			As BxSAP_Config.SAPSysRepository
				'..................................................
				lo_CntlrConf		= New xSAPCnf.Controller(co_CntlrUtil, cc_FullName)
				lo_ConnSetup		= lo_CntlrConf.GetLogonConnectionSetup()

				lo_ConnSetup.XML_FromWorkspace	= "LEGACY SYSTEMS"
				lo_ConnSetup.XML_FromNode				= ""

				lo_XMLLoader		= New	xSAPXML.SAPGuiXmlLoader(lo_ConnSetup)
				lo_SysRepos			= New BxSAP_Config.SAPSysRepository
				lo_XMLParser		= New xSAPXML.SAPGuiXmlParser
				'..................................................
				lo_XMLParser.Parse(lo_XMLLoader, lo_SysRepos)

				lo_SysRepos.WriteXml("C:\Temp\xSAPtor\SysRepos.xml")

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			<TestMethod()>
			Public Sub UT_Config_SAPGUI_XML_Loader()

				Dim	lo_CntlrConf		As xSAPCnf.iController
				Dim lo_ConnSetup		As xSAPCCf.iLogonConnSetupDTO
				Dim lo_XMLLoader		As xSAPXML.SAPGuiXmlLoader
				'..................................................
				lo_CntlrConf		= New xSAPCnf.Controller(co_CntlrUtil, cc_FullName)
				lo_ConnSetup		= lo_CntlrConf.GetLogonConnectionSetup()

				'lo_XMLLoader		= New	xSAPXML.SAPGuiXmlLoader(lo_ConnSetup)

				lo_ConnSetup.XML_FromWorkspace	= "LEGACY SYSTEMS"
				lo_ConnSetup.XML_FromNode				= "ZA"

				lo_XMLLoader		= New	xSAPXML.SAPGuiXmlLoader(lo_ConnSetup)

			End Sub





			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			<TestMethod()>
			Public Sub UT_Config_SysRepos_Systems()

				Dim	lo_CntlrConf	As xSAPCnf.iController
				Dim lo_SysRep			As xSAPSys.iSysReposDTO
				Dim lo_SysEnt			As xSAPSys.iSysReposSystemDTO
				Dim lb_Bool				As Boolean	= False
				'..................................................
				lo_CntlrConf		= New xSAPCnf.Controller(co_CntlrUtil, cc_FullName)
				lo_SysRep				= lo_CntlrConf.GetSystemRepository()
				lo_SysRep.Systems.Clear()
				'..................................................
				lo_SysEnt			= lo_CntlrConf.CreateSysReposSystemEntry()
				lo_SysEnt.ID	= "01"
				lo_SysRep.Systems.Add(lo_SysEnt.ID, lo_SysEnt)
				lo_CntlrConf.UpdateSystemRepository(lo_SysRep)
				'..................................................
				lo_CntlrConf.Save()
				lo_CntlrConf.Load()
				lo_SysRep	= lo_CntlrConf.GetSystemRepository()

				Assert.AreEqual( lo_SysRep.Systems.Count,	1, "SysRepos: Fail: Save/Load: Systems")

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			<TestMethod()>
			Public Sub UT_Config_SysRepos_Logons()

				Dim	lo_CntlrConf	As xSAPCnf.iController
				Dim lo_Logon			As xSAPSys.iSysLogonRepositoryDTO
				Dim lo_LogEnt			As xSAPSys.iSysLogonDTO
				Dim lb_Bool				As Boolean	= False
				'..................................................
				lo_CntlrConf		= New xSAPCnf.Controller(co_CntlrUtil, cc_FullName)
				lo_Logon				= lo_CntlrConf.GetSystemLogonRepository()
				lo_Logon.Logons.Clear()
				'..................................................
				lo_LogEnt		= lo_CntlrConf.CreateSystemLogonEntry()

				lo_LogEnt.DestinationID	= "A123"
				lo_Logon.Logons.Add(lo_LogEnt.DestinationID, lo_LogEnt)
				lo_CntlrConf.UpdateSystemLogonRepository(lo_Logon)
				'..................................................
				lo_CntlrConf.Save()
				lo_CntlrConf.Load()
				lo_Logon	= lo_CntlrConf.GetSystemLogonRepository()

				Assert.AreEqual( lo_Logon.Logons.Count,	1, "System Logon: Fail: Save/Load: Languages")

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			<TestMethod()>
			Public Sub UT_Config_SysRepos_Languages()

				Dim	lo_CntlrConf	As xSAPCnf.iController
				Dim lo_Langs			As xSAPSys.iSystemLanguagesDTO
				Dim lb_Bool				As Boolean	= False
				'..................................................
				lo_CntlrConf		= New xSAPCnf.Controller(co_CntlrUtil, cc_FullName)
				lo_Langs				= lo_CntlrConf.GetSystemLanguages()
				'..................................................
				lo_Langs.Languages.Clear()
				lo_Langs.Languages.Add("EN")
				lo_Langs.Languages.Add("XX")

				lo_CntlrConf.UpdateSystemLanguages(lo_Langs)
				'..................................................
				lo_CntlrConf.Save()
				lo_CntlrConf.Load()
				lo_Langs	= lo_CntlrConf.GetSystemLanguages()

				lo_Langs.Languages.Add("YY")
				lo_CntlrConf.UpdateSystemLanguages(lo_Langs)
				lo_CntlrConf.Save()

				Assert.AreEqual( lo_Langs.Languages.Count,	3, "SystemRepos: Fail: Save/Load: Languages")

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			<TestMethod()>
			Public Sub UT_Config_LogonRepos()

				Dim	lo_CntlrConf	As xSAPCnf.iController
				Dim lo_Repos			As xSAPCon.iLogonConnReposDTO
				Dim lo_ReposCon		As xSAPCon.iLogonConnectionDTO
				Dim lb_Bool				As Boolean	= False
				'..................................................
				lo_CntlrConf		= New xSAPCnf.Controller(co_CntlrUtil, cc_FullName)
				lo_Repos				= lo_CntlrConf.GetLogonConnectionsRepository()
				'..................................................
				lo_ReposCon	= lo_CntlrConf.CreateLogonConnectionsRepositoryConnection()
				lo_ReposCon.ID	= "XXXX"
				If Not lo_Repos.ConnectionsList.ContainsKey(lo_ReposCon.ID)	Then	lo_Repos.ConnectionsList.Add(lo_ReposCon.ID,	lo_ReposCon)

				lo_ReposCon	= lo_CntlrConf.CreateLogonConnectionsRepositoryConnection()
				lo_ReposCon.ID	= "YYYY"
				If Not lo_Repos.ConnectionsList.ContainsKey(lo_ReposCon.ID)	Then	lo_Repos.ConnectionsList.Add(lo_ReposCon.ID,	lo_ReposCon)

				lo_CntlrConf.UpdateLogonConnectionsRepository(lo_Repos)
				'..................................................
				lo_CntlrConf.Save()
				lo_CntlrConf.Load()
				lo_Repos	= lo_CntlrConf.GetLogonConnectionsRepository()

				Assert.AreEqual( lo_Repos.ConnectionsList.Count,	2, "LogonRepos: Fail: Save/Load: 1")

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			<TestMethod()>
			Public Sub UT_Config_LogonConnSetup()

				Dim	lo_CntlrConf	As	xSAPCnf.iController
				Dim lo_ConnSetup	As	xSAPCCf.iLogonConnSetupDTO
				'..................................................
				lo_CntlrConf		= New xSAPCnf.Controller(co_CntlrUtil, cc_FullName)
				lo_ConnSetup		= lo_CntlrConf.GetLogonConnectionSetup()

				lo_ConnSetup.XML_UseSAPGUI		= True
				lo_ConnSetup.XML_OnlyLoadGUI	= True
				lo_ConnSetup.XML_Path					= cc_Path
				lo_ConnSetup.XML_FileName			= "SAPUILandscapeS2A_001.xml"

				lo_CntlrConf.UpdateLogonConnectionSetup(lo_ConnSetup)
				lo_CntlrConf.Save()

			End Sub
			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			<TestMethod()>
			Public Sub UT_Config_LogonOptions()

				Dim	lo_CntlrConf	As xSAPCnf.iController
				Dim lo_OptsDTO		As xSAPLog.iLogonOptionsDTO
				Dim lo_OptsRet		As xSAPLog.iLogonOptionsDTO
				Dim lb_Bool				As Boolean	= False
				'..................................................
				lo_CntlrConf		= New xSAPCnf.Controller(co_CntlrUtil, cc_FullName)
				lo_OptsDTO			= lo_CntlrConf.GetLogonOptions()
				
				lo_OptsDTO.DefaultLanguage	= "XX"

				lb_Bool			=	lo_CntlrConf.UpdateLogonOptions(lo_OptsDTO)
				lo_OptsRet	=	lo_CntlrConf.GetLogonOptions()
				lo_CntlrConf.Save()

				Assert.AreEqual(lb_Bool,	True, "LogonOptions: Fail: Save")
				Assert.AreEqual(lo_OptsRet.DefaultLanguage,	lo_OptsDTO.DefaultLanguage, "LogonOptions: Fail: Save")

				lo_CntlrConf.Load()
				lo_OptsRet	=	lo_CntlrConf.GetLogonOptions()

				Assert.AreEqual(lo_OptsRet.DefaultLanguage,	lo_OptsDTO.DefaultLanguage, "LogonOptions: Fail: Re-Load")

			End Sub

		#End Region
		
	End Class

End Namespace
