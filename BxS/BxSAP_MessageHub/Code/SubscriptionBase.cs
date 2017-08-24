using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
{
	public abstract class SubscriptionBase<T>: ISubscription<T>
		{
			#region ** [Declarations] **

				protected						Type			co_MyType;
				//.................................................
				protected readonly	Guid			cc_Token;
				protected readonly	Guid			cc_ClientID;
				protected readonly	string		cc_Topic;
				protected readonly	bool			cb_Replace;
				protected readonly	bool			cb_AllowMany;

			#endregion
			//___________________________________________________________________________________________
			#region ** [Properties] **

				public Guid		MyToken			{ get { return	this.cc_Token; } }
				public Guid		ClientToken	{ get { return	this.cc_ClientID; } }
				public string	Topic				{ get { return	this.cc_Topic; } }
				public bool		AllowMany		{ get { return	this.cb_AllowMany; } }
				public bool		Replace			{ get { return	this.cb_Replace; } }
				public Type		TypeOf			{ get { return	this.co_MyType; } }

			#endregion
			//___________________________________________________________________________________________
			#region ** [Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SubscriptionBase(Guid		clientid	,
																string	topic			,
																bool		allowmany	,
																bool		replace			)
					{
						this.co_MyType		= typeof(T);
						this.cc_Token			= Guid.NewGuid();
						//...........................................
						this.cc_ClientID	= clientid;
						this.cc_Topic			= topic;
						this.cb_AllowMany	= allowmany;
						this.cb_Replace		= replace;
					}

			#endregion
			//___________________________________________________________________________________________
			#region ** [Methods: Exposed]**

				public abstract void Invoke(T Message);

			#endregion

		}

}

