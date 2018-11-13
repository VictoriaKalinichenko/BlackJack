using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace BlackJack.Entities.Entities
{
    public class GamePlayer : Base
    {
        public int CardScore { get; set; }
        public long PlayerId { get; set; }
        [Write(false)]
        public virtual Player Player { get; set; }
        public long GameId { get; set; }
        [Write(false)]
        public virtual Game Game { get; set; }
        [Write(false)]
        public virtual List<PlayerCard> PlayerCards { get; set; }
    }
}
