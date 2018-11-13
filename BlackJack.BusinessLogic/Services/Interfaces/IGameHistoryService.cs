using BlackJack.ViewModels.GameHistory;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services.Interfaces
{
    public interface IGameHistoryService
    {
        Task<GetGameHistoryView> Get();
    }
}
