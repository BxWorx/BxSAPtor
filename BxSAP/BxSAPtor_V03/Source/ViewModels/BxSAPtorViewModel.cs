using System.ComponentModel;
using System.Windows;
using BxSAPtor.MVVM;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSAPtor_V03.Source.ViewModels
	{
		[ExportViewModel("BxSAPtorViewModel", false)]
		class BxSAPtorViewModel : ViewModelBase
			{
				bool IsDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());

				public BxSAPtorViewModel()
					{

						if (IsDesignMode)	return;

						EnDisable = new RelayCommand(	() =>	{	isEnabled = !isEnabled;
																									Action.RaiseCanExecuteChanged();
																								}
																				);

						Action = new RelayCommand(() => System.Windows.MessageBox.Show("demo1 test"), () => isEnabled);
					}

				private	bool					isEnabled	= false;
				public	RelayCommand	EnDisable { get; set; }
				public	RelayCommand	Action		{ get; set; }

			}
	}
