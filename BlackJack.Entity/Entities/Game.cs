using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace BlackJack.Entities.Entities
{
    public class Game : EntityBase
    {
        public int Stage { get; set; }
        public string Result { get; set; }
    }
}
