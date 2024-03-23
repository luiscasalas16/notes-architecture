using MediatR;
using Microsoft.AspNetCore.Mvc;
using NCA.Production.Application.Features.Products.Queries;

namespace NCA.Production.ApiRest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProducts.Response>>> GetProducts(string? filterName)
        {
            return Ok(await _mediator.Send(new GetProducts.Query(filterName)));
        }
    }
}
