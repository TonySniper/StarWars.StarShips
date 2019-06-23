using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest.Antonio
{
    public class StarShipViewModel
    {
        public string ShipName { get; }
        public string NumberOfStopsRequired { get; }

        public StarShipViewModel(string shipName, int? numberOfStopsRequired)
        {
            this.ShipName = shipName;
            this.NumberOfStopsRequired = (numberOfStopsRequired.HasValue) ? numberOfStopsRequired.ToString() : "Unknown";
        }
    }
}
