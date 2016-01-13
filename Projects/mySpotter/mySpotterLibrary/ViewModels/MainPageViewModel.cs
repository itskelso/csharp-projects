using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mySpotterLibrary.Models;
using mySpotterLibrary.Common;
using mySpotterLibrary.Helpers;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace mySpotterLibrary.ViewModels
{
     public class MainPageViewModel : ViewModelBase
    {

        private DatabaseHelper dbHelper;

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { Set(() => UserName, ref userName, value); }
        }

        private UserGunSetupViewModel favoritesetup;

        

        public MainPageViewModel(DatabaseHelper db)
        {
            dbHelper = db;
            favoritesetup = new UserGunSetupViewModel(1,db);
        }

        public MainPageViewModel()
        {
          //  favoritesetup = new UserGunSetupViewModel();
        }

        private void GetFavorite()
        {// TODO implement saving favorite to settings rather than search everytime;
            ObservableCollection<History> list = dbHelper.ReadAllGeneric<History>();

         //  var common = list.GroupBy(i => i.UserGunSetupId).Max(t => t.Count());

          //  var recent = list.OrderByDescending(i => i.Date);
            
        }

        private void GetRecentHistory()
        {

        }
    }
}
