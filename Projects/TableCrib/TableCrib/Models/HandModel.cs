using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace TableCrib.Models
{
   public class HandModel
    {        
        

            public List<CardModel> Cards { get; private set; }


            public HandModel(CardModel[] cardsArray)
            {
                this.Cards = new List<CardModel>(cardsArray);
            }


        
    }
}
