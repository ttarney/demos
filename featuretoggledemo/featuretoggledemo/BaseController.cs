using FeatureToggle;
using Microsoft.AspNetCore.Mvc;


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
