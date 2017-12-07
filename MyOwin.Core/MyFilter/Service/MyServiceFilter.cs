using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace MyOwin.Core.MyFilter.Service
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MyServiceFilter : MyFilterAttribute, IMyServiceFilter
    {
        public virtual void OnServiceExecuted(IOwinContext context)
        {
            Console.WriteLine("OnServiceExecuted...");
        }

        public virtual void OnServiceExecuting(IOwinContext context)
        {
            Console.WriteLine("OnServiceExecuting...");
        }
    }
}
