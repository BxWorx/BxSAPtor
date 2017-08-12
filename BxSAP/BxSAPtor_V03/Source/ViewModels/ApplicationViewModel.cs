using BxSAPtor.MVVM;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSAPtor_V03.Source.ViewModels
	{
		[ExportViewModel("ApplicationVM", false)]
		class ApplicationViewModel : ViewModelBase
			{

				public ApplicationViewModel()
					{
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
