using NCA.Tracks.Application.Features.Albums.Queries;

namespace NCA.Tracks.ApiRestMin.Endpoints
{
    public class Albums : EndpointsMapper
    {
        public Albums(WebApplication webApplication)
            : base(webApplication) { }

        public override void Map()
        {
            var group = Group();

            group.Get(GetAlbums);
        }

        public Task<Result<List<GetAlbums.Response>>> GetAlbums(ISender sender, [AsParameters] GetAlbums.Query query)
        {
            return sender.Send(query);
        }
    }
}
