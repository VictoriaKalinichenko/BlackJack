using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Entities.Models
{
    public class Player : EntityBase
    {
        public string Name { get; set; }

        public bool IsDealer { get; set; }

        public bool IsHuman { get; set; }


        public virtual List<GamePlayer> GamePlayers { get; set; }
    }
}