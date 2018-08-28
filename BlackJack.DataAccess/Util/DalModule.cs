using Autofac;
using BlackJack.DataAccess.Interfaces;
using BlackJack.DataAccess.Repositories;

namespace BlackJack.DataAccess.Util
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>();
            builder.RegisterType<PlayerCardRepository>().As<IPlayerCardRepository>();
            builder.RegisterType<GameRepository>().As<IGameRepository>();
            builder.RegisterType<GamePlayerRepository>().As<IGamePlayerRepository>();

            base.Load(builder);
        }
    }
}
