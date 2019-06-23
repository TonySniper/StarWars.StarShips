using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest.Antonio.DTO
{
    public class StarShipDTO
    {
        public string ShipName { get; }
        public string MegalightsPerHour { get; }
        public string Consumables { get; }

        public StarShipDTO(string shipName, string megalightsPerHour, string consumables)
        {
            this.ShipName = shipName;
            this.MegalightsPerHour = megalightsPerHour;
            this.Consumables = consumables;
        }
    }
}
