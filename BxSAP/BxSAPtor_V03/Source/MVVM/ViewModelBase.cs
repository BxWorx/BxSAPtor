using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••••
namespace BxSAPtor.MVVM
	{
		public abstract class ViewModelBase : INotifyPropertyChanged
			{

				#region ** Declarations **
					
					protected string	cc_myName;

				#endregion
				//¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯
				#region ** INotifyPropertyChanged Members **

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					protected bool SetProperty<T>(								ref		T				Storage	,
																															T				Value		,
																					[CallerMemberName]	string	PropertyName = null)
						{
							if (EqualityComparer<T>.Default.Equals(Storage, Value)) return false;
							Storage = Value;
							PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
							return true;
						}

					//¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨¨
					public event PropertyChangedEventHandler PropertyChanged;

				#endregion

					protected void SetName([CallerMemberName] string CallerName = null)
						{
							this.cc_myName = CallerName;
						}

			}
	}


//IPropertyChanged(PropertyName);

//protected virtual void IPropertyChanged(string Propertname)
//	{
//	this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Propertname));
//	}
