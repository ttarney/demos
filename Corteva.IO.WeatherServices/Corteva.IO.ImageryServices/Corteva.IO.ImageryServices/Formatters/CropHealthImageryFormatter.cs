using Corteva.IO.ImageryServices.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

using Microsoft.Net.Http.Headers;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Corteva.IO.ImageryServices.Formatters
{
    public class CropHealthImageryFormatter : OutputFormatter

    {
       
        public CropHealthImageryFormatter()
        {
            MediaTypeHeaderValue csv = new MediaTypeHeaderValue("application/vnd.corteva.crophealth+csv");
            SupportedMediaTypes.Add(csv);

            MediaTypeHeaderValue vcard = new MediaTypeHeaderValue("application/vnd.corteva.crophealth+vcard");
            SupportedMediaTypes.Add(vcard);

            MediaTypeHeaderValue detail = new MediaTypeHeaderValue("application/vnd.corteva.crophealth+detail");
            SupportedMediaTypes.Add(detail);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var response = context.HttpContext.Response;
            
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<SceneModelRepresentation>)
            {
                foreach (SceneModelRepresentation sceneModel in context.Object as IEnumerable<SceneModelRepresentation>)
                {
                    // if vcard 
                    if (context.HttpContext.Request.Headers["Accept"] == "application/vnd.corteva.crophealth+vcard")
                    {
                        FormatVcard(buffer, sceneModel);
                    }
                    else if (context.HttpContext.Request.Headers["Accept"] == "application/vnd.corteva.crophealth+csv")
                    {
                        // if csv
                        FormatCSV(buffer, sceneModel);
                    }
                }
            }
            //else
            {
            //    //var contact = context.Object as Contact;
            //    //FormatVcard(buffer, contact, logger);
            }

            response.ContentType = "application/corteva.crophealth.vcard+v1";
            return response.WriteAsync(buffer.ToString());
        }

        private static void FormatVcard(StringBuilder buffer, SceneModelRepresentation sceneModel)
        {
            //buffer.AppendLine("BEGIN:VCARD");
            //buffer.AppendLine("VERSION:2.1");
            //buffer.AppendFormat($"N:{sceneModel.CloudCover};{sceneModel.Acquired}\r\n");
            //buffer.AppendFormat($"FN:{sceneModel.CloudCover} {sceneModel.Acquired}\r\n");
            //buffer.AppendFormat($"UID:{1}\r\n");
            //buffer.AppendLine("END:VCARD");
            
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(SceneModelRepresentation).IsAssignableFrom(type)
                || typeof(IEnumerable<SceneModelRepresentation>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        private void FormatCSV(StringBuilder buffer, SceneModelRepresentation model)
        {
            //string data = string.Format("{0},{1},{2},{3}", Escape(model.SceneKey),
            //    Escape(model.GapScore), Escape(model.VarScore), Escape(model.EntropyScore));
            //buffer.AppendLine(data);
        }

        static char[] _specialChars = new char[] { ',', '\n', '\r', '"' };

        private string Escape(object o)
        {
            if (o == null)
            {
                return "";
            }
            string field = o.ToString();
            if (field.IndexOfAny(_specialChars) != -1)
            {
                // Delimit the entire field with quotes and replace embedded quotes with "".
                return String.Format("\"{0}\"", field.Replace("\"", "\"\""));
            }
            else return field;
        }

    }

}
