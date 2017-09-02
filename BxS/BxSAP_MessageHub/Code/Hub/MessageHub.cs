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
						this.ct_SubsByType			= new	ConcurrentDictionary<Type, Topics>();
						this.cb_AllowMultiple		= allowmultiple;
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Exposed]**
				
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public int Count( string	Topic						= default(string)	,
													Guid		SubscriberID		= default(Guid)			)
					{
						return	this.GetSubscriptions( Topic, SubscriberID )
											.Count();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public int Count<T>(	string	Topic						= default(string)	,
															Guid		SubscriberID		= default(Guid)		,
															Guid		SubscriptionID	= default(Guid)			)
					{
						return	this.GetSubscriptions<T>( Topic, SubscriberID, SubscriptionID )
											.Count();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ISubscription CreateSubscription<T>(	Action<T> Action													,
																										string		Topic					= default(string)	,
																										Guid			SubscriberID	= default(Guid)		,
																										bool			AsWeak				= false							)
					{
						ISubscription		lo_Sub;

						if (AsWeak) {	lo_Sub	= new SubscriptionWeak	(Topic, SubscriberID, Action); }
						else				{	lo_Sub	= new Subscription			(Topic, SubscriberID, Action); }
						//.............................................
						return	lo_Sub;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void Subscribe<T>(ISubscription Subscription)
					{
						Topics	lo_Topic;

						lo_Topic	=	this.ct_SubsByType.GetOrAdd(typeof(T), (key) => new Topics(this.cb_AllowMultiple) );
						lo_Topic.Register(Subscription);
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public ISubscription Subscribe<T>(	Action<T> Action													,
																						string		Topic					= default(string)	,
																						Guid			SubscriberID	= default(Guid)		,
																						bool			AsWeak				= false							)
					{
						ISubscription	lo_Sub	= this.CreateSubscription( Action, Topic, SubscriberID, AsWeak );
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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public async Task PublishAsOneTaskAsync<T>(	T				data															,
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
				public async Task<IList<ISubscription>> PublishAsAsync<T>(T				data															,
																																	string	Topic						= default(string)	,
																																	Guid		SubscriberID		= default(Guid)		,
																																	Guid		SubscriptionID	= default(Guid)		,
																																	CancellationToken		ct	= default( CancellationToken ) )
					{
						IList<Task<ISubscription>>	lo_Tasks		= new List<Task<ISubscription>>();
						IList<ISubscription>				lt_Results	= new	List<ISubscription>();

						var lt_Subs	= this.GetSubscriptions<T>( Topic, SubscriberID, SubscriptionID );

						foreach (var lo_Sub in lt_Subs)
							{
								ISubscription lo_ExecSub		= lo_Sub;
								T							lo_ExecData		= data;

								lo_Tasks.Add(
									Task<ISubscription>.Factory.StartNew(
										() =>	{
														lo_ExecSub.Invoke( lo_ExecData );
														return	lo_ExecSub;
													},	ct																	,
															TaskCreationOptions.PreferFairness	,
															TaskScheduler.Default		)
														);
							}
						//.............................................
						while (lo_Tasks.Count > 0)
							{
								if (ct.IsCancellationRequested)
									ct.ThrowIfCancellationRequested();
								//.........................................
								Task<ISubscription> lo_FinishedTask	= await Task.WhenAny(lo_Tasks);
								lo_Tasks.Remove(lo_FinishedTask);

								if (lo_FinishedTask.Status == TaskStatus.RanToCompletion)
									{ lt_Results.Add(lo_FinishedTask.Result); }
							}
						//.............................................
						return	lt_Results;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void UnSubscribeAll()
					{
						this.co_cacheLock.EnterWriteLock();

						try
							{ this.ct_SubsByType.Values
									.ToList()
									.ForEach( (top) => top.Clear() );
							}
						finally	{	this.co_cacheLock.ExitWriteLock(); }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public void UnSubscribe(string Topic)
					{
						this.co_cacheLock.EnterWriteLock();

						try
							{ this.ct_SubsByType.Values
									.ToList()
									.ForEach( (top) => top.Clear(Topic) );
							}
						finally	{	this.co_cacheLock.ExitWriteLock(); }
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Private]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<ISubscription> GetSubscriptions(	string	topic					,
																												Guid		subscriberid		)
					{
						this.co_cacheLock.EnterReadLock();

						try
							{
								return	this.ct_SubsByType.Values
													.ToList()
													.SelectMany( top => top.GetSubscriptions(topic, subscriberid))
													.ToList();
							}
						finally	{	this.co_cacheLock.ExitReadLock(); }
					}

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
