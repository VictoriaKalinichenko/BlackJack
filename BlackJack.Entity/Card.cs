using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Entity
{
    public class Card
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public string Name { get; set; }
        


        public virtual List<Deck> Decks { get; set; }

        public virtual List<Player> Players { get; set; }
    }
}
