using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureToggle;
using featuretoggledemo.featuretoggles;
using featuretoggleimpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace featuretoggledemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeatureToggleController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetToggleEnabled(string featureToggle)
        {
            SimpleFeatureToggle toggle = null;
            bool isEnabled = false;
            string message = string.Empty;

            switch (featureToggle)
            {
                case "printing":
                    toggle = new Printing();
                    break;
                case "saving":
                    toggle = new Printing();
                    break;
                default:
                    break;
            }
            return Ok(toggle.FeatureEnabled);
        }
    }
}