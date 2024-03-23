using Microsoft.AspNetCore.Mvc;
using NCA.Production.Application.Features.ProductCategories.Commands;
using NCA.Production.Application.Features.ProductCategories.Queries;

namespace NCA.Production.ApiRestCon.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Result<List<GetProductCategories.Response>>> GetProducts(string? filterName)
        {
            return await _mediator.Send(new GetProductCategories.Query(filterName));
        }

        [HttpPost]
        public async Task<Result<int>> CreateProductCategory([FromBody] CreateProductCategory.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<Result> UpdateProductCategory([FromBody] UpdateProductCategory.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{productCategoryId}")]
        public async Task<Result> DeleteProductCategory(int productCategoryId)
        {
            return await _mediator.Send(new DeleteProductCategory.Command(productCategoryId));
        }
    }
}
