using System.Threading.Tasks;
using BlackJack.ViewModels.GameHistory;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface IGameHistoryService
    {
        Task<GetGameHistoryView> Get();
    }
}
