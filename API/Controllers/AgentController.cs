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
        public Agent gobjAgent;

        // GET: api/AgentApi
        [HttpGet]
        public IEnumerable<Agent> Get()
        {
            gobjContext = AgentContext.GetAgentData();
            return gobjContext;
        }

        // GET: api/AgentApi/5
        [HttpGet("{id}", Name = "GetAgentById")]
        public Agent Get(int id)
        {
            gobjAgent = AgentContext.GetAgentData(id);
            return gobjAgent;
        }

        // POST: api/AgentApi
        [HttpPost]
        public void Post([FromBody] Agent agent)
        {
            gobjAgent = AgentContext.UpdateAgentData(agent.Id, agent);
        }
        // POST: api/AgentApi
        [HttpPut]
        public Agent Put([FromBody] Agent agent)
        {
            return gobjAgent = AgentContext.UpdateAgentData(agent.Id, agent);
        }

    }
}
