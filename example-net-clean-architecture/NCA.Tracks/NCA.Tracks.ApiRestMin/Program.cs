using System.Reflection;
using NCA.Common.Infrastructure.Log;
using NCA.Tracks.Application;
using NCA.Tracks.Infrastructure;

namespace NCA.Tracks.ApiRestMin
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
            // versioning
            builder.Services.AddCommonVersioning();

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
            // versioning
            app.UseCommonVersioning();

            // map minimals endpoints groups
            app.MapEndpoints(Assembly.GetExecutingAssembly());

            //----------
            // Run
            //----------

            app.Run();
        }
    }
}
