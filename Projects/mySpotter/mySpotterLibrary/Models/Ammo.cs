using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace mySpotterLibrary.Models
{
    public class Ammo
    {
        [PrimaryKey,AutoIncrement]
        public int AmmoId { get; set; }

        public string Name { get; set; }

        public int Weight { get; set; }

        public int Velocity { get; set; }

        public string ImagePath { get; set; }

        [ManyToMany(typeof(GunAmmoRelationship),CascadeOperations =CascadeOperation.All,ReadOnly = true)]
        public List<Gun> PossibleGuns { get; set; }

        public override string ToString()
        {
            return this.Name;
        }


    }


    public class AmmoList
    {
        public List<Ammo> Ammo { get; set; }
    }
}
