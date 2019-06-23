using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.Antonio.Api;
using TechTest.Antonio.Transformation;

namespace TechTest.Antonio
{
    class Program
    {
        static void Main(string[] args)
        {
            int distance = GetDistanceFromUserInput();

            var url = ConfigurationManager.AppSettings.Get("StarWarsApiUrl");

            using (var apiClient = new ApiClient(url))
            {
                //var apiClient = new ApiClient(url);
                var deserializer = new ApiResponseDeserializer();
                var mapper = new StarShipMap();

                var service = new StarShipService(apiClient, deserializer, mapper);

                var result = service.GetNumberOfStopsForStarShips(distance);

                foreach (var item in result.OrderBy(x => x.ShipName))
                {
                    //Console.WriteLine("Ship name: {0} Number of stops: {1}", item.ShipName, item.NumberOfStopsRequired);
                    Console.WriteLine("{0}: {1}", item.ShipName, item.NumberOfStopsRequired);
                }

                Console.ReadKey();
            }
        }

        private static int GetDistanceFromUserInput()
        {
            int result = 0;
            bool isValidInput = false;

            while (!isValidInput)
            {
                Console.WriteLine("Please input a valid distance (numbers only)");
                isValidInput = int.TryParse(Console.ReadLine(), out result);                
            }

            return result;
        }
    }
}
