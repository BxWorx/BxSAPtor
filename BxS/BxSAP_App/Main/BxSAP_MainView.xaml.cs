using System.Windows;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSAP_App.Main
{
	/// <summary>
	/// Interaction logic for BxSAP_MainView.xaml
	/// </summary>
	public partial class BxSAP_MainView : Window
			{
				public BxSAP_MainView(BxSAP_MainViewModel vm)
					{
						InitializeComponent();
						DataContext	= vm;
					}
			}
	}
