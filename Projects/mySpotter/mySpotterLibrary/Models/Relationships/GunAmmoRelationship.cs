using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySpotterLibrary.Models
{
    public class GunAmmoRelationship
    {
        [ForeignKey(typeof(Gun))]
        public int GunId { get; set; }

        [ForeignKey(typeof(Scope))]
        public int AmmoId { get; set; }


    }
}
