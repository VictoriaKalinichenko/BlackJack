using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Entity
{
    public class Game
    {
        public int Id { get; set; }

        public int Stage { get; set; }

        public virtual Deck Deck { get; set; }

        public virtual List<Player> Players { get; set; }
    }
}
