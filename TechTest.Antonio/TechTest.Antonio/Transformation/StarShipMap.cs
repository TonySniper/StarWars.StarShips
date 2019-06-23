using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.Antonio.DTO;

namespace TechTest.Antonio.Transformation
{
    public class StarShipMap
    {
        private const string unknownValue = "unknown";

        public StarShip Map(StarShipDTO dto)
        {
            var megaliths = this.GetMegaliths(dto);
            var consumables = this.GetConsumables(dto);
            var consumablesTimeMeasure = this.GetConsumablesTimeMeasure(dto);

            var starShip = new StarShip(dto.ShipName, megaliths, consumables, consumablesTimeMeasure);

            return starShip;
        }

        private int? GetMegaliths(StarShipDTO dto)
        {
            if (!this.IsStarShipValidForCalculations(dto))
                return null;

            var megalights = int.Parse(dto.MegalightsPerHour);

            return megalights;
        }

        private int? GetConsumables(StarShipDTO dto)
        {
            if (!this.IsStarShipValidForCalculations(dto))
                return null;

            var consumables = string.Join("", dto.Consumables.Where(char.IsDigit));

            return int.Parse(consumables);
        }

        private TimeMeasure GetConsumablesTimeMeasure(StarShipDTO dto)
        {
            string day = "day";
            string week = "week";
            string month = "month";
            string year = "year";

            if (!this.IsStarShipValidForCalculations(dto))
                return TimeMeasure.Unknown;

            var timeMeasure = string.Join("", dto.Consumables.Where(char.IsLetter)).ToLower();

            if (timeMeasure.Contains(day))
                return TimeMeasure.Day;

            if (timeMeasure.Contains(week))
                return TimeMeasure.Week;

            if (timeMeasure.Contains(month))
                return TimeMeasure.Month;

            if (timeMeasure.Contains(year))
                return TimeMeasure.Year;

            throw new Exception(string.Format("Invalid time measure: {0}", timeMeasure));
        }

        private bool IsStarShipValidForCalculations(StarShipDTO dto)
        {
            if (string.Equals(dto.Consumables.ToLower(), unknownValue) || string.Equals(dto.MegalightsPerHour, unknownValue) || !dto.Consumables.Any(char.IsDigit))
                return false;
            return true;
        }
    }
}
