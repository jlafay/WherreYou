using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace WherreYou.View
{
    public partial class HomeView : PhoneApplicationPage
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Syp(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/SendYourPositionView.xaml", UriKind.Relative));
        }

        private void Sc(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AddContactView.xaml", UriKind.Relative));
        }
    }
}