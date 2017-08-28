namespace MsgHubv01
{
	using System;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public interface ISubscriptionsByTopic
		{
			#region ** [Properties] **

				Type		TypeOf				{	get; }
				string	TypeID				{	get; }
				Guid		MyToken				{ get; }
				Guid		SubscriberID	{ get; }
				string	Topic					{ get; }
				bool		IsAlive				{ get; }

			#endregion
			//___________________________________________________________________________________________
			#region ** [Methods: Exposed] **

				void Invoke<T>(T data);

			#endregion

		}
}