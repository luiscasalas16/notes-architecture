using NCA.COL.Infrastructure.Repositories;
using NCA.Common.Application.Abstractions;
using NCA.Common.Application.Results;
using NCA.Common.Scheduler.Jobs;
using Quartz;
using Quartz.Impl.Matchers;

namespace NCA.Common.Scheduler.Features.ApiRestJobs.Queries
{
    public class Status
    {
        public class Query : QueryBase<Result<List<Response>>> { }

        public class QueryHandler : QueryHandlerBase<Query, Result<List<Response>>>
        {
            private readonly ISchedulerFactory _sf;
            private readonly SchedulerDbContext _db;

            public QueryHandler(ISchedulerFactory sf, SchedulerDbContext db)
                : base()
            {
                _sf = sf;
                _db = db;
            }

            public override async Task<Result<List<Response>>> Handle(Query request)
            {
                var datas = _db.ApiRestDatas!.ToList();

                var results = datas.Select(t => new Response() { Code = t.Code, Status = t.Status }).ToList();

                var scheduler = await _sf.GetScheduler();

                var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());

                foreach (var result in results)
                {
                    var jobKey = jobKeys.FirstOrDefault(t => t.Name == result.Code);

                    if (jobKey != null && result.Status == ApiRestJob.StatusInactive)
                        return Result.Failure<List<Response>>(ApiRestErrors.InvalidState());
                    else if (jobKey == null && result.Status == ApiRestJob.StatusActive)
                        return Result.Failure<List<Response>>(ApiRestErrors.InvalidState());
                }

                return Result.Success(results);
            }
        }

        public class Response
        {
            public required string Code { get; set; }
            public required string Status { get; set; }
        }
    }
}
