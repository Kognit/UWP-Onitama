using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Onitama
{
    class Square
    {
        Game Game;
        internal Point Position;
        internal bool isSelected;
        internal bool isHighlighted;
        internal int Throne = 2;

        internal Pawn Pawn;

        internal Rectangle Rect;
        internal Point RectCenter;

        internal Square(Game g, int i, int ii)
        {
            Game = g;
            Position = new Point(i, ii);
        }

        internal void RegisterRect(Rectangle rect, int width)
        {
            Rect = rect;
            Rect.PointerEntered += Rect_PointerEntered;
            Rect.PointerExited += Rect_PointerExited;
            Rect.PointerCaptureLost += Rect_PointerExited;
            Rect.Tapped += Rect_Tapped;
            RectCenter = new Point(rect.Margin.Left + width / 2, rect.Margin.Bottom + width / 2);
        }

        private void Rect_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

            //IF TAPPED SQUARE NOT A POSSIBLE MOVE
            if (!isHighlighted)
            {
                //RESET SQUARES
                Game.Board.ResetSquares();


                //HIGHLIGHT MOVES
                if (Pawn != null && Pawn.MovesPossible.Count > 0)
                {
                    isSelected = true;
                    Game.selectedPawn = Pawn;
                    Rect.Fill = new SolidColorBrush(Colors.Gray);
                    foreach (Square s in Pawn.MovesPossible)
                    {
                        s.Rect.Fill = new SolidColorBrush(Colors.LightGray);
                        s.isHighlighted = true;
                    }
                }
            }
            else //DO MOVE
            {
                

                //DETERMINE USED CARD
                int index = Game.selectedPawn.MovesPossible.IndexOf(this);
                int cardoptions = Game.selectedPawn.CardsPossible[index];
                Card c = null;
                Game.tappedSquare = this;
                if (cardoptions == 0 || cardoptions == 1)
                {
                    c = Game.Players[Game.ActivePlayer].Cards[cardoptions];
                    Game.DoMove(c);
                }
                else
                {
                    IList<Grid> grids = new List<Grid>() { Game.Board.P1Grid, Game.Board.P2Grid };
                    grids[Game.InactivePlayer].Opacity = 0.2;
                    grids[Game.InactivePlayer].IsHitTestVisible = false;
                    Game.Players[Game.ActivePlayer].NextCard.Visual.Opacity = 0.2;
                    Game.Players[Game.ActivePlayer].NextCard.Visual.IsHitTestVisible = false;
                    Game.Board.ChooseCardGrid.Visibility = Visibility.Visible;
                    //AWAIT USER INPUT FOR CARD CHOICE
                }

                

            }

        }

        private void Rect_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (isHighlighted)
            {
                Rect.Fill = new SolidColorBrush(Colors.DarkGray);
            }
            else if(isSelected)
            {
                Rect.Fill = new SolidColorBrush(Colors.Gray);
            }
            else
            {
                Rect.Fill = new SolidColorBrush(Colors.LightGray);
            }


            //CHECK IF ANY SQUARE SELECTED
            bool selectionExists = false;
            foreach (IList<Square> slist in Game.Board.Squares)
            {
                foreach (Square s in slist)
                {
                    if (s.isSelected)
                        selectionExists = true;
                }
            }


            if (Pawn != null && !selectionExists) {
                foreach(Square s in Pawn.MovesPossible)
                {
                    s.Rect.Fill = new SolidColorBrush(Colors.LightGray);
                }
            }
        }
        private void Rect_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {

            foreach (IList<Square> slist in Game.Board.Squares)
            {
                foreach (Square s in slist)
                {
                    if (!s.isHighlighted && !s.isSelected)
                    {
                        s.Rect.Fill = new SolidColorBrush(Colors.WhiteSmoke);
                    }
                    else if (s.isHighlighted)
                    {
                        s.Rect.Fill = new SolidColorBrush(Colors.LightGray);
                    }
                    else
                    {
                        s.Rect.Fill = new SolidColorBrush(Colors.DarkGray);
                    }
                }
            }
        }

    }
}
