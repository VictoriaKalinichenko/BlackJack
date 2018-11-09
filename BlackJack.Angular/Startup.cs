﻿using BlackJack.Angular.Config;
using BlackJack.BusinessLogic.Mappers;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartup(typeof(BlackJack.Angular.Startup))]

namespace BlackJack.Angular
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.Initialize();
            AutofacConfig.ConfigureContainer();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}