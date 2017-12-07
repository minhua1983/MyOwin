using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace MyOwin.Core.MyFilter.Service
{
    public class MyServiceFilter3 : MyServiceFilter
    {
        public override void OnServiceExecuting(IOwinContext context)
        {
            Console.WriteLine("MyServiceFilter3.OnServiceExecuting...");
            //base.OnServiceExecuting(context);
        }

        public override void OnServiceExecuted(IOwinContext context)
        {
            Console.WriteLine("MyServiceFilter3.OnServiceExecuted...");
            //base.OnServiceExecuting(context);
        }
    }
}
