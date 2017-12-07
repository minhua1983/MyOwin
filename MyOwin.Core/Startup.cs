using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.Owin;
using Owin;
using MyOwin.Core.MyContainer;
using MyOwin.Core.MyService;
using MyOwin.Core.MyModel;
using MyOwin.Core.MyMiddleware;
using Newtonsoft.Json;

[assembly: OwinStartup(typeof(MyOwin.Core.Startup))]

namespace MyOwin.Core
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            //请求初始化
            app.Use<RequestInitializationMiddleware>();
            //注册验证检查的中间件
            app.Use<AuthenticationCheckMiddleware>();
            //注册模型验证的中间件
            app.Use<ModelCheckMiddleware>();
            //最终运行的中间件，仔细看Run方法的介绍，此方法之后就不会再调用之后的中间件了
            app.Run(CallService);
        }

        public Task CallService(IOwinContext context)
        {
            //定义一个空对象
            string json = "{}";
            //获取服务对象
            Type type = context.Get<Type>("ServiceType");
            //如果服务类存在
            if (type != null)
            {
                string content = context.Get<string>("RequestBodyContent");
                dynamic d = context.Get<dynamic>("ServiceInstance");
                
                //执行实例的Ready方法
                dynamic result = d.Ready(content, context);
                //将结果序列化成字符串
                json = JsonConvert.SerializeObject(result);
            }

            //最终输出结果
            return context.Response.WriteAsync(json);
        }
    }
}
