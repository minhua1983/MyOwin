using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyOwin.Core.Startup))]

namespace MyOwin.Core
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            app.Run(iOwinContext => {
                return iOwinContext.Response.WriteAsync("aaa");
            });
        }
    }
}
