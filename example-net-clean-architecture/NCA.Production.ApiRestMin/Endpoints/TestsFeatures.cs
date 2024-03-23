using MediatR;
using NCA.Common.ApiRest.Endpoints;
using NCA.Common.Application.Results;
using NCA.Production.Application.Features.Tests.Commands;

namespace NCA.Production.ApiRestMin.Endpoints
{
    public class TestsFeatures : EndpointGroup
    {
        public override void Map(WebApplication app)
        {
            // csharpier-ignore
            app.Group(this)

                .Get("FeatureValidation", TestValidation)

                .Get("FeatureException", TestException)

                ;
        }

        public async Task<Result> TestException(ISender sender)
        {
            await sender.Send(new TestException.Command());
            return Result.Success();
        }

        public async Task<Result> TestValidation(ISender sender)
        {
            await sender.Send(new TestValidation.Command());
            return Result.Success();
        }
    }
}
