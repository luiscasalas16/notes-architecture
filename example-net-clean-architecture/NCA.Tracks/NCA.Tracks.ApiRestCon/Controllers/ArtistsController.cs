using Microsoft.AspNetCore.Mvc;
using NCA.Tracks.Application.Features.Artists.Commands;
using NCA.Tracks.Application.Features.Artists.Queries;

namespace NCA.Tracks.ApiRestCon.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArtistsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Result<List<GetArtists.Response>>> GetAlbums(string? filterName)
        {
            return await _mediator.Send(new GetArtists.Query(filterName));
        }

        [HttpPost]
        public async Task<Result<int>> CreateArtist([FromBody] CreateArtist.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<Result> UpdateArtist([FromBody] UpdateArtist.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{ArtistId}")]
        public async Task<Result> DeleteArtist(int ArtistId)
        {
            return await _mediator.Send(new DeleteArtist.Command(ArtistId));
        }
    }
}
