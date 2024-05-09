using NCA.Common.Api.Endpoints;
using NCA.Common.Application.Results;
using NCA.Common.Scheduler.Features.ApiRestJobs.Commands;
using NCA.Common.Scheduler.Features.ApiRestJobs.Queries;
using MediatR;

namespace NCA.Common.Scheduler.Endpoints
{
    public class ApiRestJobs : EndpointGroup
    {
        public override void Map(WebApplication app)
        {
            var group = app.Group(this);

            group.Get("/status", ApiRestJobsStatus);
            group.Get("/active", ApiRestJobActive);
            group.Get("/inactive", ApiRestJobInactive);
        }

        public async Task<Result<List<Status.Response>>> ApiRestJobsStatus(ISender sender, [AsParameters] Status.Query query)
        {
            return await sender.Send(query);
        }

        public async Task<Result> ApiRestJobActive(ISender sender, [AsParameters] Active.Command command)
        {
            return await sender.Send(command);
        }

        public async Task<Result> ApiRestJobInactive(ISender sender, [AsParameters] Inactive.Command command)
        {
            return await sender.Send(command);
        }
    }
}
