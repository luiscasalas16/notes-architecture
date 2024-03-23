using FPlus.COL.Tests.Functional.Abstractions;

namespace NCA.Production.Tests.ApiRest.Functional.ApiRestMin
{
    [Collection(nameof(ShareTestsFixtureDefinition))]
    public class BaseApiRestMinTests
    {
        protected HttpClient HttpClient { get; }

        internal BaseApiRestMinTests(ShareTestsFixtureImplementation factory)
        {
            HttpClient = factory.WebHttpClient;
        }
    }
}
