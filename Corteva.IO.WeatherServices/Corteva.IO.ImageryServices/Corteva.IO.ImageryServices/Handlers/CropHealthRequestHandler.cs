using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Corteva.IO.ImageryServices.Handlers
{

    public class CropHealthImageryHandler
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger _logger;
        //private IApiKeyService _service;

        public CropHealthImageryHandler(RequestDelegate next)//, ILoggerFactory loggerFactory, IApiKeyService service)
        {
            _next = next;
           
            //_service = service
        }

        public async Task Invoke(HttpContext context)
        {
            //  operation.Produces.Add(@"application/vnd.corteva.crophealth+csv");
            // operation.Produces.Add(@"application/vnd.corteva.crophealth+vcard");
            //operation.Produces.Add(@"application/vnd.corteva.crophealth+detail");
            if (context.Request.Headers["Accept"] == @"application/vnd.corteva.crophealth+csv")
            {
                context.Request.Path += "/raw";
                

            }
            //if (context.Request.Headers["Accept"] == @"application/vnd.corteva.crophealth+vcard")
            //{
            //    int x = 0;
            //}
            //if (context.Request.Headers["Accept"] == @"application/vnd.corteva.crophealth+detail")
            //{
            //    int x = 0;
            //}


            await _next.Invoke(context);

            
        }
    }
    public class CropHealthRequestHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Uri baseUri = new Uri(request.RequestUri.AbsoluteUri.Replace(request.RequestUri.PathAndQuery, String.Empty));

            // get the api explorer for this api
           //IApiExplorer apiExplorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();

           // // options support to describe
           // OptionsSupport optionsSupport = new OptionsSupport { BaseUri = baseUri.ToString(), SubDomainDetails = new List<SubDomainDetail>() };

           // // get the configuration
           // DefaultHttpControllerSelector controllerSelector = new DefaultHttpControllerSelector(GlobalConfiguration.Configuration);

            
           // //// build descriptor
           // HttpControllerDescriptor descriptor = controllerSelector.SelectController(request);

           // if (descriptor.ControllerName == "Discovery")
           // {
           // }
           return base.SendAsync(request, cancellationToken);
        }
    }
}
