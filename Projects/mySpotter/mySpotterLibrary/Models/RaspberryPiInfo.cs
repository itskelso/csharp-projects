using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace mySpotterLibrary.Models
{
    public class RaspberryPiInfo
    {
        [PrimaryKey, AutoIncrement]
        public int RaspberryPiId { get; set; }

        public string Name { get; set; }

        public string IpAddress { get; set; }

        public string Port { get; set; }

        public bool isDefault { get; set; }


    }
}
