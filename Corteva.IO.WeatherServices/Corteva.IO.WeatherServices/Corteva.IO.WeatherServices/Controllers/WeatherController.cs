using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Corteva.IO.WeatherServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        // GET api/values
        [HttpGet("everywhere")]
        public async Task<IActionResult> GetCurrentWeatherForEverywhere()
        {
            return Ok("It's beautful");
        }

        [HttpGet("{zipcode}")]
        public async Task<IActionResult> GetCurrentWeatherForZipCode(string zipcode)
        {

            HttpClient zipCodeClient = new HttpClient()
            {
                BaseAddress = new Uri(@"https://www.zipcodeapi.com/rest/w9bHnPj7BInZmfRTNBGBZpd3TkbvZUcrJaZe0F3clOALVr2DxAzLMDafkuPut8hs/info.json/" + zipcode)
            };
            string content = string.Empty;

            var response = zipCodeClient.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                content = response.Content.ReadAsStringAsync().Result;
            }
            return Ok("It's beautful in " + content);
        }
    }
}
