using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Diagnostics;

[assembly: OwinStartup(typeof(BlackJack.UI.Startup))]

namespace BlackJack.UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(async (context, nextMiddleWare) =>
            {
                await nextMiddleWare.Invoke();
            });

            app.Use(async (context, nextMiddleWare) =>
            {
                await nextMiddleWare.Invoke();
            });
        }
    }
}