using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

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
                Console.WriteLine("started...");
                Console.ReadLine();
            }
        }
    }
}
