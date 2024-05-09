using Microsoft.AspNetCore.Mvc.Testing;
using NCA.Tracks.ApiRestMin;

namespace NCA.Tracks.Tests.ApiRest.Functional.ApiRestMin
{
    [Collection(nameof(ApiRestMinTestsFixtureDefinition))]
    public class ApiRestMinTestsBase
    {
        protected HttpClient HttpClient { get; }

        internal ApiRestMinTestsBase(ApiRestMinTestsFixtureImplementation factory)
        {
            HttpClient = factory.WebHttpClient;
        }
    }

    [CollectionDefinition(nameof(ApiRestMinTestsFixtureDefinition))]
    public class ApiRestMinTestsFixtureDefinition : ICollectionFixture<ApiRestMinTestsFixtureImplementation>;

    public class ApiRestMinTestsFixtureImplementation : WebApplicationFactory<Program>
    {
        public HttpClient WebHttpClient
        {
            get { return CreateClient(); }
        }
    }
}
