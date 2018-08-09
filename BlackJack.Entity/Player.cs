using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Entity
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Score { get; set; }

        public bool IsDealer { get; set; }

        public bool IsHuman { get; set; }


        public int Bet { get; set; }

        public int RoundScore { get; set; }


        public virtual List<Card> Cards { get; set; }

        public virtual Game Game { get; set; }
    }
}