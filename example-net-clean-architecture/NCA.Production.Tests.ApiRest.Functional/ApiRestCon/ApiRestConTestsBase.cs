﻿using Microsoft.AspNetCore.Mvc.Testing;
using NCA.Production.ApiRestCon;

namespace NCA.Production.Tests.ApiRest.Functional.ApiRestCon
{
    [Collection(nameof(ApiRestConTestsFixtureDefinition))]
    public class ApiRestConTestsBase
    {
        protected HttpClient HttpClient { get; }

        internal ApiRestConTestsBase(ApiRestConTestsFixtureImplementation factory)
        {
            HttpClient = factory.WebHttpClient;
        }
    }

    [CollectionDefinition(nameof(ApiRestConTestsFixtureDefinition))]
    public class ApiRestConTestsFixtureDefinition : ICollectionFixture<ApiRestConTestsFixtureImplementation>;

    public class ApiRestConTestsFixtureImplementation : WebApplicationFactory<Program>
    {
        public HttpClient WebHttpClient
        {
            get { return CreateClient(); }
        }
    }
}
