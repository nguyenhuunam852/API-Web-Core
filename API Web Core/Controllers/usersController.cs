using API_Web_Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Web_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        // GET: api/<usersController>
        [JwtAuthorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Console.WriteLine(Request.Headers);
            return new string[] { "Ello", "Adu" };
        }

        // GET api/<usersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<usersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<usersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<usersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
