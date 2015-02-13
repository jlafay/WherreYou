using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WherreYou.Model;
using WherreYou.ViewModel.Interface;
using WherreYou.FrameworkMvvm;
using System.Windows.Input;
using Microsoft.Phone.Tasks;
using System.Windows;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Shell;

namespace WherreYou.ViewModel
{
    public class AddContactViewModel : ViewModelBase
    {
        PhoneNumberChooserTask pnct;

        private string error;
        public string Error
        {
            get { return error; }
            set { NotifyPropertyChanged(ref error, value); }
        }

        public AddContactViewModel()
        {
            pnct = new PhoneNumberChooserTask();
            pnct.Completed += new EventHandler<PhoneNumberResult>(AddContact);

            AddContactCommand = new RelayCommand(buAddContactClick);
        }

        public ICommand AddContactCommand { get; private set; }

        public void buAddContactClick()
        {
            pnct.Show();
        }

        private void AddContact(object sender, PhoneNumberResult pnr)
        {
            if (pnr.TaskResult == TaskResult.OK)
            {
                if (IsolatedStorageSettings.ApplicationSettings.Contains(pnr.DisplayName))
                {
                    Error = "This name already exists";
                }
                else
                {
                    ShellTile testTile = ShellTile.ActiveTiles.FirstOrDefault(elt => elt.NavigationUri.ToString().Contains("name=" + pnr.DisplayName));
                    if (testTile == null)
                    {
                        ContactService cs = new ContactService();
                        cs.SaveContact(pnr.DisplayName, pnr.PhoneNumber);
                        FlipTileData tile = new FlipTileData
                        {
                            SmallBackgroundImage = new Uri("/Assets/Tiles/StoreLogo.png", UriKind.Relative),
                            BackgroundImage = new Uri("/Assets/Tiles/StoreLogo.png", UriKind.Relative),
                            Title = pnr.DisplayName,
                            BackContent = "WherreYou? -\n" + pnr.DisplayName
                        };
                        ShellTile.Create(new Uri("/View/SendToContactView.xaml?name=" + pnr.DisplayName, UriKind.Relative), tile, false);
                    }
                }
            }
        }
    }
}
