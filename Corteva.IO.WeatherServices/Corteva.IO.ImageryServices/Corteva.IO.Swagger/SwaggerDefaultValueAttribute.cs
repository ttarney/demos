using System;

namespace Corteva.IO.Swagger
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerDefaultValueAttribute : Attribute
    {
        public SwaggerDefaultValueAttribute(string parameterName, object value)
        {
            this.ParameterName = parameterName;
            this.Value = value;
        }

        public string ParameterName { get; set; }

        public object Value { get; set; }
    }
}
