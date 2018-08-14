using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Providers.ValidationProviders.Name
{
    public interface INameValidationProvider
    {
        void Validate(string name);
    }
}
