using Corteva.IO.ImageryServices.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Corteva.IO.ImageryServices.Patterns
{
    public static class SceneModelFactory
    {
        public static List<SceneModelRepresentation> Create(string json)
        {
            SceneModelRepresentation scene = new SceneModelRepresentation();
            List<SceneModelRepresentation> models = new List<SceneModelRepresentation>();
            JToken jtoken = JToken.Parse(json);
            JArray jArray = JArray.Parse(jtoken.ToString());
            foreach (JToken jt in jArray)
            {
                SceneModelRepresentation sceneModel = new SceneModelRepresentation();
                sceneModel.CloudCover = jt["cloud_cover"].Value<double>();
                sceneModel.SceneKey = jt["aoi_scene_key"].Value<string>();
                sceneModel.Acquired = jt["acquired"].Value<DateTime>();
                models.Add(sceneModel);
            }
            return models;
        }

        public static List<SceneModelRepresentation> CreateAll(string json)
        {
            SceneModelRepresentation scene = new SceneModelRepresentation();
            List<SceneModelRepresentation> models = new List<SceneModelRepresentation>();
            JToken jtoken = JToken.Parse(json);
            JArray jArray = JArray.Parse(jtoken.ToString());
            foreach (JToken jt in jArray)
            {
                SceneModelRepresentation sceneModel = new SceneModelRepresentation();
                sceneModel.CloudCover = jt["cloud_cover"].Value<double>();
                sceneModel.SceneKey = jt["aoi_scene_key"].Value<string>();
                sceneModel.Acquired = jt["acquired"].Value<DateTime>();
                // attributes
                sceneModel.GapScore = jt["attributes"]["chi_gap_score"] != null ? jt["attributes"]["chi_gap_score"].Value<string>() : string.Empty;
                sceneModel.VarScore = jt["attributes"]["chi_var_score"] != null ? jt["attributes"]["chi_var_score"].Value<string>() : string.Empty;
                sceneModel.ColorRamp = jt["attributes"]["chi_color_ramp"] != null ? jt["attributes"]["chi_color_ramp"].Value<string>() : string.Empty;
                sceneModel.LowAreaScore = jt["attributes"]["chi_low_area_score"] != null ? jt["attributes"]["chi_low_area_score"].Value<string>() : string.Empty;
                sceneModel.EntropyScore = jt["attributes"]["chi_entropy_score"] != null ? jt["attributes"]["chi_entropy_score"].Value<string>() : string.Empty;
                sceneModel.SceneProducts = new List<SceneProductRepresentation>();
                foreach (JToken productJt in jt["products"])
                {
                    SceneProductRepresentation sceneProduct = new SceneProductRepresentation();
                    sceneProduct.Date = productJt["date"] != null ? productJt["date"].Value<string>() : string.Empty;
                    sceneProduct.Uri = productJt["uri"] != null ? productJt["uri"].Value<string>() : string.Empty;
                    sceneProduct.Modified = productJt["modified"] != null ? productJt["modified"].Value<string>() : string.Empty;
                    sceneProduct.Statsuri = productJt["stats_uri"] != null ? productJt["stats_uri"].Value<string>() : string.Empty;
                    sceneProduct.Type = productJt["type"] != null ? productJt["type"].Value<string>() : string.Empty;


                    //    SceneProductBoundsRepresenation sceneProductBounds = new SceneProductBoundsRepresenation();
                    //    sceneProductBounds.X = productJt["bounds"][0].Value<string>();
                    //    sceneProductBounds.Y = productJt["bounds"][1].Value<string>();
                    //    sceneProductBounds.XX = productJt["bounds"][2].Value<string>();
                    //    sceneProductBounds.YY = productJt["bounds"][3].Value<string>();
                    //    sceneProduct.SceneBounds = sceneProductBounds;
                        sceneModel.SceneProducts.Add(sceneProduct);
                    //
                }

                    models.Add(sceneModel);
            }
            return models;
        }
    }
}
