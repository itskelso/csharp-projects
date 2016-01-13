using mySpotterLibrary.Helpers;
using mySpotterLibrary.Models;
using mySpotterLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml;

namespace mySpotterLibrary.ViewModels
{
     public class UserGunSetupViewModel : ViewModelBase
    {

        private bool iseditable;

        public bool IsEditable
        {
            get { return iseditable; }
            set { Set(() => IsEditable, ref iseditable, value); }
        }

        private bool isSaved;

        public bool IsSaved
        {
            get { return isSaved; }
            set { Set(() => IsSaved, ref isSaved, value); }
        }


        private UserGunSetup setup;

        public UserGunSetup Setup
        {
            get { return setup; }
            set {
                Set(() => Setup, ref setup, value);
                
                }
        }

        private GunCatalog groups;

        public GunCatalog Groups
        {
            get { return groups; }
            set { Set(() => Groups, ref groups, value); }
        }

        private DatabaseHelper dbHelper;

        public DatabaseHelper DbHelper
        {
            get { return dbHelper; }
            set { Set(() => DbHelper, ref dbHelper, value); }
        }


        public UserGunSetupViewModel(int setupID, DatabaseHelper helper)
        {
            dbHelper = helper;

            if(setupID != 0)
            {
                setup = dbHelper.ReadSingleGeneric<UserGunSetup>(setupID);
                iseditable = false;
                RaisePropertyChanged("Setup");
            }
            

            else
            {
                setup = new UserGunSetup();
                 
                iseditable = true;
                GetOtherGuns();
            }

            
        }

        public UserGunSetupViewModel()
        {
            iseditable = true;
            dbHelper = new DatabaseHelper(DatabaseHelper._SQLitePlatformWinRT, DatabaseHelper.DB_PATH);
            Setup = new UserGunSetup();
            GetOtherGuns();
         //   int t = 0;
        }

        public async Task GetUserSetup(int setupId)
        {
            setup = DbHelper.ReadSingleGeneric<UserGunSetup>(setupId);
        }

        public void GetOtherGuns()
        {
            groups = new GunCatalog();

            groups.GunGroups = DbHelper.ReadAllGeneric<GunDataGroup>();
        }

        private ICommand getNextGunCommand;

        public ICommand GetNextGunCommand
        {
            get
            {
                if (getNextGunCommand == null)
                {
                    getNextGunCommand = new RelayCommand(
                        () =>
                        {
                            GetNextGun();
                        },
                        () =>
                        {
                            var currentGunsGroup = Groups.GunGroups.Where(t => t.Guns.Contains(Setup.Gun));
                            var highestId = currentGunsGroup.ToList().Count();
                            return (Setup.Gun.GunId != highestId);
                        });

                }
                return getNextGunCommand;
            }
        }

        public void GetNextGun()
        {
          Setup.Gun =  DbHelper.ReadSingleGeneric<Gun>(Setup.GunId + 1);
        }

        private ICommand loadGunGroupCommand;

        public ICommand LoadGunGroupCommand
        {
            get
            {
                if (loadGunGroupCommand == null)
                {
                    loadGunGroupCommand = new RelayCommand(
                        () =>
                        {
                            GetOtherGuns();
                        },
                        () =>
                        {
                            return (true);
                        });

                }
                return loadGunGroupCommand;
            }
        }

        private RelayCommand saveSetupCommand;

        public RelayCommand SaveSetupCommand
        {
            get
            {
                if (saveSetupCommand == null)
                {
                    saveSetupCommand = new RelayCommand(() => { SaveSetup(); }, () =>  canSave(Setup) );

                    
                }
                 
                return saveSetupCommand;
            }
        }

        private RelayCommand acceptAndShootCommand;
        public RelayCommand AcceptAndShootCommand
        {
            get
            {
                if (acceptAndShootCommand == null)
                {
                    acceptAndShootCommand = new RelayCommand(() => { ToSession(); }, () => canShoot());


                }

                return acceptAndShootCommand;
            }
        }

        public async void ToSession()
        {
            
        }

        public bool canShoot()
        {
            if(IsSaved)
            {
                return true;
            }
            return false;
        }
        public bool canSave(UserGunSetup setup)
        {
            try
            {
            if(!String.IsNullOrEmpty(Setup.SetupName) && Setup.Gun != null && Setup.Ammo != null && Setup.Scope != null)
            return true;
            }
            catch
            {
                return false;
            }
           

            return false;
        }

        public async void SelectCommand(object sender, object parameter)
        {
            var arg = parameter as Windows.UI.Xaml.Controls.ItemClickEventArgs;
            

            if (arg.ClickedItem.GetType() == typeof(GunDataGroup))
            {
                (arg.OriginalSource as Windows.UI.Xaml.Controls.GridView).ItemsSource = (arg.ClickedItem as GunDataGroup).Guns;
            }
            
            if (arg.ClickedItem.GetType() == typeof(Gun))
            {

                //  UserSetupViewModel.Setup = new UserGunSetup();
                Setup.Gun = arg.ClickedItem as Gun;
             //   GalaSoft.MvvmLight.Command.RelayCommand command = new RelayCommand(GetNextGun);
            //    RaisePropertyChanged("Setup");
                (((arg.OriginalSource as Windows.UI.Xaml.Controls.GridView).Parent as FlyoutPresenter).Parent as Popup).IsOpen = false;
                //    GunButton.Content = UserSetupViewModel.Setup.Gun.Name;
                //   UserSetupViewModel.SelectGunCommand(e.ClickedItem as Gun);

                //  dynamic vars = arg.OriginalSource;
                //    vars.Parent.Parent.IsOpen = false;
                SaveSetupCommand.RaiseCanExecuteChanged();
            }

        }

        public void SelectAmmoCommand(object sender,object parameter)
        {
            var arg = parameter as Windows.UI.Xaml.Controls.ItemClickEventArgs;

            Setup.Ammo = arg.ClickedItem as Ammo;
            (((arg.OriginalSource as Windows.UI.Xaml.Controls.GridView).Parent as FlyoutPresenter).Parent as Popup).IsOpen = false;
            SaveSetupCommand.RaiseCanExecuteChanged();

        }

        public void SelectScopeCommand(object sender,object parameter)
        {
            var arg = parameter as Windows.UI.Xaml.Controls.ItemClickEventArgs;

            Setup.Scope = arg.ClickedItem as Scope;
            (((arg.OriginalSource as Windows.UI.Xaml.Controls.GridView).Parent as FlyoutPresenter).Parent as Popup).IsOpen = false;
            SaveSetupCommand.RaiseCanExecuteChanged();
        }

        public void SetNameCommand(object sender, object parameter)
        {
            var arg = parameter as RoutedEventArgs;
            Setup.SetupName = (arg.OriginalSource as TextBox).Text;
            SaveSetupCommand.RaiseCanExecuteChanged();
            int i = 0;
        }

        public void SaveSetup()
        {
            var setups = dbHelper.ReadAllGeneric<UserGunSetup>();

            if (setups.Any(x => x.SetupName == Setup.SetupName))
            {
                Setup.UserGunSetupId = setups.First(x => x.SetupName == Setup.SetupName).UserGunSetupId;
            }

            dbHelper.InsertUserSetup(Setup);


            IsSaved = true;
        }
    }
}
