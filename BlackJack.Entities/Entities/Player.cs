using BlackJack.Entities.Enums;

namespace BlackJack.Entities.Entities
{
    public class Player : Base
    {
        public string Name { get; set; }
        public PlayerType Type { get; set; }
    }
}