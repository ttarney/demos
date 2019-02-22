using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using featuretoggledemo.featuretoggles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace featuretoggledemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureToggleController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetToggleEnabled()
        {
            string message = string.Empty;
            var p = new Printing();

            var s = new Saving();
            try
            {
                if (p.FeatureEnabled)
                {
                    message = "printing enabled : ";
                }
                if (s.FeatureEnabled)
                {
                    message = "saving enabled : ";
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }


            return Ok(message);
        }
    }
}