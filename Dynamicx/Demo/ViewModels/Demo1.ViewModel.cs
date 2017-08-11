using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Dynamicx.MVVM;

namespace Demo.ViewModels
{
    [ExportViewModel("Demo1", false)]
    class Demo1ViewModel : ViewModel
    {
        public Demo1ViewModel()
        {
            EnDisable = new RelayCommand(() =>
            {
                isEnabled = !isEnabled;
                Action.RaiseCanExecuteChanged();
            });
            Action = new RelayCommand(() => MessageBox.Show("demo1 test"), () => isEnabled);
        }

        #region Properties
        private bool isEnabled = false;
        public RelayCommand EnDisable { get; set; }
        public RelayCommand Action { get; set; } 
        #endregion
    }
}
