using Microsoft.AspNetCore.Mvc;
using NCA.Tracks.Application.Features.Albums.Queries;

namespace NCA.Tracks.ApiRest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlbumsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAlbums.Response>>> GetAlbums(string? filterName)
        {
            return Ok(await _mediator.Send(new GetAlbums.Query(filterName)));
        }
    }
}
