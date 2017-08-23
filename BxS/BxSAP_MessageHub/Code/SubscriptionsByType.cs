using System;
using System.Collections.Generic;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		internal class SubscriptionsByType
			{

				#region **[Declarations]**

					private string										cc_MyTopic;
					private Dictionary<Type, object>	ct_SubsByType;		// key = typeof, value=Dictionary<Guid, subscriptions<T>>

				#endregion
				//_________________________________________________________________________________________
				#region **[Properties]**

					internal int Count
						{
							get
								{
									int ln_Tally	= 0;

									foreach (var lo_Typ in this.ct_SubsByType.Values)
										{
											var lt_Subs	= (Dictionary<Type, object>)lo_Typ;
											ln_Tally		+= lt_Subs.Count;
										}
									return	ln_Tally;
								}
						}

				#endregion
				//_________________________________________________________________________________________
				#region **[Constructors]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal SubscriptionsByType(string ForTopic)
						{
							this.cc_MyTopic			= ForTopic;
							//...........................................
							this.ct_SubsByType	= new Dictionary<Type, object>();
						}

				#endregion
				//_________________________________________________________________________________________
				#region **[Methods: Exposed]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal bool DeRegister<T>(ISubscription<T> Subscription)
						{
							return	this.GetOrAdd<T>().Remove(Subscription.MyToken);
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal bool Register<T>(ISubscription<T> Subscription)
						{
							bool	lb_Ret			= false;
							var		lt_SubsType	= this.GetOrAdd<T>();
							//...........................................
							if ( lt_SubsType.ContainsKey(Subscription.MyToken) )
								{
									if (Subscription.Replace)
										{
											lt_SubsType[Subscription.MyToken]	= Subscription;
											lb_Ret	= true;
										}
								}
							else
								{
									lt_SubsType.Add(Subscription.MyToken, Subscription);
								}
							//...........................................
							return	lb_Ret;
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal void Reset()
						{
							this.ct_SubsByType.Clear();
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal bool SubscriptionExists(Guid Token)
						{
							foreach (var lo_Typ in this.ct_SubsByType.Values)
								{
									//var lt_Subs	= (Dictionary<Type, ISubscription>)lo_Typ;
									//ln_Tally	+= lt_Subs.Count;
								}

							return	true;
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal Dictionary<Guid, ISubscription<T>> GetOrAdd<T>()
						{
							var lo_T	= typeof(T);

							if (!this.ct_SubsByType.ContainsKey(lo_T))
								{
									this.ct_SubsByType.Add( lo_T, new Dictionary<Guid, ISubscription<T>>() );
								}

							return	(Dictionary<Guid, ISubscription<T>>)this.ct_SubsByType[lo_T];
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal void Remove<T>()
						{
							this.ct_SubsByType.Remove(typeof(T));
						}

				#endregion
		
			}
	}
