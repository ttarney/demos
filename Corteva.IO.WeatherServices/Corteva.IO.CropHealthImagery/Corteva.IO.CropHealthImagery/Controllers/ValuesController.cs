//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Corteva.IO.SwaggerSupport.Support;
//using Microsoft.AspNetCore.Mvc;
//using Swashbuckle.AspNetCore.Annotations;

//namespace Corteva.IO.CropHealthImagery.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ValuesController : ControllerBase
//    {
//        // GET api/values
//        [HttpGet]
//        //[SwaggerOperation(
//        //        Summary = "Creates a new product",
//        //        Description = "Requires admin privileges",
//        //        OperationId = "CreateProduct",
//        //        Tags = new[] { "Purchase", "Products" }
//        //)]
      
//        public ActionResult<IEnumerable<string>> Get()
//        {
//            return new string[] { "value1", "value2" };
//        }

//        // GET api/values/5
//        [HttpGet("{id}/{otherid}")]
       
//        [SwaggerDefaultValue("id", 177)]
//        [SwaggerDefaultValue("otherid", "foo")]
//        public ActionResult<string> Get(
//                                     [SwaggerParameter("Id of the value to be returned" )] int id,
//                                     [SwaggerParameter("other Id of the value to be returned")] string otherid)
//        {
//            return id.ToString() + ":" + otherid;
//        }

//        // POST api/values
//        //[HttpPost]
//        //public void Post([FromBody] string value)
//        //{
//        //}

//        [HttpPost]
       
//        public ActionResult<int> Post([FromBody] Foo value)
//        {
//            return value.MyIntProperty;
//        }

//        // PUT api/values/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/values/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }

   
//}
