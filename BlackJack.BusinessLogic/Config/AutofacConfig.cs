using Autofac;
using BlackJack.BusinessLogic.Managers;
using BlackJack.BusinessLogic.Managers.Interfaces;
using BlackJack.BusinessLogic.Services;
using BlackJack.BusinessLogic.Services.Interfaces;

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
            builder.RegisterType<HistoryMessageManager>().As<IHistoryMessageManager>();
            
            builder.RegisterType<StartService>().As<IStartService>();
            builder.RegisterType<RoundService>().As<IRoundService>();
            builder.RegisterType<GameHistoryService>().As<IGameHistoryService>();

            builder.RegisterModule(new DataAccess.Config.AutofacConfig(_connectionString));
            base.Load(builder);
        }
    }
}
