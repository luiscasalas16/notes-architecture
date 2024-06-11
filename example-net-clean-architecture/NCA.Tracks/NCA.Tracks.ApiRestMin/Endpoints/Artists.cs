using NCA.Tracks.Application.Features.Artists.Commands;
using NCA.Tracks.Application.Features.Artists.Queries;
using NCA.Tracks.Domain.Models;

namespace NCA.Tracks.ApiRestMin.Endpoints
{
    public class Artists : EndpointsMapper
    {
        public Artists(WebApplication webApplication)
            : base(webApplication) { }

        public override void Map()
        {
            var group = Group();

            group.Get(GetArtists);
            group.Post(CreateArtist);
            group.Put(UpdateArtist);
            group.Delete("{id}", DeleteArtist);
        }

        public async Task<Result<List<GetArtists.Response>>> GetArtists(ISender sender, [AsParameters] GetArtists.Query query)
        {
            return await sender.Send(query);
        }

        public async Task<Result<int>> CreateArtist(ISender sender, CreateArtist.Command command)
        {
            return await sender.Send(command);
        }

        public async Task<Result> UpdateArtist(ISender sender, UpdateArtist.Command command)
        {
            return await sender.Send(command);
        }

        public async Task<Result> DeleteArtist(ISender sender, int id)
        {
            return await sender.Send(new DeleteArtist.Command() { ArtistId = id });
        }
    }
}
