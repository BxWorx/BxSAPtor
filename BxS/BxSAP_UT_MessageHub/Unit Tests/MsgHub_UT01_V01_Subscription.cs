using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsgHub;
using MsgHubv01;

namespace BxSAP_UT_MessageHub.Unit_Tests
{
	/// <summary>
	/// Summary description for MsgHub_UT_Subscription
	/// </summary>
	[TestClass]
	public class MsgHub_UT01_V01_Subscription
	{

	  private string cc_Test;
		private static Guid co_GuidEmpty;

		public MsgHub_UT01_V01_Subscription()
		{
			co_GuidEmpty = new Guid();
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
		public void MsgHub_UT01_V01_Subscription_Base()
			{
				var lo_guid		= Guid.NewGuid();
				var lc_Topic	= "Test";

				var lo_ActionO	= new Action<string>	( (data) => this.TestStr(data) );
				var lo_ActionX	= new Action<int>			( (data) => this.TestInt(data) );

				ISubscription lo_Subw		= new SubscriptionWeak	( lc_Topic, lo_guid, lo_ActionO );
				ISubscription	lo_Subs		= new Subscription			( lc_Topic, lo_guid, lo_ActionO );
				ISubscription	lo_Sube		= new Subscription			( lc_Topic, lo_guid, lo_ActionX );

				Assert.IsNotNull(lo_Subw);
				Assert.IsNotNull(lo_Subs);

				Assert.AreNotEqual(co_GuidEmpty			, lo_Subw.MyToken);
				Assert.AreNotEqual(co_GuidEmpty			, lo_Subs.MyToken);
				Assert.AreNotEqual(lo_Subw.MyToken	, lo_Subs.MyToken);

				Assert.AreEqual(typeof(string).Name	, lo_Subw.TypeID);
				Assert.AreEqual(lo_Subw.TypeID			, lo_Subs.TypeID);

				lo_Subw?.Invoke("A");
				Assert.AreEqual("A", this.cc_Test);

				lo_Subs?.Invoke("B");
				Assert.AreEqual("B", this.cc_Test);

				lo_Sube?.Invoke(123);
				Assert.AreEqual("123", this.cc_Test);

				lo_Sube?.Invoke("456");
				Assert.AreNotEqual("456", this.cc_Test);
			}

		private void TestStr(string msg)
			{ this.cc_Test	= msg; }

		private void TestInt(int data)
			{ this.cc_Test	= String.Format("{0}",data); }

	}
}
