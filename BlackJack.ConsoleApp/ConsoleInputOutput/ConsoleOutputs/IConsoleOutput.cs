using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.ViewModels;

namespace BlackJack.ConsoleApp.ConsoleInputOutput.ConsoleOutputs
{
    public interface IConsoleOutput
    {
        void ExitWithMessage(string message);

        void RoundStartPageOutput(List<PlayerViewModel> players);

        void RoundFirstPhaseOutput(List<PlayerViewModel> players);

        void RoundSecondPhaseOutput(List<PlayerViewModel> players);
    }
}
