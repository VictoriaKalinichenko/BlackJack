using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Entities.Models
{
    public class PlayerCard : EntityBase
    {
        public int GamePlayerId { get; set; }

        public virtual GamePlayer GamePlayer { get; set; }

        public int CardId { get; set; }
    }
}
