using Autofac;
using BlackJack.DataAccess.Interfaces;
using BlackJack.DataAccess.Repositories;
using BlackJack.Configuration;

namespace BlackJack.DataAccess.Util
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerRepository>()
                .As<IPlayerRepository>()
                .WithParameter("connectionString", Config.ConnectionStringForDapper);

            builder.RegisterType<PlayerCardRepository>()
                .As<IPlayerCardRepository>()
                .WithParameter("connectionString", Config.ConnectionStringForDapper);

            builder.RegisterType<GameRepository>()
                .As<IGameRepository>()
                .WithParameter("connectionString", Config.ConnectionStringForDapper);

            builder.RegisterType<GamePlayerRepository>()
                .As<IGamePlayerRepository>()
                .WithParameter("connectionString", Config.ConnectionStringForDapper);

            base.Load(builder);
        }
    }
}
