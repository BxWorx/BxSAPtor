namespace MsgHubv01
{
	using System;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public sealed class SubscriptionKey
		{
			#region **[Properties]**

				internal string Topic					{ get; private set; }
				internal Guid		SubscriberID	{ get; private set; }

			#endregion
			//___________________________________________________________________________________________
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SubscriptionKey(	string	topic					,
																	Guid		subscriberid		)
					{
						this.Topic				= topic;
						this.SubscriberID	= subscriberid;
					}

			#endregion

		}
}
