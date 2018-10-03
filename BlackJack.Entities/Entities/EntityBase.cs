using System;

namespace BlackJack.Entities.Entities
{
    public abstract class EntityBase
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public EntityBase()
        {
            CreationDate = DateTime.Now;
        }
    }
}
