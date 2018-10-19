using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.Entities.Entities;

namespace BlackJack.DataAccess.Repositories
{
    public class HistoryMessageRepository : BaseRepository<HistoryMessage>, IHistoryMessageRepository
    {
        public HistoryMessageRepository(string connectionString) : base(connectionString)
        { }
    }
}
