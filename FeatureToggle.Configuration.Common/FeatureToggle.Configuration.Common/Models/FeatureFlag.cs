using System;
using System.Collections.Generic;

namespace FeatureToggle.Configuration.Common.Models
{
    public partial class FeatureFlag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
