using System;
using System.Reflection;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		public sealed class Subscription
			{
				#region ** [Declarations] **

					private readonly Guid			cc_Token;
					private readonly Guid			cc_ClientID;
					private readonly string		cc_Topic;
					private readonly bool			cb_Replace;
					private readonly bool			cb_AllowMany;
					//...............................................
					private Type						co_Type;
					private WeakReference		co_TObj;
					private MethodInfo			co_MInfo;
					private bool						cb_IsStatic;

				#endregion
				//_________________________________________________________________________________________
				#region ** [Properties] **

					public Guid		MyToken			{ get { return	this.cc_Token; } }
					public Guid		ClientToken	{ get { return	this.cc_ClientID; } }
					public string	Topic				{ get { return	this.cc_Topic; } }
					public bool		AllowMany		{ get { return	this.cb_AllowMany; } }
					public bool		Replace			{ get { return	this.cb_Replace; } }

				#endregion
				//_________________________________________________________________________________________
				#region ** [Constructors]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public Subscription(Guid					token					,
															Guid					clientid			,
															string				topic					,
															bool					allowmany			,
															bool					replace				,
															Type					type					,
															WeakReference targetobject	,
															MethodInfo		targetmethod	,
															bool					isstatic				)
						{
							this.cc_Token			= token;
							this.cc_ClientID	= clientid;
							this.cc_Topic			= topic;
							this.cb_AllowMany	= allowmany;
							this.cb_Replace		= replace;

							this.co_Type			= type;
							this.co_TObj			= targetobject;
							this.co_MInfo			= targetmethod;
							this.cb_IsStatic	= isstatic;
						}

				#endregion
				//_________________________________________________________________________________________
				#region ** [Methods: Exposed] **

					internal void ReplaceAction<T>(Action<T> action)
						{
							this.co_Type			= typeof(T);
							this.co_TObj			= new WeakReference(action.Target);
							this.co_MInfo			= action.Method;
							this.cb_IsStatic	= (action.Target == null) ? true : false;
						}

					//____________________________________________________________________________________________
					internal void Invoke<T>(T Msg)
						{
							Action<T> lo_Action = null;
							//..................................................
							if (this.co_TObj.Target != null && this.co_TObj.IsAlive)
								{
									lo_Action = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), this.co_TObj.Target, this.co_MInfo);
								}
							else if (this.cb_IsStatic)
								{
									lo_Action = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), this.co_MInfo);
								}
							//..................................................
							lo_Action?.Invoke( Msg );
						}

				#endregion
			}
	}

