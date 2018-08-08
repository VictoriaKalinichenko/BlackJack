using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Entity
{
    public class PlayerCard
    {
        public int Id { get; set; }



        public virtual Player Player { get; set; }

        public virtual Card Card { get; set; }
    }
}
