using FeatureToggle;
using System;
using System.Collections.Generic;
using System.Text;

namespace featuretoggleimpl.Custom_Toggles
{
    public interface IFeatureOneToggle : IFeatureToggle
    {

    }

    public interface IFeatureTwoToggle : IFeatureToggle
    {

    }
    public class FeatureOneToggle : IFeatureOneToggle
    {
        public bool FeatureEnabled
        {
            get
            {
                return false;
            }
        }
    }

    public class FeatureTwoToggle : IFeatureTwoToggle
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
