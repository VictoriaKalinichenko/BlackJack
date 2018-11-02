using Dapper.Contrib.Extensions;

namespace BlackJack.Entities.Entities
{
    public class HistoryMessage : Base
    {
        public long GameId { get; set; }
        public string Message { get; set; }
        [Write(false)]
        public virtual Game Game { get; set; }
    }
}
