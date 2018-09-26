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
            builder.RegisterType<GamePlayerProvider>().As<IGamePlayerProvider>();

            builder.RegisterType<StartService>().As<IStartService>();
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<LogService>().As<ILogService>();

            builder.RegisterModule(new DataAccessConfig());
            base.Load(builder);
        }
    }
}
