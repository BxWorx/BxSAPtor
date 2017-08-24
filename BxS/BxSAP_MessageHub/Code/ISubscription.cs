using System;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace MsgHub
{
	public interface ISubscription<T>
		{
			#region ** [Properties] **

				Guid		MyToken			{ get; }
				Guid		ClientToken	{ get; }
				string	Topic				{ get; }
				bool		AllowMany		{ get; }
				bool		Replace			{ get; }

				Type		TypeOf			{	get; }

			#endregion
			//___________________________________________________________________________________________
			#region ** [Methods: Exposed] **

				void Invoke(T Message);

			#endregion

		}
}