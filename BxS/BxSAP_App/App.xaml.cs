using MsgHub;
using System.Windows;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSAP_App
{
	public partial class App : Application
		{

			#region **[Declarations]**

				private IMessageHub co_MsgHub;

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Exposed]**

				protected override void OnStartup(StartupEventArgs e)
					{
						App.Current.ShutdownMode	= ShutdownMode.OnMainWindowClose;
						base.OnStartup(e);
						//.................................................
						this.co_MsgHub	= new MessageHub();
						//.................................................
						var MainVM	= new BxSAP_App.Main.BxSAP_MainViewModel(this.co_MsgHub);
						var MainVW	= new BxSAP_App.Main.BxSAP_MainView(MainVM);
						//.................................................
						MainVW.ShowDialog();
					}

			#endregion

		}
}
