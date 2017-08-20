using System;
using System.Reflection;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		public sealed class Subscription
			{
				#region ** [Declarations] **

					private readonly Guid							cc_Token;

					private readonly Type							co_Type;
					private readonly WeakReference		co_TObj;
					private readonly MethodInfo				co_MInfo;
					private readonly bool							cb_IsStatic;

				#endregion
				//_________________________________________________________________________________________
				#region ** [Properties] **

					public Guid	MyToken	{ get { return	this.cc_Token; } }

					public Guid		ClientID		{ get;	set; }
					public string	Topic				{ get;	set; }
					public bool		AllowMany		{ get;	set; }

				#endregion
				//_________________________________________________________________________________________
				#region ** [Constructors]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public Subscription(Guid					clientid			,
															string				topic					,
															bool					allowmany			,
															Type					type					,
															WeakReference targetobject	,
															MethodInfo		targetmethod	,
															bool					isstatic				)
						{
							this.cc_Token			= Guid.NewGuid();

							this.ClientID			= clientid;
							this.Topic				= topic;
							this.AllowMany		= allowmany;

							this.co_Type			= type;
							this.co_TObj			= targetobject;
							this.co_MInfo			= targetmethod;
							this.cb_IsStatic	= isstatic;
						}

				#endregion
				//_________________________________________________________________________________________
				#region ** [Methods: Exposed] **

					//____________________________________________________________________________________________
					internal void Invoke()
						{
							//	lo_Action == (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), this.co_TrgtObj.Target, this.co_MthdInf)
						}

				#endregion
			}
	}

