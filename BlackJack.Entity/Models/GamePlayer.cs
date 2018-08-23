using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Entities.Models
{
    public class GamePlayer : EntityBase
    {
        public int PlayerId { get; set; }

        public int GameId { get; set; }
        
        public int Score { get; set; }
        
        public int Bet { get; set; }

        public int RoundScore { get; set; }

        public float BetPayCoefficient { get; set; }
        

        public virtual Player Player { get; set; }

        public virtual Game Game { get; set; }

        public virtual List<PlayerCard> PlayerCards { get; set; }
    }
}
