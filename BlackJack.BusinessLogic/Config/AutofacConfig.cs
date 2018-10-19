using Autofac;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.BusinessLogic.Managers;
using BlackJack.BusinessLogic.Services;

namespace BlackJack.BusinessLogic.Config
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
            builder.RegisterType<GamePlayerManager>().As<IGamePlayerManager>();
            builder.RegisterType<HistoryMessageManager>().As<IHistoryMessageManager>();
            
            builder.RegisterType<StartService>().As<IStartService>();
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<LogService>().As<ILogService>();

            builder.RegisterModule(new DataAccess.Config.AutofacConfig(_connectionString));
            base.Load(builder);
        }
    }
}
