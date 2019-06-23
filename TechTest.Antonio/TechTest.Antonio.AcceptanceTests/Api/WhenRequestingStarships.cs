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
    public class WhenRequestingStarships
    {
        private readonly string url;
        private readonly ApiClient apiClient;

        public WhenRequestingStarships()
        {
            this.url = ConfigurationManager.AppSettings.Get("StarWarsApiUrl");
            this.apiClient = new ApiClient(this.url);
        }

        [TestMethod]
        public async Task ItShouldReturnASuccessHttpRequestStatusCode()
        {
            var startShipList = await this.apiClient.GetStarShipList();

            Assert.IsTrue(startShipList.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task ItShouldReturnADeserializedApiResponse()
        {
            var response = await this.apiClient.GetStarShipList();

            var deserializer = new ApiResponseDeserializer();

            var deserializedResponse = await deserializer.DeserializeResponse(response);

            Assert.IsNotNull(deserializedResponse.NextPageNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(deserializedResponse.StarShipCount));

            Assert.IsFalse(deserializedResponse.StarShips.Any(x => string.IsNullOrWhiteSpace(x.ShipName)));
            Assert.IsFalse(deserializedResponse.StarShips.Any(x => string.IsNullOrWhiteSpace(x.MegalightsPerHour)));
            Assert.IsFalse(deserializedResponse.StarShips.Any(x => string.IsNullOrWhiteSpace(x.Consumables)));
        }

        [TestMethod]
        public async Task ItShouldReturnADeserializedApiResponseIfThePageRequestedWasPage4()
        {
            var response = await this.apiClient.GetStartShipListByPage(4);

            var deserializer = new ApiResponseDeserializer();

            var deserializedResponse = await deserializer.DeserializeResponse(response);

            Assert.IsNull(deserializedResponse.NextPageNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(deserializedResponse.StarShipCount));

            Assert.IsFalse(deserializedResponse.StarShips.Any(x => string.IsNullOrWhiteSpace(x.ShipName)));
            Assert.IsFalse(deserializedResponse.StarShips.Any(x => string.IsNullOrWhiteSpace(x.MegalightsPerHour)));
            Assert.IsFalse(deserializedResponse.StarShips.Any(x => string.IsNullOrWhiteSpace(x.Consumables)));
        }
    }
}
