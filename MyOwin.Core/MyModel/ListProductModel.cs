using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwin.Core.MyModel
{
    public class ListProductRequest : BaseRequest
    {
        public int Top { get; set; }
    }

    public class ListProductResponse : BaseResponse
    {
        public List<string> Data { get; set; }
    }
}
