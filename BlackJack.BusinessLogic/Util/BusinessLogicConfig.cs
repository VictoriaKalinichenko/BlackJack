using Autofac;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Providers;
using BlackJack.BusinessLogic.Services;
using BlackJack.DataAccess.Util;

namespace BlackJack.BusinessLogic.Util
{
    public class BusinessLogicConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerCardProvider>().As<IPlayerCardProvider>();
            builder.RegisterType<GamePlayerProvider>().As<IGamePlayerProvider>();
            builder.RegisterType<CardCheckProvider>().As<ICardCheckProvider>();

            builder.RegisterType<StartGameService>().As<IStartGameService>();
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<LogService>().As<ILogService>();

            builder.RegisterModule(new DataAccessConfig());
            base.Load(builder);
        }
    }
}
