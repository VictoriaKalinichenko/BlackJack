using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories
{
    public class CardRepository : GenericRepository<Card>, ICardRepository
    {
        public CardRepository(string connectionString) : base(connectionString)
        { }
    }
}
