using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
{
	public class MessageHub	: IMessageHub
		{
			#region **[Declarations]**

				private	readonly	ConcurrentDictionary<string, SubscriptionsByTopic>	ct_SubsByTopic;		// key=topic

			#endregion
			//___________________________________________________________________________________________
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public MessageHub()
					{
						this.ct_SubsByTopic	= new ConcurrentDictionary<string,	SubscriptionsByTopic>();
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Exposed]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ISubscription<T>	CreateSubscription<T>(Guid			clientid					,
																											string		topic							,
																											Action<T>	action						,
																											bool			allowmany = false	,
																											bool			replace		= true		)
					{
						return	new Subscription<T>(clientid, topic, action, allowmany, replace);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ISubscription<T>	CreateSubscriptionWeak<T>(Guid			clientid					,
																													string		topic							,
																													Action<T>	action						,
																													bool			allowmany = false	,
																													bool			replace		= true		)
					{
						return	new SubscriptionWeak<T>(clientid, topic, action, allowmany, replace);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ISubscription<T>	Subscribe<T>(	Guid			clientid					,
																							string		topic							,
																							Action<T>	action						,
																							bool			allowmany = false	,
																							bool			replace		= true	,
																							bool			asweak		= true		)
					{
						ISubscription<T>	lo_Sub;
						//.............................................
						if (asweak)
							{
								lo_Sub	= new SubscriptionWeak<T>	(clientid, topic, action, allowmany, replace);
							}
						else
							{
								lo_Sub	= new Subscription<T>			(clientid, topic, action, allowmany, replace);
							}

						this.Subscribe(lo_Sub);
						//.............................................
						return	lo_Sub;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public Guid Subscribe<T>(ISubscription<T> subscription)
					{
						var	lt_Subscriptions	= this.GetAddTopicSubscriptions(subscription.Topic);

						if (lt_Subscriptions.Register(subscription))
							{	return	subscription.MyToken;	}

						return	new Guid();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Publish<T>(string topic, T message, CancellationToken ct = default(CancellationToken))
					{
						foreach (var lo_sub in this.ct_SubsByTopic[topic].SubscriptionList<T>())
							{
								if (ct.IsCancellationRequested)	break;
								if (lo_sub != null)	lo_sub.Invoke(message);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task PublishAsync<T>(string topic, T message, CancellationToken ct = default(CancellationToken))
					{
						await Task.Factory.StartNew( () =>
							{
								foreach (var lo_sub in this.ct_SubsByTopic[topic].SubscriptionList<T>())
									{
										if (lo_sub != null)	lo_sub.Invoke(message);
									}
							}	, ct,	TaskCreationOptions.PreferFairness, TaskScheduler.Default );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void PublishBackground<T>(string topic, T message, CancellationToken ct = default(CancellationToken))
					{
						foreach (var lo_sub in this.ct_SubsByTopic[topic].SubscriptionList<T>())
							{
								if (lo_sub != null)
									{
										ISubscription<T> lo_ExecSub	= lo_sub;
										Task.Factory.StartNew( () =>
											{
												lo_ExecSub.Invoke(message);
											} , ct	,	TaskCreationOptions.PreferFairness,	TaskScheduler.Default );
									}
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void UnSubscribe<T>(ISubscription<T> subscription)
					{
						this.UnsubscribeFromTypes(subscription);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void UnSubscribe(string topic)
					{
						SubscriptionsByTopic lo_Subs;

						if (this.ct_SubsByTopic.TryGetValue(topic, out lo_Subs))		lo_Subs.Reset();							
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void UnSubscribeAll()
					{
						this.ct_SubsByTopic.Clear();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public int SubscriptionCount(string topic)
					{
						SubscriptionsByTopic lo_Subs;

						if (this.ct_SubsByTopic.TryGetValue(topic, out lo_Subs))
							{	return	lo_Subs.Count; }
						else
							{ return 0;	}
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Private]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private void UnsubscribeFromTypes<T>(ISubscription<T> subscription)
					{
						foreach (var lo_Subs in this.ct_SubsByTopic)
							{
								if (lo_Subs.Value.DeRegister(subscription)) { }
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private SubscriptionsByTopic GetAddTopicSubscriptions(string topic)
					{
						SubscriptionsByTopic lo_Subs;

						if (!this.ct_SubsByTopic.TryGetValue(topic, out lo_Subs))
							{
								lo_Subs = new SubscriptionsByTopic(topic);
								this.ct_SubsByTopic.TryAdd(topic, lo_Subs);
							}

						return lo_Subs;
					}

			#endregion

		}
}
