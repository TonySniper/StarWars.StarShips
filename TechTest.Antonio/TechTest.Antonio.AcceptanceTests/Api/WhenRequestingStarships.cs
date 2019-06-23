using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TechTest.Antonio.Api;
using TechTest.Antonio.DTO;

namespace TechTest.Antonio.AcceptanceTests.Api
{
    [TestClass]
    public class WhenRequestingStarships : AcceptanceTestsBase
    {

        public WhenRequestingStarships()
        {
        }

        [TestMethod]
        public async Task ItShouldReturnASuccessHttpRequestStatusCode()
        {
            var startShipList = await this.ApiClient.GetStarShipList();

            Assert.IsTrue(startShipList.IsSuccessStatusCode);
        }

        
    }
}
