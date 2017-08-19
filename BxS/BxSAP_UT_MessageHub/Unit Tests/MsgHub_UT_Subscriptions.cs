using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsgHub;

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
				var lc_Topic					= "Test";
				var lo_Subs						= new MsgHub.Subscriptions(lc_Topic);
				ISubscription lo_Sub	= new MsgHub.Subscription();

				Assert.IsNotNull(lo_Subs);
				Assert.AreSame(lc_Topic, lo_Subs.Topic);

				lo_Subs.AddUpdateSubscription(lo_Sub.MyID, lo_Sub);
				Assert.AreEqual(1, lo_Subs.Count);
				Assert.IsTrue(lo_Subs.ContainsKey(lo_Sub.MyID));
				Assert.IsTrue(lo_Subs.RemoveSubscription(lo_Sub.MyID));

				lo_Subs.AddUpdateSubscription(lo_Sub);
				Assert.AreEqual(1, lo_Subs.Count);
				Assert.IsTrue(lo_Subs.ContainsKey(lo_Sub.MyID));

				lo_Subs.Clear();
				Assert.AreEqual(0, lo_Subs.Count);
		}
	}
}
