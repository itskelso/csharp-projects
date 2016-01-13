using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace TableCrib.Models
{
    public class ScoringHandModel : ObservableObject
    {
        private List<CardModel> cards;
        public List<CardModel> Cards 
        {
            get { return cards; }
            
        }

        

        private ScoringType scoreType;
        public ScoringType ScoreType 
        {
            get { return scoreType; }
            set { Set(() => ScoreType, ref scoreType, value); }
        }
        public ScoreValue Value { get { return GetValue(); } }

            private ScoreValue GetValue()
            {
                
                ScoreValue sv = (ScoreValue)Enum.Parse(typeof(ScoreValue), this.ScoreType.ToString());
                

                return sv;
            }

            public ScoringHandModel(HandModel cards, ScoringType scoreType)
            {
                this.cards = cards.Cards;
                this.ScoreType = scoreType;
            }


            public static ScoringHandModel NineteenHand(HandModel hand)
            {
                return new ScoringHandModel(hand, ScoringType.Nineteen);
            }

            public static ScoringHandModel Fifteen(HandModel hand)
            {
                return new ScoringHandModel(hand, ScoringType.Fifteen);
            }

            public static ScoringHandModel Pair(HandModel hand)
            {
                return new ScoringHandModel(hand, ScoringType.Pair);
            }

            public static ScoringHandModel ThreeOfAKind(HandModel hand)
            {
                return new ScoringHandModel(hand, ScoringType.ThreeOfAKind);
            }

            public static ScoringHandModel FourOfAKind(HandModel hand)
            {
                return new ScoringHandModel(hand, ScoringType.FourOfAKind);
            }

            public static ScoringHandModel RunOfThree(HandModel hand)
            {
                return new ScoringHandModel(hand, ScoringType.RunOfThree);
            }

            public static ScoringHandModel RunOfFour(HandModel hand)
            {
                return new ScoringHandModel(hand, ScoringType.RunOfFour);
            }

            public static ScoringHandModel RunOfFive(HandModel hand)
            {
                return new ScoringHandModel(hand, ScoringType.RunOfFive);
            }
            public static ScoringHandModel Flush(HandModel hand)
            {
                return new ScoringHandModel(hand, ScoringType.Flush);
            }
            public static ScoringHandModel Nobs(HandModel hand)
            {
                return new ScoringHandModel(hand, ScoringType.Nobs);
            }
            public enum ScoringType
            {
                Fifteen = 0,
                Pair = 1,
                ThreeOfAKind = 2,
                FourOfAKind = 3,
                RunOfThree = 4,
                RunOfFour = 5,
                RunOfFive = 6,
                Flush = 7,
                Nobs = 8,
                Nineteen = 9

            }

            public enum ScoreValue : int
            {
                Fifteen = 2,
                Pair = 2,
                ThreeOfAKind = 6,
                FourOfAKind = 12,
                RunOfThree = 3,
                RunOfFour = 4,
                RunOfFive = 5,
                Flush = 5,
                Nobs = 1,
                Nineteen = 0
            }


            
    }
}
