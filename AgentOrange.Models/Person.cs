using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AgentOrange.Models
{
    [DataContract]
    public class Person
    {
        //[DataMember]
        //public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        
    }

   
}
