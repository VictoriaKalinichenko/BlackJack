using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Interfaces
{
    public interface ILogService
    {
        Task<IEnumerable<GetLogsViewModel>> GetAll();
    }
}
