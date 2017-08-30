namespace MsgHubv01
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	internal sealed class SubscriptionsByTopic
		{
			#region **[Declarations]**

				private ConcurrentDictionary<string, IList<ISubscription>>	ct_SubsByTopic;

				private bool		cb_AllowMultiple;
				private object	co_Lock;

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Exposed]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Clear(string topic = default(string))
					{
						if (string.IsNullOrEmpty(topic))
							{
								lock (this.co_Lock)
									{
										this.ct_SubsByTopic.Clear();
									}
							}
						else
							{
								IList<ISubscription>	lt_Subs;

								if (this.ct_SubsByTopic.TryGetValue(topic, out lt_Subs))
									{	
										lock (this.co_Lock)
											{
												lt_Subs.Clear();
											}
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void DeRegister(ISubscription subscription)
					{
						IList<ISubscription>	lt_Subs;

						if (this.ct_SubsByTopic.TryGetValue(subscription.Topic, out lt_Subs))
							{
								lock (this.co_Lock)
									{
										lt_Subs.Remove(subscription);
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Register(ISubscription subscription)
					{
						var lt_Subs	= this.ct_SubsByTopic.GetOrAdd(subscription.Topic, (key) => new List<ISubscription>());

						lock (this.co_Lock)
							{
								lt_Subs.Add(subscription);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal int Count(string topic	= default(string), Guid subscriberid = default(Guid), Guid subscriptionid = default(Guid))
					{
						return	this.FetchSubscriptions(topic, subscriberid, subscriptionid).Count();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<ISubscription> GetSubscriptions(string topic	= default(string), Guid subscriberid = default(Guid), Guid subscriptionid = default(Guid))
					{
						return	this.FetchSubscriptions(topic, subscriberid, subscriptionid);
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SubscriptionsByTopic(bool allowmultiple = false)
					{
						this.co_Lock					= new object();
						this.cb_AllowMultiple	= allowmultiple;
						this.ct_SubsByTopic		= new	ConcurrentDictionary<string, IList<ISubscription>>();
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods: Private]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<ISubscription> FetchSubscriptions( string topic, Guid	subscriberid, Guid subscriptionid )
					{
						IList<ISubscription> lt_List;

						lock (this.co_Lock)
							{
								if (string.IsNullOrEmpty(topic) && subscriberid == default(Guid))
									{
										lt_List	= this.ct_SubsByTopic
																.SelectMany(kvp => kvp.Value)
																.ToList();
									}

								else if (!string.IsNullOrEmpty(topic) && subscriberid != default(Guid))
									{
										lt_List	= this.ct_SubsByTopic
																.Where(kvp => kvp.Key == topic)
																.SelectMany(val => val.Value)
																.Where(sub => sub.SubscriberID == subscriberid)
																.ToList();
									}

								else if (subscriberid != default(Guid))
									{
										lt_List	= this.ct_SubsByTopic
																.SelectMany(kvp => kvp.Value)
																.Where(sub => sub.SubscriberID == subscriberid)
																.ToList();
									}

								else
									{
										lt_List	= this.ct_SubsByTopic
																.Where(kvp => kvp.Key == topic)
																.SelectMany(val => val.Value)
																.ToList(); }
							}
							//.............................................
							if (subscriberid != default(Guid))
								{	return	lt_List.Where(sub => sub.MyToken == subscriberid).ToList();	}

							return	lt_List;
					}

			#endregion

		}
}
