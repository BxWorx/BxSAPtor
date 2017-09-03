namespace BxSAP_App
{
	using MsgHubv01;
	using System;
	using System.Windows;
	//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
	public partial class App : Application
		{

			#region **[Declarations]**

				private IMessageHub co_AppMsgHub;

			#endregion
			//___________________________________________________________________________________________
			#region **[Methods:Exposed]**

				//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
				protected override void OnStartup(StartupEventArgs e)
					{
						App.Current.ShutdownMode	= ShutdownMode.OnMainWindowClose;
						//.................................................
						base.OnStartup(e);
						//.................................................
						this.co_AppMsgHub	= new MessageHub();
						//.................................................
						var MainVM	= new Main.BxSAP_MainViewModel(this.co_AppMsgHub);
						var MainVW	= new Main.BxSAP_MainView(MainVM);
						//.................................................
						var lo_Action	= new Action<string>( (data) => this.MsgHandler(data) );

						this.co_AppMsgHub.Subscribe()


						MainVW.ShowDialog();
					}

					protected override void OnExit(ExitEventArgs e)
						{

							//.................................................
							base.OnExit(e);
						}

			#endregion


			private void MsgHandler(string CMD)
				{
					switch (CMD)
						{
							case "Shutdown":	Application.Current.MainWindow.Close();
							break;
								
							default:
								break;
						}

				}

		}
}
