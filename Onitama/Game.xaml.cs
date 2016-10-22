using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Onitama
{
    public sealed partial class Game : Page
    {
        internal ScrollViewer GameViewer;

        internal Board Board;
        internal IList<Player> Players;
        internal IList<Card> Cards;
        internal IList<Card> CardsInPlay;

        internal Pawn selectedPawn;

        internal int ActivePlayer = 1;
        internal int InactivePlayer = 0;
        internal int Turn = -1;

        internal Square tappedSquare;
        

        public Game()
        {
            this.InitializeComponent();
            
            //UI
            GameViewer = new ScrollViewer();
            //GameViewer.Background = new SolidColorBrush(Colors.LightSeaGreen);
            GameViewer.HorizontalAlignment = HorizontalAlignment.Stretch;
            GameViewer.VerticalAlignment = VerticalAlignment.Stretch;
            GameViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            GameViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            GameViewer.VerticalScrollMode = ScrollMode.Disabled;
            GameViewer.HorizontalScrollMode = ScrollMode.Disabled;
            gamegrid.Children.Add(GameViewer);
            

            //LOGIC
            Players = new List<Player>();
            Players.Add(new Player(0));
            Players.Add(new Player(1));


            //CARDS
            Cards = Cardset.GetCards();
            CardsInPlay = new List<Card>();
            


            //DETERMINE 5 CARDS TO PLAY WITH
            Random r = new Random();
            for (int i = 0; i < 5; i++)
            {
                while (CardsInPlay.Count <= i)
                {
                    int num = r.Next(Cards.Count);
                    if (!CardsInPlay.Contains(Cards[num]))
                    {
                        Cards[num].Game = this;
                        CardsInPlay.Add(Cards[num]);
                        if (i >= 3)
                        {
                            Cards[num].PlayerHolding = 1;
                            Players[1].Cards.Add(Cards[num]);
                        }
                        else if(i < 2)
                        {
                            Players[0].Cards.Add(Cards[num]);
                        }
                        else if(i == 2)
                        {
                            Players[0].NextCard = Cards[num];
                        }
                    }
                }
            }


            //GENERATE BOARD
            Board = new Board(this);
            Board.Generate();
            Board.GenerateCardVisuals();


            //START FIRST TURN
            NextTurn();

        }

        internal void NextTurn()
        {
            Turn++;
            if (ActivePlayer == 0)
            {
                ActivePlayer = 1;
                InactivePlayer = 0;
            }
            else if (ActivePlayer == 1)
            {
                ActivePlayer = 0;
                InactivePlayer = 1;
            }

            //COMPUTE POSSIBLE MOVES
            foreach (Player player in Players)
            {
                foreach(Pawn pawn in player.Pawns)
                {
                    pawn.ComputePossibleMoves();
                }
            }
            

            //SET CARD VISUALS
            Board.GenerateCardVisuals();

        }

        internal void DoMove(Card c)
        {
            Square s = tappedSquare;

            //REMOVE EXISTING PAWN
            if (s.Pawn != null)
            {
                s.Pawn.Visual.Visibility = Visibility.Collapsed;
                Players[s.Pawn.Player.Number].Pawns.Remove(s.Pawn);
            }

            //SET SQUARE DATA
            selectedPawn.Square.Pawn = null;
            s.Pawn = selectedPawn;

            //HANDLE CARD BEHAVIOUR
            c.PlayerHolding = InactivePlayer;
            Players[InactivePlayer].NextCard = c;

            Players[ActivePlayer].Cards.Remove(c);
            Players[ActivePlayer].Cards.Add(Players[ActivePlayer].NextCard);
            Players[ActivePlayer].NextCard = null;

            //SET PAWN DATA
            selectedPawn.Square = s;
            selectedPawn.Visual.Margin = new Thickness(s.RectCenter.X - selectedPawn.Radius, 0, 0, s.RectCenter.Y - selectedPawn.Radius);
            selectedPawn.Position = s.Position;
            selectedPawn.CardsPossible.Clear();

            //SET BOARD DATA
            selectedPawn = null;

            //RESET SQUARES
            Board.ResetSquares();

            //CHECK FOR WIN
            if (s.Throne == InactivePlayer && s.Pawn.Player == Players[ActivePlayer] || Players[InactivePlayer].Pawns.Count == 0)
            {
                DoWin(Players[ActivePlayer]);
                return;
            }
            //INITIATE NEXT TURN
            NextTurn();
        }

        internal void DoWin(Player p)
        {
            wingrid.Visibility = Visibility.Visible;
            if (p.Number == 0)
            {
                wintext.Text = "Blue wins!";
                wintext.Foreground = new SolidColorBrush(Colors.SteelBlue);
            }
            if (p.Number == 1)
            {
                wintext.Text = "Red wins!";
                wintext.Foreground = new SolidColorBrush(Colors.Crimson);
            }

        }


        private void gamegrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //FIT BOARD TO WINDOW
            double r1 = e.NewSize.Height / (Board.BoardGrid.ActualHeight+ Board.P1Grid.ActualHeight+ Board.P2Grid.ActualHeight + 40);
            double r2 = e.NewSize.Width / Board.BoardGrid.ActualWidth;
            if (r1 < r2)
            {
                GameViewer.ChangeView(0, 0, (float)r1); 
            }
            else if (r2 < r1)
            {
                GameViewer.ChangeView(0, 0, (float)r2); 
            }
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            MainPage.MainFrame.Navigate(typeof(Game));
        }
    }
}
