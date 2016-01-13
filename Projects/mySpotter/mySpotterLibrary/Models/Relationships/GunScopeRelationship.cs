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
    public class GunScopeRelationship
    {
        [ForeignKey(typeof(Gun))]
        public int GunId { get; set; }

        [ForeignKey(typeof(Scope))]
        public int ScopeId { get; set; }

    }
}
