using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HALDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBeers()
        {
            return Ok("All beers");
        }

        [HttpGet("{beerName}", Name = "GetBeerByName")]
        public async Task<IActionResult> GetBeerByName([FromRoute] string beerName)
        {
            return Ok($"a beer: {beerName}");
        }

        [HttpPost]
        public async Task<IActionResult> AddBeer([FromBody] BeerRequest beerRequest)
            {
            return CreatedAtRoute("GetBeerByName",
                  new
                  {
                      name = beerRequest.Name
                  },
                  beerRequest);
        }
    }
}
