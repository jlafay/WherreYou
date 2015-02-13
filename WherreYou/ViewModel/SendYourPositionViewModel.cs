using System.Text;
using System.Threading.Tasks;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WherreYou.ViewModel.Interface;
using WherreYou.FrameworkMvvm;

namespace WherreYou.ViewModel
{
    public class SendYourPositionViewModel : ViewModelBase
    {
        PhoneNumberChooserTask pnct;
        private Geoposition CurrentPos;
        private Geolocator geo;
        private String CurrentAddress;
        private bool checkLoading;

        private string lo;
        public string Lo
        {
            get { return lo; }
            set { NotifyPropertyChanged(ref lo, value); }
        }

        private string la;
        public string La
        {
            get { return la; }
            set { NotifyPropertyChanged(ref la, value); }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { NotifyPropertyChanged(ref address, value); }
        }

        private string loading;
        public string Loading
        {
            get { return loading; }
            set { NotifyPropertyChanged(ref loading, value); }
        }

        public SendYourPositionViewModel() 
        {
            Loading = "Loading... Please, wait.";
            checkLoading = false;

            chooseContactCommand = new RelayCommand(chooseContactClick);

            geo = new Geolocator();
            geo.DesiredAccuracy = PositionAccuracy.High;
            geo.MovementThreshold = 100;
            geo.PositionChanged += geoPositionChanged;
            geo.StatusChanged += geoStatusChanged;

            if (geo.LocationStatus == PositionStatus.Disabled)
            {
                this.IsValidGPS();
            }    

            pnct = new PhoneNumberChooserTask();
            pnct.Completed += new EventHandler<PhoneNumberResult>(sendClick);
        }

        private void IsValidGPS()
        {
            App.RootFrame.Dispatcher.BeginInvoke(() =>
            {
                var message = MessageBox.Show("Please, enable location services", "Disabled GPS", MessageBoxButton.OK);
            });
        }

        void geoStatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            switch (args.Status)
            {
                case PositionStatus.Disabled:
                    this.IsValidGPS();
                    break;
                case PositionStatus.Initializing:
                    break;
                case PositionStatus.NoData:
                    break;
                case PositionStatus.NotAvailable:
                    break;
                case PositionStatus.NotInitialized:
                    break;
                case PositionStatus.Ready:
                    break;
                default:
                    break;
            }
        }

        void geoPositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            App.RootFrame.Dispatcher.BeginInvoke(() =>
            {
                this.CurrentPos = args.Position;
                Lo = this.CurrentPos.Coordinate.Longitude.ToString();
                La = this.CurrentPos.Coordinate.Latitude.ToString();

                this.LocationChanged(this.CurrentPos.Coordinate.Latitude, this.CurrentPos.Coordinate.Longitude);
            });
        }

        private void LocationChanged(double la, double lo)
        {
            ReverseGeocodeQuery rgeoquery = new ReverseGeocodeQuery();
            rgeoquery.GeoCoordinate = new GeoCoordinate(la, lo);
            rgeoquery.QueryCompleted += rgeoqueryCompleted;
            rgeoquery.QueryAsync();
        }

        void rgeoqueryCompleted(object sender, QueryCompletedEventArgs<System.Collections.Generic.IList<MapLocation>> args)
        {
            App.RootFrame.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    if (args.Result.Count > 0)
                    {
                        MapAddress address = args.Result[0].Information.Address;
                        CurrentAddress = String.Format("{0} {1} {2} {3} {4}", address.HouseNumber, address.Street, address.City + " \n", address.PostalCode, address.Country);
                        Address = String.Format("{0} {1} {2} {3} {4}", address.HouseNumber + " \n", address.Street + " \n", address.City + " \n", address.PostalCode + " \n", address.Country + " \n");
                        Loading = null;
                        checkLoading = true;
                    }
                    else
                    {
                        CurrentAddress = "Address not found";
                        Address = CurrentAddress;
                    }
                }
                catch
                {
                    CurrentAddress = "An error has occurred";
                    Address = CurrentAddress;
                }
            });
        }

        public ICommand chooseContactCommand { get; private set; }

        public void chooseContactClick()
        {
            if (checkLoading == true)
            {
                pnct.Show();
            }
        }

        private void sendClick(object sender, PhoneNumberResult pnr)
        {
            if (pnr.TaskResult == TaskResult.OK)
            {
                SmsComposeTask sct = new SmsComposeTask { To = pnr.PhoneNumber, Body = "Hello ! My latitude is : " + this.CurrentPos.Coordinate.Latitude + " and my longitude is : " + this.CurrentPos.Coordinate.Longitude + " (" + CurrentAddress + ")." };
                sct.Show();
            }
        }
    }
}
