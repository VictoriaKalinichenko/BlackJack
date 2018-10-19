using Autofac;
using BlackJack.DataAccess.Repositories;
using BlackJack.DataAccess.Repositories.Interfaces;

namespace BlackJack.DataAccess.Config
{
    public class AutofacConfig : Module
    {
        private string _connectionString;

        public AutofacConfig(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerRepository>()
                .As<IPlayerRepository>()
                .WithParameter("connectionString", _connectionString);

            builder.RegisterType<PlayerCardRepository>()
                .As<IPlayerCardRepository>()
                .WithParameter("connectionString", _connectionString);

            builder.RegisterType<GameRepository>()
                .As<IGameRepository>()
                .WithParameter("connectionString", _connectionString);

            builder.RegisterType<GamePlayerRepository>()
                .As<IGamePlayerRepository>()
                .WithParameter("connectionString", _connectionString);
            
            builder.RegisterType<HistoryMessageRepository>()
                .As<IHistoryMessageRepository>()
                .WithParameter("connectionString", _connectionString);

            builder.RegisterType<CardRepository>()
                .As<ICardRepository>()
                .WithParameter("connectionString", _connectionString);
           
            base.Load(builder);
        }
    }
}
