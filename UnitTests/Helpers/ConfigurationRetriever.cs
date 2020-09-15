using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace UnitTests.Helpers
{
    public class ConfigurationRetriever
    {
        public string GetConnectionString()
        {
            var config = this.InitializeConfiguration();
            string connectionString = config
                                    .GetSection("endoscopesTrackingDatabase")
                                    .GetSection("connectionString")
                                    .Value;
            return connectionString;
        }

        private IConfiguration InitializeConfiguration()
        {
            var configFile = "testSettings.json";
            var config = new ConfigurationBuilder()
                            .AddJsonFile(configFile)
                            .Build();
            return config;
        }
    }
}