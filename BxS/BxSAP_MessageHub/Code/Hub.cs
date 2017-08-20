using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		public class Hub
			{
				#region **[Declarations]**

					private	readonly ConcurrentDictionary<string,	Subscriptions	>		ct_Topics;				// key  = topic			,	value = subscription

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Properties]**
				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Constructors]**

					//____________________________________________________________________________________________
					public Hub()
						{
							this.ct_Topics	= new ConcurrentDictionary<string,	Subscriptions>();
						}

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Methods:Exposed]**

					//____________________________________________________________________________________________
					public int SubscriptionCount(string Topic)
						{ return this.SubsCount(Topic); }

					//____________________________________________________________________________________________
					public int ClientCount(string Topic)
						{ return this.ClntCount(Topic); }

					//____________________________________________________________________________________________
					public Guid Subscribe(Subscription subscription)
						{
							var	lt_Subscriptions	= this.GetAddTopicSubscriptions(subscription.Topic);

							if (lt_Subscriptions.Register(subscription))
								{	return	subscription.MyToken;	}

							return	new Guid();
						}

					//____________________________________________________________________________________________
					public Guid Subscribe<T>(Guid clientid, string topic, Action<T> action, bool allowmany = false, bool replace = true)
						{
							return	this.Subscribe(	MsgHubFactory.Subscription(clientid, topic, allowmany, replace, action) );
						}

					//____________________________________________________________________________________________
					//public Guid UnSubscribe()
					//	{
					//		//var Sub = new Subscription();
							
							


					//		//return Sub.MyID;
					//	}

					//____________________________________________________________________________________________
					//public Guid UnSubscribeAll()
					//	{
					//		//var Sub = new Subscription();
							
							


					//		//return Sub.MyID;
					//	}

					


				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Methods:Private]**

					////____________________________________________________________________________________________
					//private bool IsClientSubscribedToTopic(Guid ClientID, string Topic)
					//	{
					//		return this.GetClientTopics(ClientID).Contains(Topic);
					//	}

					////____________________________________________________________________________________________
					//private IList<string>	GetClientTopics(Guid clientid)
					//	{
					//		IList<string> lt_Topics;

					//		if (!this.ct_ClientTopics.TryGetValue(clientid, out lt_Topics))
					//			{
					//				lt_Topics = new List<string>();
					//			}

					//		return lt_Topics;
					//	}

					//____________________________________________________________________________________________
					private Subscriptions GetAddTopicSubscriptions(string topic)
						{
							Subscriptions lo_Subs;

							if (!this.ct_Topics.TryGetValue(topic, out lo_Subs))
								{
									lo_Subs = new Subscriptions(topic);
									this.ct_Topics.TryAdd(topic, lo_Subs);
								}

							return lo_Subs;
						}

					//____________________________________________________________________________________________
					private int SubsCount(string topic)
						{
							Subscriptions lo_Subs;

							if (this.ct_Topics.TryGetValue(topic, out lo_Subs))
								{	return	lo_Subs.SubscriptionCount; }
							else
								{ return 0;	}
						}

					//____________________________________________________________________________________________
					private int ClntCount(string topic)
						{
							Subscriptions lo_Subs;

							if (this.ct_Topics.TryGetValue(topic, out lo_Subs))
								{	return	lo_Subs.ClientCount; }
							else
								{ return 0;	}
						}

				#endregion

			}
	}
