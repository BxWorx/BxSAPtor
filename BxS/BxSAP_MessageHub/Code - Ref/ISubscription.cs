using System;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		internal interface ISubscription
			{
				Guid MyID { get; }
			}
	}
