using System;
using System.Collections.Generic;

namespace BlackJack.ViewModels.GameHistory
{
    public class GetGameHistoryView
    {
        public List<HistoryMessageGetGameHistoryViewItem> HistoryMessages { get; set; }

        public GetGameHistoryView(List<HistoryMessageGetGameHistoryViewItem> historyMessages)
        {
            HistoryMessages = historyMessages;
        }
    }

    public class HistoryMessageGetGameHistoryViewItem
    {
        public DateTime CreationDate { get; set; }
        public long GameId { get; set; }
        public string Message { get; set; }
    }
}