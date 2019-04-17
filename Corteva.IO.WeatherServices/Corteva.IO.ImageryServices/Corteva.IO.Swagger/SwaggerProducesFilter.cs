using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corteva.IO.Swagger
{
    public class ProducesFilter : IOperationFilter
    {
        //public void Apply(Operation operation, SchemaRegistry schemaRegistry, System.Web.Http.Description.ApiDescription apiDescription)
        //{
        //    operation.produces.Clear();
        //    operation.produces.Add(@"application/hal+json");
        //    operation.produces.Add(@"application/json");
        //}

        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Produces.Clear();
            operation.Produces.Add(@"application/hal+json");
            operation.Produces.Add(@"application/vnd.corteva.crophealth+csv");
            operation.Produces.Add(@"application/vnd.corteva.crophealth+vcard");
            operation.Produces.Add(@"application/vnd.corteva.crophealth+detail");
            

        }
    }
}
