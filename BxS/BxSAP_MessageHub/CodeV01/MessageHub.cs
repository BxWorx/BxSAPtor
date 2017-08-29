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
				//private ConditionalWeakTable<SubscriptionKey, ISubscription>	co_cwt;

			#endregion
			//___________________________________________________________________________________________
			#region **[Properties]**

				public bool	AllowMultiple { get; private set; }
				public int	Count					{ get {	return	this.CountAll(); } }

			#endregion
			//___________________________________________________________________________________________
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public MessageHub( bool allowmultiple = false )
					{
						this.co_Lock				= new object();
						this.AllowMultiple	= allowmultiple;
						//this.co_cwt					= new ConditionalWeakTable<SubscriptionKey, ISubscription>();
						this.ct_SubsByType	= new	ConcurrentDictionary<Type, SubscriptionsByTopic>();
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Exposed]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Subscribe<T>(string topic, Guid subscriberid, Action<T> action)
					{
											//var	lo_Key	= new SubscriptionKey	(topic, subscriberid);
						ISubscription	lo_Sub	= new Subscription		(topic, subscriberid, action);

						//this.co_cwt.Add(lo_Key, lo_Sub);
						
						var lt_SubsType	=	this.ct_SubsByType.GetOrAdd(typeof(T), (key) => new SubscriptionsByTopic(this.AllowMultiple) );
						lt_SubsType.Register(lo_Sub);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Publish<T>(string topic, Guid subscriberid, T data)
					{
						//					var	lo_Key	= new SubscriptionKey	(topic, subscriberid);
						//ISubscription	lo_Subs	= null;

						//if (this.co_cwt.TryGetValue(lo_Key, out lo_Subs))
						//	{
						//		lo_Subs.Invoke(data);
						//	}
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
				//public void PublishBackground<T>(string topic, T message, CancellationToken ct = default(CancellationToken))
				//	{
				//		foreach (var lo_sub in this.ct_SubsByTopic[topic].SubscriptionList<T>())
				//			{
				//				if (lo_sub != null)
				//					{
				//						ISubscription	lo_ExecSub	= lo_sub;
				//						Task.Factory.StartNew( () =>
				//							{
				//								lo_ExecSub.Invoke(message);
				//							} , ct	,	TaskCreationOptions.PreferFairness,	TaskScheduler.Default );
				//					}
				//			}
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

				private int CountAll(string topic	= default(string), Guid subscriber	= default(Guid)	)
					{
						KeyValuePair<Type, SubscriptionsByTopic>[]	lt_Snap;

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
