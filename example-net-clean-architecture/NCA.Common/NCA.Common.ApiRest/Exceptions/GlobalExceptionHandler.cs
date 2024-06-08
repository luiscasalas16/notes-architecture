using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NCA.Common.Application.Exceptions;
using NCA.Common.Application.Results;
using NCA.Common.Domain.Models;

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
                { typeof(BadHttpRequestException), HandleBadHttpRequestException },
            };
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionType = exception.GetType();

            if (_exceptionHandlers.TryGetValue(exceptionType, out Func<HttpContext, Exception, Task>? value))
                await value.Invoke(httpContext, exception);
            else
                await HandleUnknownException(httpContext, exception);

            return true;
        }

        private static async Task HandleValidationException(HttpContext httpContext, Exception ex)
        {
            var exception = (ValidationException)ex;

            ResultClientError resultClientError = new(exception.Errors);

            httpContext.Response.StatusCode = resultClientError.Status;

            await httpContext.Response.WriteAsJsonAsync(resultClientError);
        }

        private static async Task HandleBadHttpRequestException(HttpContext httpContext, Exception ex)
        {
            var exception = (BadHttpRequestException)ex;

            System.Diagnostics.Debug.WriteLine(exception);

            ResultClientError resultClientError = new ResultClientError([new Error($"Global", "Http request error caused by parameters used.")]);

            httpContext.Response.StatusCode = resultClientError.Status;

            await httpContext.Response.WriteAsJsonAsync(resultClientError);
        }

        private static async Task HandleUnknownException(HttpContext httpContext, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(exception);

            ResultServerError resultServerError = new(exception);

            httpContext.Response.StatusCode = resultServerError.Status;

            await httpContext.Response.WriteAsJsonAsync(resultServerError);
        }
    }
}
