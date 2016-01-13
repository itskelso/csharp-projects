using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace mySpotterLibrary.Models
{
    public class UserGunSetup : ObservableObject
    {
        private Gun gun;
        private Ammo ammo;
        private Scope scope;
        private string setupName;

        [PrimaryKey, AutoIncrement]
        public int UserGunSetupId { get; set; }

        [Unique]
        public string SetupName
        {
            get { return setupName; }
            set { Set(() => SetupName, ref setupName, value); }
        }

        [ForeignKey(typeof(Gun))]
        public int GunId { get; set; }

        [OneToOne]
        public Gun Gun
        {
            get { return gun; }
            set { Set(() => Gun, ref gun, value); }
        }

        

        [ForeignKey(typeof(Ammo))]
        public int AmmoId { get; set; }

        [OneToOne]
        public Ammo Ammo
        {
            get { return ammo; }
            set { Set(() => Ammo, ref ammo, value); }
        }

        [ForeignKey(typeof(Scope))]
        public int ScopeId { get; set; }

        [OneToOne]
        public Scope Scope
        {
            get { return scope; }
            set { Set(() => Scope, ref scope, value); }
        }


        public DateTime DateCreated { get; set; }


        public DateTime LastUsed { get; set; }


        public int TimesUsed { get; set; }


        public int BulletCount { get; set; }


        public double Accuracy { get; set; }

        public double AvgSpread { get; set; }

        public int TargetDistance { get; set; }


    }


    public class MyArmory : ObservableObject
    {
        public ObservableCollection<UserGunSetup> MySetups { get; set; }
    }
}
