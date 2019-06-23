using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTest.Antonio.Api;

namespace TechTest.Antonio.AcceptanceTests.Deserializer
{
    [TestClass]
    public class WhenDeserializingAnApiResponse : AcceptanceTestsBase
    {
        [TestMethod]
        public async Task ItShouldReturnADeserializedApiResponse()
        {
            var response = await this.ApiClient.GetStarShipList();

            var deserializer = new ApiResponseDeserializer();

            var deserializedResponse = await deserializer.DeserializeResponse(response);

            Assert.IsNotNull(deserializedResponse.NextPageNumber);
            Assert.IsFalse(string.IsNullOrWhiteSpace(deserializedResponse.StarShipCount));

            Assert.IsFalse(deserializedResponse.StarShips.Any(x => string.IsNullOrWhiteSpace(x.ShipName)));
            Assert.IsFalse(deserializedResponse.StarShips.Any(x => string.IsNullOrWhiteSpace(x.MegalightsPerHour)));
            Assert.IsFalse(deserializedResponse.StarShips.Any(x => string.IsNullOrWhiteSpace(x.Consumables)));
        }

        [TestMethod]
        public async Task ItShouldReturnADeserializedApiResponseIfThePageRequestedWasTheLastPage()
        {
            //Page 4 is currently the last page for the StarShips API
            var response = await this.ApiClient.GetStartShipListByPage(4);

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
