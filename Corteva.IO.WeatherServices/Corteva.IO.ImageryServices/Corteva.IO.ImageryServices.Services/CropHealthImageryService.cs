using System;
using System.Net.Http;

namespace Corteva.IO.ImageryServices.Services
{
    public interface ICropHealthImageryService
    {
        string GetScenes(Guid fieldkey);
    }
    public class CropHealthImageryService : ICropHealthImageryService
    {

        private HttpClient _orionHttpClient = null;
        public CropHealthImageryService(HttpClient orionHttpClient)
        {
            _orionHttpClient = orionHttpClient;
        }

        public string GetScenes(Guid fieldkey)
        {
            string content = string.Empty;
            string resourcePath = string.Format
                (
                    @"{0}/products?products=ps_chi_thumb_small,ps_tc_detail,ps_chi_detail",
                    fieldkey.ToString()
                );

            try
            {
                var response = _orionHttpClient.GetAsync("remote-sensing/aois/" + resourcePath).Result;
                if (response.IsSuccessStatusCode)
                {
                    content = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    // error
                }
            }
            catch (Exception ex)
            {

            }
            return content;
        }
    }
}
