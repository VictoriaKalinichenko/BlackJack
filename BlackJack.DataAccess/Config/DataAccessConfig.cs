using Autofac;
using BlackJack.DataAccess.Repositories.Interfaces;
using BlackJack.DataAccess.Repositories;

namespace BlackJack.DataAccess.Config
{
    public class DataAccessConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerRepository>()
                .As<IPlayerRepository>()
                .WithParameter("connectionString", Configuration.Config.ConnectionStringForDapper);

            builder.RegisterType<PlayerCardRepository>()
                .As<IPlayerCardRepository>()
                .WithParameter("connectionString", Configuration.Config.ConnectionStringForDapper);

            builder.RegisterType<GameRepository>()
                .As<IGameRepository>()
                .WithParameter("connectionString", Configuration.Config.ConnectionStringForDapper);

            builder.RegisterType<GamePlayerRepository>()
                .As<IGamePlayerRepository>()
                .WithParameter("connectionString", Configuration.Config.ConnectionStringForDapper);
            
            builder.RegisterType<LogRepository>()
                .As<ILogRepository>()
                .WithParameter("connectionString", Configuration.Config.ConnectionStringForDapper);

            builder.RegisterType<CardRepository>()
                .As<ICardRepository>()
                .WithParameter("connectionString", Configuration.Config.ConnectionStringForDapper);

            base.Load(builder);
        }
    }
}
