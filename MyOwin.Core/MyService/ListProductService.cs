using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyOwin.Core.MyService;
using MyOwin.Core.MyModel;

namespace MyOwin.Core.MyService
{
    public class ListProductService : BaseService<ListProductRequest, ListProductResponse>
    {
        public override ListProductResponse Any(ListProductRequest requestModel)
        {
            ListProductResponse responseModel = new ListProductResponse()
            {
                Data = new List<string>() {
                    "A",
                    "B",
                    "C"
                }
            };
            return responseModel;
        }
    }
}
