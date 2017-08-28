namespace MsgHubv01
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;

	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	internal sealed class SubscriptionsByTopic
		{
			#region **[Declarations]**

				private ConcurrentDictionary<string, IList<ISubscription>>	ct_SubsByTopic;

				private bool		cb_AllowMultiple;
				private object	co_Lock;

			#endregion
			//___________________________________________________________________________________________
			#region **[Properties]**

				internal int Count { get { return	this.CountAll(); } }

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Exposed]**

				internal void Subscribe(ISubscription subscription)
					{
						var lt_Subs	= this.ct_SubsByTopic.GetOrAdd(subscription.Topic, (key) => new List<ISubscription>());

						lock (this.co_Lock)
							{
								lt_Subs.Add(subscription);
							}				
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				internal SubscriptionsByTopic(bool allowmultiple = false)
					{
						this.co_Lock					= new object();
						this.cb_AllowMultiple	= allowmultiple;
						this.ct_SubsByTopic		= new	ConcurrentDictionary<string, IList<ISubscription>>();
					}

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods: Private]**
				
				private IList<ISubscription>	Subscibers(string topic	= default(string), Guid subscriber = default(Guid))
					{

						

						return	new List<ISubscription>(this.ct_SubsByTopic.Values.ToList().Any(  );
					}

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	int CountAll(string topic	= default(string), Guid subscriber = default(Guid))
					{
						//IList<ISubscription>[]	ct_Topic;
						bool	lb_SkipTopic	= string.IsNullOrEmpty(topic)				? true : false;
						bool	lb_SkipSubsc	=	subscriber.Equals(default(Guid))	? true : false;

						lock (this.co_Lock)
							{
								int lo_QTally	= (	from lo_S4T in this.ct_SubsByTopic
																		where (lb_SkipTopic || lo_S4T.Key.Equals(topic)) &&
																					(lb_SkipSubsc	|| lo_S4T.Value.First( x => x.SubscriberID == subscriber ) != null )
																			select lo_S4T.Value.Count	).Sum();
								return	lo_QTally;
							}
					}

			#endregion

		}
}
