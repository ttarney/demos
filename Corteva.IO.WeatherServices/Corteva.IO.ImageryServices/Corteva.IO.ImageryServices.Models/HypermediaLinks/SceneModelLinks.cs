using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Hal;

namespace Corteva.IO.ImageryServices.Models.HypermediaLinks
{
    public static class SceneModelLinks
    {
        public static Link GetSceneLink(string scenekey)
        {
            return new Link("scenes", "~/crophealth?scenekey={id}", scenekey);
        }

        public static Link GetScenesLink(string fieldKey)
        {
            return new Link("scenes", "~/crophealth/{id}", fieldKey);
        }

        public static Link GetProductLink(string scenekey)
        {
            return new Link("products", "~/crophealth?scenekey={id}", scenekey);
        }

        public static Link GetProductsLink(string fieldKey)
        {
            return new Link("products", "~/crophealth/{id}", fieldKey);
        }
    }
}
