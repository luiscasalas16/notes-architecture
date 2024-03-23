# example-net-clean-architecture

proyectos:
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
			Install-Package Microsoft.EntityFrameworkCore.SqlServer  -ProjectName NCA.Production.Infrastructure
	- Infrastructure.Common
		- tipo: Class Library
		- referencias: Application.Common
		- packages:
			Install-Package Microsoft.EntityFrameworkCore.SqlServer  -ProjectName NCA.Common.Infrastructure

mejoras:
	- unificación de comando, handler y validación en features.
		- estructura original
			- ProductCategories
				- Commands
					- CreateProductCategory
						- CreateProductCategoryCommand.cs
						- CreateProductCategoryCommandHandler.cs
						- CreateProductCategoryCommandValidator.cs
					- UpdateProductCategory
						- UpdateProductCategoryCommand.cs
						- UpdateProductCategoryCommandHandler.cs
						- UpdateProductCategoryCommandValidator.cs
					- DeleteProductCategory
						- DeleteProductCategoryCommand.cs
						- DeleteProductCategoryCommandHandler.cs
		- estructura mejorada
			- ProductCategories
				- Commands
					- CreateProductCategory.cs
					- UpdateProductCategory.cs
					- DeleteProductCategory.cs
	- manejo de errores de validación por NCA.Common.Application.Behaviours.ValidationBehaviour
	- manejo de excepciones unificado por NCA.Common.ApiRest.Filters.ApiExceptionFilterAttribute
	- uso de apis controladores o de apis mínimos
	- uso de clases base para implementación de commands y queries
		- validador
			- implementación tradicional
				```csharp
				public class CommandValidator : AbstractValidator<Command>
				```
			- implementación con clases base
				```csharp
				public class CommandValidator : CommandValidatorBase<Command>
				```
		- commands
			- implementación tradicional sin retorno
				```csharp
				public class Command : IRequest
				public class CommandHandler : IRequestHandler<Command>
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
				```
			- implementación tradicional con retorno
				```csharp
				public class Command : IRequest<int>
				public class CommandHandler : IRequestHandler<Command, int>
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
				```
			- implementación con clases base sin retorno
				```csharp
				public class Command : CommandBase
				public class CommandHandler : CommandHandlerBase<Command>
				//
				private readonly IProductCategoryRepository _repository;
				//
				public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
					: base(mapper, logger)
				{
					_repository = repository;
				}
				```
			- implementación con clases base con retorno
				```csharp
				public class Command : CommandBase<int>
				public class CommandHandler : CommandHandlerBase<Command, int>
				//
				private readonly IProductCategoryRepository _repository;
				//
				public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
					: base(repository, mapper, logger)
				{
					_repository = repository;
				}
				```
			- implementación con clases base y repositorio sin retorno
				```csharp
				public class Command : CommandBase
				public class CommandHandler : CommandHandlerRepositoryBase<Command, IProductCategoryRepository>
				//
				public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
					: base(repository, mapper, logger) 
				{
				}
				```
			- implementación con clases base y repositorio con retorno
				```csharp
				public class Command : CommandBase<int>
				public class CommandHandler : CommandHandlerRepositoryBase<Command, int, IProductCategoryRepository>
				//
				public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
					: base(repository, mapper, logger) 
				{
				}
				```
		- queries
			- implementación tradicional
				```csharp
				public class Query : IRequest<List<Response>>
				public class QueryHandler : IRequestHandler<Query, List<Response>>
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
				```
			- implementación con clases base
				```csharp
				public class Query : QueryBase<List<Response>>
				public class QueryHandler : QueryHandlerBase<Query, List<Response>>
				//
				private readonly IProductCategoryRepository _repository;
				//
				public QueryHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
					: base(mapper, logger)
				{
					_repository = repository;
				}
				```
			- implementación con clases base y repositorio
				```csharp
				public class Query : QueryBase<List<Response>>
				public class QueryHandler : QueryHandlerRepositoryBase<Query, List<Response>, IProductCategoryRepository>
				//
				public QueryHandler(IProductCategoryRepository repository, IMapper mapper, ILogService logger)
					: base(repository, mapper, logger) 
				{
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
			//
			public class UpdateProductCategory
			{
				public class Command : CommandBase, IMapTo<ProductCategory>
			//
			public class GetProductCategories
			{
				public class Response : IMapFrom<ProductCategory>
			```
	- manejo de resultados y errores
		- estandarizar erorres en cliente 400 o servidor 500
		- errores de validación estandar 400
		```json
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
		//
		{
		  "title": "Not Found",
		  "status": 404,
		}
		```
		- errores de validación de arquitectura 400
		```json
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
		//
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
		- error interno estandar 500
		```json
		{
		  "title": "An error occurred while processing your request.",
		  "status": 500
		}
		```
		- error interno de arquitectura 500 en desarrollo
		```json
		{
		  "title": "Internal Server Error",
		  "status": 500,
		  "detail": "System.Exception: Test Exception\r\n   at NCA.Production.Application.Features.Tests.Commands.TestException.CommandHandler.Handle(Command request, CancellationToken cancellationToken) in C:\\Source\\example-net-clean-architecture\\NCA.Production.Application\\Features\\Tests\\Commands\\TestException.cs:line 15\r\n   at MediatR.Wrappers.RequestHandlerWrapperImpl`2.<>c__DisplayClass1_0.<Handle>g__Handler|0()\r\n   at NCA.Common.Application.Behaviours.ValidationBehaviour`2.Handle(TRequest request, RequestHandlerDelegate`1 next, CancellationToken cancellationToken) in C:\\Source\\example-net-clean-architecture\\NCA.Common.Application\\Behaviours\\ValidationBehaviour.cs:line 32\r\n   at NCA.Production.ApiRestMin.Endpoints.TestsFeatures.TestException(ISender sender) in C:\\Source\\example-net-clean-architecture\\NCA.Production.ApiRestMin\\Endpoints\\TestsFeatures.cs:line 24\r\n   at Microsoft.AspNetCore.Http.RequestDelegateFactory.ExecuteTaskResult[T](Task`1 task, HttpContext httpContext)\r\n   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)\r\n   at Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddlewareImpl.<Invoke>g__Awaited|10_0(ExceptionHandlerMiddlewareImpl middleware, HttpContext context, Task task)"
		}
		```
		- error interno de arquitectura 500 en producción
		```json
		{
		  "title": "Internal Server Error",
		  "status": 500
		}
		```
		- referencias
			- https://code-maze.com/using-the-problemdetails-class-in-asp-net-core-web-api
			- https://www.milanjovanovic.tech/blog/global-error-handling-in-aspnetcore-8
			- https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern
			- https://medium.com/codex/custom-error-responses-with-asp-net-core-6-web-api-and-fluentvalidation-888a3b16c80f
			- https://github.com/ardalis/Result
	- uso de usings globales
referencias:
	- https://dev.to/isaacojeda/parte-1-cqrs-y-mediatr-implementando-cqrs-en-aspnet-56oe
	- https://github.com/dotnet-architecture/eShopOnContainers
	- https://github.com/dotnet/eShop
	- https://github.com/aspnetrun/run-aspnetcore-microservices
	- https://github.com/jasontaylordev/CleanArchitecture
	- https://github.com/ardalis/CleanArchitecture
	- https://github.com/matt-bentley/CleanArchitecture
	- https://github.com/ezzylearning/CleanArchitectureDemo