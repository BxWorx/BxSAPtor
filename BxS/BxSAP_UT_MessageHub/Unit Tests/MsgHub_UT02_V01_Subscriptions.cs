using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MsgHubv01;

namespace BxSAP_UT_MessageHub.Unit_Tests
{
	/// <summary>
	/// Summary description for MsgHub_UT_Subscriptions
	/// </summary>
	[TestClass]
	public class MsgHub_UT02_V01_Subscriptions
	{
		public MsgHub_UT02_V01_Subscriptions()
			{
				//
				// TODO: Add constructor logic here
				//
			}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void MsgHub_UT02_V01_Subscriptions_Base()
			{
				var lo_Guid		= Guid.NewGuid();
				var lc_Topic	= "Test";
				var lo_Subs		= new Subscriptions();

				ISubscription lo_Sub0	= new Subscription(lc_Topic, lo_Guid, new Action<string>( (string msg) => this.test(msg) ) );

				lo_Subs.Register(lo_Sub0);
				Assert.AreEqual(1, lo_Subs.Count(), "001");

				lo_Subs.DeRegister(lo_Sub0);
				Assert.AreEqual(0, lo_Subs.Count(), "002");

				lo_Subs.Register(lo_Sub0);
				Assert.AreEqual(1, lo_Subs.Count(), "003");

				lo_Subs.Clear();
				Assert.AreEqual(0, lo_Subs.Count(), "004");

				lo_Subs.Register(lo_Sub0);
				Assert.AreEqual(1, lo_Subs.Count( lo_Guid ), "005");
				Assert.AreEqual(1, lo_Subs.Count( lo_Guid, lo_Sub0.MyToken ), "006");
				Assert.AreEqual(1, lo_Subs.Count( subscriptionid: lo_Sub0.MyToken ), "007");
			}

		private void test(string name)
			{ }

	}
}
