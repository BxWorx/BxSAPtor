using MVVM;
using MsgHub;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSAP_App.Main
{
	public class BxSAP_MainViewModel : ViewModelBase
		{
			private IMessageHub	co_MsgHub;


			public BxSAP_MainViewModel(IMessageHub MsgHub)
				{
					this.co_MsgHub	= MsgHub;
				}
		}
}
