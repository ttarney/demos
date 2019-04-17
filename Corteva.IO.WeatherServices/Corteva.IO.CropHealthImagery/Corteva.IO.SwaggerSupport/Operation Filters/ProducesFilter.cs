using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Corteva.IO.SwaggerSupport
{
    public class ProducesFilter : IOperationFilter
    {
       

        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Produces.Clear();

            ////operation.produces.Add(@"application/vnd.Pioneer.FieldTools.CropHealth.json");
            ////operation.produces.Add(@"application/vnd.Pioneer.FieldTools.CropHealth+raw.json");
            operation.Produces.Add(@"application/json");
            operation.Produces.Add(@"application/hal+json");

        }
    }
}
