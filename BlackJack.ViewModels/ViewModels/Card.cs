using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity.Enums;

namespace BlackJack.ViewModels.ViewModels
{
    public class Card
    {
        public int Id { get; set; }

        public CardName CardName { get; set; }

        public CardType CardType { get; set; }


        public override string ToString()
        {
            string result;

            result = CardName + " " + CardType;

            return result;
        }
    }
}
