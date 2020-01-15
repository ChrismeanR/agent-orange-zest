using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Description;
using AgentOrange.Models;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        public AgentOrange.Models.Data.JsonDataContext AgentContext = new AgentOrange.Models.Data.JsonDataContext();

        // GET: api/AgentApi
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Agent>))]
        public IEnumerable<Agent> Get()
        {
            return AgentContext.GetAgentData();
        }

        // GET: api/AgentApi/5
        [HttpGet("{id}", Name = "GetAgentById")]
        [ResponseType(typeof(Agent))]
        public Agent Get(int id)
        {
            return AgentContext.GetAgentData(id);
        }

        // POST: api/AgentApi
        [HttpPost]
        [ResponseType(typeof(Agent))]
        public Agent Post([FromBody] Agent agent)
        {
            return AgentContext.CreateAgentData(agent);
        }

        // PUT: api/AgentApi
        [HttpPut]
        [ResponseType(typeof(Agent))]
        public Agent Put([FromBody] Agent agent)
        {
            return AgentContext.UpdateAgentData(agent);
        }

    }
}
