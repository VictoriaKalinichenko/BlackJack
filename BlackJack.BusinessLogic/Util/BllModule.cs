using Autofac;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Providers;
using BlackJack.BusinessLogic.Services;
using BlackJack.DataAccess.Util;

namespace BlackJack.BusinessLogic.Util
{
    public class BllModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerCardProvider>().As<IPlayerCardProvider>();
            builder.RegisterType<GamePlayerProvider>().As<IGamePlayerProvider>();
            builder.RegisterType<CardCheckProvider>().As<ICardCheckProvider>();

            builder.RegisterType<StartGameService>().As<IStartGameService>();
            builder.RegisterType<GameService>().As<IGameService>();

            builder.RegisterModule(new DalModule());
            base.Load(builder);
        }
    }
}
