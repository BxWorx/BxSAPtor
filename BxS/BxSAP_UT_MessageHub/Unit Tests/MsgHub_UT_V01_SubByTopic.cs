using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsgHubv01;

namespace BxSAP_UT_MessageHub.Unit_Tests
{
	/// <summary>
	/// Summary description for MsgHub_UT_SubByTopic
	/// </summary>
	[TestClass]
	public class MsgHub_UT_V01_SubByTopic
		{

				private int cn_Cnt;
				private SubscriptionsByTopic	co_SbT;
				private Topics								co_Topics;

			public MsgHub_UT_V01_SubByTopic()
				{
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
			 [TestInitialize()]
			 public void MyTestInitialize()
				{
					this.co_SbT	= new SubscriptionsByTopic();
					this.co_Topics	= new Topics();
				}
			
			// Use TestCleanup to run code after each test has run
			// [TestCleanup()]
			// public void MyTestCleanup() { }
			//
			#endregion

			[TestMethod]
			public void MsgHub_UT_V01_SubByTopic_Base()
				{
					this.cn_Cnt		= 0;
					Guid	lc_ID		= Guid.NewGuid();

					ISubscription lo_Sub1	= new Subscription("XX", Guid.NewGuid()	, new Action<string>( (string msg) => this.test(msg) ) );
					ISubscription lo_Sub2	= new Subscription("XX", lc_ID					, new Action<string>( (string msg) => this.test(msg) ) );
					ISubscription lo_Sub3	= new Subscription("YY", Guid.NewGuid()	, new Action<string>( (string msg) => this.test(msg) ) );
					ISubscription lo_Sub4	= new Subscription("ZZ", lc_ID					, new Action<string>( (string msg) => this.test(msg) ) );

					this.co_SbT.Register(lo_Sub1);
					this.co_SbT.Register(lo_Sub2);
					this.co_SbT.Register(lo_Sub3);
					this.co_SbT.Register(lo_Sub4);
					
					Assert.AreEqual(4, this.co_SbT.Count());
					Assert.AreEqual(2, this.co_SbT.Count(subscriberid: lc_ID));
					Assert.AreEqual(1, this.co_SbT.Count("XX", lc_ID));
					Assert.AreEqual(2, this.co_SbT.Count("XX"));

					Assert.AreEqual(0, this.co_SbT.Count(subscriberid: Guid.NewGuid()));
					Assert.AreEqual(0, this.co_SbT.Count("X!"));
					Assert.AreEqual(0, this.co_SbT.Count("X!", lc_ID));
					Assert.AreEqual(0, this.co_SbT.Count("XX", Guid.NewGuid()));

					var lt_List	= this.co_SbT.GetSubscriptions();
					Assert.AreEqual(4, lt_List.Count);

					this.co_SbT.DeRegister(lo_Sub3);
					Assert.AreEqual(3, this.co_SbT.Count());
					this.co_SbT.Clear("XX");
					Assert.AreEqual(1, this.co_SbT.Count());
					this.co_SbT.Clear();
					Assert.AreEqual(0, this.co_SbT.Count());
				}


				private void test(string data)
					{
						this.cn_Cnt += 1;
					}

		}
}
