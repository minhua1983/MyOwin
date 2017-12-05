using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using System.Reflection;
using Microsoft.Owin;
using MyOwin.Core.MyContainer;
using MyOwin.Core.MyService;

namespace MyOwin.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            StartOptions startOptions = new StartOptions();
            startOptions.Urls.Add("http://localhost:10001");
            startOptions.Urls.Add("http://localhost:10002");
            startOptions.Urls.Add("http://localhost:10003");
            startOptions.ServerFactory = "Microsoft.Owin.Host.HttpListener";

            using (WebApp.Start<Startup>(startOptions))
            {
                Console.WriteLine("application is started...");
                Console.WriteLine("service is registed...");
                //*把MyService下面的服务类注册到容器中
                Container _container = Container.GetInstance();
                Assembly assembly = Assembly.GetExecutingAssembly();
                List<Type> typeList = assembly.GetTypes().Where(t => t.Namespace.StartsWith("MyOwin.Core.MyService") == true).ToList();
                typeList.ForEach(type =>
                {
                    _container.Register(type);
                });
                //*/
                Console.ReadLine();
            }
        }
    }
}
