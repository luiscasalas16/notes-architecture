using System.Reflection;
using MediatR;
using NCA.COL.Infrastructure.Repositories;
using NCA.Common.Application.Behaviours;
using NCA.Common.Scheduler.Jobs;
using Quartz;

namespace NCA.Common.Scheduler
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSchedulerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

            services.AddQuartz(q =>
            {
                ApiRestScheduler.AddApiRestJobs(q, configuration);
            });

            services.AddQuartzHostedService(opt =>
            {
                opt.WaitForJobsToComplete = true;
                opt.AwaitApplicationStarted = true;
            });

            services.AddDbContext<SchedulerDbContext>();
            services.AddHealthChecks().AddDbContextCheck<SchedulerDbContext>();

            services.AddHttpClient();

            return services;
        }
    }
}
