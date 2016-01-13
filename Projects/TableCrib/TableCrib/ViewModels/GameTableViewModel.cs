using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;
using System.Threading.Tasks;

using Windows.UI.Xaml.Shapes;
using System.Diagnostics;
using System.ComponentModel;
using TableCrib.Models;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TableCrib.ViewModels
{
    public class GameTableViewModel : ViewModelBase
    {

        
        
        private Grid tablePivotGrid;
        private Button lastButton;



        public ObservableCollection<ScoringHandModel> ScoringCombos = new ObservableCollection<ScoringHandModel>();
        

        public CardModel[][] Table = new CardModel[5][] { new CardModel[5], new CardModel[5], new CardModel[5], new CardModel[5], new CardModel[5] };
            
            
            
        
        public Grid TablePivotGrid
        {
            get
            { 
                return tablePivotGrid;
            }
            set
            {
                Set(() => TablePivotGrid, ref tablePivotGrid, value);
            }
        }
        public Button LastButton
        {
            get
            {
                return lastButton;
            }
            set
            {
                Set(() => LastButton, ref lastButton, value);
            }
        }

        

        #region // constructor

        public GameTableViewModel()
        {
            TablePivotGrid = new Grid();
            for (int x = 0; x < 5; x++)
            {
                TablePivotGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1,GridUnitType.Star) });
                TablePivotGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            TablePivotGrid.AllowDrop = true;
            
            TablePivotGrid.VerticalAlignment = VerticalAlignment.Stretch;
       

                
            
        }



        #endregion
        


        

        
        
        

        public  ObservableCollection<ScoringHandModel> CountHands(HandModel hand)
        {


            ScoringCombos.Clear();
            

            

            //Get Sets of 2,3,4 and 5 is just hand            

            HandsetModel allHands = new HandsetModel();

            allHands.Hands.Add(hand);
            allHands.Hands.AddRange(HandsetModel.SetsOfFour(hand).Hands);
            allHands.Hands.AddRange(HandsetModel.SetsOfThree(hand).Hands);
            allHands.Hands.AddRange(HandsetModel.SetsOfTwo(hand).Hands);
            
            //Check 15s First

            

            

            CheckFor15s(allHands);


            CheckForMatches(allHands);
            
            
            
                //Check for Runs



            CheckForRuns(allHands);
                

                      
                
                        
                    
                //Check For Flush
                if (hand.Cards.All(x => x.Suit == hand.Cards[0].Suit))
                {
                    ScoringCombos.Add(ScoringHandModel.Flush(hand));
                }
            

            

            
          //  player.Score += score;
                return ScoringCombos;
            
        }

        private void CheckForRuns(HandsetModel hands)
       {
           foreach (HandModel handtocheck in hands.Hands)
           {
            var handArray = handtocheck.Cards.OrderBy(x => (int)x.CardNumber).Select(x => (int)x.CardNumber).ToArray();
           

          int min = handtocheck.Cards.Min(x => (int)x.CardNumber);
          int max = handtocheck.Cards.Count;
         
          var range = Enumerable.Range(min, max).ToArray();

          var result = ArraysEqual(range, handArray);
          
               if (result)
              {
              
                   switch (handtocheck.Cards.Count)
              
                   {
                  case 5:
                      ScoringCombos.Add(ScoringHandModel.RunOfFive(handtocheck));
                      return;
                  case 4:
                      ScoringCombos.Add(ScoringHandModel.RunOfFour(handtocheck));
                      break;
                  case 3:
                      if (ScoringCombos.Any(x => x.ScoreType == ScoringHandModel.ScoringType.RunOfFour))
                          return;
                      ScoringCombos.Add(ScoringHandModel.RunOfThree(handtocheck));
                      break;
              
                   }
          
               }

           }       
           
           
        
       }


       private static bool ArraysEqual(int[] a1, int[] a2)
       {
           if (a1.Length == a2.Length)
           {
               for (int i = 0; i < a1.Length; i++)
               {
                   if (a1[i] != a2[i])
                   {
                       return false;
                   }
               }
               return true;
           }
           return false;
       }



       private void CheckFor15s(HandsetModel hands)
       {
           foreach (HandModel c in hands.Hands)
           {

               if (c.Cards.Sum(x => x.CardValue) == 15)
               {

                   ScoringCombos.Add(ScoringHandModel.Fifteen(c));
               }
           }
       }


       private void CheckForMatches(HandsetModel hands)
       {
           foreach (HandModel c in hands.Hands)
           {
               if (c.Cards.All(x => x.CardNumber == c.Cards[0].CardNumber))
               {
                   if (c.Cards.Count == 4)
                   {
                    ScoringCombos.Add(ScoringHandModel.FourOfAKind(c));
                    return;
                   }


                   if (c.Cards.Count == 3)
                   {
                    ScoringCombos.Add(ScoringHandModel.ThreeOfAKind(c));
                    continue;
                   }

                   if (c.Cards.Count == 2 && !(ScoringCombos.Any(x => x.ScoreType == ScoringHandModel.ScoringType.ThreeOfAKind && x.Cards[0].CardNumber == c.Cards[0].CardNumber)))
                   {
                       ScoringCombos.Add(ScoringHandModel.Pair(c));
                       
                   }
               }
           }
       }

        
        
    }






    
}
