using MVVM;
using MsgHubv01;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSAP_App.Main
{
	public class BxSAP_MainViewModel : ViewModelBase
		{
			private IMessageHub	co_AppMsgHub;

			public BxSAP_MainViewModel(IMessageHub AppMsgHub)
				{
					this.co_AppMsgHub	= AppMsgHub;
				}
		}
}
