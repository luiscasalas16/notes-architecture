using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using NCA.Production.ApiRestMin;

namespace FPlus.COL.Tests.Functional.Abstractions
{
    public class ShareTestsFixtureImplementation : WebApplicationFactory<Program>
    {
        // connection string de la base de datos
        public HttpClient WebHttpClient
        {
            get { return CreateClient(); }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // List of settings to be overrrided in the configuration.
            List<KeyValuePair<string, string?>> settings = [];

            builder
                // This configuration is used in the creation of the application.
                .UseConfiguration(new ConfigurationBuilder().AddInMemoryCollection(settings).Build())
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    // This overrides configuration settings that were added as part of host building.
                    configurationBuilder.AddInMemoryCollection(settings);
                });
        }
    }
}
