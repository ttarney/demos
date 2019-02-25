using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using featuretoggleimpl.Custom_Toggles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace featuretoggledemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeatureOneController : BaseController
    {
        public FeatureOneController()
        {
            // inject it here if we can figure it out
            _rootFeatureToggle = new FeatureOneToggle();
        }

        [HttpGet]
        public async Task<IActionResult> GetAPIEnabled()
        {
            return Ok(_rootFeatureToggle.FeatureEnabled);
        }
    }
}