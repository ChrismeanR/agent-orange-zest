using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Newtonsoft.Json.Linq;

namespace API.Controllers.Tests
{
    [TestClass()]
    public class AgentControllerTests
    {
//        public string gobjJsonCustomer = @"[{'_id': 5054,
//        'agent_id': 1987,
//        'guid': '54fc8606-0630-42f9-9e3c-716772df09bf',
//        'isActive': true,
//        'balance': '$1,578.40',
//        'age': 57,
//        'eyeColor': 'blue',
//        'name': {
//            'first': 'Neva',
//            'last': 'Calderon'
//        },
//        'company': 'ISOTRONIC',
//        'email': 'neva.calderon@isotronic.info',
//        'phone': '+1 (985) 502-2956',
//        'address': '573 Turner Place, Yukon, Federated States Of Micronesia, 762',
//        'registered': 'Wednesday, January 31, 2018 12:40 PM',
//        'latitude': '76.989498',
//        'longitude': '26.410977',
//        'tags': [
//            'eiusmod',
//            'reprehenderit',
//            'labore',
//            'ut',
//            'dolor'
//        ]
//}]";
        [TestMethod()]
        public void GetTest()
        {
            var jsonDataContext = new AgentOrange.Models.Data.JsonDataContext();
            var list = jsonDataContext.GetAgentData();

            //Assert.IsInstanceOfType(list, typeof(List<AgentOrange.Models.Agent>));
            Assert.IsNotNull(list);
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PutTest()
        {
            Assert.Fail();
        }
    }
}