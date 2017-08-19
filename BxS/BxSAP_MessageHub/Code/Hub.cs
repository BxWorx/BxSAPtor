using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		public class Hub
			{
				#region Declarations

					private	readonly ConcurrentDictionary<	string, Subscriptions	>		_topics;
					private readonly ConcurrentDictionary<	string, IList<Guid>		>		_clienttopics;

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region Constructors

					//____________________________________________________________________________________________
					public Hub()
						{
							this._topics				= new ConcurrentDictionary<string, Subscriptions>();
							this._clienttopics	= new ConcurrentDictionary<string, IList<Guid>>();
						}

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region **[Methods:Exposed]**

					//____________________________________________________________________________________________
					public Guid Subscribe(Guid ClientID, string Topic)
						{
							var						Sub = new Subscription();
							Subscriptions Subs;

							if (this._topics.ContainsKey(Topic))
								{
									if (this._topics.TryGetValue(Topic, out Subs))
										{ }
								}
							else
								{
									this.Subs = new Subscriptions();
								}

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
			}
	}
