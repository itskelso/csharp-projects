using System;
using System.Collections.Generic;
using System.Text;
using TableCrib.Models;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TableCrib.ViewModels
{
    public class PlayersViewModel : ViewModelBase
    {
        
        private Player player1;
        private Player player2;
        private ObservableCollection<Player> players;

        public ObservableCollection<Player> Players
        {
            get { return players; }
            set { Set(() => Players, ref players, value); }
        }

        public Player Player1
        {
            get 
            { 
                return player1;
            }
            set
            {
                Set(() => Player1, ref player1, value);
            }

        }

        public Player Player2
        {
            get
            {
                return player2;
            }
            set
            {
                Set(() => Player2, ref player2, value);
            }

        }
        public Player CurrentPlayer
        {
            get 
            { 
                return  Players.FirstOrDefault(x => x.IsTurn);
            }
            set
            {
                Player t = Players.FirstOrDefault(x => x.IsTurn);
                Set(() => CurrentPlayer, ref t, value);
            }
        }
        

        public enum PlayerCombo
        {
            OnePlayer = 0,
            TwoPlayers = 1,
            TwoPlayersRemote =2,

        }

        public void GetPlayers(int combo)
        {

            ObservableCollection<Player> a = new ObservableCollection<Player>();
            switch (combo)
            {
                case 0:
                    a.Add(new Player(Player.PlayersType.HumanPlayer, true));
                    a.Add(new Player(Player.PlayersType.ComputerPlayer, true));

                    
                    break;
                case 1:
                        a.Add(new Player(Player.PlayersType.HumanPlayer, true));
                        a.Add(new Player(Player.PlayersType.HumanPlayer, true));
                    break;
                case 2:
                    //Need to implement Multiplayer here;
                    break;
                 default :
                    a.Add(new Player(Player.PlayersType.HumanPlayer, true));
                    a.Add(new Player(Player.PlayersType.ComputerPlayer, true));
                    break;

            }

            Players = a;


        }

        public void SwitchTurns()
        {
            foreach (Player p in Players)
            {
                p.IsTurn = !p.IsTurn;
            }
            
            
        }


    }
}
