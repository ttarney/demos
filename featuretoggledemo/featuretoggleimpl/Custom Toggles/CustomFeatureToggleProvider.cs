using FeatureToggle;
using System;
using System.Collections.Generic;
using System.Text;

namespace featuretoggleimpl.Custom_Toggles
{
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
