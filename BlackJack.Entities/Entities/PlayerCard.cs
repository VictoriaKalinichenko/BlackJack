using Dapper.Contrib.Extensions;

namespace BlackJack.Entities.Entities
{
    public class PlayerCard : Base
    {
        public long CardId { get; set; }
        [Write(false)]
        public virtual Card Card { get; set; }
        public long GamePlayerId { get; set; }
        [Write(false)]
        public virtual GamePlayer GamePlayer { get; set; }
    }
}
