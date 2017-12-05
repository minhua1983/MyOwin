using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Owin;
using Newtonsoft.Json;

namespace MyOwin.Core.MyMiddleware
{
    public class ModelCheckMiddleware : OwinMiddleware
    {
        readonly bool isValidModel = ConfigurationManager.AppSettings["IsValidModel"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsValidModel"]);
        readonly string validModelTypeNamePrefix = ConfigurationManager.AppSettings["ValidModelTypeNamePrefix"];

        public ModelCheckMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            BaseResponse baseResponse = new BaseResponse();
            string json = "";
            if (isValidModel)
            {
                string content = context.Get<string>("RequestBodyContent");
                dynamic d = context.Get<dynamic>("ServiceInstance");
                if (string.IsNullOrEmpty(content))
                {
                    //GET
                    ValidModel(d.GetRequestModel(content), baseResponse);
                }
                else
                {
                    //POST
                    ValidModel(d.GetRequestModel(content), baseResponse);
                }
            }

            if (baseResponse.ErrorFlag)
            {
                json = JsonConvert.SerializeObject(baseResponse);
                await context.Response.WriteAsync(json);
            }
            else
            {
                Console.WriteLine("pre ModelCheckMiddleware...");
                await Next.Invoke(context);
                Console.WriteLine("post ModelCheckMiddleware...");
            }
        }


        void ValidModel(object model, BaseResponse response)
        {
            if (model != null)
            {
                PropertyInfo[] proertyInfoArray = model.GetType().GetProperties();
                foreach (PropertyInfo propertyInfo in proertyInfoArray)
                {
                    var propertyValue = propertyInfo.GetValue(model, null);
                    if (propertyValue != null)
                    {
                        if (propertyInfo.PropertyType.Namespace.StartsWith(validModelTypeNamePrefix))
                        {
                            //项目自定义类型，需要递归检查
                            ValidModel(propertyValue, response);
                        }
                        else
                        {
                            //.net自带类型，直接检查
                            ValidAtrribute(response, propertyValue, propertyInfo);
                        }
                    }
                }
            }
        }

        void ValidAtrribute(BaseResponse response, object propertyValue, PropertyInfo propertyInfo)
        {
            var attributes = propertyInfo.GetCustomAttributes(false);
            if (attributes != null && attributes.Length > 0)
            {
                foreach (var attribute in attributes)
                {
                    if (attribute.GetType() == typeof(RegularExpressionAttribute))
                    {
                        var expression = (RegularExpressionAttribute)attribute;
                        if (propertyValue != null)
                        {
                            var v = propertyValue.ToString();
                            var isValid = Regex.IsMatch(v, expression.Pattern);
                            if (!isValid)
                            {
                                var errorMessage = expression.ErrorMessage ?? "";
                                response.ErrorFlag = true;
                                response.ErrorMessage = @"[" + propertyInfo.Name + @"]不符合规则[" + errorMessage + @"]";
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
