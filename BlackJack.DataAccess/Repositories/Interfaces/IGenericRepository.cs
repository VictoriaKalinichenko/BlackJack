using System.Threading.Tasks;

namespace BlackJack.DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<T> Get(long id);

        Task<long> Create(T item);

        Task Delete(T item);
    }
}
