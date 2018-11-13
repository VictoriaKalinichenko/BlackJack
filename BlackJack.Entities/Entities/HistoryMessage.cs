using Dapper.Contrib.Extensions;

namespace BlackJack.Entities.Entities
{
    public class HistoryMessage : Base
    {
        public string Message { get; set; }
        public long GameId { get; set; }
        [Write(false)]
        public virtual Game Game { get; set; }
    }
}
