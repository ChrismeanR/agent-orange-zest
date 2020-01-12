using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgentOrange.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        public AgentOrange.Models.Data.JsonDataContext AgentContext = new AgentOrange.Models.Data.JsonDataContext();
        public IList<Agent> gobjContext;

        // GET: api/AgentApi
        [HttpGet]
        public IEnumerable<Agent> Get()
        {
            //return new string[] { "value1", "value2" };
            gobjContext = AgentContext.AgentData();
            return gobjContext;
        }

        // GET: api/AgentApi/5
        [HttpGet("{id}", Name = "GetAgentById")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AgentApi
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/AgentApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
