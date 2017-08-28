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
				
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				private	int CountAll(string topic	= default(string), Guid subscriber = default(Guid))
					{
						IList<ISubscription>[]	ct_Topic;

						lock (this.co_Lock)
							{
								var q = (from lo_Sub4Topic in this.ct_SubsByTopic
													select lo_Sub4Topic.Value.Count).Sum();
								return	q;
							}
					}

			#endregion

		}
}
