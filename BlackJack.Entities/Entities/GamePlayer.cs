﻿using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace BlackJack.Entities.Entities
{
    public class GamePlayer : Base
    {
        public long PlayerId { get; set; }
        public long GameId { get; set; }
        [Write(false)]
        public virtual Player Player { get; set; }
        [Write(false)]
        public virtual List<PlayerCard> PlayerCards { get; set; }
    }
}
