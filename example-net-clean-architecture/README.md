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
- Unificación de comando, handler y validación en un mismo archivo para los commands y queries de CQRS.

  - estructura original

  ```text
  + ProductCategories
    + Commands
      + CreateProductCategory
          - CreateProductCategoryCommand.cs
          - CreateProductCategoryCommandHandler.cs
          - CreateProductCategoryCommandValidator.cs
      + UpdateProductCategory
          - UpdateProductCategoryCommand.cs
          - UpdateProductCategoryCommandHandler.cs
          - UpdateProductCategoryCommandValidator.cs
      + DeleteProductCategory
          - DeleteProductCategoryCommand.cs
          - DeleteProductCategoryCommandHandler.cs
  ```

  - estructura mejorada

  ```text
  + ProductCategories
      + Commands
          - CreateProductCategory.cs
          - UpdateProductCategory.cs
          - DeleteProductCategory.cs
  ```

- uso de clases base para implementación de commands y queries de CQRS.

  - validador

    - implementación tradicional

      ```csharp
      public class CommandValidator : AbstractValidator<Command> {}
      ```

    - implementación con clases base

      ```csharp
      public class CommandValidator : CommandValidatorBase<Command> {}
      ```

  - commands

    - implementación tradicional sin retorno

      ```csharp
      public class Command : IRequest {}
      public class CommandHandler : IRequestHandler<Command>
      {
        //
        private readonly IProductCategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        //
        public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogger logger)
        {
          _repository = repository;
          _mapper = mapper;
          _logger = logger;
        }
      }
      ```

    - implementación tradicional con retorno

      ```csharp
      public class Command : IRequest<int> {}
      public class CommandHandler : IRequestHandler<Command, int>
      {
        //
        private readonly IProductCategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        //
        public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogger logger)
        {
          _repository = repository;
          _mapper = mapper;
          _logger = logger;
        }
      }
      ```

    - implementación con clases base sin retorno

      ```csharp
      public class Command : CommandBase {}
      public class CommandHandler : CommandHandlerBase<Command>
      {
        //
        private readonly IProductCategoryRepository _repository;
        //
        public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
          : base(mapper, logger)
        {
          _repository = repository;
        }
      }
      ```

    - implementación con clases base con retorno

      ```csharp
      public class Command : CommandBase<int> {}
      public class CommandHandler : CommandHandlerBase<Command, int>
      {
        //
        private readonly IProductCategoryRepository _repository;
        //
        public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
          : base(repository, mapper, logger)
        {
          _repository = repository;
        }
      }
      ```

    - implementación con clases base y repositorio defecto sin retorno

      ```csharp
      public class Command : CommandBase {}
      public class CommandHandler : CommandHandlerRepositoryBase<Command, IProductCategoryRepository>
      {
        //
        public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
          : base(repository, mapper, logger)
        {
        }
      }
      ```

    - implementación con clases base y repositorio defecto con retorno

      ```csharp
      public class Command : CommandBase<int> {}
      public class CommandHandler : CommandHandlerRepositoryBase<Command, int, IProductCategoryRepository>
      {
        //
        public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
          : base(repository, mapper, logger)
        {
        }
      }
      ```

  - queries

    - implementación tradicional

      ```csharp
      public class Query : IRequest<List<Response>> {}
      public class QueryHandler : IRequestHandler<Query, List<Response>>
      {
        //
        private readonly IProductCategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogService _logger;
        //
        public QueryHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
        {
          _repository = repository;
          _mapper = mapper;
          _logger = logger;
        }
      }
      ```

    - implementación con clases base

      ```csharp
      public class Query : QueryBase<List<Response>> {}
      public class QueryHandler : QueryHandlerBase<Query, List<Response>>
      {
        //
        private readonly IProductCategoryRepository _repository;
        //
        public QueryHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
          : base(mapper, logger)
        {
          _repository = repository;
        }
      }
      ```

    - implementación con clases base y repositorio defecto

      ```csharp
      public class Query : QueryBase<List<Response>> {}
      public class QueryHandler : QueryHandlerRepositoryBase<Query, List<Response>, IProductCategoryRepository>
      {
        //
        public QueryHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
          : base(repository, mapper, logger)
        {
        }
      }
      ```

  - uso de interfaces de mapeo de objetos

    - implementación tradicional

      ```csharp
      public class MapProfile : Profile
      {
        public MapProfile()
        {
        CreateMap<ProductCategory, GetProductCategories.Response>();
        CreateMap<CreateProductCategory.Command, ProductCategory>();
        CreateMap<UpdateProductCategory.Command, ProductCategory>();
        }
      }
      ```

    - implementación con interfaces

      ```csharp
      public class MapProfile : MapProfileBase
      {
        public MapProfile()
        : base(Assembly.GetExecutingAssembly())
        {
        //
        }
      }
      //
      public class CreateProductCategory
      {
        public class Command : CommandBase<int>, IMapTo<ProductCategory>
      }
      //
      public class UpdateProductCategory
      {
        public class Command : CommandBase, IMapTo<ProductCategory>
      }
      //
      public class GetProductCategories
      {
        public class Response : IMapFrom<ProductCategory>
      }
      ```

  - Manejo estandarizado de errores de validación y servidor.

    - Errores de validación generados por un error en los datos enviados por el cliente, se retornan de forma estándar con un código de resultado 400
    - Errores de servidor generados por una excepción no administrada del lado del servidor, se retornan de forma estándar con un código de resultado 500.
    - <https://code-maze.com/using-the-problemdetails-class-in-asp-net-core-web-api>
    - errores de validación comúnmente utilizados

      ```json
          //validaciones FluentValidation
          {
            "title": "One or more validation errors occurred.",
            "status": 400,
            "errors": {
            "Parameter1": [
              "Cannot be null.",
              "Cannot be blank."
            ],
            "Parameter2": [
              "Cannot be null.",
              "Cannot be blank."
            ]
            }
          }
          //validaciones
          {
            "title": "Not Found",
            "status": 404,
          }
      ```

    - errores de validación estándar en la arquitectura

      ```json
      //validaciones FluentValidation
      {
        "title": "Bad Request Error",
        "status": 400,
        "errors": [
        {
          "property": "Parameter1",
          "code": "NotNullValidator",
          "message": "Cannot be null."
        },
        {
          "property": "Parameter1",
          "code": "NotEmptyValidator",
          "message": "Cannot be blank."
        },
        {
          "property": "Parameter2",
          "code": "ParameterNotNull",
          "message": "Cannot be null."
        },
        {
          "property": "Parameter2",
          "code": "ParameterNotBlank",
          "message": "Cannot be blank."
        }
        ]
      }
      //validaciones
      {
        "title": "Bad Request Error",
        "status": 400,
        "errors": [
        {
          "code": "ProductCategory.NotFound",
          "message": "The entity with the Id = '0' was not found."
        }
        ]
      }
      ```

    - error interno comúnmente utilizado

      ```json
      {
        "title": "An error occurred while processing your request.",
        "status": 500
      }
      ```

    - error interno estándar en la arquitectura para desarrollo

      ```json
      {
        "title": "Internal Server Error",
        "status": 500,
        "detail": "System.Exception: Test Exception
           at NCA.Production.Application.Features.Tests.Commands.TestException.CommandHandler.Handle(Command request, CancellationToken cancellationToken) in C:\\Source\\example-net-clean-architecture\\NCA.Production.Application\\Features\\Tests\\Commands\\TestException.cs:line 15
           at MediatR.Wrappers.RequestHandlerWrapperImpl`2.<>c__DisplayClass1_0.<Handle>g__Handler|0()
           at NCA.Common.Application.Behaviours.ValidationBehaviour`2.Handle(TRequest request, RequestHandlerDelegate`1 next, CancellationToken cancellationToken) in C:\\Source\\example-net-clean-architecture\\NCA.Common.Application\\Behaviours\\ValidationBehaviour.cs:line 32
           at NCA.Production.ApiRestMin.Endpoints.TestsFeatures.TestException(ISender sender) in C:\\Source\\example-net-clean-architecture\\NCA.Production.ApiRestMin\\Endpoints\\TestsFeatures.cs:line 24
           at Microsoft.AspNetCore.Http.RequestDelegateFactory.ExecuteTaskResult[T](Task`1 task, HttpContext httpContext)
           at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)
           at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)"
      }
      ```

    - error interno estándar en la arquitectura para producción

    ```json
    {
      "title": "Internal Server Error",
      "status": 500
    }
    ```

  - Implementación de patrón resultado para el retorno de commands y queries de CQRS.

    - <https://code-maze.com/using-the-problemdetails-class-in-asp-net-core-web-api>
    - <https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern>
    - <https://github.com/ardalis/Result>

  - Implementación de usings globales.

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
    - <https://github.com/phongnguyend/Practical.CleanArchitecture>
    - <https://github.com/kalintsenkov/BookStore/tree/main>
    - <https://github.com/DijanaPenic/DDD-VShop/tree/main>
    - <https://github.com/fullstackhero/dotnet-webapi-starter-kit/tree/master>
    - <https://github.com/mdarlea/DDD-Seedwork>
    - <https://www.youtube.com/watch?v=5OtUm1BLmG0>
