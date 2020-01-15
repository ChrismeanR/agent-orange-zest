using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace AgentOrange.Models
{
    [DataContract]
    public class AgentCustomers
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }

    }
}
