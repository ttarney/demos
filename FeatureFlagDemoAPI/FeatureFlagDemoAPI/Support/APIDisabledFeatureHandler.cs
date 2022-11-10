using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureFlagDemoAPI.Support
{

    public class APIDisabledFeatureHandler : IDisabledFeaturesHandler
    {
        public Task HandleDisabledFeatures(IEnumerable<string> features, ActionExecutingContext context)
        {
            context.Result = new ServiceUnavailableResult("Feature Not Enabled"); // generate a 403
            return Task.CompletedTask;
        }
    }

    public class ServiceUnavailableResult : IActionResult
    {
        private readonly string _message = string.Empty;
        public ServiceUnavailableResult() { }

        public ServiceUnavailableResult(string message)
        {
            _message = message;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult objectResult = null;

            if (string.IsNullOrEmpty(_message))
            {
                objectResult = new ObjectResult("Service Unavailable");
            }
            else
            {
                objectResult = new ObjectResult($"Service Unavailable: {_message}");
            }

            objectResult.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await objectResult.ExecuteResultAsync(context);

        }
    }
}
