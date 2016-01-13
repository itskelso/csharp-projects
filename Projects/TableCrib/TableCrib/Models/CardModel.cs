using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Windows.UI.Xaml.Media;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

using GalaSoft.MvvmLight;

namespace TableCrib.Models
{
    public class CardModel : ObservableObject
    {
        private Suit suit;
        public Suit Suit 
        { 
            get 
            { 
                return suit;
            } 
            set 
            {
                Set(() => Suit, ref suit, value);
            } 
        }

        private CardNumber cardNumber;
        public CardNumber CardNumber
        {
            get
            {
                return cardNumber;
            }
            set
            {
                Set(() => CardNumber, ref cardNumber, value);
            }
        }

        private string imagePath;
        public string ImagePath 
        { 
            get 
            { 
                return imagePath;
            } 
            set 
            {
                Set(() => ImagePath, ref imagePath, value);
            } 
        }



        public Image Image
        {
            get { return new Image() { Source = new BitmapImage(new Uri(ImagePath, UriKind.RelativeOrAbsolute)) }; }
            

        }


        private int cardValue;
        public int CardValue 
        {
            get
            {
                return cardValue;
            }
            set
            {
                Set(() => CardValue, ref cardValue, value);
                
            }
        }


        

    }
           

    public enum Suit
    {
        None = 0,
        Clubs = 1,
        Diamonds = 2,
        Hearts = 3,
        Spades = 4,
    }

    
    public enum CardNumber
    {
        None = 0,
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
    }  
    
}

