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
            app.Use<AuthenticationCheckMiddleware>();
            app.Use<ModelCheckMiddleware>();
            app.Run(CallService);
        }

        public Task CallService(IOwinContext iOwinContext)
        {
            //设置输出格式
            iOwinContext.Response.ContentType = "application/json";
            //获取请求路径（此方法不带任何querystring参数，连?都不包含）
            string path = iOwinContext.Request.Path.Value;
            //定义一个空对象
            string json = "{}";
            //尝试清空一下最后的一个“/”符号
            if (path.EndsWith("/")) path = path.Substring(0, path.Length - 1);
            //如果还有“/”符号
            if (path.IndexOf(@"/") >= 0)
            {
                //分割path
                string[] pathArray = path.Split('/');
                //获取服务名称
                string serviceName = pathArray[pathArray.Length - 1];
                //获取服务类
                Type type = Type.GetType("MyOwin.Core.MyService." + serviceName, false, true);
                //如果服务类存在
                if (type != null)
                {
                    //object requestModel = null;
                    string content = "";
                    //获取请求体内容原生是Request.InputStream，Owin中封装成了Request.Body
                    using (StreamReader reader = new StreamReader(iOwinContext.Request.Body))
                    {
                        content = reader.ReadToEnd();
                    }

                    //获取容器对象
                    Container _container = Container.GetInstance();
                    //从容器中获取到实例
                    dynamic d = _container.Resolve(type);
                    //执行实例的Ready方法
                    dynamic result = d.Ready(content);
                    //将结果序列化成字符串
                    json = JsonConvert.SerializeObject(result);
                }
            }
            //最终输出结果
            return iOwinContext.Response.WriteAsync(json);

        }
    }
}
