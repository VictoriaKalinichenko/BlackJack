using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BlackJack.DAL.Context;
using BlackJack.Entity.Models;
using BlackJack.Entity.Enums;

namespace BlackJack.DAL.Initializer
{
    public class DbInitializer : DropCreateDatabaseAlways<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            context.Players.Add(new Player { Name = BotName.Andrew.ToString(), IsDealer = true });
            context.Players.Add(new Player { Name = BotName.Jack.ToString() });
            context.Players.Add(new Player { Name = BotName.James.ToString() });
            context.Players.Add(new Player { Name = BotName.Jessie.ToString() });
            context.Players.Add(new Player { Name = BotName.Joe.ToString() });
            context.Players.Add(new Player { Name = BotName.John.ToString() });

            base.Seed(context);
        }
    }
}
