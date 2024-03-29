﻿using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using NCA.Production.Application.Features.ProductCategories.Queries;

namespace NCA.Production.Tests.ApiRest.Functional.ApiRestCon
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

            HttpResponseMessage response = await HttpClient.GetAsync("/api/v1/ProductCategories");

            // Assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var entities = await response.Content.ReadFromJsonAsync<List<GetProductCategories.Response>>();

            entities.Should().HaveCountGreaterThanOrEqualTo(1);
        }
    }
}
