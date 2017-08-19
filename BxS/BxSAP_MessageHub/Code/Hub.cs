using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		public class Hub
			{
				#region **[Declarations]**

					private	readonly ConcurrentDictionary<string,	Subscriptions	>		ct_Topics;
					private readonly ConcurrentDictionary<Guid,		IList<string>	>		ct_ClientTopics;

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Properties]**
				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Constructors]**

					//____________________________________________________________________________________________
					public Hub()
						{
							this.ct_Topics					= new ConcurrentDictionary<string,	Subscriptions>();
							this.ct_ClientTopics		= new ConcurrentDictionary<Guid,		IList<string>>();
						}

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Methods:Exposed]**

					//____________________________________________________________________________________________
					public Guid Subscribe(Guid ClientID, string Topic, bool AllowMultiple = true)
						{
							var	lt_Subs	= this.GetAddTopicSubscriptions(Topic);

							if (!AllowMultiple)
								{
									if (this.IsClientSubscribedToTopic(ClientID, Topic))
										{
											
										}
								}
							
							var	Sub	= new Subscription();
							lt_Subs.AddUpdateSubscription(Sub.MyID, Sub);

							return Sub.MyID;
						}

					//____________________________________________________________________________________________
					public Guid UnSubscribe()
						{
							var Sub = new Subscription();
							
							


							return Sub.MyID;
						}

					//____________________________________________________________________________________________
					public Guid UnSubscribeAll()
						{
							var Sub = new Subscription();
							
							


							return Sub.MyID;
						}

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Methods:Private]**

					//____________________________________________________________________________________________
					private bool IsClientSubscribedToTopic(Guid ClientID, string Topic)
						{
							return this.GetClientTopics(ClientID).Contains(Topic);
						}

					//____________________________________________________________________________________________
					private IList<string>	GetClientTopics(Guid ClientID)
						{
							IList<string> lt_Topics;

							if (!this.ct_ClientTopics.TryGetValue(ClientID, out lt_Topics))
								{
									lt_Topics = new List<string>();
								}

							return lt_Topics;
						}

					//____________________________________________________________________________________________
					private Subscriptions GetAddTopicSubscriptions(string Topic)
						{
							Subscriptions lo_Subs;

							if (!this.ct_Topics.TryGetValue(Topic, out lo_Subs))
								{
									lo_Subs = new Subscriptions(Topic);
									this.ct_Topics.TryAdd(Topic, lo_Subs);
								}

							return lo_Subs;
						}

				#endregion
			}
	}
