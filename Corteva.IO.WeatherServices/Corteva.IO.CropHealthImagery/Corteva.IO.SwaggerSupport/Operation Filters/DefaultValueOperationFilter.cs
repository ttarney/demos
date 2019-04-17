using Corteva.IO.SwaggerSupport.Support;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Corteva.IO.SwaggerSupport.Operation_Filters
{
    public class AddDefaultValuesFilter : IOperationFilter
    {

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                return;
            

            var actionParams = new List<ParameterDescriptor>(context.ApiDescription.ActionDescriptor.Parameters);
            var customAttributes = context.MethodInfo.GetCustomAttributes(typeof(SwaggerDefaultValueAttribute), false);
            
            foreach (var param in operation.Parameters)
            {
                
                var actionParam = actionParams.FirstOrDefault(p => p.Name == param.Name);

                if (actionParam != null)
                {
                    SwaggerDefaultValueAttribute defaultValueAttribute = customAttributes.FirstOrDefault(p  => (p as SwaggerDefaultValueAttribute).ParameterName == param.Name) as SwaggerDefaultValueAttribute;
                    if (defaultValueAttribute != null)
                    {
                        param.Extensions["default"] = defaultValueAttribute.Value;
                    }
                }
            }
        }
    }
}
