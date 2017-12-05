using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using MyOwin.Core.MyModel;
using Newtonsoft.Json;

namespace MyOwin.Core.MyService
{
    public class ListUserService : BaseService<ListUserRequest, ListUserResponse>
    {
        public override ListUserResponse Any(ListUserRequest requestModel)
        {
            ListUserResponse responseModel = new ListUserResponse()
            {

                Data = new List<string>(){
                    "minhua",
                    "marcus"
                },
                ErrorFlag = true
            };
            if (requestModel != null) responseModel.Data = responseModel.Data.Take(requestModel.Top).ToList();
            return responseModel;
        }
    }
}
