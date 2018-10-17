using System;

namespace BlackJack.ViewModels.ViewModels.Log
{
    public class GetAllViewModel
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public long GameId { get; set; }
        public string Message { get; set; }
    }
}