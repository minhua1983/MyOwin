using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Owin;
using MyOwin.Core.MyContainer;
using Newtonsoft.Json;

namespace MyOwin.Core.MyMiddleware
{
    public class RequestInitializationMiddleware : OwinMiddleware
    {
        public RequestInitializationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            if (context.Request.Method.ToUpper() != "POST")
            {
                BaseResponse baseResponse = new BaseResponse();
                baseResponse.ErrorFlag = true;
                baseResponse.ErrorMessage = @"This HttpMethod is not supported.";
                string json = JsonConvert.SerializeObject(baseResponse);

                //最终输出结果
                await context.Response.WriteAsync(json);

            }
            else
            {
                //设置输出格式
                context.Response.ContentType = "application/json";
                //获取请求路径（此方法不带任何querystring参数，连?都不包含）
                string path = context.Request.Path.Value;
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
                        //将ServiceType写入context
                        context.Set<Type>("ServiceType", type);
                        //object requestModel = null;
                        string content = "";
                        //获取请求体内容原生是Request.InputStream，Owin中封装成了Request.Body
                        using (StreamReader reader = new StreamReader(context.Request.Body))
                        {
                            content = reader.ReadToEnd();
                            //将RequestBodyContent写入context
                            context.Set<string>("RequestBodyContent", content);
                        }

                        //获取容器对象
                        Container _container = Container.GetInstance();
                        //从容器中获取到实例
                        dynamic d = _container.Resolve(type);
                        //将ServiceInstance写入context
                        context.Set<dynamic>("ServiceInstance", d);
                    }
                }

                Console.WriteLine("pre RequestInitializationMiddleware...");
                await Next.Invoke(context);
                Console.WriteLine("pre RequestInitializationMiddleware...");
            }
        }
    }
}
