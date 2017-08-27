namespace MsgHubv01
{
	using System;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public abstract class SubscriptionBase	: ISubscription
		{
			#region ** [Declarations] **

				protected	Type			co_MyType;
				protected	Delegate	co_Action;
				//.................................................
				protected readonly	Guid		cc_Token;
				protected readonly	Guid		cc_SubscriberID;
				protected readonly	string	cc_Topic;

			#endregion
			//___________________________________________________________________________________________
			#region ** [Properties] **

				public Type		TypeOf				{ get { return	this.co_MyType; } }
				public string	TypeID				{ get { return	this.co_MyType.Name; } }
				public Guid		MyToken				{ get { return	this.cc_Token; } }
				public Guid		SubscriberID	{ get { return	this.cc_SubscriberID; } }
				public string	Topic					{ get { return	this.cc_Topic; } }
				public bool		IsAlive				{ get { return	this.GetIsAlive(); } }

			#endregion
			//___________________________________________________________________________________________
			#region ** [Constructors]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public SubscriptionBase(string	topic					,
																Guid		subscriberid		)
					{
						this.cc_Token	= Guid.NewGuid();
						//...........................................
						this.cc_Topic					= topic;
						this.cc_SubscriberID	= subscriberid;
					}

			#endregion
			//___________________________________________________________________________________________
			#region ** [Methods: Protected]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected virtual bool GetIsAlive()
					{
						return	true;
					}

			#endregion
			//___________________________________________________________________________________________
			#region ** [Methods: Exposed]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				public virtual void Invoke<T>(T data)
					{
						try
							{
								((Action<T>)this.co_Action)(data);
							}
						catch (Exception)
							{
							}
					}

			#endregion

		}

}

