using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace TableCrib.Models
{
   public class VerticalHandsetModel : HandsetModel
    {
       

       public VerticalHandsetModel(CardModel[][] table)
       {

           
           CardModel[] hand = new CardModel[5];
           for (int x = 0; x < 5; x++)
           {
               for (int y = 0; y < 5; y++)
               {
                    
                   hand[y] = table[y][x];

               }
               this.Hands.Add(new HandModel(hand));
           }

           
       }

    }
}
