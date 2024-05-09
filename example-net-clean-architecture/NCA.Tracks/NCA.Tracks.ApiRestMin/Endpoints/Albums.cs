using NCA.Tracks.Application.Features.Albums.Queries;

namespace NCA.Tracks.ApiRestMin.Endpoints
{
    public class Albums : EndpointGroup
    {
        public override void Map(WebApplication app)
        {
            var group = app.Group(this);

            group.Get(GetAlbums);
        }

        public Task<Result<List<GetAlbums.Response>>> GetAlbums(ISender sender, [AsParameters] GetAlbums.Query query)
        {
            return sender.Send(query);
        }
    }
}
