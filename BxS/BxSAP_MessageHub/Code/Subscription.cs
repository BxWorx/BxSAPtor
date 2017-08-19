using System;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
	{
		public class Subscription : ISubscription
			{
				#region ** Declarations **

					private readonly	Guid	_id;

				#endregion
				//_________________________________________________________________________________________
				#region ** Properties **

					public Guid MyID { get { return	this._id; } }

				#endregion
				//_________________________________________________________________________________________
				#region ** [Constructors]**

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public Subscription()
						{
							this._id	= Guid.NewGuid();
						}

				#endregion
			}
	}
