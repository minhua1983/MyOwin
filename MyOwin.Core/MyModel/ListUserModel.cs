using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwin.Core.MyModel
{
    public class ListUserRequest : BaseRequest
    {
        public int Top { get; set; }
    }

    public class ListUserResponse : BaseResponse
    {
        public List<string> Data { get; set; }
    }
}
