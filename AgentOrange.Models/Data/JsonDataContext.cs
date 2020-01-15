using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web.Http;
using static AgentOrange.Models.HelperUtils;

namespace AgentOrange.Models.Data
{
    public class JsonDataContext
    {
        public IList<Agent> gobjAgents = new List<Agent>();
        public Agent gobjAgent = new Agent();
        public IList<Customer> gobjCustomers = new List<Customer>();
        public Customer gobjCustomer = new Customer();

        public const string agentDataFile = @"C:\Projects\AgentOrangeZest\AgentOrange.Models\Data\agents.json";
        public const string customerDataFile = @"C:\Projects\AgentOrangeZest\AgentOrange.Models\Data\customers.json";

        /// <summary>
        /// Get all agents from json file
        /// </summary>
        /// <returns></returns>
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
                    Address = (string)x["address"],
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

        /// <summary>
        /// Get agent info by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                    Address = (string)x["address"],
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

        /// <summary>
        /// Create a new agent, assign an ID that does not already exist
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public Agent CreateAgentData(Agent agent)
        {
            int newId = 0;
            Random randomInt = new Random();
            gobjAgents = GetAgentData();

            foreach (Agent person in gobjAgents)
            {
                newId = randomInt.Next(0, 99999); // put a limit on these
                if (agent.Id == 0 && person.Id != newId)
                {
                    agent.Id = newId;
                    gobjAgents.Add(agent);
                    break;
                }
            }

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CustomContractResolver();
            var convertedAgent = JsonConvert.SerializeObject(gobjAgents, Formatting.Indented, settings);
            SaveAgentData(convertedAgent);

            return agent;
        }

        /// <summary>
        /// serialize all updated agent info, store it into json file
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agent"></param>
        /// <returns></returns>
        public Agent UpdateAgentData(Agent agent)
        {
            // serialize all updated agent info, store it into json file
            gobjAgents = GetAgentData();
            //verify user in data
            var exists = gobjAgents.Select(x => x.Id == agent.Id).Any();

            foreach (Agent person in gobjAgents)
            {
                int index = gobjAgents.IndexOf(person);

                if (index >= 0 && person.Id == agent.Id)
                {
                    gobjAgents[index] = agent;
                    break;
                }
            }
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CustomContractResolver();
            var convertedAgent = JsonConvert.SerializeObject(gobjAgents, Formatting.Indented, settings);
            SaveAgentData(convertedAgent);
            return agent;
        }

        /// <summary>
        /// Store the serialized object back into the json file
        /// </summary>
        /// <param name="convertedAgent"></param>
        private void SaveAgentData(string convertedAgent)
        {
            // save to file here with convertedCustomer
            using (StreamWriter file = new StreamWriter(agentDataFile))
            {
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    writer.WriteRaw(convertedAgent);
                }
            }
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        public IList<Customer> GetCustomerData()
        {
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

            return gobjCustomers ?? null;
        }

        /// <summary>
        /// Get specific customer data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get all customers by agent
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<AgentCustomers> GetCustomersByAgent(int id)
        {
            gobjCustomers = GetCustomerData();
            var obj = gobjCustomers.Where(x => x.AgentId == id).ToList();
            var colReturn = new List<AgentCustomers>();
            char[] delimiterChars = { ',' };

            foreach (var customer in obj)
            {
                var agentCust = new AgentCustomers();
                var addressSections = customer.Address.Split(delimiterChars);

                agentCust.City = addressSections[1];
                agentCust.State = addressSections[2];
                agentCust.Name = $"{customer.Name.LastName}, {customer.Name.FirstName}";
                colReturn.Add(agentCust);
            }

            return colReturn;
        }

        /// <summary>
        /// Update this customers value(s)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public Customer UpdateCustomerData(Customer customer)
        {
            // deserialize entire json blob via linq
            gobjCustomers = GetCustomerData();

            //get item by index, update this object in the list
            foreach (Customer person in gobjCustomers)
            {
                int index = gobjCustomers.IndexOf(person);

                if (index >= 0 && person.Id == customer.Id)
                {
                    gobjCustomers[index] = customer;
                    break;
                }
            }

            var settings = new JsonSerializerSettings();
            // Glom-esque contract resolver for precise formatting
            settings.ContractResolver = new CustomContractResolver();
            var convertedCustomer = JsonConvert.SerializeObject(gobjCustomers, Formatting.Indented, settings);
            SaveCustomerData(convertedCustomer);

            return customer;
        }

        /// <summary>
        /// Store the serialized object back into the json file
        /// </summary>
        /// <param name="convertedCustomer"></param>
        private static void SaveCustomerData(string convertedCustomer)
        {
            // save to file here with convertedCustomer
            using (StreamWriter file = new StreamWriter(customerDataFile))
            {
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    writer.WriteRaw(convertedCustomer);
                }
            }
        }

        /// <summary>
        /// Remove customer from the file by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<Customer> DeleteCustomerData(int id)
        {
            Customer customer = GetCustomerData(id);
            // deserialize via linq
            gobjCustomers = GetCustomerData();
            Console.WriteLine(gobjCustomers.Count);

            //remove this object from the list
            foreach (Customer person in gobjCustomers)
            {
                if (person.Id == customer.Id)
                {
                    gobjCustomers.Remove(person);
                    break;
                }
            }

            // serialize 
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CustomContractResolver();
            var convertedCustomer = JsonConvert.SerializeObject(gobjCustomers, Formatting.Indented, settings);
            SaveCustomerData(convertedCustomer);

            return gobjCustomers;
        }

        /// <summary>
        /// Create a new customer, assign new ID, guid, and formatted date
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public Customer CreateCustomer(Customer customer)
        {
            int newId = 0;

            Random randomInt = new Random();

            // deserialize entire json blob via linq
            gobjCustomers = GetCustomerData();

            //get item by index, update this object in the list
            foreach (Customer person in gobjCustomers)
            {
                newId = randomInt.Next();

                if (customer.Id == 0 && person.Id != newId)
                {
                    customer.Id = newId; // need new id
                    customer.CustomerGuid = Guid.NewGuid(); // generate guid
                    customer.Registered = DateTime.Parse(DateTime.Now.ToString("f"));
                    gobjCustomers.Add(customer);
                    break;
                }
            }

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CustomContractResolver();
            var convertedCustomer = JsonConvert.SerializeObject(gobjCustomers, Formatting.Indented, settings);
            SaveCustomerData(convertedCustomer);

            return customer;
        }
    }
}
