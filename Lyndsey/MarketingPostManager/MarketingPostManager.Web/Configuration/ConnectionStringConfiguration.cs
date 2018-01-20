using System.Configuration;
using System.Globalization;

namespace MarketingPostManager.Web.Configuration
{
    public class ConnectionStringConfiguration : IConnectionStringConfiguration
    {
        public string MarketingPostManager => GetConnectionString("MarketingPostManager");

        private string GetConnectionString(string databaseName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[databaseName].ConnectionString;
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ConfigurationErrorsException($"Missing connection string for {databaseName}");
            }
            return connectionString;
        }
    }
}