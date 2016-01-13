using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySpotterLibrary.Models
{
    public class Shot
    {
        public int ShotNumber { get; set; }

        public double DistanceFromCenter { get; set; }

        public double Score { get; set; }

        public bool IsHit { get; set; }

        public double DirectionFromCenter { get; set; }

        public double AccuracyRating { get; set; }


    }
}
