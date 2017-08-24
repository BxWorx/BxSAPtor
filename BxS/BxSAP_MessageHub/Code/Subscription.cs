using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
{
	public sealed class Subscription<T> : SubscriptionBase<T>
		{
			#region ** [Declarations] **

				private Action<T>		co_Action;

			#endregion
			//___________________________________________________________________________________________
			#region ** [Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
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
			//___________________________________________________________________________________________
			#region ** [Methods: Exposed] **

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public override void Invoke(T Msg)
					{
						this.co_Action?.Invoke(Msg);
					}

			#endregion

		}
}

