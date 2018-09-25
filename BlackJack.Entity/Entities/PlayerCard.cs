using Dapper.Contrib.Extensions;

namespace BlackJack.Entities.Entities
{
    public class PlayerCard : EntityBase
    {
        public long GamePlayerId { get; set; }
        public long CardId { get; set; }
        [Write(false)]
        public virtual Card Card { get; set; }
    }
}
