using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<T> Get(long id);

        Task<IEnumerable<T>> GetAll();

        Task<long> Create(T item);

        Task CreateMany(IEnumerable<T> items, string tableName);

        Task Delete(T item);

        Task DeleteMany(IEnumerable<T> items, string tableName);
    }
}
