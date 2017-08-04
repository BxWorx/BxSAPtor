using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSAPtor.Configurator.Helpers
	{
		internal class ViewModelBase : INotifyPropertyChanged
			{

				#region ** Declarations **
					
					protected string cc_myName;

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region ** INotifyPropertyChanged Members **

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					protected bool SetProperty<T>(									ref		T				Storage	,
																																			T				Value		,
																									[CallerMemberName]	string	PropertyName = null)
						{
							if (EqualityComparer<T>.Default.Equals(Storage, Value)) return false;
							Storage = Value;
							this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
							//IPropertyChanged(PropertyName);
							return true;
						}
					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public event PropertyChangedEventHandler PropertyChanged;

	
					//protected virtual void IPropertyChanged(string Propertname)
					//	{
					//	this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Propertname));
					//	}

				#endregion

		}
	}
