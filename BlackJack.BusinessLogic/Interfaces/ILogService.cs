using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels.Log;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface ILogService
    {
        Task<IEnumerable<GetAllLogView>> GetAll();
    }
}
