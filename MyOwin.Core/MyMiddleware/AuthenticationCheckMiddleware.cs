using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace MyOwin.Core.MyMiddleware
{
    public class AuthenticationCheckMiddleware : OwinMiddleware
    {
        public AuthenticationCheckMiddleware(OwinMiddleware next) : base(next)
        {

        }

        public async override Task Invoke(IOwinContext context)
        {
            Console.WriteLine("pre ServiceRegisterMiddleware...");
            await Next.Invoke(context);
            Console.WriteLine("post ServiceRegisterMiddleware...");
        }
    }
}
