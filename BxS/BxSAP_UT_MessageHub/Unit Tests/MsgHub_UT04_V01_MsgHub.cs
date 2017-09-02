using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsgHubv01;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BxSAP_UT_MessageHub.Unit_Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class MsgHub_UT04_V01_MsgHub
		{

			private IMessageHub	co_Hub;
			private int					cn_Int;
			private static int	cn_Cnt;

			public MsgHub_UT04_V01_MsgHub()
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
				[TestInitialize()]
				public void MyTestInitialize()
					{
						this.co_Hub = new MessageHub();
						this.cn_Int	= 0;
					}
			//
			// Use TestCleanup to run code after each test has run
			// [TestCleanup()]
			// public void MyTestCleanup() { }
			//
			#endregion

			//*************************************************************************
			[TestMethod]
			public void MsgHub_UT04_V01_MsgHub_Subscribe()
				{
					var lo_SubID		= Guid.NewGuid();
					var lo_ActionO	= new Action<int>( (data) => this.TestInt(data) );
					var lo_Subm			= new Subscription("MM", Guid.NewGuid(), lo_ActionO);
					var lo_Subc			= this.co_Hub.CreateSubscription(lo_ActionO, "MM", Guid.NewGuid());

					this.co_Hub.Subscribe(lo_ActionO, "XX", lo_SubID);
					this.co_Hub.Subscribe(lo_ActionO, "YY", lo_SubID);
					this.co_Hub.Subscribe(lo_ActionO, "ZZ", lo_SubID);

					Assert.AreEqual(3, this.co_Hub.Count<int>());

					var lo_sub	= this.co_Hub.Subscribe(lo_ActionO, "11", lo_SubID);
					Assert.AreEqual("11", lo_sub.Topic);

					this.co_Hub.Subscribe<int>(lo_Subm);
					this.co_Hub.Subscribe<int>(lo_Subc);
					Assert.AreEqual(6, this.co_Hub.Count<int>());
					Assert.AreEqual(6, this.co_Hub.Count());
					Assert.AreEqual(2, this.co_Hub.Count("MM"));

					this.co_Hub.UnSubscribeAll();
					Assert.AreEqual(0, this.co_Hub.Count("MM"));
					Assert.AreEqual(0, this.co_Hub.Count());

					this.co_Hub.Subscribe<int>(lo_Subm);
					this.co_Hub.Subscribe<int>(lo_Subc);
					Assert.AreEqual(2, this.co_Hub.Count("MM"));
					this.co_Hub.UnSubscribe("xx");
					Assert.AreEqual(2, this.co_Hub.Count("MM"));
					this.co_Hub.UnSubscribe("MM");
					Assert.AreEqual(0, this.co_Hub.Count("MM"));
				}

			//*************************************************************************
			[TestMethod]
			public void MsgHub_UT04_V01_MsgHub_Publish()
				{
					var lo_SubID		= Guid.NewGuid();
					var lo_ActionO	= new Action<int>( (data) => this.TestInt(data) );
					this.co_Hub.Subscribe(lo_ActionO, "XX", lo_SubID);

					this.co_Hub.Publish(11);
					Assert.AreEqual(11, this.cn_Int);
					this.co_Hub.Publish(11,"XX");
					Assert.AreEqual(22, this.cn_Int);
					this.co_Hub.Publish(11,"XX",lo_SubID);
					Assert.AreEqual(33, this.cn_Int);
				}

			//*************************************************************************
			[TestMethod]
			public void MsgHub_UT04_V01_MsgHub_PublishAsBackground()
				{
					string	lc_Topic	= "XX";
					int			ln_Qty		= 10;
					this.LoadSubscribers(lc_Topic, ln_Qty);

					cn_Cnt	= 0;
					this.co_Hub.PublishAsBackgroundTasks(1, lc_Topic);
					Thread.Sleep( (ln_Qty*10)+1 );
					Assert.AreEqual(ln_Qty, cn_Cnt, "MsgHub: Publish: As background: 001: Count Error");
				}

			//*************************************************************************
			[TestMethod]
			public void MsgHub_UT04_V01_MsgHub_PublishAsOneTaskAsync()
				{
					string	lc_Topic	= "XX";
					int			ln_Qty		= 10;
					this.LoadSubscribers(lc_Topic, ln_Qty);

					cn_Cnt	= 0;
					this.co_Hub.PublishAsOneTaskAsync(1, lc_Topic).Wait();
					Assert.AreEqual(ln_Qty, cn_Cnt, "MsgHub: Publish: Async: 001: Count Error");
				}

			//*************************************************************************
			[TestMethod]
			public async Task MsgHub_UT04_V01_MsgHub_PublishAsAsync()
				{
					string	lc_Topic	= "XX";
					int			ln_Qty		= 10;
					this.LoadSubscribers(lc_Topic, ln_Qty);

					cn_Cnt	= 0;
					var lt_Results	= await this.co_Hub.PublishAsAsync(1, lc_Topic);
					Assert.AreEqual(ln_Qty, cn_Cnt					, "MsgHub: Publish: As background: 001: Count Error");
					Assert.AreEqual(ln_Qty, lt_Results.Count, "MsgHub: Publish: As background: 002: Count Error");
				}

			//*************************************************************************
			private void LoadSubscribers(string topic, int howmany)
				{
					var lo_ActionO	= new Action<int>( (data) => this.TestCnt(data) );

					for (int i = 0; i < howmany; i++)
						{
							this.co_Hub.Subscribe(lo_ActionO, topic, Guid.NewGuid());
						}
				}

				private void TestInt(int data)
					{
						this.cn_Int += data;
					}

				private void TestCnt(int data)
					{
						Interlocked.Add(ref	cn_Cnt, data);
						Debug.WriteLine( $"UT: On thread: {Thread.CurrentThread.ManagedThreadId}" );
						Thread.Sleep(10);
					}
		}
}
