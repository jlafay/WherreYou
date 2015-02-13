using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Maps.Services;
using System.Device.Location;
using Microsoft.Phone.Tasks;
using WherreYou.Model;
using WherreYou.ViewModel;

namespace WherreYou.View
{
    public partial class SendYourPositionView : PhoneApplicationPage
    {
        public SendYourPositionView()
        {
            InitializeComponent();
        }
    }
}