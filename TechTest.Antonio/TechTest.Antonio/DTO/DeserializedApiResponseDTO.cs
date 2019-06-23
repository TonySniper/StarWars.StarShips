using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest.Antonio.DTO
{
    public class DeserializedApiResponseDTO
    {
        public int? NextPageNumber { get; }
        public string StarShipCount { get; }
        public IEnumerable<StarShipDTO> StarShips { get; }

        public DeserializedApiResponseDTO(int? pageNumber, string starShipCount, IEnumerable<StarShipDTO> starShips)
        {
            this.NextPageNumber = pageNumber;
            this.StarShipCount = starShipCount;
            this.StarShips = starShips;
        }
    }
}
