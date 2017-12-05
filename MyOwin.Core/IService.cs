using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwin.Core
{
    public interface IService<TRequest, TResponse> where TResponse : new()
    {
        TResponse Any(TRequest requestModel);
    }
}
