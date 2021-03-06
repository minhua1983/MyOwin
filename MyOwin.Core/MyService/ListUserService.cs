﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;
using MyOwin.Core.MyModel;
using MyOwin.Core.MyFilter.Service;

namespace MyOwin.Core.MyService
{
    public class ListUserService : BaseService<ListUserRequest, ListUserResponse>
    {
        [MyServiceFilter2]
        [MyServiceFilter1]
        [MyServiceFilter3]
        public override ListUserResponse Any(ListUserRequest requestModel)
        {
            ListUserResponse responseModel = new ListUserResponse()
            {
                Data = new List<string>(){
                    "minhua",
                    "marcus"
                }
            };
            if (requestModel != null) responseModel.Data = responseModel.Data.Take(requestModel.Top).ToList();
            return responseModel;
        }
    }
}
