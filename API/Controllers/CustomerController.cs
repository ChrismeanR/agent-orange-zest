using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgentOrange.Models;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public AgentOrange.Models.Data.JsonDataContext CustomerContext = new AgentOrange.Models.Data.JsonDataContext();
        public IList<Customer> gobjListCustomer;
        public Customer gobjCustomer;

        // GET: api/Customer
        [HttpGet]
        public IEnumerable<AgentOrange.Models.Customer> Get()
        {
            gobjListCustomer = CustomerContext.GetCustomerData();
            
            return gobjListCustomer;
        }

        // GET: api/Customer/5
        [HttpGet("{id}", Name = "GetCustomerById")]
        public Customer Get(int id)
        {
            gobjListCustomer = CustomerContext.GetCustomerData();
            var customer = gobjListCustomer.Select(item => item).Where(x=> x.Id ==id).FirstOrDefault();
            return customer;
        }

        // POST: api/Customer
        [HttpPost]
        public void Post([FromBody] Customer customer)
        {
            gobjCustomer = CustomerContext.UpdateCustomerData(customer.Id, customer);
        }

        // POST: api/Customer
        [HttpPut]
        public void Put([FromBody] Customer customer)
        {
            CustomerContext.UpdateCustomerData(customer.Id, customer); 
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            CustomerContext.DeleteCustomerData(id);
        }
    }
}
