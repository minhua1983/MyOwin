using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;
using Owin;
using Microsoft.Owin;

namespace MyOwin.Core
{
    public class BaseService<TRequest, TResponse> : IService<TRequest, TResponse> where TResponse : BaseResponse, new()
    {
        public TRequest GetRequestModel(string bodyContent)
        {
            TRequest requestModel = JsonConvert.DeserializeObject<TRequest>(bodyContent);
            return requestModel;
        }

        public TResponse Ready(string bodyContent, IOwinContext context)
        {

            try
            {
                TRequest requestModel;
                TResponse responseModel;
                //获取Any方法上的所有自定义特性
                List<object> attributeList = this.GetType().GetMethod("Any").GetCustomAttributes(false).ToList();

                //*按注册顺序执行自定义特性的OnServiceExecuting方法
                if (attributeList != null && attributeList.Count > 0)
                {
                    attributeList.ForEach(attribute =>
                    {
                        MethodInfo methodInfo = attribute.GetType().GetMethod("OnServiceExecuting");
                        object[] objectArray = new object[] { context };
                        methodInfo.Invoke(attribute, objectArray);
                    });
                }
                //*/

                //*执行Any方法
                if (string.IsNullOrEmpty(bodyContent))
                {
                    //处理GET请求
                    responseModel = Any(default(TRequest));
                }
                else
                {
                    //处理POST请求
                    requestModel = JsonConvert.DeserializeObject<TRequest>(bodyContent);
                    responseModel = Any(requestModel);

                }
                //*/

                //*按注册倒序执行自定义特性的OnServiceExecuted方法
                if (attributeList != null && attributeList.Count > 0)
                {
                    attributeList.Reverse();
                    attributeList.ForEach(attribute =>
                    {
                        MethodInfo methodInfo = attribute.GetType().GetMethod("OnServiceExecuted");
                        object[] objectArray = new object[] { context };
                        methodInfo.Invoke(attribute, objectArray);
                    });
                }
                //*/

                responseModel.ErrorFlag = false;
                return responseModel;
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
