using System;
using System.Collections.Generic;
using System.Text;

namespace Corteva.IO.CropHealthImagery.Models
{
    public class SceneModel
    {
        public string SceneKey { get; set; }

        /// <summary>
        /// ??? Not sure at this time what this represents
        /// </summary>
        public DateTime Acquired { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public List<ICropHealthProduct> Products { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public ICropHealthAttribute Attributes { get; set; }

        /// <summary>
        /// Represents cloud_cover from Encirca
        /// </summary>
        public double CloudCover { get; set; }
    }

}
