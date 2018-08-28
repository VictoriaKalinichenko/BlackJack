using Autofac;
using Autofac.Integration.Mvc;
using BlackJack.BusinessLogic.Util;
using System.Web.Mvc;

namespace BlackJack.UI.Util
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new BllModule());

            var container = builder.Build();
            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}