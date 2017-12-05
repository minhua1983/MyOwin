using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyOwin.Core
{
    public class BaseService<TRequest, TResponse> : IService<TRequest, TResponse> where TResponse : BaseResponse, new()
    {
        public TRequest GetRequestModel(string bodyContent)
        {
            TRequest requestModel = JsonConvert.DeserializeObject<TRequest>(bodyContent);
            return requestModel;
        }

        public TResponse Ready(string bodyContent)
        {
            try
            {
                if (string.IsNullOrEmpty(bodyContent))
                {
                    //处理GET请求
                    var responseModel = Any(default(TRequest));
                    responseModel.ErrorFlag = false;
                    return responseModel;
                }
                else
                {
                    //处理POST请求
                    TRequest requestModel = JsonConvert.DeserializeObject<TRequest>(bodyContent);
                    var responseModel= Any(requestModel);
                    responseModel.ErrorFlag = false;
                    return responseModel;
                }
            }
            catch (Exception e)
            {
                TResponse responseModel = new TResponse()
                {
                    ErrorFlag = true,
                    ErrorMessage = e.Message
                };
                return responseModel;
            }
        }

        public virtual TResponse Any(TRequest requestModel)
        {
            return default(TResponse);
        }
    }
}
