using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyOwin.Core.MyModel
{
    public class ListUserRequest : BaseRequest
    {
        [RegularExpression(@"^[1-2]$",ErrorMessage ="Top值不符合规范")]
        public int Top { get; set; }
    }

    public class ListUserResponse : BaseResponse
    {
        public List<string> Data { get; set; }
    }
}
