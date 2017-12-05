using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace MyOwin.Core.MyMiddleware
{
    public class ModelCheckMiddleware : OwinMiddleware
    {
        public ModelCheckMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            Console.WriteLine("pre ModelCheckMiddleware...");
            await Next.Invoke(context);
            Console.WriteLine("post ModelCheckMiddleware...");
        }
    }
}
