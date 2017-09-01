namespace MsgHubv01
{
	using System;
	using System.Collections.Concurrent;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using System.Linq;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public class MessageHub	: IMessageHub
		{
			#region **[Declarations]**

				private	ReaderWriterLockSlim									co_cacheLock;
				private	ConcurrentDictionary<Type, Topics>		ct_SubsByType;
				private readonly bool													cb_AllowMultiple;

			#endregion
			//___________________________________________________________________________________________
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public MessageHub( bool allowmultiple = false )
					{
						this.co_cacheLock				= new ReaderWriterLockSlim();
						this.cb_AllowMultiple		= allowmultiple;
						this.ct_SubsByType			= new	ConcurrentDictionary<Type, Topics>();
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Exposed]**

				public int Count<T>( string Topic = default(string), Guid SubscriberID = default(Guid), Guid SubscriptionID = default(Guid) )
					{
						return	this.GetSubscriptions<T>( Topic, SubscriberID, SubscriptionID )
											.Count();
					}
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Subscribe<T>(ISubscription Subscription)
					{
						Topics	lo_Topic;

						lo_Topic	=	this.ct_SubsByType.GetOrAdd(typeof(T), (key) => new Topics(this.cb_AllowMultiple) );
						lo_Topic.Register(Subscription);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ISubscription Subscribe<T>(Action<T> Action, string Topic = default(string), Guid SubscriberID = default(Guid), bool AsWeak = false)
					{
						ISubscription		lo_Sub;

						if (AsWeak) { lo_Sub	= new SubscriptionWeak	(Topic, SubscriberID, Action); }
						else				{	 lo_Sub	= new Subscription			(Topic, SubscriberID, Action); }
						//.............................................
						this.Subscribe<T>(lo_Sub);
						//.............................................
						return	lo_Sub;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Publish<T>( T data, string Topic = default(string), Guid SubscriberID = default(Guid), Guid SubscriptionID = default(Guid) )
					{
						foreach (var lo_Sub in this.GetSubscriptions<T>(Topic, SubscriberID, SubscriptionID))
							{
								lo_Sub.Invoke(data);
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task<IList<ISubscription>> PublishAsAsync<T>(T				data															,
																					string	Topic						= default(string)	,
																					Guid		SubscriberID		= default(Guid)		,
																					Guid		SubscriptionID	= default(Guid)		,
																					CancellationToken		ct	= default( CancellationToken ) )
					{

						IList<ISubscription>	lt_list = new List<ISubscription>();

						return	lt_list;

						//var lt_Subs	= this.GetSubscriptions<T>( Topic, SubscriberID, SubscriptionID );

						//await Task.Factory.StartNew( () =>
						//	{
						//		foreach (var lo_Sub in lt_Subs )
						//			{
						//				lo_Sub.Invoke( data );
						//			}
						//	},	ct																	,
						//			TaskCreationOptions.PreferFairness	,
						//			TaskScheduler.Default									);
					}


				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task PublishAsync<T>(T				data															,
																					string	Topic						= default(string)	,
																					Guid		SubscriberID		= default(Guid)		,
																					Guid		SubscriptionID	= default(Guid)		,
																					CancellationToken		ct	= default( CancellationToken ) )
					{
						var lt_Subs	= this.GetSubscriptions<T>( Topic, SubscriberID, SubscriptionID );

						await Task.Factory.StartNew( () =>
							{
								foreach (var lo_Sub in lt_Subs )
									{
										lo_Sub.Invoke( data );
									}
							},	ct																	,
									TaskCreationOptions.PreferFairness	,
									TaskScheduler.Default									);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void PublishAsBackgroundTasks<T>(	T				data															,
																									string	Topic						= default(string)	,
																									Guid		SubscriberID		= default(Guid)		,
																									Guid		SubscriptionID	= default(Guid)		,
																									CancellationToken		ct	= default( CancellationToken ) )
					{
						var lt_Subs	= this.GetSubscriptions<T>( Topic, SubscriberID, SubscriptionID );

						foreach (var lo_Sub in lt_Subs)
							{
								ISubscription lo_ExecSub = lo_Sub;
								Task.Factory.StartNew(	() =>
									{
										lo_ExecSub.Invoke( data );
									},	ct																	,
											TaskCreationOptions.PreferFairness	,
											TaskScheduler.Default									);
							}
						}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Private]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<ISubscription> GetSubscriptions<T>(	string	topic						,
																													Guid		subscriberid		,
																													Guid		subscriptionid		)
					{
						this.co_cacheLock.EnterReadLock();

						try
							{
								Topics	lo_Topic;
								//.............................................
								if (this.ct_SubsByType.TryGetValue(typeof(T), out lo_Topic))
									{
										return	lo_Topic.GetSubscriptions(topic, subscriberid, subscriptionid);
									}
								//.............................................
								return	new List<ISubscription>();
							}
						finally	{	this.co_cacheLock.ExitReadLock(); }
					}

			#endregion

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
		//public void UnSubscribe<T>(ISubscription subscription)
		//	{
		//		this.UnsubscribeFromTypes(subscription);
		//	}

		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public void UnSubscribe(Guid subscriptionid)
		//	{
		//		//Topics lo_Subs;

		//		//if (this.ct_SubsByTopic.TryGetValue(topic, out lo_Subs))		lo_Subs.Reset();							
		//	}
		////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
		//public void UnSubscribe(string topic)
		//	{
		//		Topics lo_Subs;

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
		//		Topics lo_Subs;

		//		if (this.ct_SubsByTopic.TryGetValue(topic, out lo_Subs))
		//			{	return	lo_Subs.Count; }
		//		else
		//			{ return 0;	}
		//	}
