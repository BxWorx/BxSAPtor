using System;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		public interface ISubscription
			{
				Guid MyID { get; }
			}
	}
