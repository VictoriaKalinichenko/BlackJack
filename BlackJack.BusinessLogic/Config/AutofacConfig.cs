using Autofac;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Providers;
using BlackJack.BusinessLogic.Services;

namespace BlackJack.BusinessLogic.Config
{
    public class AutofacConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GamePlayerProvider>().As<IGamePlayerProvider>();

            builder.RegisterType<StartService>().As<IStartService>();
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<LogService>().As<ILogService>();

            builder.RegisterModule(new DataAccess.Config.AutofacConfig());
            base.Load(builder);
        }
    }
}
