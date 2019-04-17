using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;



namespace CustomFormatterDemo.Formatters
{
    #region classdef
    public class VcardOutputFormatter : TextOutputFormatter
    #endregion
    {
        #region ctor
        public VcardOutputFormatter()
        {
            MediaTypeHeaderValue t = new MediaTypeHeaderValue("text/vcard");
            SupportedMediaTypes.Add(t);

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

       

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {

        }
        #endregion

        #region canwritetype
        protected override bool CanWriteType(Type type)
        {
            if (typeof(object).IsAssignableFrom(type)
                || typeof(IEnumerable<object>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        #endregion

        #region writeresponse
        //public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        //{
        //    //IServiceProvider serviceProvider = context.HttpContext.RequestServices;
        //    //var logger = serviceProvider.GetService(typeof(ILogger<VcardOutputFormatter>)) as ILogger;

        //    var response = context.HttpContext.Response;

        //    var buffer = new StringBuilder();
        //    //if (context.Object is IEnumerable<Contact>)
        //    //{
        //    //    foreach (Contact contact in context.Object as IEnumerable<Contact>)
        //    //    {
        //    //        FormatVcard(buffer, contact, logger);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    var contact = context.Object as Contact;
        //    //    FormatVcard(buffer, contact, logger);
        //    //}
        //    return response.WriteAsync(buffer.ToString());
        //}

        
      


        #endregion
    }
}