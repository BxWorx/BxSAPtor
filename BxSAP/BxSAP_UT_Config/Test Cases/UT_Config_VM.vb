Imports	BxSMdl	= BxSAP_Config.Settings.Model		' Data Layer
Imports	BxSVM		= BxSAP_Config.Settings.VM
'••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
<TestClass()>
Public Class UT_Config_VM

			Private Shared	cc_Path					As String
			Private Shared	cc_FullName			As String
			'....................................................
			Private	co_Repos		As BxSMdl.SettingReposDM
			Private	co_Xlator		As BxSVM.BxS_Settings_Xlator

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

#Region "Additional test attributes"
	'
	' You can use the following additional attributes as you write your tests:
	'
	' Use ClassInitialize to run code before running the first test in the class
	<ClassInitialize()>
	Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)

		cc_Path				= "c:\temp\xSAPtor"
		cc_FullName		= cc_Path & "\Config.xml"

	End Sub
	'
	' Use ClassCleanup to run code after all tests in a class have run
	' <ClassCleanup()> Public Shared Sub MyClassCleanup()
	' End Sub
	'
	' Use TestInitialize to run code before running each test
	<TestInitialize()>
	Public Sub MyTestInitialize()

		Me.co_Repos		= New BxSMdl.SettingReposDM(cc_FullName)
		Me.co_Xlator	= New BxSVM.BxS_Settings_Xlator

	End Sub
	'
	' Use TestCleanup to run code after each test has run
	' <TestCleanup()> Public Sub MyTestCleanup()
	' End Sub
	'
#End Region

		#Region "Test Units"

			'¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
			<TestMethod()>
			Public Sub UT_Config_VM_Settings()

				Dim lo_VM				As BxSVM.iBxS_SettingsVM
				Dim lo_DTOLog		As BxSVM.LogonDTO
				Dim lo_DTOCon		As BxSVM.ConnectionDTO
				'..................................................
				lo_VM			= New BxSVM.BxS_SettingsVM(Me.co_Repos, Me.co_Xlator)
				lo_VM.Open_Repository()
				lo_DTOLog	= lo_VM.GetLogonSettings()
				lo_DTOCon	= lo_VM.GetConnectionSettings()

				lo_DTOLog.DefaultLanguage	= "ss"
				lo_DTOCon.ConnectionIdleTimeout	= 99
		
				lo_VM.UpdateLogonSettings(lo_DTOLog)
				lo_VM.UpdateConnectionSettings(lo_DTOCon)

				lo_VM.Save_Repository()
				lo_VM.Close_Repository()
				'..................................................
				lo_VM.Open_Repository()
				lo_DTOLog	= lo_VM.GetLogonSettings()
				lo_DTOCon	= lo_VM.GetConnectionSettings()

				Assert.AreEqual( lo_DTOLog.DefaultLanguage			,	"ss", "VM: Settings: Log" )
				Assert.AreEqual( lo_DTOCon.ConnectionIdleTimeout,	99	, "VM: Settings: Con" )

			End Sub

		#End Region

End Class
