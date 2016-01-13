using System;
using System.Collections.Generic;
using System.Text;

namespace TableCrib.Models
{
    public class DiagonalHandsetModel : HandsetModel
    {
       


        public DiagonalHandsetModel(CardModel[][] table)
        {
            
            CardModel[] hand = new CardModel[5];
            for (int x = 0; x < 5; x++)
            {
                 
                hand[x] = table[x][x]; 
            }
            this.Hands.Add(new HandModel(hand));
            
            for (int x = 0; x < 5; x++)
            {
                int y = 4 - x;                 
                hand[x] = table[x][y];

            }
            this.Hands.Add(new HandModel(hand));

            
        }
    }
}
