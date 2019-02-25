using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace featuretoggledemo.handlers
{
    public class FeatureFlagRoutingHandler
    {
        private readonly RequestDelegate _next;
        public FeatureFlagRoutingHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers["Accept"] == @"application/vnd.temp.toggle+bronze")
            {
            }
            else if (context.Request.Headers["Accept"] == @"application/vnd.temp.toggle+silver")
            {
                context.Request.Path += "/two";
            }
            await _next.Invoke(context);
        }
    }
}
