using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		internal sealed class SubscriptionsByTopic
			{
				#region **[Declarations]**

					private readonly string														cc_Topic;
					private readonly SubscriptionsByType							co_SubsByType;
					private readonly ConcurrentDictionary<Guid, int>	ct_Clients;		// key=client ID,	value	= membership count

					private	object co_lock;

				#endregion
				//_________________________________________________________________________________________
				#region **[Properties]**

					internal  string	Topic					{ get { return	this.cc_Topic; } }
					internal  int			Count					{ get { return	this.co_SubsByType.Count; } }
					internal  int			ClientCount		{ get { return	this.ct_Clients.Count; } }

				#endregion
				//_________________________________________________________________________________________
				#region **[Constructors]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal SubscriptionsByTopic(string Topic)
						{
							this.cc_Topic				= Topic;
							this.co_SubsByType	= new SubscriptionsByType(Topic);
							this.ct_Clients			= new ConcurrentDictionary<Guid, int>();
						}

				#endregion
				//_________________________________________________________________________________________
				#region **[Methods:Exposed]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal IList<ISubscription<T>> SubscriptionList<T>()
						{
							lock (co_SubsByType)
								{
									return	new List<ISubscription<T>>(this.co_SubsByType.GetOrAdd<T>().Values);
								}
						}
	
					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal  void Reset()
						{
							lock (co_SubsByType)
								{
									this.co_SubsByType.Reset();
									this.ct_Clients.Clear();
								}
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal  bool Register<T>(ISubscription<T> Subscription)
						{
							bool	lb_Ret	= false;
							//...........................................
							lock (co_SubsByType)
								{
									if (this.co_SubsByType.Register(Subscription))
										{
											if (!this.ClientHasSubscribed(Subscription.ClientToken) || Subscription.AllowMany)
												{
													this.ct_Clients.AddOrUpdate(Subscription.ClientToken		,
																											1														,
																											(key, oldVal)	=> oldVal + 1		);
												}
										}
								}
							//...........................................
							return	lb_Ret;
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal bool DeRegister<T>(ISubscription<T> Subscription)
						{
							if (this.co_SubsByType.DeRegister(Subscription))
								{
									int ln_CurVal;
									int ln_NewVal;

									if (this.ct_Clients.TryGetValue(Subscription.ClientToken, out ln_CurVal))
										{
											ln_NewVal	= ln_CurVal - 1;
											this.ct_Clients.TryUpdate(Subscription.ClientToken, ln_NewVal, ln_CurVal);
											return	true;
										}

								}

							return	false;

						}


					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal  bool ClientHasSubscribed(Guid ClientToken)
						{
							int ln_CurVal	= 0;

							if (this.ct_Clients.TryGetValue(ClientToken, out ln_CurVal))	{ }
							//...........................................
							return	(ln_CurVal == 0) ? false : true ;
						}


				#endregion
			}
	}



//private readonly ConcurrentDictionary<Guid, Subscription>	ct_Subscriptions;   // key	= subscription ID		value = subscription
//private readonly ConcurrentDictionary<Guid, SubscriptionsByType>		ct_Subs;						// key	= subscription ID		value = subscription
//public IList<Subscription>	SubscriptionList	{ get { return new List<Subscription>(this.ct_Subscriptions.Values); } }

//public int		SubscriptionCount	{ get { return	this.ct_Subscriptions.Count; } }

//internal  IList<ISubscription<T>>	SubsList<T>
//	{
//		get
//			{
//				var lt_Ret	= new List<Subscription>();
//				foreach (var item in this.ct_Subs.Values)
//					{
//						lt_Ret.Add((Subscription)item);
//					}
//				return lt_Ret;
//			}
//	}

//this.ct_Subscriptions		= new ConcurrentDictionary<Guid, Subscription>();
//this.ct_Subs						= new ConcurrentDictionary<Guid, object>();

//this.ct_Subscriptions.Clear();


//this.ct_Subscriptions.AddOrUpdate(Subscription.MyToken	,
//																	Subscription					,
//																	(key, OldVal) => Subscription	);


////...........................................
//if ( lt_SubsType.ContainsKey(Subscription.MyToken)				&& !Subscription.Replace )		{ return	false; }
//if ( this.ClientHasSubscribed(Subscription.ClientToken)		&& !Subscription.AllowMany )	{ return false; }
////...........................................



//this.ct_Subs.AddOrUpdate(	Subscription.MyToken	,
//													Subscription					,
//													(key, OldVal) => Subscription	);

//this.ct_Clients.AddOrUpdate(Subscription.ClientToken		,
//														1														,
//														(key, oldVal)	=> oldVal + 1		);
//return	true;




//ISubscription<T>	lo_Sub;

//if (this.ct_Subscriptions.TryRemove(SubscriptionToken, out lo_Sub))
//	{
//		int ln_CurVal;
//		int ln_NewVal;

//		if (this.ct_Clients.TryGetValue(lo_Sub.ClientToken, out ln_CurVal))
//			{
//				ln_NewVal	= ln_CurVal - 1;
//				this.ct_Clients.TryUpdate(lo_Sub.ClientToken, ln_NewVal, ln_CurVal);
//				lb_Ret	= true;
//			}
//	}

//object	lo_Obj;

//if (this.ct_Subs.TryRemove(Subscription.MyToken, out lo_Obj))
//	{
//		int ln_CurVal;
//		int ln_NewVal;

//		var	lo_Sub	= (ISubscription)lo_Obj;
//		if (this.ct_Clients.TryGetValue(lo_Sub.ClientToken, out ln_CurVal))
//			{
//				ln_NewVal	= ln_CurVal - 1;
//				this.ct_Clients.TryUpdate(lo_Sub.ClientToken, ln_NewVal, ln_CurVal);
//				return	true;
//			}
//	}
////...........................................
//return	false;


////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
//internal bool SubscriptionExists(Guid SubscriptionToken)
//	{
//	return this.ct_Subs.ContainsKey(SubscriptionToken);
//	//return	this.ct_Subscriptions.ContainsKey(SubscriptionToken);
//	}
