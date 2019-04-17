using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corteva.IO.ImageryServices.Models;
using Corteva.IO.ImageryServices.Patterns;
using Corteva.IO.ImageryServices.Services;
using Corteva.IO.Swagger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Corteva.IO.ImageryServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropHealthController : ControllerBase
    {

        private ICropHealthImageryService _cropHealthImageryService;
        public CropHealthController(ICropHealthImageryService cropHealthImageryService)
        {
            _cropHealthImageryService = cropHealthImageryService;
        }

        [HttpGet("{fieldkey}")]
        [SwaggerDefaultValue("fieldkey", "221a93b9-cbd6-4610-b0a8-a8df3f909bca")]
        public async Task<IActionResult> GetScenes(Guid fieldkey)
        {
            string scenesAsString = _cropHealthImageryService.GetScenes(fieldkey);

            List<SceneModelRepresentation> models = SceneModelFactory.CreateAll(scenesAsString);

            SceneModelRepresentations represenatations = new SceneModelRepresentations
            {
                FieldKey = fieldkey,
                ServiceModels = models
            };
            return Ok(represenatations);
        }

        [HttpGet("{fieldkey}/raw")]
        [SwaggerDefaultValue("fieldkey", "221a93b9-cbd6-4610-b0a8-a8df3f909bca")]
        public async Task<IActionResult> GetScenesRaw(Guid fieldkey)
        {
            string scenesAsString = _cropHealthImageryService.GetScenes(fieldkey);
            List<SceneModelRepresentation> models = SceneModelFactory.CreateAll(scenesAsString);
            SceneModelRepresentations represenatations = new SceneModelRepresentations();
            represenatations.FieldKey = fieldkey;
            represenatations.ServiceModels = models;
            return Ok(represenatations);
        }

        [HttpGet("{scenekey}")]
        [SwaggerDefaultValue("scenekey", "221a93b9-cbd6-4610-b0a8-a8df3f909bca")]
        public async Task<IActionResult> GetSceneByScenekey(Guid scenekey)
        {
            //string scenesAsString = _cropHealthImageryService.GetScenes(scenekey);


            //List<SceneModelRepresentation> models = SceneModelFactory.CreateAll(scenesAsString);
            //return Ok(models);
            return Ok("foo");
        }
    }
}