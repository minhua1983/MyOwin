using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
namespace MyOwin.Core.MyFilter.Service
{
    public interface IMyServiceFilter
    {
        void OnServiceExecuting(IOwinContext context);
        void OnServiceExecuted(IOwinContext context);
    }
}
