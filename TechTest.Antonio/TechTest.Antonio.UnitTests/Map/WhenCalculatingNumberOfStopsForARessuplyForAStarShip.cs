using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TechTest.Antonio.UnitTests.Map
{
    [TestClass]
    public class WhenCalculatingNumberOfStopsForARessuplyForAStarShip
    {
        [TestMethod]
        public void ItShouldReturn9StopsForMillenniumFalcon()
        {
            var ship = new StarShip("Millennium Falcon", 75, 2, TimeMeasure.Month);

            int givenDistanceInMegalights = 1000000;
            int expectedValue = 9;

            var numberOfStops = ship.GetNumberOfStopsRequired(givenDistanceInMegalights);

            Assert.AreEqual(expectedValue, numberOfStops);
        }

        [TestMethod]
        public void ItShouldReturn74StopsForY_Wing()
        {
            var ship = new StarShip("Y-Wing", 80, 1, TimeMeasure.Week);

            int givenDistanceInMegalights = 1000000;
            int expectedValue = 74;

            var numberOfStops = ship.GetNumberOfStopsRequired(givenDistanceInMegalights);

            Assert.AreEqual(expectedValue, numberOfStops);
        }

        [TestMethod]
        public void ItShouldReturn11StopsForRebelTransport()
        {
            var ship = new StarShip("Rebel Transport", 20, 6, TimeMeasure.Month);

            int givenDistanceInMegalights = 1000000;
            int expectedValue = 11;

            var numberOfStops = ship.GetNumberOfStopsRequired(givenDistanceInMegalights);

            Assert.AreEqual(expectedValue, numberOfStops);
        }

        [TestMethod]
        public void ItShouldReturnNullForAShipWithUknownConsumables()
        {
            var ship = new StarShip("ship ship", 10, 10, TimeMeasure.Unknown);

            int distance = 1;

            var numberOfStops = ship.GetNumberOfStopsRequired(distance);

            Assert.IsNull(numberOfStops);
        }
    }
}
