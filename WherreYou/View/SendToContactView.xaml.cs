using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Services;
using System.Device.Location;
using System.IO.IsolatedStorage;
using WherreYou.Model;
using WherreYou.ViewModel;

namespace WherreYou.View
{
    public partial class SendToContactView : PhoneApplicationPage
    {
        public SendToContactView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string name;
            if (NavigationContext.QueryString.TryGetValue("name", out name))
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains(name))
                {
                    ((SendToContactViewModel)DataContext).UpdateValue(name);
                }
            }
            base.OnNavigatedTo(e);
        }
    }
}