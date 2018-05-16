using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRabbit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyRabbit.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ILogger<ValuesController> _logger;
        private readonly ApplicationDbContext _db;

        public ValuesController(ILogger<ValuesController> logger, ApplicationDbContext dbContext)
        {
            _db = dbContext;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Calculation> Get()
        {
            _logger.LogInformation("logged request");
            return null;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _logger.LogInformation("logged request 2");
            
            int a = 5;
            int b = 0;

            try
            {
                var c = a / b;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error encountered.", new { a, b });
            }

            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
