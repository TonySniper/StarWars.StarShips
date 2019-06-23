using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.Antonio.Api;
using TechTest.Antonio.DTO;
using TechTest.Antonio.Transformation;

namespace TechTest.Antonio
{
    public class StarShipService
    {
        private ApiClient apiClient;
        private ApiResponseDeserializer responseDeserializer;
        private StarShipMap mapper;

        public StarShipService(ApiClient apiClient, ApiResponseDeserializer apiResponseDeserializer, StarShipMap mapper)
        {
            this.apiClient = apiClient;
            this.responseDeserializer = apiResponseDeserializer;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<StarShipViewModel>> GetNumberOfStopsForStarShips(int distance)
        {
            var responseDtoList = await this.GetStarShipsFromApi();
            IList<StarShipViewModel> shipList = new List<StarShipViewModel>();
            
            foreach (var item in responseDtoList.SelectMany(x => x.StarShips))
            {
                var shipEntity = this.mapper.Map(item);
                int? numberOfStops = shipEntity.GetNumberOfStopsRequired(distance);
                var vm = new StarShipViewModel(shipEntity.Name, numberOfStops);
                shipList.Add(vm);
            }

            return shipList;
        }

        private async Task<IEnumerable<DeserializedApiResponseDTO>> GetStarShipsFromApi()
        {
            var apiResponse = apiClient.GetStarShipList();
            var deserializedResponse = await this.responseDeserializer.DeserializeResponse(await apiResponse);

            var dtoList = new List<DeserializedApiResponseDTO>();
            dtoList.Add(deserializedResponse);

            bool hasNext = deserializedResponse.NextPageNumber.HasValue;
            
            int? nextPageNumber = deserializedResponse.NextPageNumber;

            while(hasNext)
            {
                var pagedResponse = await this.GetStarShipsFromApiByPage(nextPageNumber.Value);

                dtoList.Add(pagedResponse);

                nextPageNumber = pagedResponse.NextPageNumber;
                hasNext = nextPageNumber.HasValue;
            }

            return dtoList;
        }

        private async Task<DeserializedApiResponseDTO> GetStarShipsFromApiByPage(int pageNumber)
        {
            var response = apiClient.GetStartShipListByPage(pageNumber);
            var deserializedResponse = await this.responseDeserializer.DeserializeResponse(await response);

            return deserializedResponse;
        }
    }
}
