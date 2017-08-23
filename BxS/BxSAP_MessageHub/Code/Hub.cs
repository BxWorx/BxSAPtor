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

					private	readonly	ConcurrentDictionary<string,	SubscriptionsByType	>		ct_Topics;		// key  = topic

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Properties]**

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Constructors]**

					//____________________________________________________________________________________________
					public Hub()
						{
							this.ct_Topics	= new ConcurrentDictionary<string,	SubscriptionsByType>();
						}

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Methods:Exposed]**

					//____________________________________________________________________________________________
					public void Publish<T>(string topic, T message, CancellationToken ct = default(CancellationToken))
						{
							//foreach (var lo_sub in this.ct_Topics[topic].SubscriptionList)
							//	{
							//		if (lo_sub != null)	lo_sub.Invoke(message);
							//	}
						}

					//____________________________________________________________________________________________
					public async Task PublishAsync<T>(string topic, T message, CancellationToken ct = default(CancellationToken))
						{
							await Task.Factory.StartNew(	() =>
									{
										//foreach (var lo_sub in this.ct_Topics[topic].SubscriptionList)
										//	{
										//		if (lo_sub != null)	lo_sub.Invoke(message);
										//	}
									}	, ct,	TaskCreationOptions.PreferFairness, TaskScheduler.Default );
						}

					//____________________________________________________________________________________________
					public void PublishBackground<T>(string topic, T message, CancellationToken ct = default(CancellationToken))
						{
							//foreach (var lo_sub in this.ct_Topics[topic].SubscriptionList)
							//	{
							//		if (lo_sub != null)
							//			{
							//				Subscription lo_ExecSub	= lo_sub;
							//				Task.Factory.StartNew( () =>
							//					{
							//						lo_ExecSub.Invoke(message);
							//					} , ct	,	TaskCreationOptions.PreferFairness,	TaskScheduler.Default );
							//			}
							//	}
						}

					//____________________________________________________________________________________________
					public int SubscriptionCount(string topic)
						{
							SubscriptionsByType lo_Subs;

							if (this.ct_Topics.TryGetValue(topic, out lo_Subs))
								{	return	lo_Subs.SubscriptionCount; }
							else
								{ return 0;	}
						}

					//____________________________________________________________________________________________
					public int ClientCount(string topic)
						{
							SubscriptionsByType lo_Subs;

							if (this.ct_Topics.TryGetValue(topic, out lo_Subs))
								{	return	lo_Subs.ClientCount; }
							else
								{ return 0;	}
						}

					//____________________________________________________________________________________________
					public Guid Subscribe<T>(SubscriptionWeak<T> subscription)
						{
							var	lt_Subscriptions	= this.GetAddTopicSubscriptions(subscription.Topic);

							if (lt_Subscriptions.Register(subscription))
								{	return	subscription.MyToken;	}

							return	new Guid();
						}

					//____________________________________________________________________________________________
					public Guid Subscribe<T>(Guid clientid, string topic, Action<T> action, bool allowmany = false, bool replace = true)
						{
							var lo_Subs	= new SubscriptionWeak<T>(clientid, topic, allowmany, replace, action);
							return	this.Subscribe(	lo_Subs );
							//return	this.Subscribe(	MsgHubFactory.Subscription(clientid, topic, allowmany, replace, action) );
						}

					//____________________________________________________________________________________________
					public void UnSubscribe(ISubscription subscription)
						{
							this.UnsubscribeToken(subscription.MyToken);
						}

					//____________________________________________________________________________________________
					public void UnSubscribe(Guid token)
						{
							this.UnsubscribeToken(token);
						}

					//____________________________________________________________________________________________
					public void UnSubscribe(string topic)
						{
							SubscriptionsByType lo_Subs;

							if (this.ct_Topics.TryGetValue(topic, out lo_Subs))		lo_Subs.Reset();							
						}

					//____________________________________________________________________________________________
					public void UnSubscribeAll()
						{
							this.ct_Topics.Clear();
						}

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Methods:Private]**

					//____________________________________________________________________________________________
					private void UnsubscribeToken(Guid token)
						{
							foreach (var lo_Subs in this.ct_Topics)
								{
									//if (lo_Subs.Value.DeRegister(token)) { break; }
								}
						}

					//____________________________________________________________________________________________
					private SubscriptionsByType GetAddTopicSubscriptions(string topic)
						{
							SubscriptionsByType lo_Subs;

							if (!this.ct_Topics.TryGetValue(topic, out lo_Subs))
								{
									lo_Subs = new SubscriptionsByType(topic);
									this.ct_Topics.TryAdd(topic, lo_Subs);
								}

							return lo_Subs;
						}

					//____________________________________________________________________________________________
					private IList<ISubscription> GetTopicSubscriptions(string topic)
						{
							return	new List<ISubscription>();
							//return	this.ct_Topics[topic].SubscriptionList;
						}

				#endregion

			}
	}



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
