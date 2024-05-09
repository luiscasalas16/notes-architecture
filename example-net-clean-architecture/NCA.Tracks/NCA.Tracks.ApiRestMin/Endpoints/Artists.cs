using NCA.Tracks.Application.Features.Artists.Commands;
using NCA.Tracks.Application.Features.Artists.Queries;

namespace NCA.Tracks.ApiRestMin.Endpoints
{
    public class Artists : EndpointGroup
    {
        public override void Map(WebApplication app)
        {
            var group = app.Group(this);

            group.Get(GetArtists);
            group.Post(CreateArtist);
            group.Put(UpdateArtist);
            group.Delete("{ArtistId}", DeleteArtist);
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

        public async Task<Result> DeleteArtist(ISender sender, int ArtistId)
        {
            return await sender.Send(new DeleteArtist.Command(ArtistId));
        }

        //public Task<List<GetArtists.Response>> GetArtists(ISender sender, [AsParameters] GetArtists.Query query)
        //{
        //    return sender.Send(query);
        //}

        //public Task<int> CreateArtist(ISender sender, CreateArtist.Command command)
        //{
        //    return sender.Send(command);
        //}

        //public async Task<IResult> UpdateArtist(ISender sender, UpdateArtist.Command command)
        //{
        //    await sender.Send(command);

        //    return Results.NoContent();
        //}

        //public async Task<IResult> DeleteArtist(ISender sender, int ArtistId)
        //{
        //    await sender.Send(new DeleteArtist.Command(ArtistId));

        //    return Results.NoContent();
        //}
    }
}
