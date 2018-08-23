using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Entities.Models
{
    public class Game : EntityBase
    {
        public int Stage { get; set; }


        public virtual List<GamePlayer> GamePlayers { get; set; }
    }
}
