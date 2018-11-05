using System.Threading.Tasks;
using BlackJack.ViewModels.GameHistory;

namespace BlackJack.BusinessLogic.Services.Interfaces
{
    public interface IGameHistoryService
    {
        Task<GetGameHistoryView> Get();
    }
}
