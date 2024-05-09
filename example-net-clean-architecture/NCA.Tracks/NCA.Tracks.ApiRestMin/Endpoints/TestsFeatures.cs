using NCA.Tracks.Application.Features.Tests.Commands;

namespace NCA.Tracks.ApiRestMin.Endpoints
{
    public class TestsFeatures : EndpointGroup
    {
        public override void Map(WebApplication app)
        {
            var group = app.Group(this);

            group.Get("FeatureValidation", TestValidation);
            group.Get("FeatureException", TestException);
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
