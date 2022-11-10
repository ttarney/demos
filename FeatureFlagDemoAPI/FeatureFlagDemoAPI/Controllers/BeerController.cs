using FeatureFlagDemoAPI.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FeatureFlagDemoAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [FeatureGate(FeatureFlags.BeersAPI)]
    public class BeerController : ControllerBase
    {
        private readonly IFeatureManager _featureManager;

        
        public BeerController(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }
       
        [HttpGet]
        [FeatureGate(RequirementType.All, FeatureFlags.BeersAPI, FeatureFlags.BeersAPIGet)]
        public async Task<IActionResult> Get()
        {
            IActionResult result = null;
            if (await _featureManager.IsEnabledAsync(FeatureFlags.AllBeers))
            {
                result = Ok("all beers");
            }
            else
            {
                result = Ok("one beer");
            }
            return result;
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok("a beer");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {

            return Ok("added beer");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            return Ok("updated beer");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok("deleted a beer");
        }
    }
}
