using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest.Antonio
{
    public class StarShip
    {
        public string Name { get; set; }
        public int? MegalightsPerHour { get; set; }
        public int? Consumables { get; set; }
        public TimeMeasure ConsumablesTimeMeasure { get; set; }

        public StarShip()
        {

        }

        public StarShip(string name, int? megalightsPerHour, int? consumables, TimeMeasure ConsumablesTimeMeasure)
        {
            this.Name = name;
            this.MegalightsPerHour = megalightsPerHour;
            this.Consumables = consumables;
            this.ConsumablesTimeMeasure = ConsumablesTimeMeasure;
        }

        public int? GetNumberOfStopsRequired(int distanceInMegalights)
        {
            if (this.MegalightsPerHour == null || this.Consumables == null || this.ConsumablesTimeMeasure == TimeMeasure.Unknown)
                return null;

            var consumablesInDays = this.ConvertToDays(this.Consumables);

            var targetMegalights = distanceInMegalights / this.MegalightsPerHour;
            var totalConsumableTimeInHours = consumablesInDays * 24;
            var result = targetMegalights / totalConsumableTimeInHours;

            return result;
        }

        private int? ConvertToDays(int? value)
        {
            switch (this.ConsumablesTimeMeasure)
            {
                case TimeMeasure.Day:
                    return value;
                case TimeMeasure.Week:
                    return value * 7;
                case TimeMeasure.Month:
                    return value * 30;
                case TimeMeasure.Year:
                    return value * 365;
                case TimeMeasure.Unknown:
                    return null;
                default:
                    throw new Exception("Failed to convert consumables time value to days");
            }
        }

        
    }

    public enum TimeMeasure
    {
        Day = 1,
        Week = 2,
        Month = 3,
        Year = 4,
        Unknown = 10
    }
}
