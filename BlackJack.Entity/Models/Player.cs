namespace BlackJack.Entities.Models
{
    public class Player : EntityBase
    {
        public string Name { get; set; }

        public bool IsDealer { get; set; }

        public bool IsHuman { get; set; }
    }
}