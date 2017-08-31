namespace MsgHubv01
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	internal sealed class Topics
		{
			#region **[Declarations]**

				private	ReaderWriterLockSlim																co_cacheLock;
				private ConcurrentDictionary<string, ReaderWriterLockSlim>	ct_Locks;
				private ConcurrentDictionary<string, Subscriptions>					ct_Topics;

				private bool		cb_AllowMultiple;

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Exposed]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Clear(string topic = default(string))
					{
						if (string.IsNullOrEmpty(topic))
							{
								this.co_cacheLock.EnterWriteLock();

								try
									{	this.ct_Topics.Clear(); }
								finally
									{	this.co_cacheLock.ExitWriteLock(); }
							}
						else
							{
								this.co_cacheLock.EnterReadLock();

								try
									{
										var lo_Lock	=	this.ct_Locks.GetOrAdd(topic, (key) => new ReaderWriterLockSlim());

										lo_Lock.EnterWriteLock();

										try
											{
												Subscriptions	lo_Subs;

												if (this.ct_Topics.TryGetValue(topic, out lo_Subs))
													{	
														lo_Subs.Clear();
													}
											}
										finally
											{	lo_Lock.ExitWriteLock(); }
									}
								finally
									{	this.co_cacheLock.ExitReadLock(); }
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void DeRegister(ISubscription subscription)
					{
						this.co_cacheLock.EnterReadLock();
						try
							{
								var lo_Lock	=	this.ct_Locks.GetOrAdd(subscription.Topic, (key) => new ReaderWriterLockSlim());

								lo_Lock.EnterReadLock();

								try
									{
										Subscriptions	lo_Subs;
						
										if (this.ct_Topics.TryGetValue(subscription.Topic, out lo_Subs))
											{
												lo_Subs.DeRegister(subscription);
											}
									}
									finally
										{	lo_Lock.ExitReadLock(); }
							}
						finally
							{	this.co_cacheLock.ExitReadLock(); }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal void Register(ISubscription subscription)
					{
						this.co_cacheLock.EnterReadLock();
						try
							{
								var lo_Lock	=	this.ct_Locks.GetOrAdd(subscription.Topic, (key) => new ReaderWriterLockSlim());

								lo_Lock.EnterReadLock();

								try
									{
										var lo_Subs	= this.ct_Topics.GetOrAdd(	subscription.Topic	,
																														(key) => new Subscriptions()	);
										lo_Subs.Register(subscription);
									}
									finally
										{	lo_Lock.ExitReadLock(); }
							}
						finally
							{	this.co_cacheLock.ExitReadLock(); }
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal int Count(	string	topic						= default(string)	,
														Guid		subscriberid		= default(Guid)		,
														Guid		subscriptionid	= default(Guid)			)
					{
						return	this.FetchSubscriptions(topic, subscriberid, subscriptionid).Count();
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<ISubscription> GetSubscriptions(	string	topic						= default(string)	,
																												Guid		subscriberid		= default(Guid)		,
																												Guid		subscriptionid	= default(Guid)			)
					{
						return	this.FetchSubscriptions(topic, subscriberid, subscriptionid);
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal Topics(bool allowmultiple = false)
					{
						this.cb_AllowMultiple	= allowmultiple;
						//.............................................
						this.co_cacheLock			= new ReaderWriterLockSlim();
						this.ct_Locks					= new	ConcurrentDictionary<string, ReaderWriterLockSlim>();
						this.ct_Topics				= new	ConcurrentDictionary<string, Subscriptions>();
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods: Private]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private IList<ISubscription> FetchSubscriptions(	string	topic						,
																													Guid		subscriberid		,
																													Guid		subscriptionid		)
					{
						this.co_cacheLock.EnterReadLock();
						try
							{
								if (!string.IsNullOrEmpty(topic))		// most likely case so handle specifically
									{
										var lo_Lock	=	this.ct_Locks.GetOrAdd(topic, (key) => new ReaderWriterLockSlim());

										lo_Lock.EnterReadLock();

										try
											{
												Subscriptions lo_Subs;

												if (this.ct_Topics.TryGetValue(topic, out lo_Subs))
													{	return	lo_Subs.GetSubscriptions(subscriberid, subscriptionid); }
												else
													{ return	new List<ISubscription>(); }
											}
											finally
												{	lo_Lock.ExitReadLock(); }
									}
								//.............................................
								return	this.ct_Topics
														.Values
														.SelectMany( val => val.GetSubscriptions(subscriberid, subscriptionid) )				
														.ToList();
							}
						finally
							{	this.co_cacheLock.ExitReadLock(); }
					}

			#endregion

		}
}
