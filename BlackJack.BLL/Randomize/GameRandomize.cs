using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Entity;

namespace BlackJack.BLL.Randomize
{
    public class GameRandomize : IRandomize
    {
        private Random random = new Random();

        public int BetGenerate(int PlayerScore)
        {
            int bet = 0;
            
            bet = random.Next(PlayerScore / 50 + 1) * 50;
            
            return bet;
        }

        public int CardIdSelection(int AmountOfCard)
        {
            int cardId;

            cardId = random.Next(AmountOfCard);

            return cardId;
        }
    }
}
