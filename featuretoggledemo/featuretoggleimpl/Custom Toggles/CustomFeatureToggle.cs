using FeatureToggle;
using System;
using System.Collections.Generic;
using System.Text;

namespace featuretoggleimpl.Custom_Toggles
{
    public class FeatureOneToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get
            {
                return false;
            }
        }
    }

    public class FeatureTwoToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get
            {
                return true;
            }
        }
    }
}
