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
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Shapes;
using System.Diagnostics;
using System.ComponentModel;
using TableCrib.Models;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
//using TableCrib.Actions;


namespace TableCrib.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private DeckModel deck;
        private GameTableViewModel gameTable;
        private GameBoardViewModel gameBoardViewModel;        
        private PlayersViewModel players;


        private Image directionImage;
        private CardModel currentCard;

        private HandModel currentHand;


        public Storyboard CurrentHandStoryboard { get; set; }

        private ObservableCollection<ScoringHandModel> scoringHands;
        public ObservableCollection<ScoringHandModel> ScoringHands
        {
            get
            {
                return scoringHands;
            }
            set
            {
                Set(() => ScoringHands, ref scoringHands, value);
            }
        }

        public ObservableCollection<CardScoreAnimationModel> CardAnimations { get; private set; }

        private CardScoreAnimationModel card1Animation;
        public CardScoreAnimationModel Card1Animation
        {
            get
            {
                return card1Animation;
            }
            set
            {
                Set(() => Card1Animation, ref card1Animation, value);
            }
        }
        private CardScoreAnimationModel card2Animation;
        public CardScoreAnimationModel Card2Animation
        {
            get
            {
                return card2Animation;
            }
            set
            {
                Set(() => Card2Animation, ref card2Animation, value);
            }
        }
        private CardScoreAnimationModel card3Animation;
        public CardScoreAnimationModel Card3Animation
        {
            get
            {
                return card3Animation;
            }
            set
            {
                Set(() => Card3Animation, ref card3Animation, value);
            }
        }

        private CardScoreAnimationModel card4Animation;
        public CardScoreAnimationModel Card4Animation
        {
            get
            {
                return card4Animation;
            }
            set
            {
                Set(() => Card4Animation, ref card4Animation, value);
            }
        }


        private CardScoreAnimationModel card5Animation;
        public CardScoreAnimationModel Card5Animation
        {
            get
            {
                return card5Animation;
            }
            set
            {
                Set(() => Card5Animation, ref card5Animation, value);
            }
        }
        



        public GameTableViewModel GameTable
        {
            get
            {
                return gameTable;
            }
            set
            {
                Set(() => GameTable, ref gameTable, value);
            }
        }
        public GameBoardViewModel GameBoardVM
        {
            get
            {
                return gameBoardViewModel;
            }
            set
            {
                Set(() => GameBoardVM, ref gameBoardViewModel, value);
            }
        }

        public HandModel CurrentHand
        {
            get
            {
                return currentHand;
            }
            set
            {
                Set(() => CurrentHand, ref currentHand, value);
            }
        }

        public bool Busy { get; set; }
        public DeckModel Deck 
        {
            get
            {
                return deck;
            }
            set
            {
                Set(() => Deck, ref deck, value);
            }
        }

        public PlayersViewModel Players
        {
            get
            {
                return players;
            }
            set
            {
                Set(() => Players, ref players, value);
            }
        }

        public CardModel CurrentCard
        {
            get
            {
                return currentCard;
            }
            set
            {
                Set(() => CurrentCard, ref currentCard, value);
            }
        }

        public Image DirectionImage
        {
            get
            {
                return directionImage;
            }
            set
            {
                Set(() => DirectionImage, ref directionImage, value);
            }
        }

        private bool isFlyoutClosed;
        public bool IsFlyoutClosed
        {
            get
            {
                return isFlyoutClosed;
            }
            set
            {

                Set(ref isFlyoutClosed, value);
                if (value)
                    IsFlyoutClosed = false;
                    
            }
        }

        private bool isFlyoutOpen;
        public bool IsFlyoutOpen
        {
            get
            {
                return isFlyoutOpen;
            }
            set
            {

                Set(ref isFlyoutOpen, value);
                if (value)
                    IsFlyoutOpen = false;

            }
        }

        

        
      
        
        

        public GameViewModel()
        {
            
            GameBoardVM = new GameBoardViewModel();
            GameTable = new GameTableViewModel();
            Players = new PlayersViewModel();
           // CurrentCard = new CardModel();
            DirectionImage = new Image();
           // CurrentCard = new CardModel();
            Deck = new DeckModel();
            Deck.Shuffle();
            Players.GetPlayers(0);
         //   isFlyoutClosed = true;
            Random rand = new Random();
            int a = rand.Next(0, 2);
            Players.Players[a].IsTurn = true;
            Players.Players[a].HasCrib = true;

            SetDirectionImage("Vertical");
            
           
            
            //takeCardCommand.CanExecute = true;
            Players.Players[0].PossiblePoints = GameBoardVM.PlayerOnePoints;
            Players.Players[1].PossiblePoints = GameBoardVM.PlayerTwoPoints;
            

            WireCommands();
            PlaceButtonsCommand.Execute(null);
            DrawFirstCard();

            SetupGameViewStoryboard();
         
        }

        public void SetupGameViewStoryboard()
        {
            CurrentHandStoryboard = new Storyboard();

            Card1Animation = new CardScoreAnimationModel();
            Card2Animation = new CardScoreAnimationModel();
            Card3Animation = new CardScoreAnimationModel();
            Card4Animation = new CardScoreAnimationModel();
            Card5Animation = new CardScoreAnimationModel();

            
            Storyboard.SetTargetProperty(Card1Animation.Animation, "(Image.RenderTransform).(TranslateTransform.X)");

            
            Storyboard.SetTargetProperty(Card2Animation.Animation, "(Image.RenderTransform).(TranslateScaleTransform.X)");

            
            Storyboard.SetTargetProperty(Card3Animation.Animation, "(Image.RenderTransform).(TranslateTransform.X)");

            
            Storyboard.SetTargetProperty(Card4Animation.Animation, "(Image.RenderTransform).(TranslateTransform.X)");

            
            Storyboard.SetTargetProperty(Card5Animation.Animation, "(Image.RenderTransform).(TranslateTransform.X)");

            CurrentHandStoryboard.Children.Add(Card1Animation.Animation);
            CurrentHandStoryboard.Children.Add(Card2Animation.Animation);
            CurrentHandStoryboard.Children.Add(Card3Animation.Animation);
            CurrentHandStoryboard.Children.Add(Card4Animation.Animation);
            CurrentHandStoryboard.Children.Add(Card5Animation.Animation);

            CardAnimations = new ObservableCollection<CardScoreAnimationModel>();
            CardAnimations.Add(Card1Animation);
            CardAnimations.Add(Card2Animation);
            CardAnimations.Add(Card3Animation);
            CardAnimations.Add(Card4Animation);
            CardAnimations.Add(Card5Animation);
        }

        public void SetDirectionImage(string direction)
        {

            if (direction == "Vertical")
            {
                Players.Players[0].IsCountUp = true;
                Players.Players[1].IsCountUp = false;
                DirectionImage.Source = new BitmapImage(new Uri("ms-appx:/Assets/arrowup.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                Players.Players[0].IsCountUp = false;
                Players.Players[1].IsCountUp = true;
                DirectionImage.Source = new BitmapImage(new Uri("ms-appx:/Assets/arrowright.png", UriKind.RelativeOrAbsolute));
            }
        }
       
        
        
        
        
        

        

        public RelayCommand TakeCardCommand
        {
            get;
            private set;

        }

        public RelayCommand AbleToPlayCommand
        {
            get;
            private set;
        }

        public RelayCommand AbleToDrawCommand
        {
            get;
            private set;
        }

        public RelayCommand PlaceCardCommand
        {
            get;
            private set;
        }

        public RelayCommand NextPlayerTurnCommand
        {
            get;
            private set;
        }

        public RelayCommand<Button> EmptySpotButtonClickCommand
        {
            get;
            private set;
        }

        public RelayCommand PlaceButtonsCommand
        {
            get;
            private set;
        }

        public RelayCommand<ObservableCollection<ScoringHandModel>> ScoreRecapCommand
        {
            get;
            private set;
        }

        

        public RelayCommand SendCommand { get; set; }


        public void WireCommands()
        {
            TakeCardCommand = new RelayCommand(DrawCard);
            AbleToPlayCommand = new RelayCommand(AbleToPlay);
            AbleToDrawCommand = new RelayCommand(AbleToDraw);
            PlaceCardCommand = new RelayCommand(PlaceCard);
            NextPlayerTurnCommand = new RelayCommand(NextPlayerTurn);
            EmptySpotButtonClickCommand = new RelayCommand<Button>((a) => EmptySpotButtonClick(a));
            PlaceButtonsCommand = new RelayCommand(PlaceButtons);
            ScoreRecapCommand = new RelayCommand<ObservableCollection<ScoringHandModel>>((a) => ScoreRecap(a));
            SendCommand = new RelayCommand(() =>
            {
                
                IsFlyoutClosed = true;
                IsFlyoutOpen = false;
                if(this.tcs != null && !this.tcs.Task.IsCompleted)
                this.tcs.SetResult(true);
            });
            
           
            
        }

        public void DrawCard()
        {
            if (CurrentCard != null)
            {
                return;
            }

             CardModel c = Deck.TakeCard();
             CurrentCard = c;
             
        
            
          

        }

        

        

        private TaskCompletionSource<Boolean> tcs; 
        public void  ScoreRecap(ObservableCollection<ScoringHandModel> scores)
        {
            IsFlyoutClosed = false;
            IsFlyoutOpen = true;

           
            
            if (scores.Count == 0)
            {
                scores.Add(ScoringHandModel.NineteenHand(CurrentHand));
            }
            this.tcs = new TaskCompletionSource<Boolean>();
            

         
        }

        public void AbleToPlay()
        {

        }

        public void AbleToDraw()
        {

        }

        public void PlaceCard()
        {

            if (CurrentCard == null)
            {
                return;
            }

            int x = this.GameTable.TablePivotGrid.Children.IndexOf(this.GameTable.LastButton);

            Image image = new Image();
            image.Source = new BitmapImage(new Uri(CurrentCard.ImagePath, UriKind.RelativeOrAbsolute));
            int y = Grid.GetColumn((FrameworkElement)this.GameTable.TablePivotGrid.Children[x]);
            int x2 = Grid.GetRow((FrameworkElement)this.GameTable.TablePivotGrid.Children[x]);
            Grid.SetColumn(image, y);
            Grid.SetRow(image, x2);
            GameTable.Table[x2][y] = CurrentCard;
            image.RenderTransform = new TranslateTransform();
            this.GameTable.TablePivotGrid.Children[x] = image;
            CurrentCard = null;

            NextPlayerTurnCommand.Execute(null);
            
        
        }

        public void DrawFirstCard()
        {

            CardModel FirstCard = Deck.TakeCard();
            Image cardimage = new Image();
            //  Binding b = new Binding();
            //  b.Source = c.ImagePath;
            cardimage.Source = new BitmapImage(new Uri(FirstCard.ImagePath, UriKind.RelativeOrAbsolute));
            Grid.SetRow(cardimage, 2);
            Grid.SetColumn(cardimage, 2);
            GameTable.Table[2][2] = FirstCard;
            cardimage.RenderTransform = new TranslateTransform();
            this.GameTable.TablePivotGrid.Children[12] = cardimage;
            
        }

        public async void NextPlayerTurn()
        {
            

            if (GameTable.TablePivotGrid.Children.All(x => x.GetType() == typeof(Image)))
            {

                await ScoreGame();

                if (Players.Players.Any(x => x.Score >= 120))
                {
                    EndGame();
                }

                foreach (Player p in Players.Players)
                {
                    p.SwitchCrib();
                    p.EndTurn();

                }


                  ReadyNextRound();
            }
            else
            {
                Players.SwitchTurns();
                if (Players.CurrentPlayer.PlayerType == Player.PlayersType.ComputerPlayer)
                {
                   await ComputerPlayerPlay();
                }
            }
        }

        public void ReadyNextRound()
        {
            GameTable = new GameTableViewModel();
            PlaceButtonsCommand.Execute(null);
            Deck.Reset();
            Deck.Shuffle();
            DrawFirstCard();
        }

        public async Task ComputerPlayerPlay()
        {
            await Task.Delay(500);
            TakeCardCommand.Execute(null);
            await Task.Delay(500);
            List<Button> buttons = new List<Button>();
            foreach (var child in GameTable.TablePivotGrid.Children)
            {
                if (child.GetType() == typeof(Button))
                {
                    buttons.Add((Button)child);
                }
            }
            
            Random rand = new Random();
            int index = rand.Next(0, buttons.Count);

            GameTable.LastButton = buttons[index];
            PlaceCardCommand.Execute(null);
        }

        public void EndGame()
        {
             Players.Players.OrderByDescending(x => x.Score);

         //    Player winner = Players.Players[0];
         //    Player loser = Players.Players[1];
        }

        public void EmptySpotButtonClick(Button a)
        {
            GameTable.LastButton = a;
            PlaceCardCommand.Execute(null);
        }


        public void PlaceButtons()
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    Button b = new Button();
                    b.Background = new SolidColorBrush(Windows.UI.Colors.DarkRed);
                    b.Command = EmptySpotButtonClickCommand;
                    b.CommandParameter = b;
                    
                    b.VerticalAlignment = VerticalAlignment.Center;
                    b.HorizontalAlignment = HorizontalAlignment.Center;
                    b.HorizontalContentAlignment = HorizontalAlignment.Center;
                    b.VerticalContentAlignment = VerticalAlignment.Center;
                    b.MinHeight = 75;
                    b.MinWidth = 75;
                    
                    Grid.SetRow(b, x);
                    Grid.SetColumn(b, y);
                    this.GameTable.TablePivotGrid.Children.Add(b);
                    b.Tag = this.GameTable.TablePivotGrid.Children.IndexOf(b);

                }
            }

            
        }

        



        


        public async Task ScoreGame()
        {
            HorizontalHandsetModel Horizontalhands = new HorizontalHandsetModel(GameTable.Table);
            VerticalHandsetModel Verticalhands = new VerticalHandsetModel(GameTable.Table);
            DiagonalHandsetModel Diagonalhands = new DiagonalHandsetModel(GameTable.Table);
            
            
            
            Player currentPlayer = Players.Players.First(x => !x.IsCountUp);
            foreach (HandModel h in Horizontalhands.Hands)
            {

                await Score(currentPlayer, h, (Horizontalhands.Hands.IndexOf(h) + 1));
                //   MainPivotLayout.SelectedIndex = 1;
                Task.WaitAll();
                //    await Task.Delay(TimeSpan.FromSeconds(2));

            }

            currentPlayer = Players.Players.First(x => x.IsCountUp);
            foreach (HandModel h in Verticalhands.Hands)
            {

                await Score(currentPlayer, h, (Verticalhands.Hands.IndexOf(h) + 1));
                //       MainPivotLayout.SelectedIndex = 1;
                Task.WaitAll();
            }


            currentPlayer = Players.Players.First(x => x.HasCrib == true);
            foreach (HandModel h in Diagonalhands.Hands)
            {

                await Score(currentPlayer, h, (Diagonalhands.Hands.IndexOf(h) + 1));
                Task.WaitAll();
            }


        }


        public async Task Score(Player player, HandModel hands, int currentHandNumber)
        {
            
            this.CurrentHand = hands;
                        
            ScoringHands =  GameTable.CountHands(hands);
             int x = player.Score;
           // int sum = scoringhands.Sum(x => (int)x.Value);
          //  player.Score += sum;
            if (ScoringHands.Count > 0)
            {
                
                await AnimateScore(ScoringHands, player, currentHandNumber);
                x = player.Score;
              Clamp<int>(x, 0, 120);
                await AnimatePegMovement(player.PossiblePoints[player.LastPosition], player.PossiblePoints[x], player);

            }


            //Probly should do animation here while each hand is being counter, or in CountHands I could send it over to a new pivot to be counted.
           ScoreRecapCommand.Execute(ScoringHands);
           await tcs.Task;
           
            player.LastPosition = x;
            
            await Task.Delay(TimeSpan.FromSeconds(.5));
        }


        public async Task AnimateScore(ObservableCollection<ScoringHandModel> scores, Player player, int currentHandCounter)
        {
          

            var a = GameTable.TablePivotGrid.Children.ToList();

            
                CurrentHandStoryboard.Stop();
            

            for (int i = 0; i < 5;i++ )
            {
                Storyboard.SetTarget(CurrentHandStoryboard.Children[i], GameTable.TablePivotGrid.Children.First(x => ((x as Image).Source as BitmapImage).UriSource.ToString() == CurrentHand.Cards[i].ImagePath));
            }

            foreach (ScoringHandModel score in scores)
            {
                player.Score += (int)score.Value;
                for (int c = 0; c <= 4; c++)
                {

                    if (score.Cards.Contains(CurrentHand.Cards[c]))
                    {
                        CardScoreAnimationModel ani = CardAnimations[c];
                        ani.Animation.By = 30;
                    }
                    else
                    {
                        CardScoreAnimationModel ani = CardAnimations[c];
                        ani.Animation.By = 0;
                    }

                }

                await StoryboardExtensions.BeginAsync(CurrentHandStoryboard);
                    Task.WaitAll();

                // ScoreType.Text = Enum.GetName(typeof(ScoringHandModel.ScoringType), score.ScoreType) + currentHandCounter.ToString();
            }


        }


        public async Task AnimatePegMovement(Point position1, Point position2, Player currentPlayer)
        {

            Point b = position2;
            Point a = position1;
            if (currentPlayer.Number == 1)
            {
                GameBoardVM.Player1PegYAnimation.Animation.From = a.Y;
                GameBoardVM.Player1PegYAnimation.Animation.To = b.Y;
                GameBoardVM.Player1PegXAnimation.Animation.From = a.X;
                GameBoardVM.Player1PegXAnimation.Animation.To = b.X;
                await StoryboardExtensions.BeginAsync(GameBoardVM.PlayerOnePegMovementStoryboard);
                //   Task.WaitAll();
            }
            else
            {
                GameBoardVM.Player2PegXAnimation.Animation.From = a.X;
                GameBoardVM.Player2PegXAnimation.Animation.To = b.X;
                GameBoardVM.Player2PegYAnimation.Animation.From = a.Y;
                GameBoardVM.Player2PegYAnimation.Animation.To = b.Y;
                await StoryboardExtensions.BeginAsync(GameBoardVM.PlayerTwoPegMovementStoryboard);
                //      Task.WaitAll();
            }

        }


        public static void  Clamp<T>(T value, T max, T min)
         where T : System.IComparable<T>
        {
            if (value.CompareTo(max) > 0)
                value = max;
            if (value.CompareTo(min) < 0)
                value = min;
          //  return Value;
        }
        
        
        
    }
}
