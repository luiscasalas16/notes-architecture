using System.Reflection;
using NCA.Common.Api.Exceptions;
using NCA.Common.Infrastructure.Log;
using NCA.Production.Application;
using NCA.Production.Infrastructure;

namespace NCA.Production.ApiRestMin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
                //Fix the error System.InvalidOperationException: Can't use schemaId "$MyType" for type "$OneNamespace.MyType". The same schemaId is already used for type "$OtherNamespace.MyType".
                options.CustomSchemaIds(s => s.FullName!.Replace("+", "."));
            });

            //global exceptions handler
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddInfrastructureCommonLoggers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //global exceptions handler
            app.UseExceptionHandler();

            app.UseAuthorization();

            app.MapEndpoints(Assembly.GetExecutingAssembly());

            app.Run();
        }
    }
}
