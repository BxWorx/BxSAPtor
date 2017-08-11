using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Dynamicx.MVVM;

namespace Demo.ViewModels
{
    [ExportViewModel("Demo2", false)]
    class Demo2ViewModel : ViewModel
    {
        public Demo2ViewModel()
        {
            EnDisable = new RelayCommand(() =>
            {
                isEnabled = !isEnabled;
                Action.RaiseCanExecuteChanged();
            });
            Action = new RelayCommand(() => MessageBox.Show("demo2 test"), () => isEnabled);
        }

        #region Properties
        private bool isEnabled = false;
        public RelayCommand EnDisable { get; set; }
        public RelayCommand Action { get; set; }
        #endregion
    }
}
