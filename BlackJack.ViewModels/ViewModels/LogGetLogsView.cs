using System;

namespace BlackJack.ViewModels.ViewModels
{
    public class LogGetLogsView
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public long GameId { get; set; }
        public string Message { get; set; }
    }
}
