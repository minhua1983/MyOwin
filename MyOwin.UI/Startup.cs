using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyOwin.UI.Startup))]

namespace MyOwin.UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888

            //IAppBuilder的Run方法的参数为：Func<IOwinContext, Task> handler，此参数用于执行MiddleWare里面的逻辑，而为何不见Owin规范中的IDictionary<string, object>，因为IOwinContext接口已经定义了一个类型为IDictionary<string, object>的属性Environment，为了将字典中的keyvalue，更方便的让使用者调用，因此将各个keyvalue都封装到各个属性中去了，如Request，Response，但是我个人认为Owin规范没执行到底...
            app.Run(iOwinContext =>
            {
                object v = "";
                iOwinContext.Environment.TryGetValue("owin.RequestMethod", out v);
                iOwinContext.Response.ContentType = "text/plain";
                return iOwinContext.Response.WriteAsync("111" + v);
            });
        }
    }
}
