using mySpotterLibrary.Models;
using mySpotterLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mySpotterLibrary.Helpers;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;

namespace mySpotterLibrary.ViewModels
{
   public class GunCatalogViewModel : BindableBase
    {

        public GunCatalogViewModel()
        {
            databaseHelper = new DatabaseHelper(DatabaseHelper._SQLitePlatformWinRT, DatabaseHelper.DB_PATH);
            GetGun();
        }

        private int gunId;

        public int GunId
        {
            get { return gunId; }
            set { SetProperty(ref gunId, value); }
        }

        public AmmoList ammo { get; set; }

        public ScopeList scopes { get; set; }
        

        private GunCatalog guns;

        public GunCatalog Guns
        {
            get { return guns; }
            set { SetProperty(ref guns, value); }
        }

        private DatabaseHelper databaseHelper;

        public DatabaseHelper DatabaseHelper
        {
            get { return databaseHelper; }
            set { SetProperty(ref databaseHelper, value); }
        }


        private RelayCommand<Gun> getGunStatsCommand;
        
        public RelayCommand<Gun> GetGunStatsCommand
        {
            get
            {
                if(getGunStatsCommand == null)
                {
                    getGunStatsCommand = new RelayCommand<Gun>(
                       async (gun)=> 
                        {
                            ammo.Ammo = gun.PossibleAmmo;
                           await GetGunStats(gun);
                        },
                        (gun) => 
                        {
                            return (guns != null);
                        });

                }
                return getGunStatsCommand;
            }
        }

        private async Task GetGunStats(Gun gun)
        {
          ObservableCollection<History> hist = databaseHelper.GetSpecificGunHistory(gun.GunId);

            if (hist.Count == 0)
            {

            }

        }

        private ICommand updateGunCommand;

        public ICommand UpdateGunCommand
        {
            get
            {
                if (updateGunCommand == null)
                {
                    updateGunCommand = new RelayCommand(
                        () =>
                        {
                            UpdateGun();
                        },
                        () =>
                        {
                            return (guns != null);
                        });

                }
                return updateGunCommand;
            }
        }

        public RelayCommand PreviousGun
        {
            get;
            set;
        }

        

        public RelayCommand DeleteGun
        {
            get;
            set;
        }

       

        public void WireCommands()
        {

        }


        public void UpdateGun()
        {
          
        }

        public void GetGun()
        {
            Guns = new GunCatalog();
            Guns.GunGroups = new System.Collections.ObjectModel.ObservableCollection<GunDataGroup>();
            guns.GunGroups = DatabaseHelper.ReadAllGeneric<GunDataGroup>();
            ammo = new AmmoList();
            ammo.Ammo = new List<Ammo>();
            ammo.Ammo = DatabaseHelper.ReadAllGeneric<Ammo>().ToList();
            scopes = new ScopeList();
            scopes.Scopes = new List<Scope>();
            scopes.Scopes = DatabaseHelper.ReadAllGeneric<Scope>().ToList();
        }

        

    }
}
