using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace TableCrib.Models
{
    public class DeckModel : ObservableObject
    {

        public DeckModel()
        {
            Reset();
        }

        private List<CardModel> cards = new List<CardModel>();

        public List<CardModel> Cards { get { return cards; } }
        

        public void Reset()
        {
            cards = Enumerable.Range(1, 4)
                .SelectMany(s => Enumerable.Range(1, 13)
                                    .Select(c => new CardModel()
                                    {
                                        Suit = (Suit)s,
                                        CardNumber = (CardNumber)c,
                                        CardValue = Clamp<int>(c,10,1),
                                        ImagePath = "ms-appx:/CardImages/" + ((CardNumber)c).ToString() + ((Suit)s).ToString() + ".png"
                                    }
                                            )
                            )
                   .ToList();
        }

        public void Shuffle()
        {
            cards = Cards.OrderBy(c => Guid.NewGuid())
                         .ToList();
        }

        public CardModel TakeCard()
        {
            CardModel card = Cards.FirstOrDefault();
            Cards.Remove(card);

            return card;
        }

        public IEnumerable<CardModel> TakeCards(int numberOfCards)
        {
            var cardsToTake = Cards.Take(numberOfCards);

            var takeCards = cardsToTake as CardModel[] ?? cardsToTake.ToArray();
            Cards.RemoveAll(takeCards.Contains);

            return takeCards;
        }

        public static T Clamp<T>(T value, T max, T min)
         where T : System.IComparable<T>
        {
            if (value.CompareTo(max) > 0)
                return max;
            if (value.CompareTo(min) < 0)
                return min;
            return value;
        }

        
    }
}
