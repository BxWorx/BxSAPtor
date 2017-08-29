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

					this.co_SbT.Subscribe(lo_Sub1);
					this.co_SbT.Subscribe(lo_Sub2);
					this.co_SbT.Subscribe(lo_Sub3);
					this.co_SbT.Subscribe(lo_Sub4);
					
					Assert.AreEqual(2, this.co_SbT.GetSubscriptions(lc_ID).Count);
					Assert.AreEqual(1, this.co_SbT.GetSubscriptions("XX", lc_ID).Count);
					Assert.AreEqual(2, this.co_SbT.GetSubscriptions("XX").Count);

					Assert.AreEqual(0, this.co_SbT.GetSubscriptions(Guid.NewGuid()).Count);
					Assert.AreEqual(0, this.co_SbT.GetSubscriptions("X!").Count);
					Assert.AreEqual(0, this.co_SbT.GetSubscriptions("X!", lc_ID).Count);
					Assert.AreEqual(0, this.co_SbT.GetSubscriptions("XX", Guid.NewGuid()).Count);


					//Assert.AreEqual(4,	this.co_SbT.Count()						,	"001"	);
					//Assert.AreEqual(2,	this.co_SbT.Count("XX")				,	"002"	);
					//Assert.AreEqual(0,	this.co_SbT.Count("11")				,	"003"	);

					//Assert.AreEqual(2,	this.co_SbT.Count(subscriber:	lc_ID),	"004"	);

					//Assert.AreEqual(1,	this.co_SbT.Count("XX",	lc_ID),	"005"	);
					//Assert.AreEqual(1,	this.co_SbT.Count("YY",	lc_ID),	"006"	);

				}


				private void test(string data)
					{
						this.cn_Cnt += 1;
					}

		}
}
