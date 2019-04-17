using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corteva.IO.SwaggerSupport.Support
{

    [SwaggerSchemaFilter(typeof(FooSchemaFilter))]
    public class Foo
    {
        public string MyProperty { get; set; }
        public int MyIntProperty { get; set; }
    }

}
