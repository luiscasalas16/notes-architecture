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

            // versions
            builder.AddCommonVersions(versions: [1, 2]);
            // global exceptions handler
            builder.AddCommonExceptionHandler();
            // health check
            builder.AddCommonHealthCheck();
            // swagger
            builder.AddCommonSwagger();

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

            // versions
            app.UseCommonVersions();
            // global exceptions handler
            app.UseCommonExceptionHandler();
            // health check
            app.UseCommonHealthCheck();
            //swagger
            app.UseCommonSwagger();

            // map minimals endpoints groups
            app.MapEndpoints(Assembly.GetExecutingAssembly());

            //----------
            // Run
            //----------

            app.Run();
        }
    }
}
