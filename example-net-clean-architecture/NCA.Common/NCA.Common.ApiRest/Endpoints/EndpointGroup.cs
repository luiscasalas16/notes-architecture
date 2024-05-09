using Microsoft.AspNetCore.Builder;

namespace NCA.Common.Api.Endpoints
{
    public abstract class EndpointGroup
    {
        public abstract void Map(WebApplication app);
    }
}
