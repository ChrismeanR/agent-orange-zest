using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int AgentId { get; set; } //map this to Agent.id
        [DataMember]
        [JsonPropertyName("guid")]
        public Guid CustomerGuid { get; set; }
        [DataMember]
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        [DataMember]
        [JsonPropertyName("balance")]
        public string Balance { get; set; }
        [DataMember]
        [JsonPropertyName("company")]
        public string Company { get; set; }
        [DataMember]
        [DataType(DataType.EmailAddress)]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [DataMember]
        [JsonPropertyName("phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataMember]
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [DataMember]
        [JsonPropertyName("registered")]
        public DateTime Registered { get; set; }
        [DataMember]
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [DataMember]
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
        [DataMember]
        [JsonPropertyName("tags")]
        public string[] Tags { get; set; }
        [DataMember]
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [DataMember]
        [JsonPropertyName("eyeColor")]
        public string EyeColor { get; set; }
        [DataMember]
        [JsonPropertyName("name")]
        public Person Name { get; set; }
    }


}
