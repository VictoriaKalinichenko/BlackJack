using BlackJack.Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> Get(long id);

        Task<IEnumerable<T>> GetAll();

        Task<long> Create(T item);

        Task CreateMany(IEnumerable<T> items);

        Task Update(T item);

        Task UpdateMany(IEnumerable<T> items);

        Task Delete(T item);

        Task DeleteMany(IEnumerable<T> items);
    }
}
