using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace FeatureFlagDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerFeatureController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public BeerFeatureController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_configuration.GetSection("FeatureManagement").GetChildren());
        }

        [HttpPut]
        public async Task<IActionResult> Update(string featureFlag, bool enabled)
        {
            var filePath = Path.Combine(@"C:\projects\repos\github\demos\FeatureFlagDemoAPI\FeatureFlagDemoAPI", "appSettings.json");

            string appSettingsJson = System.IO.File.ReadAllText(filePath);
            JToken appSettingsJsonObject = JObject.Parse(appSettingsJson);
            JToken? featureFlagToken = appSettingsJsonObject.SelectToken($"FeatureManagement.{featureFlag}");
            if (featureFlagToken != null)
            {
                featureFlagToken.Replace(enabled);
            }
            
            System.IO.File.WriteAllText(filePath, appSettingsJsonObject.ToString());
            
            return Ok($"{featureFlag} update to {enabled}");
        }
    }
}
