using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Configuration;
using System.Web.Mvc;

namespace BlackJack.Angular.Config
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            string connectionString = ConfigurationManager.ConnectionStrings["dataBaseConnection"].ConnectionString;

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            builder.RegisterModule(new BusinessLogic.Config.AutofacConfig(connectionString));
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
        }
    }
}