using BlackJack.ViewModels.Enums;

namespace BlackJack.BusinessLogic.Helpers
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
