using System.Reflection;
using NCA.Common.Api.Exceptions;
using NCA.Common.Api.Helpers;
using NCA.Common.Infrastructure.Log;
using NCA.Production.Application;
using NCA.Production.Infrastructure;

namespace NCA.Production.ApiRestMin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //----------
            // Build
            //----------

            var builder = WebApplication.CreateBuilder(args);

            //swagger
            builder.Services.AddCommonSwagger();

            // global exceptions handler
            builder.Services.AddCommonExceptionHandler();

            // health check
            builder.Services.AddCommonHealthCheck();

            // dependency injection Application
            builder.Services.AddApplicationServices();
            // dependency injection Infrastructure
            builder.Services.AddInfrastructureServices(builder.Configuration);
            // dependency injection Infrastructure Common Log
            builder.Services.AddInfrastructureCommonLogServices();

            var app = builder.Build();

            //----------
            // Configure
            //----------

            if (app.Environment.IsDevelopment())
            {
                //swagger
                app.UseCommonSwagger();
            }

            // global exceptions handler
            app.UseCommonExceptionHandler();

            // health check
            app.UseCommonHealthCheck();

            // map minimals endpoints groups
            app.MapEndpoints(Assembly.GetExecutingAssembly());

            //----------
            // Run
            //----------

            app.Run();
        }
    }
}
