using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
{
	internal sealed class SubscriptionsByTopic
		{
			#region **[Declarations]**

				private readonly string														cc_Topic;
				private readonly SubscriptionsByType							co_SubsByType;
				private readonly ConcurrentDictionary<Guid, int>	ct_Clients;			// key=client ID,	value	= membership count

			#endregion
			//___________________________________________________________________________________________
			#region **[Properties]**

				internal  string	Topic					{ get { return	this.cc_Topic; } }
				internal  int			Count					{ get { return	this.co_SubsByType.Count; } }
				internal  int			ClientCount		{ get { return	this.ct_Clients.Count; } }

			#endregion
			//___________________________________________________________________________________________
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SubscriptionsByTopic(string Topic)
					{
						this.cc_Topic				= Topic;
						this.co_SubsByType	= new SubscriptionsByType(Topic);
						this.ct_Clients			= new ConcurrentDictionary<Guid, int>();
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Exposed]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal IList<ISubscription<T>> SubscriptionList<T>()
					{
						lock (co_SubsByType)
							{
								return	new List<ISubscription<T>>(this.co_SubsByType.GetOrAdd<T>().Values);
							}
					}
	
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal  void Reset()
					{
						lock (co_SubsByType)
							{
								this.co_SubsByType.Reset();
								this.ct_Clients.Clear();
							}
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal  bool Register<T>(ISubscription<T> Subscription)
					{
						lock (co_SubsByType)
							{
								if (this.co_SubsByType.Register(Subscription))
									{
										if (!this.ClientHasSubscribed(Subscription.ClientToken) || Subscription.AllowMany)
											{
												this.ct_Clients.AddOrUpdate(Subscription.ClientToken		,
																										1														,
																										(key, oldVal)	=> oldVal + 1		);
												return	true;
											}
									}
							}

						return	false;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
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

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal  bool ClientHasSubscribed(Guid ClientToken)
					{
						int ln_CurVal	= 0;

						if (this.ct_Clients.TryGetValue(ClientToken, out ln_CurVal))	{ }
						//...........................................
						return	(ln_CurVal == 0) ? false : true ;
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal bool SubscriptionExists<T>(ISubscription<T> Subscription)
					{
						var lo_S	= this.SubscriptionList<T>();

						if (lo_S.Count.Equals(0))
							return	false;

						return	lo_S.First( lo => lo.MyToken == Subscription.MyToken ) != null ? true : false;
					}

			#endregion

		}
}
