using System;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		internal static	class MsgHubFactory
			{
				#region ** [Methods: Exposed] **

					//____________________________________________________________________________________________
					internal static Subscription Subscription<T>(Guid clientid, string topic, bool allowmany, Action<T> action)
						{
							var lo_Type			= typeof(T);
							var	lo_TObj			= new WeakReference(action.Target);
							var lo_MInfo		= action.Method;
							var lb_IsStatic	= (action.Target == null) ? true : false;

							return	new Subscription(clientid, topic, allowmany, lo_Type, lo_TObj, lo_MInfo, lb_IsStatic);
						}

				#endregion
			}
	}
