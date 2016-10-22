using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    class Player
    {
        internal int Number;
        internal IList<Pawn> Pawns;
        internal IList<Card> Cards;
        internal Card NextCard;

        public Player(int num)
        {
            Number = num;
            Pawns = new List<Pawn>();
            Cards = new List<Card>();
        }
    }
}
