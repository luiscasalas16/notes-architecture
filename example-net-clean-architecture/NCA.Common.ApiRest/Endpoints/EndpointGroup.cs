using Microsoft.AspNetCore.Builder;

namespace NCA.Common.ApiRest.Endpoints
{
    public abstract class EndpointGroup
    {
        public abstract void Map(WebApplication app);
    }
}
