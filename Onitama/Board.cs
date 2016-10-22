using System;
using System.Collections.Generic;
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
    class Board
    {
        Game Game;
        internal IList<IList<Square>> Squares;


        internal StackPanel GamePanel;
        internal Grid BoardGrid;
        internal Grid P1Grid;
        internal Grid P2Grid;
        internal Grid ChooseCardGrid;
        //internal Grid GameGrid;


        public Board(Game g)
        {
            Game = g;
        }


        internal void Generate()
        {

            GamePanel = new StackPanel();
            GamePanel.HorizontalAlignment = HorizontalAlignment.Center;
            GamePanel.VerticalAlignment = VerticalAlignment.Stretch;
            GamePanel.Padding = new Thickness(0, 20, 0, 20);

            Game.GameViewer.Content = GamePanel;

            BoardGrid = new Grid();
            BoardGrid.HorizontalAlignment = HorizontalAlignment.Center;
            P1Grid = new Grid();
            P1Grid.HorizontalAlignment = HorizontalAlignment.Center;
            Canvas.SetZIndex(P1Grid, 2);
            P1Grid.Height = 190;
            P2Grid = new Grid();
            P2Grid.HorizontalAlignment = HorizontalAlignment.Center;
            Canvas.SetZIndex(P2Grid, 2);
            P2Grid.Height = 190;
            RotateTransform tr = new RotateTransform();
            tr.Angle = 180;
            P2Grid.RenderTransformOrigin = new Point(0.5, 0.5);
            P2Grid.RenderTransform = tr;

            GamePanel.Children.Add(P2Grid);
            GamePanel.Children.Add(BoardGrid);
            GamePanel.Children.Add(P1Grid);

            //CHOOSECARDGRID
            ChooseCardGrid = new Grid();
            ChooseCardGrid.Background = new SolidColorBrush(Color.FromArgb(150, 255, 255, 255));
            ChooseCardGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            ChooseCardGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            ChooseCardGrid.Visibility = Visibility.Collapsed;
            ChooseCardGrid.Margin = new Thickness(0, -1000, 0, -1000);
            Canvas.SetZIndex(ChooseCardGrid, 98);
            BoardGrid.Children.Add(ChooseCardGrid);

            //GENERATE SQUARES
            Squares = new List<IList<Square>>();
            for(int i = 0; i < 5; i++)
            {
                Squares.Add(new List<Square>());
                for(int ii = 0; ii < 5; ii++)
                {
                    Square toadd = new Square(Game,i,ii);
                    Squares[i].Add(toadd);

                    Rectangle rect = new Rectangle();
                    rect.Width = 100;
                    rect.Height = 100;
                    rect.HorizontalAlignment = HorizontalAlignment.Left;
                    rect.VerticalAlignment = VerticalAlignment.Bottom;
                    rect.Fill = new SolidColorBrush(Colors.WhiteSmoke);
                    rect.Stroke = new SolidColorBrush(Colors.White);
                    rect.StrokeThickness = 1;
                    rect.Margin = new Thickness(i * 100, 0, 0, ii * 100);

                    if (i == 2 && ii == 0 || i == 2 && ii == 4)
                    {
                        toadd.Throne = ii;
                        if (ii == 4)
                            toadd.Throne = 1;
                        rect.StrokeThickness = 8; 
                    }

                    toadd.RegisterRect(rect, 100);

                    BoardGrid.Children.Add(rect);
                }
            }

            //GENERATE PAWNS
            for (int ii = 0; ii < 2; ii++)
            {
                int currow;
                if(ii == 0)
                {
                    currow = 0;
                }
                else
                {
                    currow = 4;
                }

                for (int i = 0; i < 5; i++)
                {
                    Pawn p;
                    if (i == 2)
                    {
                        p = new OnmyoPawn(Game, i, currow, ii);
                    }
                    else
                    {
                        p = new Pawn(Game, i, currow, ii);
                    }
                    UIElement e = p.GenerateVisual();
                    BoardGrid.Children.Add(e);
                    Squares[i][currow].Pawn = p;
                    Game.Players[ii].Pawns.Add(p);
                }
            }

            

        }

        internal void GenerateCardVisuals()
        {


            //CLEAR GRIDS
            P1Grid.Children.Clear();
            P2Grid.Children.Clear();


            //CREATE VISUALS
            IList<Grid> grids = new List<Grid>() { P1Grid, P2Grid };
            for(int i = 0; i < 2; i++)
            {
                int extramargin = 0;
                if (Game.Players[i].NextCard != null)
                    extramargin = 82;
                for (int ii = 0; ii < 2; ii++)
                {
                    Grid toadd = Game.Players[i].Cards[ii].GenerateVisual();
                    toadd.Margin = new Thickness(2.0 * (75 * ii) + extramargin, 20, 0, 0);
                    grids[i].Children.Add(toadd);
                }

                if (Game.Players[i].NextCard != null)
                {
                    Grid toadd = Game.Players[i].NextCard.GenerateVisual(1.0);
                    toadd.Margin = new Thickness(2.0 * (75 * 2) + extramargin, 20, 0, 0);
                    grids[i].Children.Add(toadd);
                }
            }
            
        }

        internal void ResetSquares()
        {
            foreach (IList<Square> slist in Squares)
            {
                foreach (Square s in slist)
                {
                    s.isSelected = false;
                    s.isHighlighted = false;
                    s.Rect.Fill = new SolidColorBrush(Colors.WhiteSmoke);
                }
            }

        }
    }
}
