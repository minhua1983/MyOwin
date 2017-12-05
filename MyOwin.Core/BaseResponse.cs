using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwin.Core
{
    public class BaseResponse
    {
        public bool ErrorFlag { get; set; }
        public string ErrorMessage { get; set; }
        public int LogicFlag { get; set; }
        public string LogicMessage { get; set; }
    }
}
