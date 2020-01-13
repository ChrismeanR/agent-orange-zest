using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace AgentOrange.Models
{
    [DataContract]
    public class Agent
    {
        [DataMember (IsRequired = true)]
        [JsonPropertyName("_id")]
        [Key]
        public int Id { get; set;}
        //public Person Name { get; set; }
        [DataMember]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [DataMember]
        [JsonPropertyName("address")]
        public string StreetAddress { get; set; }
        [DataMember]
        [JsonPropertyName("city")]
        public string City { get; set; }
        [DataMember]
        [JsonPropertyName("state")]
        public string State { get; set; }
        [DataMember]
        [JsonPropertyName("zipCode")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
        [DataMember]
        [JsonPropertyName("tier")]
        public int Tier { get; set; }
        [DataMember]
        [JsonPropertyName("phone")]
        public Phone PhoneNumbers { get; set; }
        
    }

    [DataContract]
    public class Phone
    {
        [DataMember(IsRequired =true)]
        [DataType(DataType.PhoneNumber)]
        [JsonPropertyName("primary")]
        public string Primary { get; set; }
        [DataMember]
        [DataType(DataType.PhoneNumber)]
        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }
    }
}
