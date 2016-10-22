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
    class Card
    {
        internal Game Game;

        internal IList<IList<Point>> Options;
        internal IList<Point> OptionOrigins;
        internal IList<int> OptionTypes;
        Color c1;
        Color c2;
        internal string Name;

        internal int PlayerHolding = 0;

        internal Grid Visual;

        int extramargin = 0;

        public Card(string name)
        {
            Options = new List<IList<Point>>();
            OptionOrigins = new List<Point>();
            OptionTypes = new List<int>();
            Name = name;
        }

        internal Grid GenerateVisual(double Scale = 2.0)
        {
            int PlayerOpposing = 1;
            switch (PlayerHolding)
            {
                case 0:
                    PlayerOpposing = 1;
                    c1 = Colors.SteelBlue;
                    c2 = Colors.Crimson;
                    break;
                case 1:
                    PlayerOpposing = 0;
                    c1 = Colors.Crimson;
                    c2 = Colors.SteelBlue;
                    break;
            }

            Visual = new Grid();
            Visual.VerticalAlignment = VerticalAlignment.Top;
            Visual.HorizontalAlignment = HorizontalAlignment.Left;
            Visual.PointerEntered += Visual_PointerEntered;
            Visual.PointerExited += Visual_PointerExited;
            Visual.PointerCaptureLost += Visual_PointerExited;
            Visual.Tapped += Visual_Tapped;
            Visual.Background = new SolidColorBrush(Colors.White);
            Visual.BorderBrush = new SolidColorBrush(Colors.Black);
            Visual.Padding = new Thickness(0, 0, 0, 6);


            if (Options.Count > 1)
                extramargin = 4;

            Rectangle line = new Rectangle();
            line.HorizontalAlignment = HorizontalAlignment.Center;
            line.VerticalAlignment = VerticalAlignment.Bottom;
            line.Fill = new SolidColorBrush(Colors.Black);
            line.Margin = new Thickness(0, 0, 0, Scale * (60 + extramargin));
            line.Width = Scale * 68;
            line.Height = 2;
            Visual.Children.Add(line);

            TextBlock name = new TextBlock();
            name.HorizontalAlignment = HorizontalAlignment.Center;
            name.VerticalAlignment = VerticalAlignment.Bottom;
            name.Margin = new Thickness(0, 0, 0, Scale * (60+2+extramargin));
            name.Text = Name;
            name.FontSize = Scale * 10.0;
            Visual.Children.Add(name);


            //0 ALL OWN 1 PAWN OWN 2 ONMYO OWN 3 ALL OPP 4 PAWN OPP 5 ONMYO OPP
            
            if(Options.Count > 1)
            {
                Rectangle linemiddle = new Rectangle();
                linemiddle.HorizontalAlignment = HorizontalAlignment.Center;
                linemiddle.VerticalAlignment = VerticalAlignment.Bottom;
                linemiddle.Fill = new SolidColorBrush(Colors.Black);
                linemiddle.Margin = new Thickness(0, 0, 0, Scale * 34.5);
                linemiddle.Width = Scale * 58;
                linemiddle.Height = 2;
                //Visual.Children.Add(linemiddle);
            }
            
            for (int i = 0; i < 5; i++)
            {
                for (int ii = 0; ii < 5; ii++)
                {


                    Rectangle rect = new Rectangle();
                    rect.Width = Scale * 10;
                    rect.Height = Scale * 10;
                    rect.HorizontalAlignment = HorizontalAlignment.Left;
                    rect.VerticalAlignment = VerticalAlignment.Bottom;
                    rect.Fill = new SolidColorBrush(Colors.WhiteSmoke);
                    rect.Margin = new Thickness(Scale*(5 + i * 12),0, 0, Scale*(ii * 12));
                    Visual.Children.Add(rect);

                    if(ii >= 3)
                    {
                        rect.Margin = new Thickness(Scale * (5 + i * 12), 0, 0, Scale * (ii * 12 + extramargin));
                    }
                    
                    

                    for(int op = 0; op < Options.Count; op++)
                    {
                        if (i == OptionOrigins[op].X && ii == OptionOrigins[op].Y)
                        {
                            switch (OptionTypes[op])
                            {
                                case 0:
                                    rect.Fill = new SolidColorBrush(c1);
                                    break;
                                case 1:
                                    Polygon p1 = Pawn.GenerateHexagon((int)(Scale * 5), PlayerHolding);
                                    p1.Margin = new Thickness(rect.Margin.Left + Scale * 5,0,0,rect.Margin.Bottom + Scale * 0.87);
                                    Visual.Children.Add(p1);
                                    break;
                                case 2:
                                    Ellipse e1 = Pawn.GenerateCircle((int)(Scale * 5), PlayerHolding);
                                    e1.Margin = rect.Margin;
                                    Visual.Children.Add(e1);
                                    break;
                                case 3:
                                    rect.Fill = new SolidColorBrush(c2);
                                    break;
                                case 4:
                                    Polygon p2 = Pawn.GenerateHexagon((int)(Scale * 5), PlayerOpposing);
                                    p2.Margin = new Thickness(rect.Margin.Left + Scale * 5, 0, 0, rect.Margin.Bottom + Scale * 0.87);
                                    Visual.Children.Add(p2);
                                    break;
                                case 5:
                                    Ellipse e2 = Pawn.GenerateCircle((int)(Scale * 5), PlayerOpposing);
                                    e2.Margin = rect.Margin;
                                    Visual.Children.Add(e2);
                                    break;
                            }
                        }
                        foreach (Point option in Options[op])
                        {
                            if (i == OptionOrigins[op].X + (int)option.X && ii == OptionOrigins[op].Y + (int)option.Y)
                                rect.Fill = new SolidColorBrush(Colors.Gray);
                        }
                    }
                    
                }
            }

            return Visual;
        }

        private void Visual_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if(Game.Board.ChooseCardGrid.Visibility == Visibility.Visible)
            {
                Game.Board.ChooseCardGrid.Visibility = Visibility.Collapsed;
                IList<Grid> grids = new List<Grid>() { Game.Board.P1Grid, Game.Board.P2Grid };
                grids[Game.InactivePlayer].Opacity = 1;
                grids[Game.InactivePlayer].IsHitTestVisible = true;
                Game.Players[Game.ActivePlayer].NextCard.Visual.Opacity = 1;
                Game.Players[Game.ActivePlayer].NextCard.Visual.IsHitTestVisible = true;
                //DO MOVE
                Game.DoMove(this);
            }
        }

        private void Visual_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Visual.BorderThickness = new Thickness(0, 0, 0, 2);
            //Visual.Background = new SolidColorBrush(Color.FromArgb(50, c1.R, c1.G, c1.B));
        }

        private void Visual_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            //Visual.Background = new SolidColorBrush(Colors.Transparent);
            Visual.BorderThickness = new Thickness(0, 0, 0, 0);
        }
    }
}
