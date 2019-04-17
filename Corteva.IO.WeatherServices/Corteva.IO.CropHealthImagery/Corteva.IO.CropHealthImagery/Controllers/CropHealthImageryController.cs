using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Corteva.IO.CropHealthImagery.Models;
using Corteva.IO.CropHealthImagery.Patterns;
using Corteva.IO.CropHealthImagery.Services;
using Corteva.IO.SwaggerSupport.Support;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Corteva.IO.CropHealthImagery.Controllers
{
    /// <summary>
    /// 
    ///// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CropHealthImageryController : ControllerBase
    {
       
        private ICropHealthImageryService _cropHealthImageryService;
        public CropHealthImageryController(ICropHealthImageryService cropHealthImageryService)
        {
            _cropHealthImageryService = cropHealthImageryService;
        }

        [HttpGet("{fieldkey}")]
        [SwaggerDefaultValue("fieldkey", "221a93b9-cbd6-4610-b0a8-a8df3f909bca")]
        public async Task<IActionResult> GetScenes(Guid fieldkey)
        {
            string scenesAsString = _cropHealthImageryService.GetScenes(fieldkey);

            List<SceneModel> models = SceneModelFactory.Create(scenesAsString);
            return Ok(models);
        }


    }
}