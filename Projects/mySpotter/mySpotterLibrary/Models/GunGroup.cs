using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;
using System.Collections.ObjectModel;

namespace mySpotterLibrary.Models
{
    public class GunDataGroup
    {
        


        [PrimaryKey, AutoIncrement]
        public int GunGroupId { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Gun> Guns { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }


    public class GunCatalog
    {
       public ObservableCollection<GunDataGroup> GunGroups { get; set; }

    }
}
