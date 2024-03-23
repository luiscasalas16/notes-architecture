using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NCA.Common.Api.Endpoints;
using NCA.Common.Application.Exceptions;
using NCA.Common.Application.Results;

namespace NCA.Common.Api.Exceptions
{
    // csharpier-ignore
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

        public GlobalExceptionHandler()
        {
            _exceptionHandlers = new()
            {
                { typeof(ValidationException), HandleValidationException },
            };
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionType = exception.GetType();

            if (_exceptionHandlers.ContainsKey(exceptionType))
                await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
            else
                await HandleUnknownException(httpContext, exception);

            return true;
        }

        private async Task HandleValidationException(HttpContext httpContext, Exception ex)
        {
            var exception = (ValidationException)ex;

            ResultClientError resultClientError = new(exception.Errors);

            httpContext.Response.StatusCode = resultClientError.Status;

            await httpContext.Response.WriteAsJsonAsync(resultClientError);
        }

        private async Task HandleUnknownException(HttpContext httpContext, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception);

            ResultServerError resultServerError = new(exception);

            httpContext.Response.StatusCode = resultServerError.Status;

            await httpContext.Response.WriteAsJsonAsync(resultServerError);
        }
    }
}
