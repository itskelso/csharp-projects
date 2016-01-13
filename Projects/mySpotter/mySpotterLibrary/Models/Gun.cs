using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using GalaSoft.MvvmLight;

namespace mySpotterLibrary.Models
{
    public class Gun : ObservableObject
    {


        [PrimaryKey,AutoIncrement]
        public int GunId { get; set; }

        [ForeignKey(typeof(GunDataGroup))]
        public int GunGroupId { get; set; }
        
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        [ManyToMany(typeof(GunAmmoRelationship),CascadeOperations =CascadeOperation.All)]
        public List<Ammo> PossibleAmmo { get; set; }

        [ManyToMany(typeof(GunScopeRelationship), CascadeOperations = CascadeOperation.All)]
        public List<Scope> PossibleScopes { get; set; }

        

        public override string ToString()
        {
            return this.Name;
        }
    }


    public class GunList
    {
        public List<Gun> Guns { get; set; }
    }
}
