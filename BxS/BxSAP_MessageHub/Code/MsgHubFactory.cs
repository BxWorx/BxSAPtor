using System;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		internal static	class MsgHubFactory
			{
				#region ** [Methods: Exposed] **

					//____________________________________________________________________________________________
					internal static Subscription Subscription<T>(	Guid			clientid	,
																												string		topic			,
																												bool			allowmany	,
																												bool			replace		,
																												Action<T>	action			)
						{
							var lo_token		= Guid.NewGuid();

							var lo_Type			= typeof(T);
							var	lo_TObj			= new WeakReference(action.Target);
							var lo_MInfo		= action.Method;
							var lb_IsStatic	= (action.Target == null) ? true : false;

							return	new Subscription(lo_token, clientid, topic, allowmany, replace, lo_Type, lo_TObj, lo_MInfo, lb_IsStatic);
						}

				#endregion
			}
	}
