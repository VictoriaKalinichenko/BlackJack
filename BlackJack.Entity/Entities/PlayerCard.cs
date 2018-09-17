using Dapper.Contrib.Extensions;

namespace BlackJack.Entities.Entities
{
    public class PlayerCard : EntityBase
    {
        public int GamePlayerId { get; set; }
        public int CardId { get; set; }
        [Write(false)]
        public Card Card { get; set; }
    }
}
