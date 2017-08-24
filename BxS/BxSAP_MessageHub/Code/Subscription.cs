using System;
using System.Reflection;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		//*********************************************************************************************
		//*********************************************************************************************
		public interface ISubscription<T>
			{
				#region ** [Properties] **

					Guid		MyToken			{ get; }
					Guid		ClientToken	{ get; }
					string	Topic				{ get; }
					bool		AllowMany		{ get; }
					bool		Replace			{ get; }

					Type		TypeOf			{	get; }

				#endregion
				//_________________________________________________________________________________________
				#region ** [Methods: Exposed] **

					void Invoke(T Message);

				#endregion

			}
		//*********************************************************************************************
		//*********************************************************************************************
		public abstract class SubscriptionBase<T>: ISubscription<T>
			{
				#region ** [Declarations] **

					protected						Type			co_MyType;

					protected readonly	Guid			cc_Token;
					protected readonly	Guid			cc_ClientID;
					protected readonly	string		cc_Topic;
					protected readonly	bool			cb_Replace;
					protected readonly	bool			cb_AllowMany;

				#endregion
				//_________________________________________________________________________________________
				#region ** [Properties] **

					public Guid		MyToken			{ get { return	this.cc_Token; } }
					public Guid		ClientToken	{ get { return	this.cc_ClientID; } }
					public string	Topic				{ get { return	this.cc_Topic; } }
					public bool		AllowMany		{ get { return	this.cb_AllowMany; } }
					public bool		Replace			{ get { return	this.cb_Replace; } }

					public Type		TypeOf			{ get { return	this.co_MyType; } }

				#endregion
				//_________________________________________________________________________________________
				#region ** [Constructors]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public SubscriptionBase(Guid		clientid	,
																	string	topic			,
																	bool		allowmany	,
																	bool		replace			)
						{
							this.co_MyType		= typeof(T);
							this.cc_Token			= Guid.NewGuid();
							//...........................................
							this.cc_ClientID	= clientid;
							this.cc_Topic			= topic;
							this.cb_AllowMany	= allowmany;
							this.cb_Replace		= replace;
						}

				#endregion
				//_________________________________________________________________________________________
				#region ** [Methods: Exposed]**

					public abstract void Invoke(T Message);

				#endregion

			}
		//*********************************************************************************************
		//*********************************************************************************************
		public sealed class SubscriptionWeak<T> : SubscriptionBase<T>
			{
				#region ** [Declarations] **

					private Type						co_Type;
					private WeakReference		co_TObj;
					private MethodInfo			co_MInfo;
					private bool						cb_IsStatic;

				#endregion
				//_________________________________________________________________________________________
				#region ** [Constructors]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public SubscriptionWeak(Guid			clientid	,
																	string		topic			,
																	Action<T>	action		,
																	bool			allowmany	= false	,
																	bool			replace		= false		) : base(	clientid	, 
																																					topic			,
																																					allowmany	,
																																					replace			)
						{
							this.co_Type			= typeof(T);
							this.co_TObj			= new WeakReference(action.Target);
							this.co_MInfo			= action.Method;
							this.cb_IsStatic	= (action.Target == null) ? true : false;
						}

				#endregion
				//_________________________________________________________________________________________
				#region ** [Methods: Exposed] **

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public override void Invoke(T Msg)
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
							lo_Action?.Invoke(Msg);
						}

				#endregion

			}
		//*********************************************************************************************
		//*********************************************************************************************
		public sealed class Subscription<T> : SubscriptionBase<T>
			{
				#region ** [Declarations] **

					private Action<T>				co_Action;

				#endregion
				//_________________________________________________________________________________________
				#region ** [Constructors]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public Subscription(Guid			clientid	,
															string		topic			,
															Action<T>	action		,
															bool			allowmany	= false	,
															bool			replace		= false		) : base(	clientid	, 
																																			topic			,
																																			allowmany	,
																																			replace			)
						{
							this.co_Action	= action;
						}

				#endregion
				//_________________________________________________________________________________________
				#region ** [Methods: Exposed] **

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public override void Invoke(T Msg)
						{
							this.co_Action?.Invoke(Msg);
						}

				#endregion

			}

	}

