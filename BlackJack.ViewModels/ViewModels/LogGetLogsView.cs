using System;

namespace BlackJack.ViewModels.ViewModels
{
    public class LogGetLogsView
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int GameId { get; set; }
        public string Message { get; set; }
    }
}
