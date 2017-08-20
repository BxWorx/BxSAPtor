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
					private readonly ConcurrentDictionary<Guid,		IList<string>	>		ct_ClientTopics;	// key	= client id	,	value	= topic

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Properties]**
				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Constructors]**

					//____________________________________________________________________________________________
					public Hub()
						{
							this.ct_Topics					= new ConcurrentDictionary<string,	Subscriptions>();
							this.ct_ClientTopics		= new ConcurrentDictionary<Guid,		IList<string>>();
						}

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Methods:Exposed]**

					//____________________________________________________________________________________________
					public Guid Subscribe(Subscription subscription)
						{
							var	lt_Subscriptions	= this.GetAddTopicSubscriptions(subscription.Topic);

							if (!subscription.AllowMany)
								{
									if (this.IsClientSubscribedToTopic(subscription.ClientID, subscription.Topic))
										{
											
										}
								}

							return	lt_Subscriptions.Register(subscription);
						}

					//____________________________________________________________________________________________
					public Guid Subscribe<T>(Guid clientid, string topic, Action<T> action, bool allowmany = true)
						{
							return	this.Subscribe(	MsgHubFactory.Subscription(clientid, topic, allowmany, action) );
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

					//____________________________________________________________________________________________
					private bool IsClientSubscribedToTopic(Guid ClientID, string Topic)
						{
							return this.GetClientTopics(ClientID).Contains(Topic);
						}

					//____________________________________________________________________________________________
					private IList<string>	GetClientTopics(Guid clientid)
						{
							IList<string> lt_Topics;

							if (!this.ct_ClientTopics.TryGetValue(clientid, out lt_Topics))
								{
									lt_Topics = new List<string>();
								}

							return lt_Topics;
						}

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

				#endregion
			}
	}
