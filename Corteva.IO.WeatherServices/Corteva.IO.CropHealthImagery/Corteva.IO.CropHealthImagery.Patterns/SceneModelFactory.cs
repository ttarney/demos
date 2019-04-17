using Corteva.IO.CropHealthImagery.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Corteva.IO.CropHealthImagery.Patterns
{
    public static class SceneModelFactory
    {
        public static List<SceneModel> Create(string json)
        {
            SceneModel scene = new SceneModel();
            List<SceneModel> models = new List<SceneModel>();
            JToken jtoken = JToken.Parse(json);
            JArray jArray = JArray.Parse(jtoken.ToString());
            foreach (JToken jt in jArray)
            {
                SceneModel sceneModel = new SceneModel();
                sceneModel.CloudCover = jt["cloud_cover"].Value<double>();
                sceneModel.SceneKey = jt["aoi_scene_key"].Value<string>();
                sceneModel.Acquired = jt["acquired"].Value<DateTime>();
                models.Add(sceneModel);
            }
            return models;
        }
    }
}
