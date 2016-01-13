using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace TableCrib.Models
{
    public class HandsetModel
    {
        private List<HandModel> hands = new List<HandModel>();
        public List<HandModel> Hands { get { return hands; } }

        public static HandsetModel SetsOfTwo(HandModel hand)
        {
            HandsetModel setsOf2 = new HandsetModel();

            for (int x = 0; x < 5; x++)
            {
                for (int y = x + 1; y < 5; y++)
                {

                    setsOf2.Hands.Add(new HandModel(new CardModel[] { hand.Cards[x], hand.Cards[y] }));


                }

            }

            return setsOf2;
        }

        public static HandsetModel SetsOfThree(HandModel hand)
        {
            HandsetModel setsOf3 = new HandsetModel();

            for (int x = 0; x < 5; x++)
            {
                for (int y = x + 1; y < 5; y++)
                {
                    for (int z = y + 1; z < 5; z++)
                    {

                        setsOf3.Hands.Add(new HandModel(new CardModel[] { hand.Cards[x], hand.Cards[y], hand.Cards[z] }));
                    }
                }
            }

            return setsOf3;
        }

        public static HandsetModel SetsOfFour(HandModel hand)
        {
            HandsetModel setsOf4 = new HandsetModel();

            for (int x = 0; x < 2; x++)
            {
                for (int y = x + 1; y < 3; y++)
                {
                    for (int z = y + 1; z < 4; z++)
                    {
                        for (int w = z + 1; w < 5; w++)
                        {

                            setsOf4.Hands.Add(new HandModel(new CardModel[] { hand.Cards[x], hand.Cards[y], hand.Cards[z], hand.Cards[w] }));
                        }
                    }
                }
            }

            return setsOf4;
        }
    }
}
