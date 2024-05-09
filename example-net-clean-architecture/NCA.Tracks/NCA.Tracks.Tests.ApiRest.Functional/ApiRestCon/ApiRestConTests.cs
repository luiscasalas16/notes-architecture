using NCA.Tracks.Application.Features.Artists.Queries;

namespace NCA.Tracks.Tests.ApiRest.Functional.ApiRestCon
{
    public class ApiRestConTests : ApiRestConTestsBase
    {
        public ApiRestConTests(ApiRestConTestsFixtureImplementation factory)
            : base(factory) { }

        [Fact]
        public async Task Should_ReturnOk_WhenGetIsValid()
        {
            // Arrange

            // Act

            HttpResponseMessage response = await HttpClient.GetAsync("/api/v1/Artists");

            // Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var entities = await response.Content.ReadFromJsonAsync<List<GetArtists.Response>>();

            entities.Should().HaveCountGreaterThanOrEqualTo(1);
        }
    }
}
