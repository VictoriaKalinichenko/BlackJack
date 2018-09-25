using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace BlackJack.Entities.Entities
{
    public class GamePlayer : EntityBase
    {
        public long PlayerId { get; set; }
        public long GameId { get; set; }
        public int Score { get; set; }
        public int Bet { get; set; }
        public int RoundScore { get; set; }
        public int CardAmount { get; set; }
        public float BetPayCoefficient { get; set; }
        [Write(false)]
        public virtual Player Player { get; set; }
        [Write(false)]
        public virtual List<PlayerCard> PlayerCards { get; set; }
    }
}
