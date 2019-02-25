using FeatureToggle;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace featuretoggledemo
{
    public class BaseController : ControllerBase
    {
        protected IFeatureToggle _rootFeatureToggle;
        protected bool APIFeatureIsEnabled()
        {
            return _rootFeatureToggle.FeatureEnabled;
        }
    }
}
