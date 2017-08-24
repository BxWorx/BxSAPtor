using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsgHub;
using System;

namespace BxSAP_UT_MessageHub.Unit_Tests
{
	/// <summary>
	/// Summary description for MsgHub_UT_Subscriptions
	/// </summary>
	[TestClass]
	public class MsgHub_UT_Subscriptions
	{
		public MsgHub_UT_Subscriptions()
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
		public void MsgHub_UT_Subscriptions_Base()
			{
				var lo_guid		= new Guid();
				var lc_Topic	= "Test";
				var lo_Subs		= new SubscriptionsByTopic(lc_Topic);

				ISubscription<string> lo_Sub0	= new Subscription<string>(lo_guid, lc_Topic, this.test);

				Assert.IsNotNull(lo_Subs);
				Assert.AreSame(lc_Topic, lo_Subs.Topic);

				lo_Subs.Register(lo_Sub0);
				Assert.AreEqual(1, lo_Subs.Count);
				Assert.IsTrue(lo_Subs.SubscriptionExists(lo_Sub0));
				Assert.IsTrue(lo_Subs.DeRegister(lo_Sub0));
				Assert.AreEqual(0, lo_Subs.Count, "004");

				lo_Subs.Register(lo_Sub0);
				Assert.AreEqual(1, lo_Subs.Count);
				Assert.IsTrue(lo_Subs.SubscriptionExists(lo_Sub0));

				lo_Subs.Reset();
				Assert.AreEqual(0, lo_Subs.Count);
				Assert.IsFalse(lo_Subs.SubscriptionExists(lo_Sub0));

			}

		private void test(string name)
			{ }

	}
}
