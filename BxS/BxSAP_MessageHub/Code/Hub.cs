using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		public class Hub
			{
				#region **[Declarations]**

					private	readonly ConcurrentDictionary<string,	Subscriptions	>		ct_Topics;		// key  = topic

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
					public void Publish<T>(string topic, T message)
						{
							foreach (var lo_sub in this.ct_Topics[topic].SubscriptionList)
								{
									if (lo_sub != null)	lo_sub.Invoke(message);
								}
						}
					//____________________________________________________________________________________________
					public async Task PublishAsync<T>(string topic, T message)
						{
							await Task.Factory.StartNew(	() =>
									{
										foreach (var lo_sub in this.ct_Topics[topic].SubscriptionList)
											{
												if (lo_sub != null)	lo_sub.Invoke(message);
											}
									}
									,TaskCreationOptions.PreferFairness );
						}

		//lo_Tasks.Add(Task.Factory.StartNew<iExcelRow>(() => { return new ExcelRow(indexNo: ln_Idx, i_RowNo: ln_RowNo, i_Data: lo_RowData, i_SearchEOT: _SearchEOT); }, _ct))


					//____________________________________________________________________________________________
					public int SubscriptionCount(string topic)
						{
							Subscriptions lo_Subs;

							if (this.ct_Topics.TryGetValue(topic, out lo_Subs))
								{	return	lo_Subs.SubscriptionCount; }
							else
								{ return 0;	}
						}

					//____________________________________________________________________________________________
					public int ClientCount(string topic)
						{
							Subscriptions lo_Subs;

							if (this.ct_Topics.TryGetValue(topic, out lo_Subs))
								{	return	lo_Subs.ClientCount; }
							else
								{ return 0;	}
						}

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
					public void UnSubscribe(Subscription subscription)
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
							Subscriptions lo_Subs;

							if (this.ct_Topics.TryGetValue(topic, out lo_Subs))		lo_Subs.Clear();							
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
									if (lo_Subs.Value.DeRegister(token)) { break; }
								}
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

					//____________________________________________________________________________________________
					private IList<Subscription> GetTopicSubscriptions(string topic)
						{
							return	this.ct_Topics[topic].SubscriptionList;
						}

				#endregion

			}
	}
