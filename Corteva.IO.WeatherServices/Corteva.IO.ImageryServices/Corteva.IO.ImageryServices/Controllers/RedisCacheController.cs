using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Corteva.IO.ImageryServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisCacheController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cacheKey = "TheTime";
            var existingTime = _distributedCache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(existingTime))
            {
                return Ok("Fetched from cache : " + existingTime);
            }
            else
            {
                existingTime = DateTime.UtcNow.ToString();
                _distributedCache.SetString(cacheKey, existingTime);
                return Ok("Added to cache : " + existingTime);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            
            var existingEmployee = _distributedCache.GetString(employee.Id);
            if (!string.IsNullOrEmpty(existingEmployee))
            {
                return Ok("Fetched from cache : " + existingEmployee);
            }
            else
            {
                _distributedCache.SetString(employee.Id, JsonConvert.SerializeObject(employee));
               
                return Ok("Added to cache : " + employee);
            }
           
            return Ok(employee);
        }

        [HttpGet("ttl")]
        public async Task<IActionResult> GetTTL(string key)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:32773");
            //ConnectionMultiplexer.

            foreach (var x in redis.GetEndPoints())
            {
                int xx = 0;
            }
            
            var db = redis.GetDatabase(0);
            var timeToLive = db.KeyTimeToLive("001");
            return Ok(timeToLive);
        }
    }


    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Employee(string EmployeeId, string Name, int Age)
        {
            this.Id = EmployeeId;
            this.Name = Name;
            this.Age = Age;
        }
    }
}