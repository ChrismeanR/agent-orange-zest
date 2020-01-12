using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AgentOrange.Models
{
    [DataContract]
    public class Customer 
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int AgentId { get; set; }
        [DataMember]
        public Guid CustomerGuid { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public decimal Balance { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public DateTime Registered { get; set; }
        [DataMember]
        public string latitude { get; set; }
        [DataMember]
        public string longitude { get; set; }
        [DataMember]
        public string[] Tags { get; set; }
        [DataMember]
        public Dictionary<string, Person> Name { get; set; }

        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string EyeColor { get; set; }

    }

    
}
