using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		internal sealed class Subscriptions
			{
				#region **[Declarations]**

					private readonly string																		cc_Topic;
					private readonly ConcurrentDictionary<Guid, Subscription>	ct_Subscriptions;   // key	= subscription ID		value = subscription
					private readonly ConcurrentDictionary<Guid, int>					ct_Clients;					// key	= Client ID					value	= membership count
					//private readonly IList<Guid>															ct_Client; 

				#endregion
				//_________________________________________________________________________________________
				#region **[Properties]**

					public string	Topic							{ get { return	this.cc_Topic; } }
					public int		SubscriptionCount	{ get { return	this.ct_Subscriptions.Count; } }
					public int		ClientCount				{ get { return	this.ct_Clients.Count; } }

				#endregion
				//_________________________________________________________________________________________
				#region **[Constructors]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal Subscriptions(string Topic)
						{
							this.cc_Topic						= Topic;
							this.ct_Subscriptions		= new ConcurrentDictionary<Guid, Subscription>();
							this.ct_Clients					= new ConcurrentDictionary<Guid, int>();
						}

				#endregion
				//_________________________________________________________________________________________
				#region **[Methods:Exposed]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public void Clear()
						{
							this.ct_Subscriptions.Clear();
							this.ct_Clients.Clear();
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public bool SubscriptionExists(Guid SubscriptionToken)
						{
							return	this.ct_Subscriptions.ContainsKey(SubscriptionToken);
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public bool ClientExists(Guid ClientToken)
						{
							int ln_CurVal	= 0;

							if (this.ct_Clients.TryGetValue(ClientToken, out ln_CurVal))	{ }
							//...........................................
							return	(ln_CurVal == 0) ? false : true ;
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public bool Register(Subscription Subscription)
						{
							if ( this.SubscriptionExists(Subscription.MyToken)	&& !Subscription.Replace )		{ return	false; }
							if ( this.ClientExists(Subscription.ClientToken)		&& !Subscription.AllowMany )	{ return	false; }
							//...........................................
							this.ct_Subscriptions.AddOrUpdate(Subscription.MyToken	,
																								Subscription					,
																								(key, OldVal) => Subscription	);
							this.ct_Clients.AddOrUpdate(Subscription.ClientToken		,
																					1														,
																					(key, oldVal)	=> oldVal + 1		);
							return	true;
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public bool RemoveSubscription(Guid SubscriptionToken)
						{
							bool lb_Ret	= false;
							Subscription lo_Sub;

							if (this.ct_Subscriptions.TryRemove(SubscriptionToken, out lo_Sub))
								{
									int ln_CurVal;
									int ln_NewVal;

									if (this.ct_Clients.TryGetValue(lo_Sub.ClientToken, out ln_CurVal))
										{
											ln_NewVal	= ln_CurVal - 1;
											this.ct_Clients.TryUpdate(lo_Sub.ClientToken, ln_NewVal, ln_CurVal);
											lb_Ret	= true;
										}
								}
							//...........................................
							return	lb_Ret;
						}

				#endregion
			}
	}
