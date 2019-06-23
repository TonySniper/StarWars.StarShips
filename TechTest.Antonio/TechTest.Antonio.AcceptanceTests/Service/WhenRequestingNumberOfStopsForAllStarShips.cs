using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTest.Antonio.Api;
using TechTest.Antonio.Transformation;

namespace TechTest.Antonio.AcceptanceTests.Service
{
    [TestClass]
    public class WhenRequestingNumberOfStopsForAllStarShips : AcceptanceTestsBase
    {
        private readonly ApiResponseDeserializer deserializer;
        private readonly StarShipMap mapper;
        private readonly StarShipService service;

        public WhenRequestingNumberOfStopsForAllStarShips()
        {
            this.deserializer = new ApiResponseDeserializer();
            this.mapper = new StarShipMap();
            this.service = new StarShipService(this.ApiClient, deserializer, mapper);
        }


        [TestMethod]
        public async Task Given1000000MegalightsItShouldReturn74ForY_Wing()
        {
            int distance = 1000000;
            string shipName = "y-wing";
            int expectedStops = 74;

            var ship = (await this.service.GetNumberOfStopsForStarShips(distance)).FirstOrDefault(x => x.ShipName.ToLower() == shipName.ToLower());

            Assert.IsNotNull(ship);
            Assert.AreEqual(shipName.ToLower(), ship.ShipName.ToLower());
            Assert.AreEqual(expectedStops, int.Parse(ship.NumberOfStopsRequired));
        }

        [TestMethod]
        public async Task Given1000000MegalightsItShouldReturn9ForMillennium_Falcon()
        {
            int distance = 1000000;
            string shipName = "Millennium Falcon";
            int expectedStops = 9;

            var ship = (await this.service.GetNumberOfStopsForStarShips(distance)).FirstOrDefault(x => x.ShipName.ToLower() == shipName.ToLower());

            Assert.IsNotNull(ship);
            Assert.AreEqual(shipName.ToLower(), ship.ShipName.ToLower());
            Assert.AreEqual(expectedStops, int.Parse(ship.NumberOfStopsRequired));
        }

        [TestMethod]
        public async Task Given1000000MegalightsItShouldReturn11ForRebel_Transport()
        {
            int distance = 1000000;
            string shipName = "Rebel Transport";
            int expectedStops = 11;

            var ship = (await this.service.GetNumberOfStopsForStarShips(distance)).FirstOrDefault(x => x.ShipName.ToLower() == shipName.ToLower());

            Assert.IsNotNull(ship);
            Assert.AreEqual(shipName.ToLower(), ship.ShipName.ToLower());
            Assert.AreEqual(expectedStops, int.Parse(ship.NumberOfStopsRequired));
        }

        [TestMethod]
        public async Task ItShouldReturnUnknownForV_Wing()
        {
            int distance = 1000000;
            string shipName = "V-Wing";
            string expectedStops = "unknown";

            var ship = (await this.service.GetNumberOfStopsForStarShips(distance)).FirstOrDefault(x => x.ShipName.ToLower() == shipName.ToLower());

            Assert.IsNotNull(ship);
            Assert.AreEqual(shipName.ToLower(), ship.ShipName.ToLower());
            Assert.AreEqual(expectedStops.ToLower(), ship.NumberOfStopsRequired.ToLower());
        }
    }
}
