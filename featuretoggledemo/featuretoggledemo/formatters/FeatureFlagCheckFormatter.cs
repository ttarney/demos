using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace featuretoggledemo.formatters
{
    public class FeatureFlagCheckFormatter : OutputFormatter
    {
        public FeatureFlagCheckFormatter()
        {
            MediaTypeHeaderValue bronze = new MediaTypeHeaderValue("application/vnd.temp.toggle+bronze");
            SupportedMediaTypes.Add(bronze);

            MediaTypeHeaderValue silver = new MediaTypeHeaderValue("application/vnd.temp.toggle+silver");
            SupportedMediaTypes.Add(silver);

            MediaTypeHeaderValue gold = new MediaTypeHeaderValue("application/vnd.temp.toggle+gold");
            SupportedMediaTypes.Add(gold);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            throw new NotImplementedException();
        }
    }
}
