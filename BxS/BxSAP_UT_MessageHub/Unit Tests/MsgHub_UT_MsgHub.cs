﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BxSAP_UT_MessageHub.Unit_Tests
	{
	/// <summary>
	/// Summary description for MsgHub_UT_MsgHub
	/// </summary>
	[TestClass]
	public class MsgHub_UT_MsgHub
		{

			private static int  cn_Cnt;
			private static Guid	co_GuidEmpty;
			private MsgHub.Hub	co_Hub;
			private string			cc_Topic;

		public MsgHub_UT_MsgHub()
			{
				this.cc_Topic	= "XX";
				cn_Cnt				= 0;
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
		[ClassInitialize()]
		public static void MyClassInitialize(TestContext testContext)
			{
				co_GuidEmpty = new Guid();
			}

		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		[TestInitialize()]
		public void MyTestInitialize()
			{
			this.co_Hub = new MsgHub.Hub();
			}
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		//*************************************************************************
		[TestMethod]
		public void MsgHub_UT_MsgHub_Base()
			{
				var lo_CustID	= Guid.NewGuid();
				var lo_Token0	= this.co_Hub.Subscribe(lo_CustID, this.cc_Topic, (string msg) => this.test(msg), false);
				Assert.AreNotSame(co_GuidEmpty, lo_Token0);
			}
		//*************************************************************************
		[TestMethod]
		public void MsgHub_UT_MsgHub_Replace()
			{
				var lo_CustID	= Guid.NewGuid();
				var lo_Sub		= MsgHub.MsgHubFactory.Subscription(lo_CustID, this.cc_Topic, true, false, (string msg) => this.test(msg));
				var lo_Token0	= this.co_Hub.Subscribe(lo_Sub);
				Assert.AreEqual(lo_Sub.MyToken, lo_Token0);

				var lo_Token1	= this.co_Hub.Subscribe(lo_Sub);
				Assert.AreEqual(co_GuidEmpty, lo_Token1);

				lo_Sub		= MsgHub.MsgHubFactory.Subscription(lo_CustID, this.cc_Topic, true, true, (string msg) => this.test(msg));
				var lo_T2	= this.co_Hub.Subscribe(lo_Sub);
				Assert.AreEqual(lo_Sub.MyToken, lo_T2);
				var lo_T3 = this.co_Hub.Subscribe(lo_Sub);
				Assert.AreNotEqual(co_GuidEmpty, lo_T3);
			}
		//*************************************************************************
		[TestMethod]
		public void MsgHub_UT_MsgHub_Allowmany()
		{
			var lo_CustID = Guid.NewGuid();
			var lo_Sub		= MsgHub.MsgHubFactory.Subscription(lo_CustID, this.cc_Topic, false, true, (string msg) => this.test(msg));
			var lo_Token0 = this.co_Hub.Subscribe(lo_Sub);
			Assert.AreEqual(lo_Sub.MyToken, lo_Token0);

			var lo_Token1 = this.co_Hub.Subscribe(lo_Sub);
			Assert.AreEqual(co_GuidEmpty, lo_Token1);

			lo_Sub		= MsgHub.MsgHubFactory.Subscription(lo_CustID, this.cc_Topic, true, true, (string msg) => this.test(msg));
			var lo_T2 = this.co_Hub.Subscribe(lo_Sub);
			Assert.AreEqual(lo_Sub.MyToken, lo_T2);

			Assert.AreEqual(this.co_Hub.SubscriptionCount(this.cc_Topic), 2);
			Assert.AreEqual(this.co_Hub.ClientCount(this.cc_Topic), 1);
		}


		private void test(string name)
			{ cn_Cnt = 1; }

		private void testa(string name)
			{ cn_Cnt = 2; }

	}
}