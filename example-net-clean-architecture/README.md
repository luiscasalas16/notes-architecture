# example-net-clean-architecture

Proyecto de ejemplo de microservicios implementados utilizando clean architecture.

- NCA es la abreviatura de Net Clean Architecture.
- Los proyectos NAC.Common.\* son reutilizables para cualquier microservicio.
- Los proyectos NAC.Production.\* son para el microservicio de Production.
- Se utiliza una sola base de datos AdventureWorks cómo ejemplo, aunque deberían utilizarse una base de datos por cada microservicio.

## Proyectos

El ejemplo está compuesto por los siguientes proyectos:

- ApiRest
  - tipo: Web Api
  - referencias: Application, Infrastructure, ApiRest.Common
- ApiRest.Common
  - tipo: Class Library
  - referencias: Application.Common
- Application
  - tipo: Class Library
  - referencias: Domain, Application.Common
  - packages:
    Install-Package MediatR -ProjectName NCA.Production.Application
    Install-Package FluentValidation -ProjectName NCA.Production.Application
    Install-Package FluentValidation.DependencyInjectionExtensions -ProjectName NCA.Production.Application
    Install-Package AutoMapper -ProjectName NCA.Production.Application
- Application.Common
  - tipo: Class Library
  - referencias: Domain.Common
  - packages:
    Install-Package MediatR -ProjectName NCA.Common.Application
    Install-Package FluentValidation -ProjectName NCA.Common.Application
- Domain
  - tipo: Class Library
  - referencias: Domain.Common
- Domain.Common
  - tipo: Class Library
- Infrastructure
  - tipo: Class Library
  - referencias: Application y Infrastructure.Common
  - packages:
    Install-Package Microsoft.EntityFrameworkCore.SqlServer -ProjectName NCA.Production.Infrastructure
- Infrastructure.Common
  - tipo: Class Library
  - referencias: Application.Common
  - packages:
    Install-Package Microsoft.EntityFrameworkCore.SqlServer -ProjectName NCA.Common.Infrastructure

## Arquitectura

Los siguientes puntos de arquitectura fueron incluidos en la arquitectura:

- Implementación de validaciones utilizando [FluentValidation](https://docs.fluentvalidation.net/).
  - <https://code-maze.com/fluentvalidation-in-aspnet>
- Implementación patrones Command Query Responsibility Segregation (CQRS) y Mediator utilizando [MediatR](https://github.com/jbogard/MediatR).
  - <https://code-maze.com/cqrs-mediatr-in-aspnet-core>
- Manejo de errores de validaciones de FluentValidation en MediatR por NCA.Common.Application.Behaviours.ValidationBehaviour.
  - <https://code-maze.com/cqrs-mediatr-fluentvalidation>
  - <https://www.milanjovanovic.tech/blog/cqrs-validation-with-mediatr-pipeline-and-fluentvalidation>
- Manejo de excepciones unificado por NCA.Common.ApiRest.Filters.ApiExceptionFilterAttribute.
  - <https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling>
  - <https://www.milanjovanovic.tech/blog/global-error-handling-in-aspnetcore-8>
- Soporte de implementación de APIs utilizando ASP.NET Core Minimal APIs. Son recomendación para la implementación de microservicios.
  - <https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api>
  - <https://code-maze.com/dotnet-minimal-api>
  - <https://blog.jetbrains.com/dotnet/2023/04/25/introduction-to-asp-net-core-minimal-apis/>
- Soporte de implementación de APIs utilizando ASP.NET Core Controller APIs. No son la recomendación para la implementación de microservicios, pero históricamente se utilizan por lo que se habilita el soporte.
  - <https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api>
  - <https://www.yogihosting.com/aspnet-core-api-controllers/>
- Registro unificado de Minimal APIs.
  - <https://code-maze.com/aspnetcore-automatic-registration-of-minimal-api-endpoints>
- Unificación de comando, handler y validación en un mismo archivo para los commands y queries de CQRS. [ejemplo](README-estructura.md)
- Uso de clases base para implementación de commands y queries de CQRS. [ejemplo](README-clases-base.md)
- Manejo estandarizado de errores de validación y servidor. [ejemplo](README-errores.md)
  - Errores de validación generados por un error en los datos enviados por el cliente, se retornan de forma estándar con un código de resultado 400
  - Errores de servidor generados por una excepción no administrada del lado del servidor, se retornan de forma estándar con un código de resultado 500.
  - <https://code-maze.com/using-the-problemdetails-class-in-asp-net-core-web-api>
- Implementación de patrón resultado para el retorno de commands y queries de CQRS.
  - <https://code-maze.com/using-the-problemdetails-class-in-asp-net-core-web-api>
  - <https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern>
  - <https://github.com/ardalis/Result>
- Implementación de usings globales.
- Uso de HttpClient y HttpClientFactory para acceso a APIs.
  - <https://code-maze.com/httpclient-with-asp-net-core-tutorial>
  - <https://code-maze.com/using-httpclientfactory-in-asp-net-core-applications>
- Uso de inyección de dependencias.
  - <https://code-maze.com/dotnet-using-constructor-injection>
- Uso de health checks.
  - <https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks>
  - <https://code-maze.com/health-checks-aspnetcore/>
- Uso de versiones.
  - <https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/openapi>
  - <https://www.milanjovanovic.tech/blog/api-versioning-in-aspnetcore>
  - <https://code-maze.com/aspnetcore-api-versioning>
  - <https://github.com/dotnet/aspnet-api-versioning/tree/main/examples/AspNetCore/WebApi/MinimalOpenApiExample>

## Referencias

- Tutoriales
  - <https://dev.to/isaacojeda/parte-1-cqrs-y-mediatr-implementando-cqrs-en-aspnet-56oe>
  - <https://levelup.gitconnected.com/implementing-clean-architecture-and-ddd-project-from-scratch-to-a-working-app-step-by-step-in-net-b9a934b63aab#6589>
- Cursos
  - <https://www.udemy.com/course/aspnet-clean-architecture>
- Microsoft
  - <https://github.com/dotnet-architecture/eShopOnContainers>
  - <https://github.com/dotnet/eShop>
- Repositorios
  - Ranking \*\*\*
    - <https://github.com/aspnetrun/run-aspnetcore-microservices>
    - <https://github.com/jasontaylordev/CleanArchitecture>
    - <https://github.com/ardalis/CleanArchitecture>
  - Ranking \*\*
    - <https://github.com/ezzylearning/CleanArchitectureDemo>
    - <https://github.com/phongnguyend/Practical.CleanArchitecture>
  - Ranking\*
    - <https://github.com/matt-bentley/CleanArchitecture>
    - <https://github.com/saadjaved120/CleanArchitecture_CQRS_Pub-Sub>
    - <https://github.com/sashamarfuttech/super-note-api>
  - TODO
    - <https://learn.microsoft.com/en-us/aspnet/core/performance/caching/output>
    - <https://github.com/phongnguyend/Practical.CleanArchitecture>
    - <https://github.com/kalintsenkov/BookStore/tree/main>
    - <https://github.com/DijanaPenic/DDD-VShop/tree/main>
    - <https://github.com/fullstackhero/dotnet-webapi-starter-kit/tree/master>
    - <https://github.com/mdarlea/DDD-Seedwork>
    - <https://www.youtube.com/watch?v=5OtUm1BLmG0>
