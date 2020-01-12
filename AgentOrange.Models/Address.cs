using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AgentOrange.Models
{
    [DataContract]
    public class Address
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Street1 { get; set; }
        [DataMember]
        public string Street2 { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
    }
}
