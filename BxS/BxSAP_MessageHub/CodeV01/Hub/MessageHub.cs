namespace MsgHubv01
{
	using System;
	using System.Runtime.CompilerServices;
	using System.Collections.Concurrent;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using System.Linq;

	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public class MessageHub	: IMessageHub
		{
			#region **[Declarations]**

				private	ConcurrentDictionary<Type, SubscriptionsByTopic>			ct_SubsByType;
				private	object	co_Lock;

			#endregion
			//___________________________________________________________________________________________
			#region **[Properties]**

				public bool	AllowMultiple { get; private set; }

			#endregion
			//___________________________________________________________________________________________
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public MessageHub( bool allowmultiple = false )
					{
						this.co_Lock				= new object();
						this.AllowMultiple	= allowmultiple;
						this.ct_SubsByType	= new	ConcurrentDictionary<Type, SubscriptionsByTopic>();
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Exposed]**

				public int Count( string Topic = default(string), Guid SubscriberID = default(Guid), Guid SubscriptionID = default(Guid) )
					{
						return	this.ct_SubsByType
											.SelectMany( kvp => kvp.Value.GetSubscriptions( Topic, SubscriberID, SubscriptionID ))
											.Count();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Subscribe<T>(string topic, Guid subscriberid, Action<T> action, bool AsWeak = false)
					{
						ISubscription					lo_Sub;
						SubscriptionsByTopic	lo_Topic;
						//.............................................
						if (AsWeak) { lo_Sub	= new SubscriptionWeak	(topic, subscriberid, action); }
						else				{	 lo_Sub	= new Subscription			(topic, subscriberid, action); }
						//.............................................
						lo_Topic	=	this.ct_SubsByType.GetOrAdd(typeof(T), (key) => new SubscriptionsByTopic(this.AllowMultiple) );
						lo_Topic.Register(lo_Sub);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Publish<T>(T data, string Topic = default(string), Guid SubscriberID = default(Guid), Guid SubscriptionID = default(Guid))
					{
						foreach (var lo_Sub in this.GetSubscriptions<T>(Topic, SubscriberID, SubscriptionID))
							{
								lo_Sub.Invoke(data);
							}

						//SubscriptionsByTopic	lo_Topic;
						////.............................................
						//if (this.ct_SubsByType.TryGetValue(typeof(T), out lo_Topic))
						//	{
						//		foreach (var lo_Sub in lo_Topic.GetSubscriptions(Topic, SubscriberID, SubscriptionID))
						//			{
						//				lo_Sub.Invoke(data);
						//			}
						//	}




					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void PublishBackground<T>(	T data,	string	Topic						= default(string)	,
																									Guid		SubscriberID		= default(Guid)		,
																									Guid		SubscriptionID	= default(Guid)		,
																									CancellationToken		ct	= default( CancellationToken ) )
					{
						foreach (var lo_Sub in this.GetSubscriptions<T>(Topic, SubscriberID, SubscriptionID))
							{
								ISubscription lo_ExecSub = lo_sub;
								Task.Factory.StartNew( () =>
									{
										lo_ExecSub.Invoke( message );
									}, ct, TaskCreationOptions.PreferFairness, TaskScheduler.Default );
							}
						}



		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public ISubscription	CreateSubscription<T>(Guid			clientid					,
		//																							string		topic							,
		//																							Action<T>	action						,
		//																							bool			allowmany = false	,
		//																							bool			replace		= true		)
		//	{
		//		return	new Subscription(clientid, topic, action, allowmany, replace);
		//	}

		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public ISubscription	CreateSubscriptionWeak<T>(Guid			clientid					,
		//																									string		topic							,
		//																									Action<T>	action						,
		//																									bool			allowmany = false	,
		//																									bool			replace		= true		)
		//	{
		//		return	new SubscriptionWeak<T>(clientid, topic, action, allowmany, replace);
		//	}

		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public ISubscription	Subscribe<T>(	Guid			clientid					,
		//																			string		topic							,
		//																			Action<T>	action						,
		//																			bool			allowmany = false	,
		//																			bool			replace		= true	,
		//																			bool			asweak		= true		)
		//	{
		//		ISubscription<T>	lo_Sub;
		//		//.............................................
		//		if (asweak)
		//			{
		//				lo_Sub	= new SubscriptionWeak<T>	(clientid, topic, action, allowmany, replace);
		//			}
		//		else
		//			{
		//				lo_Sub	= new Subscription<T>			(clientid, topic, action, allowmany, replace);
		//			}

		//		this.Subscribe(lo_Sub);
		//		//.............................................
		//		return	lo_Sub;
		//	}

		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public Guid Subscribe<T>(ISubscription subscription)
		//	{
		//		var	lt_Subscriptions	= this.GetAddTopicSubscriptions(subscription.Topic);

		//		if (lt_Subscriptions.Register(subscription))
		//			{	return	subscription.MyToken;	}

		//		return	new Guid();
		//	}

		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public void Publish<T>(string topic, T message, CancellationToken ct = default(CancellationToken))
		//	{
		//		foreach (var lo_sub in this.ct_SubsByTopic[topic].SubscriptionList<T>())
		//			{
		//				if (ct.IsCancellationRequested)	break;
		//				if (lo_sub != null)	lo_sub.Invoke(message);
		//			}
		//	}

		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public async Task PublishAsync<T>(string topic, T message, CancellationToken ct = default(CancellationToken))
		//	{
		//		await Task.Factory.StartNew( () =>
		//			{
		//				foreach (var lo_sub in this.ct_SubsByTopic[topic].SubscriptionList<T>())
		//					{
		//						if (lo_sub != null)	lo_sub.Invoke(message);
		//					}
		//			}	, ct,	TaskCreationOptions.PreferFairness, TaskScheduler.Default );
		//	}


		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public void UnSubscribe<T>(ISubscription subscription)
		//	{
		//		this.UnsubscribeFromTypes(subscription);
		//	}

		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public void UnSubscribe(Guid subscriptionid)
		//	{
		//		//SubscriptionsByTopic lo_Subs;

		//		//if (this.ct_SubsByTopic.TryGetValue(topic, out lo_Subs))		lo_Subs.Reset();							
		//	}
		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public void UnSubscribe(string topic)
		//	{
		//		SubscriptionsByTopic lo_Subs;

		//		if (this.ct_SubsByTopic.TryGetValue(topic, out lo_Subs))		lo_Subs.Reset();							
		//	}

		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public void UnSubscribeAll()
		//	{
		//		this.ct_SubsByTopic.Clear();
		//	}

		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public int SubscriptionCount(string topic)
		//	{
		//		SubscriptionsByTopic lo_Subs;

		//		if (this.ct_SubsByTopic.TryGetValue(topic, out lo_Subs))
		//			{	return	lo_Subs.Count; }
		//		else
		//			{ return 0;	}
		//	}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Private]**

				private IList<ISubscription> GetSubscriptions<T>(string topic, Guid subscriberid, Guid subscriptionid)
					{
						SubscriptionsByTopic	lo_Topic;
						//.............................................
						if (this.ct_SubsByType.TryGetValue(typeof(T), out lo_Topic))
							{	return	lo_Topic.GetSubscriptions(topic, subscriberid, subscriptionid); }
						//.............................................
						return	new List<ISubscription>();
					}

				private int CountAll(string topic	= default(string), Guid subscriber	= default(Guid)	)
					{
						//KeyValuePair<Type, SubscriptionsByTopic>[]	lt_Snap;

						lock (this.co_Lock)
							{
								//lt_Snap	= this.ct_SubsByType.Values.ToList().Where( (x) => x.  .ToArray().SelectMany ;
							}

							return	0;

						//return	(from lo_Typ in lt_Snap
						//					select lo_Typ.Value.Count).Sum();
					}


				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private void UnsubscribeFromTypes<T>(ISubscription subscription)
				//	{
				//		foreach (var lo_Subs in this.ct_SubsByTopic)
				//			{
				//				if (lo_Subs.Value.DeRegister(subscription)) { }
				//			}
				//	}

				////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//private SubscriptionsByTopic GetAddTopicSubscriptions(string topic)
				//	{
				//		SubscriptionsByTopic lo_Subs;

				//		if (!this.ct_SubsByTopic.TryGetValue(topic, out lo_Subs))
				//			{
				//				lo_Subs = new SubscriptionsByTopic(topic);
				//				this.ct_SubsByTopic.TryAdd(topic, lo_Subs);
				//			}

				//		return lo_Subs;
				//	}

			#endregion

		}
}
