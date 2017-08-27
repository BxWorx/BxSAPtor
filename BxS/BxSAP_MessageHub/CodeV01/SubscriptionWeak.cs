namespace MsgHubv01
{
	using System;
	using System.Reflection;
	//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public sealed class SubscriptionWeak : SubscriptionBase
		{
			#region ** [Declarations] **

				private WeakReference		co_TObj;
				private MethodInfo			co_MInfo;
				private bool						cb_IsStatic;

			#endregion
			//___________________________________________________________________________________________
			#region ** [Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SubscriptionWeak(string		topic					,
																Guid			subscriberid	,
																Delegate	action					) : base(	topic					,
																																		subscriberid		)	
					{
						this.co_MyType	= (action.GetType()).GetGenericArguments()[0];
						this.co_Action	= null;
						//.............................................
						this.co_TObj			= new WeakReference(action.Target);
						this.co_MInfo			= action.Method;
						this.cb_IsStatic	= (action.Target == null) ? true : false;
					}

			#endregion
			//___________________________________________________________________________________________
			#region ** [Methods: Protected]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override bool GetIsAlive()
					{
						return	this.co_TObj.IsAlive;
					}

			#endregion
			//___________________________________________________________________________________________
			#region ** [Methods: Exposed] **

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void Invoke<T>(T Msg)
					{
						Action<T> lo_Action = null;
						if (!this.GetIsAlive())	return;
						//..................................................
						if (this.co_TObj.Target != null)
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

}

