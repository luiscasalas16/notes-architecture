using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NCA.COL.Infrastructure.Repositories;
using NCA.Common.Api.Endpoints;
using NCA.Common.Api.Helpers;
using NCA.Common.Infrastructure.Log;

namespace NCA.Common.Scheduler
{
    public class Program
    {
        protected Program() { }

        public static void Main(string[] args)
        {
            //----------
            // Build
            //----------

            var builder = WebApplication.CreateBuilder(args);

            //swagger
            builder.AddCommonSwagger();

            // global exceptions handler
            builder.AddCommonExceptionHandler();

            // health check
            builder.AddCommonHealthCheck();

            // dependency injection scheduler
            builder.Services.AddSchedulerServices(builder.Configuration);
            // dependency injection infrastructure common log
            builder.Services.AddInfrastructureCommonLogServices();

            var app = builder.Build();

            //----------
            // Configure
            //----------

            if (app.Environment.IsDevelopment())
            {
                VerifyInfrastructure(builder.Configuration);

                //swagger
                app.UseCommonSwagger();
            }

            // map minimals endpoints groups
            app.MapEndpoints(Assembly.GetExecutingAssembly());

            // global exceptions handler
            app.UseCommonExceptionHandler();

            // health check
            app.UseCommonHealthCheck();

            //----------
            // Run
            //----------

            app.Run();
        }

        private static void VerifyInfrastructure(IConfiguration configuration)
        {
            try
            {
                using var context = new SchedulerDbContext(configuration);

                context.Database.OpenConnection();
                context.Database.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("The SchedulerDbContext is not available.", ex);
            }
        }
    }
}
