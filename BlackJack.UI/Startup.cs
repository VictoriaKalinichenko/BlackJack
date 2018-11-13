﻿using BlackJack.BusinessLogic.Mappers;
using BlackJack.UI.Config;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartup(typeof(BlackJack.UI.Startup))]

namespace BlackJack.UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AutoMapperConfig.Initialize();
            AutofacConfig.ConfigureContainer();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}