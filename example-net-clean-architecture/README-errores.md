# Errores

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
