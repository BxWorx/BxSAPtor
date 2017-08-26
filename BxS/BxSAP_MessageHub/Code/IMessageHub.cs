using System;
using System.Threading;
using System.Threading.Tasks;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
{
	public interface IMessageHub
		{
			#region **[Declarations]**

				//private	readonly	ConcurrentDictionary<string, SubscriptionsByTopic>	ct_SubsByTopic;		// key=topic

			#endregion
			//_________________________________________________________________________________________
			#region **[Methods:Exposed]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				ISubscription<T>	CreateSubscription<T>(Guid			clientid					,
																								string		topic							,
																								Action<T>	action						,
																								bool			allowmany = false	,
																								bool			replace		= true		);

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				ISubscription<T>	CreateSubscriptionWeak<T>(Guid			clientid					,
																										string		topic							,
																										Action<T>	action						,
																										bool			allowmany = false	,
																										bool			replace		= true		);

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				ISubscription<T>	Subscribe<T>(	Guid			clientid					,
																				string		topic							,
																				Action<T>	action						,
																				bool			allowmany = false	,
																				bool			replace		= true	,
																				bool			asweak		= true		);

				Guid	Subscribe<T>(ISubscription<T> subscription);
				void	UnSubscribe<T>(ISubscription<T> subscription);
				void	UnSubscribe(string topic);
				void	UnSubscribeAll();
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				void	Publish<T>(string topic, T message, CancellationToken ct = default(CancellationToken));
				Task	PublishAsync<T>(string topic, T message, CancellationToken ct = default(CancellationToken));
				void	PublishBackground<T>(string topic, T message, CancellationToken ct = default(CancellationToken));
				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				int		SubscriptionCount(string topic);

			#endregion

		}
}
