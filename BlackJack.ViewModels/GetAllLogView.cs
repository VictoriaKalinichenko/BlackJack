using System;
using System.Collections.Generic;

namespace BlackJack
{
    public class GetAllLogView
    {
        public List<HistoryMessageGetAllLogViewItem> HistoryMessages { get; set; }
    }

    public class HistoryMessageGetAllLogViewItem
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public long GameId { get; set; }
        public string Message { get; set; }
    }
}