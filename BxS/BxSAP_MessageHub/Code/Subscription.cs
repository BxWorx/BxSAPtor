using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
{
	public sealed class Subscription<T> : SubscriptionBase<T>
		{
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

		}
}

