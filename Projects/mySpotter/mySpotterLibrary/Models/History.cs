using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace mySpotterLibrary.Models
{
    public class History
    {

        [PrimaryKey]
        public DateTime Date { get; set; }

        [ForeignKey(typeof(UserGunSetup))]
        public int UserGunSetupId { get; set; }

        [OneToOne]
        public UserGunSetup Setup { get; set; }


        public double Spread { get; set; }


        public int Score { get; set; }



        public string TargetImagesPath { get; set; }


        public int TargetDistance { get; set; }


        public int ShotsTaken { get; set; }


        public int ShotsHit { get; set; }


        public int ShotsMissed { get; set; }

        public double Accuracy { get; set; }






    }
}
