using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTest.Antonio.DTO;
using TechTest.Antonio.Transformation;

namespace TechTest.Antonio.UnitTests
{
    [TestClass]
    public class WhenMappingFromStarShipDTOToStarShip
    {
        private StarShipMap mapper;

        [TestInitialize]
        public void TestInit()
        {
            this.mapper = new StarShipMap();
        }

        [TestMethod]
        public void ItShouldReturnAStarShipObject()
        {
            string shipName = "ship ship";
            string megaliths = "10";
            string consumables = "1 day";

            var dto = new StarShipDTO(shipName, megaliths, consumables);

            var result = this.mapper.Map(dto);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StarShip));
        }

        [TestMethod]
        public void ItShouldReturnAStarShipWithShipShipName()
        {
            var dto = new StarShipDTO("ship ship", "unknown", "1 day");

            var result = this.mapper.Map(dto);
            var expected = "ship ship";

            Assert.AreEqual(expected, result.Name);
        }

        [TestMethod]
        public void ItShouldReturnAStarShipWithNullMegalights()
        {
            var dto = new StarShipDTO("ship ship", "unknown", "1 day");

            var result = this.mapper.Map(dto);

            Assert.IsNull(result.MegalightsPerHour);
        }

        [TestMethod]
        public void ItShouldReturnAStarShipWithMegalightsWithAValueOf10()
        {
            var dto = new StarShipDTO("ship ship", "10", "1 day");

            var result = this.mapper.Map(dto);
            int expectedValue = 10;

            Assert.IsNotNull(result.MegalightsPerHour);
            Assert.AreEqual(expectedValue, result.MegalightsPerHour);
        }

        [TestMethod]
        public void ItShouldReturnAStarShipWithNullConsumables()
        {
            var dto = new StarShipDTO("ship ship", "22", "unknown");

            var result = this.mapper.Map(dto);

            Assert.IsNull(result.Consumables);
        }

        [TestMethod]
        public void ItShouldReturnAStarShipWithConsumablesWithAValueOf10Days()
        {
            var dto = new StarShipDTO("ship ship", "22", "15 days");

            var result = this.mapper.Map(dto);
            int expectedDays = 15;
            TimeMeasure expectedTimeMeasure = TimeMeasure.Day;


            Assert.IsNotNull(result.Consumables);
            Assert.AreEqual(expectedDays, result.Consumables);
            Assert.AreEqual(expectedTimeMeasure, result.ConsumablesTimeMeasure);
        }

        [TestMethod]
        public void ItShouldReturnAStarShipWithConsumablesWithAValueOf7Years()
        {
            var dto = new StarShipDTO("ship ship", "22", "7 years");

            var result = this.mapper.Map(dto);
            int expectedDays = 7;
            TimeMeasure expectedTimeMeasure = TimeMeasure.Year;


            Assert.IsNotNull(result.Consumables);
            Assert.AreEqual(expectedDays, result.Consumables);
            Assert.AreEqual(expectedTimeMeasure, result.ConsumablesTimeMeasure);
        }

        [TestMethod]
        public void ItShouldThrownAnExceptionWhenTheDtoHasAnInvalidTimeMeasure()
        {
            var dto = new StarShipDTO("ship ship", "22", "10 blabla");
            var dtoWithoutTimeMeasure = new StarShipDTO("ship ship", "22", "10");


            Assert.ThrowsException<Exception>(() => this.mapper.Map(dto));
            Assert.ThrowsException<Exception>(() => this.mapper.Map(dtoWithoutTimeMeasure));
        }

        //[TestMethod]
        //public void ItShouldThrownAnExceptionWhenTheDtoHasAnInvalidConsumableValue()
        //{
        //    var dto = new StarShipDTO("ship ship", "22", "blabla");

        //    Assert.ThrowsException<Exception>(() => this.mapper.Map(dto));
        //}
    }
}
