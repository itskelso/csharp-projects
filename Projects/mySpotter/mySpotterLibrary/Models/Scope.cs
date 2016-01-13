using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace mySpotterLibrary.Models
{
    public class Scope
    {
        [PrimaryKey, AutoIncrement]
        public int ScopeId { get; set; }

        public string Name { get; set; }

        public string RecommendedDistance { get; set; }

        public string ImagePath { get; set; }

        [ManyToMany(typeof(GunScopeRelationship),CascadeOperations = CascadeOperation.All,ReadOnly =true)]
        public List<Gun> PossibleGuns { get; set; }

        public override string ToString()
        {
            return this.Name;
        }

    }

    public class ScopeList
    {
        public List<Scope> Scopes { get; set; }
    }
}
