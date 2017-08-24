using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		public class Hub
			{
				#region **[Declarations]**

					private	readonly	ConcurrentDictionary<string, SubscriptionsByTopic>	ct_SubsByTopic;		// key=topic

				#endregion
				//_________________________________________________________________________________________
				#region **[Constructors]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public Hub()
						{
							this.ct_SubsByTopic	= new ConcurrentDictionary<string,	SubscriptionsByTopic>();
						}

				#endregion
				//_________________________________________________________________________________________
				#region **[Methods:Exposed]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public ISubscription<T>	CreateSubscription<T>(Guid			clientid					,
																												string		topic							,
																												Action<T>	action						,
																												bool			allowmany = false	,
																												bool			replace		= true		)
						{
							return	new Subscription<T>(clientid, topic, action, allowmany, replace);
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public ISubscription<T>	CreateSubscriptionWeak<T>(Guid			clientid					,
																														string		topic							,
																														Action<T>	action						,
																														bool			allowmany = false	,
																														bool			replace		= true		)
						{
							return	new SubscriptionWeak<T>(clientid, topic, action, allowmany, replace);
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public Guid Subscribe<T>(ISubscription<T> subscription)
						{
							var	lt_Subscriptions	= this.GetAddTopicSubscriptions(subscription.Topic);

							if (lt_Subscriptions.Register(subscription))
								{	return	subscription.MyToken;	}

							return	new Guid();
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void Publish<T>(string topic, T message, CancellationToken ct = default(CancellationToken))
						{
							foreach (var lo_sub in this.ct_SubsByTopic[topic].SubscriptionList<T>())
								{
									if (ct.IsCancellationRequested)	break;
									if (lo_sub != null)	lo_sub.Invoke(message);
								}
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
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

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
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

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void UnSubscribe<T>(ISubscription<T> subscription)
						{
							this.UnsubscribeFromTypes(subscription);
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void UnSubscribe(string topic)
						{
							SubscriptionsByTopic lo_Subs;

							if (this.ct_SubsByTopic.TryGetValue(topic, out lo_Subs))		lo_Subs.Reset();							
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void UnSubscribeAll()
						{
							this.ct_SubsByTopic.Clear();
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public int SubscriptionCount(string topic)
						{
							SubscriptionsByTopic lo_Subs;

							if (this.ct_SubsByTopic.TryGetValue(topic, out lo_Subs))
								{	return	lo_Subs.Count; }
							else
								{ return 0;	}
						}

				#endregion
				//_________________________________________________________________________________________
				#region **[Methods:Private]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					private void UnsubscribeFromTypes<T>(ISubscription<T> subscription)
						{
							foreach (var lo_Subs in this.ct_SubsByTopic)
								{
									if (lo_Subs.Value.DeRegister(subscription)) { }
								}
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
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

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					//private IList<ISubscription> GetTopicSubscriptions(string topic)
					//	{
					//		return	new List<ISubscription>();
					//		//return	this.ct_Topics[topic].SubscriptionList;
					//	}

					////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					//public int ClientCount(string topic)
					//	{
					//		return 0;
					//		//SubscriptionsByType lo_Subs;

					//		//if (this.ct_Topics.TryGetValue(topic, out lo_Subs))
					//		//	{	return	lo_Subs.ClientCount; }
					//		//else
					//		//	{ return 0;	}
					//	}



//	lo_Tasks.Add(
//		Task.Factory.StartNew<iExcelRow>(
//			() => {
//							return new ExcelRow(indexNo: ln_Idx, i_RowNo: ln_RowNo, i_Data: lo_RowData, i_SearchEOT: _SearchEOT);
//							}, _ct ))

//IList<Task<bool>> lt_Tasks	= new List<Task<bool>>();

//foreach (var lo_sub in this.ct_Topics[topic].SubscriptionList)
//	{
//		if (lo_sub != null)
//			{
//				Subscription lo_TObj	= lo_sub;
//				lt_Tasks.Add(
//					Task.Factory.StartNew<bool>(
//						() => {
//										lo_TObj?.Invoke(message);
//										return	true;
//									}, lo_ct ));
//			}
//	}
////...........................................
//while (lt_Tasks.Count > 0)
//	{
//		if (lo_ct.IsCancellationRequested)
//			{
//				this.
//			}

//	}

////____________________________________________________________________________________________
//public Guid Subscribe<T>(Guid clientid, string topic, Action<T> action, bool allowmany = false, bool replace = true)
//	{
//	var lo_Subs = new SubscriptionWeak<T>(clientid, topic, allowmany, replace, action);
//	return this.Subscribe(lo_Subs);
//	//return	this.Subscribe(	MsgHubFactory.Subscription(clientid, topic, allowmany, replace, action) );
//	}
