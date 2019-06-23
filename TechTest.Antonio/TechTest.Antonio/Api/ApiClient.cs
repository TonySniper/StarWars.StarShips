using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TechTest.Antonio.Api
{
    public class ApiClient : IDisposable
    {
        private HttpClient _httpClient;
        private readonly string _url;

        public ApiClient(string url)
        {
            _httpClient = new HttpClient();
            _url = url;
        }

        public async Task<HttpResponseMessage> GetStarShipList()
        {
            return await _httpClient.GetAsync(_url);
        }

        public async Task<HttpResponseMessage> GetStartShipListByPage(int pageNumber)
        {
            var pagedUrl = string.Concat(_url, "?page=", pageNumber); 
            return await _httpClient.GetAsync(pagedUrl);
        }

        public void Dispose()
        {
            this._httpClient.Dispose();
        }
    }
}
