using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace AgentOrange.Models.Data
{
    public class JsonDataContext
    {
        public IList<Agent> gobjAgents = new List<Agent>();
        public IList<Customer> gobjCustomers = new List<Customer>();
        public string agentDataFile = @"C:\Projects\AgentOrangeZest\AgentOrange.Models\Data\agents.json";

        // read in both files full of data
        public IList<Agent> AgentData()
        {
            JObject agentObject = new JObject();
            // do things
            using (StreamReader reader = new StreamReader(agentDataFile))
            {
                var json = reader.ReadToEnd();
                JArray jArray = JArray.Parse(json) as JArray;

                gobjAgents = jArray.Select(x => new Agent
                {
                    Id = (int)x["_id"],
                    Name = (string)x["name"],
                    StreetAddress = (string)x["address"],
                    City = (string)x["city"],
                    State = (string)x["state"],
                    ZipCode = (string)x["zipCode"],
                    Tier = (int)x["tier"],
                    PhonePrimary = (string)x["phone"]["primary"],
                    PhoneMobile = (string)x["phone"]["mobile"]
                }).ToList();

            }
            return gobjAgents;
        }

        public IList<Customer> CustomerData() {

            var customer = new Customer();
            var ms = new MemoryStream();
            var jCustomer = new DataContractJsonSerializer(typeof(Customer));
            return gobjCustomers;
        }


        // magic
    }
}
