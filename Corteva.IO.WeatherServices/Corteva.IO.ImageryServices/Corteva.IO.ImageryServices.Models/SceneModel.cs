using Corteva.IO.ImageryServices.Models.HypermediaLinks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebApi.Hal;

namespace Corteva.IO.ImageryServices.Models
{

    public class SceneModelRepresentations : Representation
    {
        public List<SceneModelRepresentation> ServiceModels { get; set; }
        
        [JsonIgnore]
        public Guid FieldKey { get; set; }
        public override string Href
        {
            get
            {
                return SceneModelLinks.GetScenesLink(FieldKey.ToString()).CreateLink(new
                {
                    id = FieldKey
                }).Href;
            }
        }

        protected override void CreateHypermedia()
        {

            if (ServiceModels != null && ServiceModels.Count > 0)
            {

                foreach (SceneModelRepresentation smr in ServiceModels)
                {
                    Links.Add(SceneModelLinks.GetSceneLink(smr.SceneKey).CreateLink(new { scenekey = smr.SceneKey, smr }));
                }
            }
        }

    }
    public class SceneModelRepresentation : Representation
    {
        public string SceneKey { get; set; }

        public DateTime Acquired { get; set; }

        public double CloudCover { get; set; }

        public string GapScore { get; set; }
        public string VarScore { get; set; }
        public string ColorRamp { get; set; }
        public string LowAreaScore { get; set; }
        public string EntropyScore { get; set; }

        public List<SceneProductRepresentation> SceneProducts  { get; set; }

        public override string Rel
        {
            get { return SceneModelLinks.GetSceneLink(SceneKey).Rel; }
        }

        public override string Href
        {
            get
            {
                return SceneModelLinks.GetProductLink(SceneKey).CreateLink(new
                {
                    id = SceneKey
                }).Href;
            }
        }

        protected override void CreateHypermedia()
        {

            if (SceneProducts != null && SceneProducts.Count > 0)
            {

                foreach (SceneProductRepresentation spr in SceneProducts)
                {
                    Links.Add(SceneModelLinks.GetProductLink("product").CreateLink(new { scenekey = "product", spr }));
                }
            }
        }

    }

    public class SceneProductRepresentation : Representation
    {

        public string Uri { get; set; }
        public string Modified { get; set; }
        public string Statsuri { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }

        public SceneProductBoundsRepresenation SceneBounds { get; set; }
        public override string Rel
        {
            get { return SceneModelLinks.GetSceneLink("foo").Rel; }
        }

        public override string Href
        {
            get
            {
                return SceneModelLinks.GetSceneLink("foo").CreateLink(new
                {
                    id = Uri
                }).Href;
            }
        }
    }

    public class SceneProductBoundsRepresenation : Representation
    {
        public string X { get; set; }
        public string Y { get; set; }
        public string XX { get; set; }
        public string YY { get; set; }
    }
}
