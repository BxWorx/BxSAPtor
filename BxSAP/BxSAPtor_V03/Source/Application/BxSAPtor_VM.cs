using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BxSAPtor.Configurator.Helpers;
using BxSAPtor.Configurator.Controls;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSAPtor.Configurator.Application
	{

		internal class BxSAPtorViewModel : ViewModelBase
				{

					#region ** Constructors **

						internal BxSAPtorViewModel()
							{
								// Add available pages
								PageViewModels.Add(new UCSettingsVM());

								// Set starting page
								CurrentPageViewModel = PageViewModels[0];
							}

					#endregion
					//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
					#region ** Declarations **

						private ICommand			_changePageCommand;
						private iPageVM				_currentPageViewModel;
						private List<iPageVM> _pageViewModels;

					#endregion
					//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
					#region ** Properties **

				public ICommand ChangePageCommand
					{
					get
						{
						if (_changePageCommand == null)
							{
							_changePageCommand = new RelayCommand(
									p => ChangeViewModel((iPageVM)p),
									p => p is iPageVM);
							}

						return _changePageCommand;
						}
					}

				public List<iPageVM> PageViewModels
					{
					get
						{
						if (_pageViewModels == null)
							_pageViewModels = new List<iPageVM>();

						return _pageViewModels;
						}
					}

				public iPageVM CurrentPageViewModel
					{
					get
						{
						return _currentPageViewModel;
						}
					set
						{
						if (_currentPageViewModel != value)
							{
							_currentPageViewModel = value;
							OnPropertyChanged("CurrentPageViewModel");
							}
						}
					}

			#endregion

				#region ** Methods: Private **

				private void ChangeViewModel(iPageVM viewModel)
						{
						if (!PageViewModels.Contains(viewModel))
							PageViewModels.Add(viewModel);

						CurrentPageViewModel = PageViewModels
								.FirstOrDefault(vm => vm == viewModel);
						}

				#endregion

			}
	}
