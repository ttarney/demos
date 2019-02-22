using FeatureToggle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace featuretoggledemo.featuretoggles
{
    public class Printing : SimpleFeatureToggle { }
    public class Saving : SimpleFeatureToggle { }

    public class CustomFeatureToggle : IFeatureToggle
    {
        public bool FeatureEnabled
        {
            get
            {
                return true;
            }
        }
    }

    // var toggle = new CustomerFeatureToggle();
    // toggle.ToggleValueProvider = new CustomFeatureToggleProvider();
    // return toggle;
    public class CustomFeatureToggleProvider : IBooleanToggleValueProvider
    {
        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {

            return true;
        }
    }
}
