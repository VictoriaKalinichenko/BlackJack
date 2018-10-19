using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Web.Mvc;
using System.Configuration;

namespace BlackJack.UI.Config
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dataBaseConnection"].ConnectionString;
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(Startup).Assembly);
            builder.RegisterApiControllers(typeof(Startup).Assembly);
            builder.RegisterModelBinderProvider();

            builder.RegisterModule(new BusinessLogic.Config.AutofacConfig(connectionString));
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}