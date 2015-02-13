using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WherreYou.ViewModel.Interface
{
    public class RelayCommand : ICommand
    {
        private readonly Action ExecutedAction;

        public RelayCommand(Action action)
        {
            ExecutedAction = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ExecutedAction();
        }
    }
}
