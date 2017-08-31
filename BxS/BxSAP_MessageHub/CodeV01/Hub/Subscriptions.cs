namespace MsgHubv01
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	internal sealed class Subscriptions
		{
			#region **[Declarations]**

				private ConcurrentDictionary<Guid, ISubscription>	ct_Subs;

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Exposed]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Clear()
					{
						this.ct_Subs.Clear();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void DeRegister(ISubscription subscription)
					{
						ISubscription	lo_Sub;
						this.ct_Subs.TryRemove(subscription.MyToken, out lo_Sub);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Register(ISubscription subscription)
					{
						this.ct_Subs.TryAdd(subscription.MyToken, subscription);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal int Count(	Guid		subscriberid		= default(Guid)		,
														Guid		subscriptionid	= default(Guid)			)
					{
						return	this.FetchSubscriptions(subscriberid, subscriptionid).Count();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<ISubscription> GetSubscriptions(	Guid		subscriberid		= default(Guid)		,
																												Guid		subscriptionid	= default(Guid)			)
					{
						return	this.FetchSubscriptions(subscriberid, subscriptionid);
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Subscriptions()
					{
						this.ct_Subs	= new	ConcurrentDictionary<Guid, ISubscription>();
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods: Private]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<ISubscription> FetchSubscriptions(	Guid	subscriberid		,
																													Guid	subscriptionid		)
					{
						IList<ISubscription> lt_List;
						
						if (subscriberid == default(Guid) && subscriptionid == default(Guid))
							{
								lt_List	= this.ct_Subs.Values.ToList();		// true snapshot
							}

						else if (subscriberid != default(Guid))
							{
								lt_List	= this.ct_Subs.Values
														.ToList()
														.Where(sub => sub.SubscriberID == subscriberid)
														.ToList();
							}

						else
							{
								ISubscription lo_Sub;

								lt_List	= new List<ISubscription>();
								if (this.ct_Subs.TryGetValue(subscriptionid, out lo_Sub))
									lt_List.Add(this.ct_Subs[subscriptionid]);
							}

						return	lt_List;
					}

			#endregion

		}
}
