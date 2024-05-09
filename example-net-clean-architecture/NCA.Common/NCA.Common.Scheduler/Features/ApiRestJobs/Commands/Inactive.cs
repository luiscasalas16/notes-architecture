using NCA.COL.Infrastructure.Repositories;
using NCA.Common.Application.Abstractions;
using NCA.Common.Application.Results;
using NCA.Common.Scheduler.Jobs;
using Quartz;
using Quartz.Impl.Matchers;

namespace NCA.Common.Scheduler.Features.ApiRestJobs.Commands
{
    public class Inactive
    {
        public class Command : CommandBase<Result>
        {
            public required string Code { get; set; }
        }

        public class CommandHandler : CommandHandlerBase<Command, Result>
        {
            private readonly ISchedulerFactory _sf;
            private readonly SchedulerDbContext _db;

            public CommandHandler(ISchedulerFactory sf, SchedulerDbContext db)
                : base()
            {
                _sf = sf;
                _db = db;
            }

            public override async Task<Result> Handle(Command request)
            {
                var data = _db.ApiRestDatas!.Where(t => t.Code == request.Code).FirstOrDefault();

                if (data == null)
                    return Result.Failure(ApiRestErrors.JobNotFound());

                var scheduler = await _sf.GetScheduler();

                var jobKey = (await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup())).FirstOrDefault(t => t.Name == request.Code);

                if (jobKey == null)
                {
                    if (data.Status == ApiRestJob.StatusInactive)
                        return Result.Failure(ApiRestErrors.JobAlreadyStatus(ApiRestJob.StatusInactive));
                    else
                        return Result.Failure(ApiRestErrors.InvalidState());
                }

                await ApiRestScheduler.DeleteApiRestJob(scheduler, data);

                data.Status = ApiRestJob.StatusInactive;

                await _db.SaveChangesAsync();

                return Result.Success();
            }
        }
    }
}
