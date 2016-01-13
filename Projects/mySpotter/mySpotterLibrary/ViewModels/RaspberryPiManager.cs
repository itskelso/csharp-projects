using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mySpotterLibrary.Models;
using mySpotterLibrary.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace mySpotterLibrary.ViewModels
{
    public class RaspberryPiManager : ViewModelBase
    {


        public ObservableCollection<RaspberryPiInfo> PiCatalog { get; set; }

        private DatabaseHelper dbHelper { get; set; }

        private bool isEditable;
        public bool IsEditable
        {
            get { return isEditable; }
            set { Set<bool>(()=> IsEditable, ref isEditable, value); }
        }

        private bool newIsDefault;

        public bool NewIsDefault
        {
            get { return newIsDefault; }
            set { Set<bool>(() => NewIsDefault, ref newIsDefault, value); }
        }


        public RaspberryPiManager()
        {
            dbHelper = new DatabaseHelper(DatabaseHelper._SQLitePlatformWinRT, DatabaseHelper.DB_PATH);
            try
            {
                this.PiCatalog =  dbHelper.ReadAllGeneric<RaspberryPiInfo>();
                DefaultPi = PiCatalog.First(x => x.isDefault == true);
            }
            catch
            {

            }
            
            isEditable = false;
        }

        private string newName;

        public string NewName
        {
            get { return newName; }
            set { Set<string>(()=>NewName, ref newName, value); }
        }



        private string newIPAddress;

        public string NewIPAddress
        {
            get { return newIPAddress; }
            set { Set<string>(()=>NewIPAddress, ref newIPAddress, value); }
        }
        private RaspberryPiInfo defaultPi;

        public RaspberryPiInfo DefaultPi
        {
            get { return defaultPi; }
            set { Set(() => DefaultPi, ref defaultPi, value); }
        }

        private RelayCommand<RaspberryPiInfo> setNewDefaultPiCommand;

        public RelayCommand<RaspberryPiInfo> SetNewDefaultPiCommand
        {
            get
            {
                if (setNewDefaultPiCommand == null)
                {
                    setNewDefaultPiCommand = new RelayCommand<RaspberryPiInfo>(async
                        (piInfo) =>
                    {
                        await SetDefault(piInfo);
                    },
                        (piInfo) =>
                        {
                            return (NewIPAddress != null && NewName != null);
                        });

                }
                return setNewDefaultPiCommand;
            }
        }

        private RelayCommand addNewDefaultPiCommand;

        public RelayCommand AddNewDefaultPiCommand
        {
            get
            {
                if (addNewDefaultPiCommand == null)
                {
                    addNewDefaultPiCommand = new RelayCommand(async
                        () =>
                    {
                        await AddPi();
                    },
                        () =>
                        {
                            return (NewIPAddress != null && NewName != null);
                        });

                }
                return addNewDefaultPiCommand;
            }
        }

        private async Task AddPi()
        {
            dbHelper.InsertRaspberryPiInfo(new RaspberryPiInfo() { Name = NewName, IpAddress = NewIPAddress, isDefault = false, Port = "5000" });
        }

        private async Task SetDefault(RaspberryPiInfo info)
        {
           RaspberryPiInfo currentDefault = PiCatalog.First(x => x.isDefault == true);
            info.isDefault = true;
            currentDefault.isDefault = false;

            dbHelper.UpdateRaspberryPiInfo(info);
            dbHelper.UpdateRaspberryPiInfo(currentDefault);
            DefaultPi = info;

        }
    }
}
