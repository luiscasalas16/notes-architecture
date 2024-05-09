using NCA.COL.Infrastructure.Repositories;
using Quartz;

namespace NCA.Common.Scheduler.Jobs
{
    public static class ApiRestScheduler
    {
        public const string DefaultGroup = "default";

        // csharpier-ignore
        public static void AddApiRestJobs(IServiceCollectionQuartzConfigurator quartz, IConfiguration configuration)
        {
            var dbContext = new SchedulerDbContext(configuration);

            var datas = dbContext.ApiRestDatas!.Where(t => t.Status == ApiRestJob.StatusActive).ToList();

            foreach (var data in datas)
            {
                quartz.AddJob<ApiRestJob>(opts => opts
                    .WithIdentity(data.Code, DefaultGroup)
                    .DisallowConcurrentExecution(true)
                    .UsingJobData(ApiRestJob.Code, data.Code)
                    .UsingJobData(ApiRestJob.Endpoint, data.Endpoint))
                    ;

                quartz.AddTrigger(opts => opts
                    .ForJob(data.Code, DefaultGroup)
                    .WithIdentity(data.Code, DefaultGroup)
                    .WithCronSchedule(data.Schedule))
                    ;
            }
        }

        // csharpier-ignore
        public static async Task AddApiRestJob(IScheduler scheduler, ApiRestData data)
        {
            IJobDetail job = JobBuilder
                .Create<ApiRestJob>()
                .WithIdentity(data.Code, DefaultGroup)
                .DisallowConcurrentExecution(true)
                .UsingJobData(ApiRestJob.Code, data.Code)
                .UsingJobData(ApiRestJob.Endpoint, data.Endpoint)
                .Build();

            ITrigger trigger = TriggerBuilder
                .Create()
                .ForJob(data.Code, DefaultGroup)
                .WithIdentity(data.Code, DefaultGroup)
                .WithCronSchedule(data.Schedule)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public static async Task DeleteApiRestJob(IScheduler scheduler, ApiRestData data)
        {
            await scheduler.DeleteJob(new JobKey(data.Code, DefaultGroup));
        }
    }
}
