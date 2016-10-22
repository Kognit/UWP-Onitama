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
    class OnmyoPawn : Pawn
    {
        public OnmyoPawn(Game g, int x, int y, int player) : base(g, x, y, player)
        {
            Radius = 35;
        }
    }

    class Pawn
    {
        Game Game;
        internal Square Square;
        internal Point Position;
        internal Grid Visual;
        internal Player Player;

        internal IList<Square> MovesPossible;
        internal IList<int> CardsPossible;


        internal int Radius = 25;

        public Pawn(Game g, int x, int y, int player)
        {
            Game = g;
            MovesPossible = new List<Square>();
            CardsPossible = new List<int>();
            Position = new Point(x, y);
            if(player == 0)
                Player = Game.Players[0];
            if (player == 1)
                Player = Game.Players[1];
            Square = Game.Board.Squares[(int)Position.X][(int)Position.Y];
        }

        internal void ComputePossibleMoves()
        {
            MovesPossible.Clear();
            CardsPossible.Clear();
            

            foreach (Card c in Game.Players[Game.ActivePlayer].Cards)
            {
                for (int i = 0; i < c.Options.Count; i++)
                {
                    if (c.OptionTypes[i] == 0 && Player.Number == Game.ActivePlayer ||
                        c.OptionTypes[i] == 1 && Player.Number == Game.ActivePlayer && GetType() != typeof(OnmyoPawn) ||
                        c.OptionTypes[i] == 2 && Player.Number == Game.ActivePlayer && GetType() == typeof(OnmyoPawn) ||
                        c.OptionTypes[i] == 3 && Player.Number == Game.InactivePlayer ||
                        c.OptionTypes[i] == 4 && Player.Number == Game.InactivePlayer && GetType() != typeof(OnmyoPawn) ||
                        c.OptionTypes[i] == 5 && Player.Number == Game.InactivePlayer && GetType() == typeof(OnmyoPawn))
                    {
                        foreach (Point point in c.Options[i])
                        {
                            Point p = point;
                            if (Game.ActivePlayer == 1)
                            {
                                p = new Point(-p.X, -p.Y);
                            }
                            if ((int)Position.X + (int)p.X >= 0 && (int)Position.X + (int)p.X < 5 && (int)Position.Y + (int)p.Y >= 0 && (int)Position.Y + (int)p.Y < 5)
                            {
                                Square s = Game.Board.Squares[(int)Position.X + (int)p.X][(int)Position.Y + (int)p.Y];
                                if (s.Pawn == null ||
                                    s.Pawn.Player.Number != Game.ActivePlayer && Player.Number == Game.ActivePlayer ||
                                    s.Pawn.Player.Number == Game.ActivePlayer && Player.Number != Game.ActivePlayer)
                                {
                                    if (!MovesPossible.Contains(s))
                                    {
                                        MovesPossible.Add(s);
                                        CardsPossible.Add(Game.Players[Game.ActivePlayer].Cards.IndexOf(c));
                                    }
                                    else
                                    {
                                        int index = Game.Players[Game.ActivePlayer].Cards.IndexOf(c);
                                        int existingindex = MovesPossible.IndexOf(s);
                                        CardsPossible[existingindex] = 2;
                                    }
                                }
                            }
                        }
                    }
                }
            }


        }

        internal Grid GenerateVisual()
        {
            Visual = new Grid();
            if (Position.X != 2)
            {
                Polygon Vp = GenerateHexagon(Radius, Player.Number);
                Vp.Margin = new Thickness(Radius, 0, 0, 0);
                Vp.StrokeThickness = 4;
                Visual.Children.Add(Vp);
            }
            else
            {
                Ellipse Ve = GenerateCircle(Radius, Player.Number);
                //Ve.Margin = new Thickness(Square.RectCenter.X - Radius, 0, 0, Square.RectCenter.Y - Radius);
                Ve.StrokeThickness = 4;
                Visual.Children.Add(Ve);
            }
            Visual.Margin = new Thickness(Square.RectCenter.X - Radius, 0, 0, Square.RectCenter.Y - Radius);
            return Visual;
        }

        public static Polygon GenerateHexagon(int Radius, int Player)
        {
            Polygon Vp = new Polygon();
            Vp.Points.Add(new Point(Radius / 2, -Radius * 0.87));
            Vp.Points.Add(new Point(-Radius / 2, -Radius * 0.87));
            Vp.Points.Add(new Point(-Radius, 0));
            Vp.Points.Add(new Point(-Radius / 2, Radius * 0.87));
            Vp.Points.Add(new Point(Radius / 2, Radius * 0.87));
            Vp.Points.Add(new Point(Radius, 0));
            Vp.HorizontalAlignment = HorizontalAlignment.Left;
            Vp.VerticalAlignment = VerticalAlignment.Bottom;
            Vp.IsHitTestVisible = false;
            if (Player == 0)
            {
                Vp.Fill = new SolidColorBrush(Colors.SteelBlue);
            }
            else
            {
                Vp.Fill = new SolidColorBrush(Colors.Crimson);
            }
            Vp.Stroke = new SolidColorBrush(Colors.WhiteSmoke);
            Vp.StrokeThickness = 0;
            return Vp;
        }

        public static Ellipse GenerateCircle(int Radius, int Player)
        {
            Ellipse Ve = new Ellipse();
            Ve.Width = Radius * 2;
            Ve.Height = Radius * 2;
            Ve.HorizontalAlignment = HorizontalAlignment.Left;
            Ve.VerticalAlignment = VerticalAlignment.Bottom;
            Ve.IsHitTestVisible = false;
            if (Player == 0)
            {
                Ve.Fill = new SolidColorBrush(Colors.SteelBlue);
            }
            else
            {
                Ve.Fill = new SolidColorBrush(Colors.Crimson);
            }
            Ve.Stroke = new SolidColorBrush(Colors.WhiteSmoke);
            Ve.StrokeThickness = 0;
            return Ve;
        }
    }
}
