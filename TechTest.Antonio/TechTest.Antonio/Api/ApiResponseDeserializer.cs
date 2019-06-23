using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTest.Antonio.DTO;

namespace TechTest.Antonio.Api
{
    public class ApiResponseDeserializer
    {
        private const string countAttributeName = "count";
        private const string nextPageJsonAttributeName = "next";
        private const string starshipListAttributeName = "results";

        public async Task<DeserializedApiResponseDTO> DeserializeResponse(HttpResponseMessage responseMessage)
        {
            string content = await responseMessage.Content.ReadAsStringAsync();

            var parsedObject = JObject.Parse(content);

            var nextPageNumber = this.ParseNextPageUrlFromJObject(parsedObject);
            var totalItemCount = this.ParseTotalItemCountFromJObject(parsedObject);
            var starShipList = this.GetStarShipListFromJObject(parsedObject);

            var resultDto = new DeserializedApiResponseDTO(nextPageNumber, totalItemCount, starShipList);

            return resultDto;
        }

        private int? ParseNextPageUrlFromJObject(JObject jsonObject)
        {
            string nextPageAttributeValue = (string)jsonObject[nextPageJsonAttributeName];

            if (string.IsNullOrWhiteSpace(nextPageAttributeValue))
                return null;

            var pageNumber = nextPageAttributeValue.Split('=')[1];

            return int.Parse(pageNumber);
        }

        private string ParseTotalItemCountFromJObject(JObject jsonObject)
        {
            string countAttributeValue = (string)jsonObject[countAttributeName];

            return countAttributeValue;
        }
        
        private IEnumerable<StarShipDTO> GetStarShipListFromJObject(JObject jsonObject)
        {
            string shipNameJsonAttribute = "name";
            string shipConsumablesJsonAttribute = "consumables";
            string shipMegalightsJsonAttribute = "MGLT";
            IList<StarShipDTO> resultShipList = new List<StarShipDTO>();

            var starShipList = jsonObject[starshipListAttributeName];

            foreach (var item in starShipList)
            {
                string shipName = (string)item[shipNameJsonAttribute];
                string consumables = (string)item[shipConsumablesJsonAttribute];
                string megalights = (string)item[shipMegalightsJsonAttribute];

                resultShipList.Add(new StarShipDTO(shipName, megalights, consumables));
            }

            return resultShipList;
        }
    }
}
