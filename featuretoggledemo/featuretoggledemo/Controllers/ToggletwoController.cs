
using System.Threading.Tasks;
using FeatureToggle;
using featuretoggleimpl.Custom_Toggles;
using Microsoft.AspNetCore.Mvc;

namespace featuretoggledemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToggletwoController : BaseController
    {
        public ToggletwoController(IFeatureTwoToggle toggle)
        {
            _rootFeatureToggle = toggle;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("basic: " + _rootFeatureToggle.FeatureEnabled);
        }

        [HttpGet("two")]
        public async Task<IActionResult> Get2()
        {
            return Ok("silver: " + _rootFeatureToggle.FeatureEnabled);
        }
    }
}
