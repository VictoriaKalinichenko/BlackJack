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
                .WithParameter("connectionString", Configuration.Config.ConnectionString);

            builder.RegisterType<PlayerCardRepository>()
                .As<IPlayerCardRepository>()
                .WithParameter("connectionString", Configuration.Config.ConnectionString);

            builder.RegisterType<GameRepository>()
                .As<IGameRepository>()
                .WithParameter("connectionString", Configuration.Config.ConnectionString);

            builder.RegisterType<GamePlayerRepository>()
                .As<IGamePlayerRepository>()
                .WithParameter("connectionString", Configuration.Config.ConnectionString);
            
            builder.RegisterType<LogRepository>()
                .As<ILogRepository>()
                .WithParameter("connectionString", Configuration.Config.ConnectionString);

            builder.RegisterType<CardRepository>()
                .As<ICardRepository>()
                .WithParameter("connectionString", Configuration.Config.ConnectionString);

            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>))
                .WithParameter("connectionString", Configuration.Config.ConnectionString);

            base.Load(builder);
        }
    }
}
