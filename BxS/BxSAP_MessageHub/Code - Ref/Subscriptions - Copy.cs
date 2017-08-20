using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Easy.MessageHub

	{

	using System;
	using System.Collections.Generic;
	using System.Linq;

	internal static class Subscriptions
		{
		private static readonly List<Subscription> AllSubscriptions = new List<Subscription>();
		private static int _subscriptionRevision;

		[ThreadStatic]
		private static int _localSubscriptionRevision;

		[ThreadStatic]
		private static Subscription[] _localSubscriptions;

		internal static Guid Register<T>(TimeSpan throttleBy, Action<T> action)
			{
			var type = typeof(T);
			var key = Guid.NewGuid();
			var subscription = new Subscription(type, key, throttleBy, action);

			lock (AllSubscriptions)
				{
				AllSubscriptions.Add(subscription);
				_subscriptionRevision++;
				}
			return key;
			}

		internal static void UnRegister(Guid token)
			{
			lock (AllSubscriptions)
				{
				var subscription = AllSubscriptions.FirstOrDefault(s => s.Token == token);
				var removed = AllSubscriptions.Remove(subscription);

				if (removed) { _subscriptionRevision++; }
				}
			}

		internal static void Clear()
			{
			lock (AllSubscriptions)
				{
				AllSubscriptions.Clear();
				_subscriptionRevision++;
				}
			}

		internal static bool IsRegistered(Guid token)
			{
			lock (AllSubscriptions) { return AllSubscriptions.Any(s => s.Token == token); }
			}

		internal static Subscription[] GetTheLatestRevisionOfSubscriptions()
			{
			if (_localSubscriptions == null)
				{
				_localSubscriptions = new Subscription[0];
				}

			if (_localSubscriptionRevision == _subscriptionRevision)
				{
				return _localSubscriptions;
				}

			Subscription[] latestSubscriptions;

			lock (AllSubscriptions)
				{
				latestSubscriptions = AllSubscriptions.ToArray();
				_localSubscriptionRevision = _subscriptionRevision;
				}

			_localSubscriptions = latestSubscriptions;
			return latestSubscriptions;
			}

		internal static void Dispose()
			{
			Clear();
			}
		}
		//====================================================================================
		internal sealed class Subscription
			{
			private const long TicksMultiplier = 1000 * TimeSpan.TicksPerMillisecond;
			private readonly long _throttleByTicks;
			private double? _lastHandleTimestamp;

			internal Subscription(Type type, Guid token, TimeSpan throttleBy, object handler)
				{
				Type = type;
				Token = token;
				Handler = handler;
				_throttleByTicks = throttleBy.Ticks;
				}

			internal void Handle<T>(T message)
				{
				if (!CanHandle()) { return; }

				var handler = Handler as Action<T>;
				handler(message);
				}



			internal bool CanHandle()
				{
				if (_throttleByTicks == 0) { return true; }

				if (_lastHandleTimestamp == null)
					{
					_lastHandleTimestamp = Stopwatch.GetTimestamp();
					return true;
					}

				var now = Stopwatch.GetTimestamp();
				var durationInTicks = (now - _lastHandleTimestamp) / Stopwatch.Frequency * TicksMultiplier;

				if (durationInTicks >= _throttleByTicks)
					{
					_lastHandleTimestamp = now;
					return true;
					}

				return false;

				}

			internal Guid Token { get; }
			internal Type Type { get; }
			private object Handler { get; }

			}
	}
