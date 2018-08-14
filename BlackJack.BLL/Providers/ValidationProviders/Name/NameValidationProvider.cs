using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Infrastructure;
using BlackJack.Configuration;
using BlackJack.BLL.Helpers;

namespace BlackJack.BLL.Providers.ValidationProviders.Name
{
    public class NameValidationProvider : INameValidationProvider
    {
        public void Validate (string name)
        {
            if (name == "")
            {
                throw new ValidationException(Message.NameFieldIsEmpty);
            }

            if (Config.db.Players.SelectByName(name) != null)
            {
                throw new ValidationException(Message.NameAlreadyExist);
            }
        }
    }
}
