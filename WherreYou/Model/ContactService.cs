using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WherreYou.Model
{
    public class ContactService
    {
        public void SaveContact(string pName, string pTelNumber)
        {
            Contact c = new Contact { Name = pName, TelNumber = pTelNumber };
            IsolatedStorageSettings.ApplicationSettings[pName] = c;
            IsolatedStorageSettings.ApplicationSettings.Save();  
        }
        public Contact GetContact(string pName)
        {
            Contact c = new Contact();
            c = (Contact)IsolatedStorageSettings.ApplicationSettings[pName];
            return c;
        }
    }
}
