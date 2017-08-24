using System;
using System.Collections;
using System.Collections.Generic;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		internal class SubscriptionsByType
			{

				#region **[Declarations]**

					private string												cc_MyTopic;
					private Dictionary<Type, object>			ct_SubsByType;		// key = typeof, value=Dictionary<Guid, subscriptions<T>>
					private Dictionary<Type, IList<Guid>>	ct_SubsIdByType;	// key = typeof, value=IList<subscription.token(guid)>

				#endregion
				//_________________________________________________________________________________________
				#region **[Properties]**

					internal int Count	{	get	{	return	this.CountAll(); } }

				#endregion
				//_________________________________________________________________________________________
				#region **[Constructors]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal SubscriptionsByType(string ForTopic)
						{
							this.cc_MyTopic	= ForTopic;
							//...........................................
							this.ct_SubsByType		= new Dictionary<Type, object>();
							this.ct_SubsIdByType	= new Dictionary<Type, IList<Guid>>();
						}

				#endregion
				//_________________________________________________________________________________________
				#region **[Methods: Exposed]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal bool DeRegister<T>(ISubscription<T> Subscription)
						{
							if (this.GetOrAdd<T>().Remove(Subscription.MyToken))
								return	this.ct_SubsIdByType[typeof(T)].Remove(Subscription.MyToken);

							return	false;
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal bool Register<T>(ISubscription<T> Subscription)
						{
							var		lt_SubsType	= this.GetOrAdd<T>();
							//...........................................
							if ( lt_SubsType.ContainsKey(Subscription.MyToken) )
								{
									if (Subscription.Replace)
										{
											lt_SubsType[Subscription.MyToken]	= Subscription;
											return	true;
										}
								}
							else
								{
									lt_SubsType.Add(Subscription.MyToken, Subscription);
									this.ct_SubsIdByType[typeof(T)].Add(Subscription.MyToken);
									return	true;
								}
							//...........................................
							return	false;
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal void Reset()
						{
							this.ct_SubsByType.Clear();
							this.ct_SubsIdByType.Clear();
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
									this.ct_SubsIdByType.Add( lo_T, new List<Guid>() );
								}

							return	(Dictionary<Guid, ISubscription<T>>)this.ct_SubsByType[lo_T];
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					internal void Remove<T>()
						{
							this.ct_SubsByType.Remove(typeof(T));
							this.ct_SubsIdByType.Remove(typeof(T));
						}

				#endregion
				//_________________________________________________________________________________________
				#region **[Methods: Private]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					private int CountAll()
						{
							int ln_Tally	= 0;

							foreach (var lo_Typ in this.ct_SubsIdByType.Values)
								{
									ln_Tally	+= lo_Typ.Count;
								}

							return	ln_Tally;
						}

				#endregion
		
			}
	}


					////¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					//internal int CountClient(Guid client)
					//	{
					//		int ln_Tally	= 0;

					//		foreach (var lo_Typ in this.ct_SubsByType.Values)
					//			{
					//				var lt_Subs	= (Dictionary<Guid, object>)lo_Typ;
					//				ln_Tally		+= lt_Subs.Count;
					//			}

					//		return	ln_Tally;
					//	}
