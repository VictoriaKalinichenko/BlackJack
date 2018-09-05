using Autofac;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Providers;
using BlackJack.BusinessLogic.Services;
using BlackJack.DataAccess.Config;

namespace BlackJack.BusinessLogic.Config
{
    public class BusinessLogicConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerCardProvider>().As<IPlayerCardProvider>();
            builder.RegisterType<GamePlayerProvider>().As<IGamePlayerProvider>();
            builder.RegisterType<CardCheckProvider>().As<ICardCheckProvider>();

            builder.RegisterType<StartGameService>().As<IStartGameService>();
            builder.RegisterType<GameLogicService>().As<IGameLogicService>();
            builder.RegisterType<PlayerLogicService>().As<IPlayerLogicService>();
            builder.RegisterType<LogService>().As<ILogService>();

            builder.RegisterModule(new DataAccessConfig());
            base.Load(builder);
        }
    }
}
