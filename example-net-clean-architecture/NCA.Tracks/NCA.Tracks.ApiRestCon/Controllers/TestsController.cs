using Microsoft.AspNetCore.Mvc;
using NCA.Tracks.Application.Features.Tests.Commands;

namespace NCA.Tracks.ApiRest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("TestException")]
        public async Task<OkResult> TestException()
        {
            await _mediator.Send(new TestException.Command());
            return Ok();
        }

        [HttpGet("TestValidation")]
        public async Task<OkResult> TestValidation()
        {
            await _mediator.Send(new TestValidation.Command());
            return Ok();
        }
    }
}
