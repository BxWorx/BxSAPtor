using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsgHubv01;
using System.Threading;
using System.Diagnostics;

namespace BxSAP_UT_MessageHub.Unit_Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class MsgHub_UT_V01_MsgHub
		{

			private IMessageHub	co_Hub;
			private int					cn_Int;
			private static int	cn_Cnt;

			public MsgHub_UT_V01_MsgHub()
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

			[TestMethod]
			public void MsgHub_UT_V01_MsgHub_Subscribe()
				{
					var lo_SubID		= Guid.NewGuid();
					var lo_ActionO	= new Action<int>( (data) => this.TestInt(data) );

					this.co_Hub.Subscribe(lo_ActionO, "XX", lo_SubID);
					this.co_Hub.Subscribe(lo_ActionO, "YY", lo_SubID);
					this.co_Hub.Subscribe(lo_ActionO, "ZZ", lo_SubID);

					Assert.AreEqual(3, this.co_Hub.Count());
				}

			[TestMethod]
			public void MsgHub_UT_V01_MsgHub_Publish()
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
			public void MsgHub_UT_V01_MsgHub_PublishAsync()
				{
					string	lc_Topic	= "XX";
					int			ln_Qty		= 10;
					this.LoadSubscribers(lc_Topic, ln_Qty);

					cn_Cnt	= 0;
					this.co_Hub.PublishAsync(1, lc_Topic).Wait();
					Assert.AreEqual(ln_Qty, cn_Cnt, "MsgHub: Publish: Async: 001: Count Error");
					Debug.WriteLine( "--------------------------" );

					cn_Cnt	= 0;
					this.co_Hub.PublishAsBackgroundTasks(1, lc_Topic);
					Thread.Sleep( (ln_Qty*10)+1 );
					Assert.AreEqual(ln_Qty, cn_Cnt, "MsgHub: Publish: As background: 001: Count Error");
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
