using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTest.Antonio.Api;

namespace TechTest.Antonio.AcceptanceTests
{
    public abstract class AcceptanceTestsBase
    {
        protected string Url { get; }
        protected ApiClient ApiClient { get; }

        protected AcceptanceTestsBase()
        {
            this.Url = ConfigurationManager.AppSettings.Get("StarWarsApiUrl");
            this.ApiClient = new ApiClient(this.Url);
        }
    }
}
