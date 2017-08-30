namespace MsgHubv01
{
	using System;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public sealed class Subscription : SubscriptionBase
		{
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public Subscription(string		topic					,
														Guid			subscriberid	,
														Delegate	action					) : base(	topic					,
																																subscriberid		)	
					{
						this.co_MyType	= (action.GetType()).GetGenericArguments()[0];
						this.co_Action	= action;
					}

			#endregion
		}
}
