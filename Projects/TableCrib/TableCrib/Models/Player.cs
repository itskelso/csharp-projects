using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using Windows.UI.Xaml.Media;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TableCrib.Models
{
   public class Player : ObservableObject 
    {
       private string name;
       private int number;
       private string record;
       private bool isTurn;
       private CardModel currentCard;
       private bool hasCrib;
       private bool isCountUp;
       private int score;
       private PlayersType playerType;
       private int lastPosition;
       private PointCollection possiblePoints;
       

       public string Name
       {
           get { return name; }
           set
           {
               Set(() => Name, ref name, value);
               
           }
       }
       public int Number
       {
           get { return number; }
           set
           {
               Set(() => Number, ref number, value);

           }
       }
       
       public string Record
       {
           get { return record; }
           set
           {
               Set(() => Record, ref record, value);
               
           }
       }
       public bool IsTurn
       {
           get { return isTurn; }
           set
           {
               Set(() => IsTurn, ref isTurn, value);
        
           }
       }
       public CardModel CurrentCard
       {
           get { return currentCard; }
           set
           {
               Set(() => CurrentCard, ref currentCard, value);
               
           }
       }
       public bool HasCrib
       {
           get { return hasCrib; }
           set
           {
               Set(() => HasCrib, ref hasCrib, value);
               
           }
       }
       public bool IsCountUp
       {
           get { return isCountUp; }
           set
           {
               Set(() => IsCountUp, ref isCountUp, value);
               
           }
       }
       public int Score
       {
           get { return score; }
           set
           {
               Set(() => Score, ref score, value);
               
           }
       }
       public PlayersType PlayerType
       {
           get { return playerType; }
           set
           {
               Set(() => PlayerType, ref playerType, value);
           }
           
       }
       public int LastPosition
       {
           get { return lastPosition; }
           set
           {
               Set(() => LastPosition, ref lastPosition, value);
           }

       }

       public PointCollection PossiblePoints
       {
           get { return possiblePoints; }
           set
           {
               Set(() => PossiblePoints, ref possiblePoints, value);

           }
       }


       public void TakeTurn()
       {
           this.isTurn = true;
           
       }

       public void EndTurn()
       {
           this.isTurn = !this.isTurn;
           
           
       }

       public void SwitchCrib()
       {
           this.hasCrib = !this.hasCrib;
           
       }

       public enum PlayersType
       {
           HumanPlayer = 0,
           ComputerPlayer = 1
       }


       


       public Player()
       {
           name = "User";
           isTurn = false;
           record = "0-0";
           hasCrib = false;
           currentCard = null;
           isCountUp = true;
           lastPosition = 0;
           possiblePoints = new PointCollection();
           score = 0;
        //   playerType = PlayerType.HumanPlayer;



       }

       public Player(PlayersType playerType, bool direction)
       {
           switch (playerType)
           {
               case PlayersType.HumanPlayer:
                   
                       name = "User";
                       number = 1;
                       isTurn = false;
                       record = "0-0";
                       hasCrib = false;
                       currentCard = null;
                       isCountUp = direction;
                       lastPosition = 0;
                       possiblePoints = new PointCollection();
                       score = 0;
                       break;

               case PlayersType.ComputerPlayer:
                        name = "Computer";
                        number = 2;
                        isTurn = false;
                        record = "0-0";
                        hasCrib = false;
                        currentCard = null;
                        isCountUp = !direction;
                        lastPosition = 0;
                        possiblePoints = new PointCollection();
                        score = 0;
                        PlayerType = PlayersType.ComputerPlayer;
                        break;

                       
                   
           }
       }



       public static Player NewPlayer()
       {
           return new Player();
       }
    }
}
