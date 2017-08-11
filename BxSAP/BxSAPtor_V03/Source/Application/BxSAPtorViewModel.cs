using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using BxSAPtor.Configurator.Helpers;
using BxSAPtor.Configurator.Controls;
using BxSAPtor.Configurator.MVVM;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSAPtor.Configurator.Application
	{

		public class BxSAPtorViewModel : ViewModelBase
			{

				#region ** Constructors **

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public BxSAPtorViewModel()
						{

							this.SaveCommand = new RelayCommand<bool>(p => this.OnSave()	,
																												p => this.CanSave			);
							
							// Add available pages

							this._pageViewModels	=	new List<iPageVM> { };
							PageViewModels.Add(new UCSettingsVM());

							// Set starting page
							CurrentPageViewModel = PageViewModels[0];


						}

						private bool OnSave() {
							int x = 1;
							x += 1;
							return	true;
							}

						private bool _isDirty	= false;

						public bool CanSave {
							get {	return  this._isDirty; }
							set { SetProperty(ref this._isDirty, value); }
						}


				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region ** Declarations **

					private ICommand				_changePageCommand;
					private iPageVM					_currentPageViewModel;
					private List<iPageVM>		_pageViewModels;

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region ** Properties **

					private ICommand	_savecommand;

					public ICommand SaveCommand {
						get { return this._savecommand; }
						private	set { this._savecommand = value; }
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public ICommand ChangePageCommand
						{
							get
								{
									if (_changePageCommand == null)
										{
											_changePageCommand = new RelayCommand<iPageVM>(
																								p => this.ChangeViewModel((iPageVM)p)	,
																								p => p is iPageVM												);
										}

									return _changePageCommand;
								}
						}
					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public List<iPageVM> PageViewModels
						{
							get
								{
									if (this._pageViewModels == null)
										this._pageViewModels = new List<iPageVM>();

									return	this._pageViewModels;
								}
						}
					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public iPageVM CurrentPageViewModel
						{
							get
								{
									return this._currentPageViewModel;
								}
							set
								{
									if (this.SetProperty(ref this._currentPageViewModel, value))
										{
										}
						
									//if (_currentPageViewModel != value)
									//	{
									//	_currentPageViewModel = value;
									//	OnPropertyChanged("CurrentPageViewModel");
								}
						}
					
				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region ** Methods: Private **

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					private void ChangeViewModel(iPageVM viewModel)
						{
							if (!this.PageViewModels.Contains(viewModel))
								this.PageViewModels.Add(viewModel);

							this.CurrentPageViewModel = this.PageViewModels.FirstOrDefault(vm => vm == viewModel);
							
						}

				#endregion

			}
	}
