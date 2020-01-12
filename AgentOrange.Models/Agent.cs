using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AgentOrange.Models
{
    [DataContract]
    public class Agent
    {
        [DataMember]
        public int Id { get; set;}
        //public Person Name { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string StreetAddress { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public int Tier { get; set; }
        [DataMember]
        public Dictionary<string, Phone> PhoneNumbers { get; set; }
        [DataMember]
        public string PhoneMobile { get; set; }
        [DataMember]
        public string PhonePrimary { get; set; }
    }

    [DataContract]
    public class Phone
    {
        [DataMember]
        public string Primary { get; set; }
        [DataMember]
        public string Mobile { get; set; }
    }
}
