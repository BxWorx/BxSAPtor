using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsgHub;

namespace BxSAP_UT_MessageHub.Unit_Tests
{
	/// <summary>
	/// Summary description for MsgHub_UT_Subscription
	/// </summary>
	[TestClass]
	public class MsgHub_UT_Subscription
	{
		public MsgHub_UT_Subscription()
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
		public void MsgHub_UT_Subscription_Base()
			{
				var lo_guid		= new Guid();
				var lo_Sub		= MsgHubFactory.Subscription<string>(this.test);

				Assert.IsNotNull(lo_Sub);
				Assert.AreNotEqual(lo_guid,lo_Sub.MyToken);
			}

		private void test(string name)
			{ }

	}
}
