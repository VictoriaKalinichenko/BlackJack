using BlackJack.Entities.Enums;

namespace BlackJack.Entities.Entities
{
    public class Game : Base
    {
        public GameStage Stage { get; set; }
        public string Result { get; set; }
    }
}
