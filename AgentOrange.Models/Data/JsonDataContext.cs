using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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

        public Agent UpdateAgentData(int? id, Agent? agent)
        {
            // serialize all updated agent info, store it into json file
            var cereal = JsonConvert.SerializeObject(agent);
            Console.WriteLine(cereal);
            return agent;
        }
        public Customer UpdateCustomerData(Customer customer)
        {
            using (StreamReader reader = new StreamReader(customerDataFile))
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
            }

            var settings = new JsonSerializerSettings();
            // Glom-esque contract resolver for precise formatting
            settings.ContractResolver = new CustomContractResolver();
            var convertedCustomer = JsonConvert.SerializeObject(gobjCustomers, Formatting.Indented, settings);
            SaveCustomerData(convertedCustomer);

            return customer;
        }

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

        public IList<Customer> DeleteCustomerData(int id)
        {
            Customer customer = GetCustomerData(id);

            using (StreamReader reader = new StreamReader(customerDataFile))
            {
                // read the file
                var jsonFileData = reader.ReadToEnd();
                // deserialize via linq
                gobjCustomers = GetCustomerData();
                Console.WriteLine(gobjCustomers.Count);

                //remove this object from the list
                foreach (Customer person in gobjCustomers)
                {
                    //int index = gobjCustomers.IndexOf(person);

                    if (person.Id == customer.Id)//  && index >=0)
                    {
                        //var temp = gobjCustomers;
                        // get index of temp list @ person, remove, store back to gobjCustomers

                        gobjCustomers.Remove(person);
                        break;
                    }
                }
            }

            // serialize 
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CustomContractResolver();
            var convertedCustomer = JsonConvert.SerializeObject(gobjCustomers, Formatting.Indented, settings);
            SaveCustomerData(convertedCustomer);

            return gobjCustomers;
        }

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

                if (customer.Id == 0 || person.Id != newId)
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
        // magic
    }
}
