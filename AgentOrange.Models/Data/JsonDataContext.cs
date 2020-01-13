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
        public Agent gobjAgent = new Agent();
        public IList<Customer> gobjCustomers = new List<Customer>();
        public Customer gobjCustomer = new Customer();

        public const string agentDataFile = @"C:\Projects\AgentOrangeZest\AgentOrange.Models\Data\agents.json";
        public string customerDataFile = @"C:\Projects\AgentOrangeZest\AgentOrange.Models\Data\customers.json";



        // read in both files full of data
        public IList<Agent> GetAgentData()
        {
            JObject agentObject = new JObject();
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
                    PhoneNumbers = x.Select(y => new Phone
                    {
                        Primary = (string)x["phone"]["primary"],
                        Mobile = (string)x["phone"]["mobile"]
                    }).FirstOrDefault(),

                }).ToList();

            }
            return gobjAgents;
        }
        public Agent GetAgentData(int id)
        {
            JObject agentObject = new JObject();
            using (StreamReader reader = new StreamReader(agentDataFile))
            {
                var json = reader.ReadToEnd();
                JArray jArray = JArray.Parse(json) as JArray;

                gobjAgent = jArray.Select(x => new Agent
                {
                    Id = (int)x["_id"],
                    Name = (string)x["name"],
                    StreetAddress = (string)x["address"],
                    City = (string)x["city"],
                    State = (string)x["state"],
                    ZipCode = (string)x["zipCode"],
                    Tier = (int)x["tier"],
                    PhoneNumbers = x.Select(y => new Phone
                    {
                        Primary = (string)x["phone"]["primary"],
                        Mobile = (string)x["phone"]["mobile"]
                    }).FirstOrDefault(),

                }).Where(x => x.Id == id).FirstOrDefault();

            }
            return gobjAgent;
        }

        public IList<Customer> GetCustomerData()
        {
            var customer = new Customer();

            using (StreamReader reader = new StreamReader(customerDataFile))
            {
                var json = reader.ReadToEnd();
                JArray jArray = JArray.Parse(json) as JArray;

                gobjCustomers = jArray.Select(x =>
                {
                    return new Customer
                    {
                        Id = (int)x["_id"],
                        AgentId = (int)x["agent_id"],
                        CustomerGuid = (Guid)x["guid"],
                        IsActive = (bool)x["isActive"],
                        Balance = (string)x["balance"],
                        Age = (int)x["age"],
                        EyeColor = (string)x["eyeColor"],
                        Name = x.Select(y => new Person
                        {
                            FirstName = (string)x["name"]["first"],
                            LastName = (string)x["name"]["last"]
                        }).FirstOrDefault(),
                        Company = (string)x["company"],
                        Email = (string)x["email"],
                        Phone = (string)x["phone"],
                        Address = (string)x["address"],
                        Registered = (DateTime)x["registered"], // this is string version of date time stamp
                        Latitude = (string)x["latitude"],
                        Longitude = (string)x["longitude"],
                        Tags = JToken.Parse(x["tags"].ToString()).ToObject<string[]>()
                    };
                }).ToList();
            }

            return gobjCustomers;

        }

        public Customer GetCustomerData(int id)
        {
            var customer = new Customer();

            using (StreamReader reader = new StreamReader(customerDataFile))
            {
                var json = reader.ReadToEnd();
                JArray jArray = JArray.Parse(json) as JArray;

                gobjCustomer = jArray.Select(x =>
                {
                    return new Customer
                    {
                        Id = (int)x["_id"],
                        AgentId = (int)x["agent_id"],
                        CustomerGuid = (Guid)x["guid"],
                        IsActive = (bool)x["isActive"],
                        Balance = (string)x["balance"],
                        Age = (int)x["age"],
                        EyeColor = (string)x["eyeColor"],
                        Name = x.Select(y => new Person
                        {
                            FirstName = (string)x["name"]["first"],
                            LastName = (string)x["name"]["last"]
                        }).FirstOrDefault(),
                        Company = (string)x["company"],
                        Email = (string)x["email"],
                        Phone = (string)x["phone"],
                        Address = (string)x["address"],
                        Registered = (DateTime)x["registered"], // this is string version of date time stamp
                        Latitude = (string)x["latitude"],
                        Longitude = (string)x["longitude"],
                        Tags = JToken.Parse(x["tags"].ToString()).ToObject<string[]>()
                    };
                }).Where(x => x.Id == id).FirstOrDefault();
            }

            return gobjCustomer;

        }

        public Agent UpdateAgentData(int? id, Agent? agent)
        {
            // serialize all updated agent info, store it into json file
            return agent;
        }
        public Customer UpdateCustomerData(int id, Customer? customer)
        {
            Customer context = GetCustomerData(id);
            // compare obj coming in for differences, update those differences

            if (customer == null)
            {
                customer = context;
            }

            // File.WriteAllText(customerDataFile, objCust);
            using (StreamReader reader = new StreamReader(customerDataFile))
            {
                JArray jArray = new JArray();
                var objCust = JsonConvert.SerializeObject(customer);
                JObject jObject = new JObject();

                var json = reader.ReadToEnd();

                using (StreamWriter file = File.CreateText(json))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    jObject.WriteTo(writer);
                }
            }
            // serialize all updated customer values, store back into json file
            return customer;

        }

        public void DeleteCustomerData(int id)
        {
            Customer customer = GetCustomerData(id);
            gobjCustomers = GetCustomerData();
            
            JArray jArray = new JArray();
            using (StreamReader reader = new StreamReader(customerDataFile))
            {
                // read the file
                var jsonFileData = reader.ReadToEnd();
                string convertedCustomer = JsonConvert.SerializeObject(customer, Formatting.Indented);
                foreach (var cust in gobjCustomers)
                {

                }


                JObject jObject = new JObject(customer);

                using (StreamWriter file = File.CreateText(jsonFileData))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    jObject.WriteTo(writer);
                }
            }

        }
        public Customer CreateCustomer(Customer customer)
        {
            var obj = new Customer();
            // get all agent & customer IDs, 
            var allIds = GetCustomerData().Select(x => x.Id);
            var allAgentId = GetCustomerData().Select(x => x.Id);

            // find largest number, add +1 to it
            Random randomInt = new Random();
            int newId = 0;
            int newAgentId = 0;
            foreach (var item in allIds)
            {
                newId = randomInt.Next();
                if (item == newId)
                {
                    newId = randomInt.Next();
                }
            }

            gobjCustomers.Add(new Customer
            {
                Id = newId, // need new id
                Address = customer.Address,
                Age = customer.Age,
                AgentId = customer.AgentId, // need new Id
                Balance = customer.Balance,
                Company = customer.Company,
                CustomerGuid = new Guid(),
                Email = customer.Email,
                EyeColor = customer.EyeColor,
                IsActive = customer.IsActive,
                Latitude = customer.Latitude,
                Longitude = customer.Longitude,
                Name = new Person { FirstName = customer.Name.FirstName, LastName = customer.Name.LastName },
                Phone = customer.Phone,
                Registered = DateTime.Parse(customer.Registered.ToString("f")),
                Tags = customer.Tags

            });

            return gobjCustomers.Where(x => x.Id == newId).FirstOrDefault();
        }
        // magic
    }
}
