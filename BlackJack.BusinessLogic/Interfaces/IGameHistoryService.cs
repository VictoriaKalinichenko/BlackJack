using System.Threading.Tasks;
using BlackJack.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameHistoryService
    {
        Task<GetGameHistoryView> Get();
    }
}
