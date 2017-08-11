using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BxSAPtor.Configurator.MVVM;
//•••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSAPtor_V03.Source.ViewModels
	{
		[ExportViewModel("BxSAPtor", false)]
		class BxSAPtorViewModel : ViewModelBase
			{

				public BxSAPtorViewModel()
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
