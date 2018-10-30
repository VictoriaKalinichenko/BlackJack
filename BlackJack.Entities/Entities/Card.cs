using BlackJack.Entities.Enums;

namespace BlackJack.Entities.Entities
{
    public class Card : Base
    {
        public CardRank Rank { get; set; }
        public CardLear Lear { get; set; }
        public int Worth { get; set; }

        public override string ToString()
        {
            string convertedString = string.Empty;
            convertedString = $"{Rank.ToString()} {Lear.ToString()}";
            return convertedString;
        }
    }
}