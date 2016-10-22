using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;

namespace Onitama
{
    class Cardset
    {


        public static IList<Card> GetCards()
        {
            IList<Card> Cards = new List<Card>();
            Card c;

            c = new Card("Boar");
            c.Options.Add(new List<Point>() { new Point(0, 1), new Point(0, -1) }); //LIST OF POSSIBLE MOVEMENT VECTORS
            c.OptionTypes.Add(0); //TYPE OF LAST ADDED LIST OF MOVEMENT VECTORS (0 = OWN PAWNS AND ONMYO, 1 = OWN PAWNS, 2 = OWN ONMYO, 3 = OPPONENT PAWNS AND ONMYO, 4 = OPPONENT PAWNS, 5 = OPPONENT ONMYO)
            c.OptionOrigins.Add(new Point(2, 2)); //COORDINATES OF THE PAWN INDICATOR ON THE CARD (SHOULD BE 2,2 FOR ALL CARDS DISPLAYING ONLY ONE LIST OF MOVEMENT VECTORS)
            Cards.Add(c);

            c = new Card("Sheep");
            c.Options.Add(new List<Point>() { new Point(-1, 1), new Point(1, 1), new Point(-1, -1), new Point(1, -1) });
            c.OptionTypes.Add(0);
            c.OptionOrigins.Add(new Point(2, 2));
            Cards.Add(c);

            c = new Card("Frog");
            c.Options.Add(new List<Point>() { new Point(0, 1) });
            c.OptionTypes.Add(1);
            c.OptionOrigins.Add(new Point(2, 0));
            c.Options.Add(new List<Point>() { new Point(-2, 1), new Point(2, 1) });
            c.OptionTypes.Add(2);
            c.OptionOrigins.Add(new Point(2, 3));
            Cards.Add(c);

            c = new Card("Ostrich");
            c.Options.Add(new List<Point>() { new Point(-2, 2), new Point(2, 2) });
            c.OptionTypes.Add(1);
            c.OptionOrigins.Add(new Point(2, 0));
            c.Options.Add(new List<Point>() { new Point(0, 1) });
            c.OptionTypes.Add(2);
            c.OptionOrigins.Add(new Point(2, 3));
            Cards.Add(c);

            c = new Card("Rabbit");
            c.Options.Add(new List<Point>() { new Point(-1, 1), new Point(1, 1), new Point(0, -1) });
            c.OptionTypes.Add(0);
            c.OptionOrigins.Add(new Point(2, 2));
            Cards.Add(c);

            c = new Card("Mouse");
            c.Options.Add(new List<Point>() { new Point(0, 1), new Point(-2, 0), new Point(2, 0) });
            c.OptionTypes.Add(0);
            c.OptionOrigins.Add(new Point(2, 2));
            Cards.Add(c);

            c = new Card("Snake");
            c.Options.Add(new List<Point>() { new Point(0, 1), new Point(-1, -1), new Point(1, -1) });
            c.OptionTypes.Add(0);
            c.OptionOrigins.Add(new Point(2, 2));
            Cards.Add(c);

            c = new Card("Ox");
            c.Options.Add(new List<Point>() { new Point(0, 1), new Point(-1, 0), new Point(1, 0) });
            c.OptionTypes.Add(0);
            c.OptionOrigins.Add(new Point(2, 2));
            Cards.Add(c);

            c = new Card("Horse");
            c.Options.Add(new List<Point>() { new Point(0, 1), new Point(-1, 1), new Point(1, 1) });
            c.OptionTypes.Add(0);
            c.OptionOrigins.Add(new Point(2, 2));
            Cards.Add(c);

            c = new Card("Dragon");
            c.Options.Add(new List<Point>() { new Point(-1, 1), new Point(1, 1), new Point(-1, 0), new Point(1, 0) });
            c.OptionTypes.Add(0);
            c.OptionOrigins.Add(new Point(2, 2));
            Cards.Add(c);

            c = new Card("Rooster");
            c.Options.Add(new List<Point>() { new Point(-1, 0), new Point(1, 0), new Point(-1, -1), new Point(1, 1) });
            c.OptionTypes.Add(0);
            c.OptionOrigins.Add(new Point(2, 2));
            Cards.Add(c);

            c = new Card("Tiger");
            c.Options.Add(new List<Point>() { new Point(0, 2) });
            c.OptionTypes.Add(0);
            c.OptionOrigins.Add(new Point(2, 2));
            Cards.Add(c);

            c = new Card("Monkey");
            c.Options.Add(new List<Point>() { new Point(1, 1), new Point(1, 0), new Point(1, -1) });
            c.OptionTypes.Add(0);
            c.OptionOrigins.Add(new Point(2, 2));
            Cards.Add(c);

            c = new Card("Turtle");
            c.Options.Add(new List<Point>() { new Point(0, 1) });
            c.OptionTypes.Add(0);
            c.OptionOrigins.Add(new Point(2, 0));
            c.Options.Add(new List<Point>() { new Point(0, -1) });
            c.OptionTypes.Add(5);
            c.OptionOrigins.Add(new Point(2, 4));
            Cards.Add(c);

            c = new Card("Dog");
            c.Options.Add(new List<Point>() { new Point(-1, 1), new Point(-1, 0), new Point(-1, -1) });
            c.OptionTypes.Add(0);
            c.OptionOrigins.Add(new Point(2, 2));
            Cards.Add(c);


            //ADDITIONAL SELFMADE CARD DESIGNS
            
            c = new Card("Alligator");
            c.Options.Add(new List<Point>() { new Point(0, 2) });
            c.OptionTypes.Add(1);
            c.OptionOrigins.Add(new Point(2, 0));
            c.Options.Add(new List<Point>() { new Point(-2, 1), new Point(2, 1) });
            c.OptionTypes.Add(3);
            c.OptionOrigins.Add(new Point(2, 3));
            Cards.Add(c);

            c = new Card("Crab");
            c.Options.Add(new List<Point>() { new Point(1, 0), new Point(-1, 0), new Point(2, 0), new Point(-2, 0) });
            c.OptionTypes.Add(2);
            c.OptionOrigins.Add(new Point(2, 1));
            c.Options.Add(new List<Point>() { new Point(-1, 0), new Point(1, 0) });
            c.OptionTypes.Add(4);
            c.OptionOrigins.Add(new Point(2, 3));
            Cards.Add(c);

            c = new Card("Squid");
            c.Options.Add(new List<Point>() { new Point(1, 1), new Point(0, 1), new Point(-1, 1), new Point(0, -1) });
            c.OptionTypes.Add(2);
            c.OptionOrigins.Add(new Point(2, 1));
            c.Options.Add(new List<Point>() { new Point(1, 1), new Point(-1, 1) });
            c.OptionTypes.Add(1);
            c.OptionOrigins.Add(new Point(2, 3));
            Cards.Add(c);

            c = new Card("Giraffe");
            c.Options.Add(new List<Point>() { new Point(-1, 2), new Point(-1, 0) });
            c.OptionTypes.Add(2);
            c.OptionOrigins.Add(new Point(2, 0));
            c.Options.Add(new List<Point>() { new Point(-1, 1), new Point(-1, 0) });
            c.OptionTypes.Add(1);
            c.OptionOrigins.Add(new Point(2, 3));
            Cards.Add(c);

            c = new Card("Prawn");
            c.Options.Add(new List<Point>() { new Point(1, 2), new Point(1, 0) });
            c.OptionTypes.Add(2);
            c.OptionOrigins.Add(new Point(2, 0));
            c.Options.Add(new List<Point>() { new Point(1, 1), new Point(1, 0) });
            c.OptionTypes.Add(1);
            c.OptionOrigins.Add(new Point(2, 3));
            Cards.Add(c);
            

            return Cards;
        }
    }
}
