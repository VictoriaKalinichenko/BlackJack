using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Entity.Models
{
    public class PlayerCard
    {
        public int Id { get; set; }

        public int GamePlayerId { get; set; }

        public int CardId { get; set; }
    }
}
