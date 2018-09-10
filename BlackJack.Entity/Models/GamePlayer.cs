using Dapper.Contrib.Extensions;

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
        [Write(false)]
        public Player Player { get; set; }
    }
}
