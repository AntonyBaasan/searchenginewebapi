using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SearchEngineTests
{
    public class SettingsUtils
    {
        private static IConfiguration Configuration { get; set; }
        public static string GetConnectionString(string connectionName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration.GetConnectionString(connectionName);

        }
    }
}
