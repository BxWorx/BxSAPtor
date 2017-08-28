using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsgHubv01;
using System.Threading;

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

					this.co_Hub.Subscribe("XX", lo_SubID, lo_ActionO);
					this.co_Hub.Subscribe("YY", lo_SubID, lo_ActionO);
					this.co_Hub.Subscribe("ZZ", lo_SubID, lo_ActionO);

					Assert.AreEqual(3, this.co_Hub.Count);
				}

			[TestMethod]
			public void MsgHub_UT_V01_MsgHub_Publish()
				{
					var lo_SubID		= Guid.NewGuid();
					var ln_Int			= 12;
					var lo_ActionO	= new Action<int>( (data) => this.TestInt(data) );

					this.co_Hub.Subscribe("XX", lo_SubID, lo_ActionO);
					this.co_Hub.Publish("XX", lo_SubID, ln_Int);
					Assert.AreEqual(ln_Int, this.cn_Int);
				}

				private void TestInt(int data)
					{
						this.cn_Int += data;
					}

				private void TestCnt(int data)
					{
						Interlocked.Add(ref	cn_Cnt, data);
					}
		}
}
