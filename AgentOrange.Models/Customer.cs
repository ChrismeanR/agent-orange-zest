using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AgentOrange.Models
{
    [DataContract]
    public class Customer 
    {
        [DataMember]
        [JsonPropertyName("_id")]
        public int Id { get; set; }
        [DataMember]
        [JsonPropertyName("agent_id")]
        //public Agent AgentId { get; set; }
        public int AgentId { get; set; }
        [DataMember]
        [JsonPropertyName("guid")]
        public Guid CustomerGuid { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public string Balance { get; set; }
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
        public string Latitude { get; set; }
        [DataMember]
        public string Longitude { get; set; }
        [DataMember]
        public string[] Tags { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public string EyeColor { get; set; }
        [DataMember]
        public Person Name { get; internal set; }
    }

    
}
