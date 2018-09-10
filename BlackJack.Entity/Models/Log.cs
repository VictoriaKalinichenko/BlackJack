using System;

namespace BlackJack.Entities.Models
{
    public class Log : EntityBase
    {
        public DateTime DateTime { get; set; }
        public int GameId { get; set; }
        public string Message { get; set; }
    }
}
