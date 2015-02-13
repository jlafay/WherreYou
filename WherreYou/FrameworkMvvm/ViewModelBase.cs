using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WherreYou.FrameworkMvvm
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string parameterName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(parameterName));
        }

        public bool NotifyPropertyChanged<T>(ref T variable, T value, [CallerMemberName] string parameterName = null)
        {
            if (object.Equals(variable, value)) return false;

            variable = value;
            NotifyPropertyChanged(parameterName);
            return true;
        }
    }
}
