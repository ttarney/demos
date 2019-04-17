using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Corteva.IO.SwaggerSupport.Support
{
    public class FooSchemaFilter : ISchemaFilter
    {
        //public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        //{
        //    schema.Example = new OpenApiObject
        //    {
        //        ["Id"] = new OpenApiInteger(1),
        //        ["Description"] = new OpenApiString("An awesome product")
        //    };
        //}

        public void Apply(Schema schema, SchemaFilterContext context)
        {
            schema.Example = new Foo
            {
                MyIntProperty = 100,
                MyProperty = "An awesome product"
            };
        }
    }
}
