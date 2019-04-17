using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolicyServer.Runtime.Client;

namespace Corteva.IO.ImageryServices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyServerRuntimeClient _policyServerClient;
        private readonly IAuthorizationService _authz;

        public PolicyController(PolicyServer.Runtime.Client.IPolicyServerRuntimeClient policyServerClient)
        {
            _policyServerClient = policyServerClient;
            int x = 0;
        }

        [HttpGet]
        //[SwaggerDefaultValue("fieldkey", "221a93b9-cbd6-4610-b0a8-a8df3f909bca")]
        public async Task<IActionResult> GetPolicy()
        {

            return Ok("GetPolicies");
        }


        [HttpGet("check")]
        public async Task<IActionResult> check(string userName)
        {
            
            Claim userClaim = new Claim(ClaimTypes.Name, userName);
            Claim subClaim = new Claim("sub", "1");
           

            ClaimsIdentity temp = new ClaimsIdentity();
            temp.AddClaim(userClaim);
            temp.AddClaim(subClaim);
            ClaimsPrincipal userPrincipal = new ClaimsPrincipal(temp);
            var policyResult = await _policyServerClient.EvaluateAsync(userPrincipal);

            
            foreach (var permission in policyResult.Roles)
            {

                //permission
            }

            foreach (var permission in policyResult.Permissions)
            {
               

                //permission
            }
            //policyResult.Permissions
            //policyResult.Roles;

            return Ok(policyResult);
        }
    }
}