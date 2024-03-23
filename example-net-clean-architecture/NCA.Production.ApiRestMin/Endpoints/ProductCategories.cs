using MediatR;
using NCA.Common.ApiRest.Endpoints;
using NCA.Common.Application.Results;
using NCA.Production.Application.Features.ProductCategories.Commands;
using NCA.Production.Application.Features.ProductCategories.Queries;

namespace NCA.Production.ApiRestMin.Endpoints
{
    public class ProductCategories : EndpointGroup
    {
        public override void Map(WebApplication app)
        {
            // csharpier-ignore
            app.Group(this)
                .Get(GetProductCategories)
                .Post(CreateProductCategory)
                .Put(UpdateProductCategory)
                .Delete("{productCategoryId}", DeleteProductCategory)
                ;
        }

        public async Task<Result<List<GetProductCategories.Response>>> GetProductCategories(ISender sender, [AsParameters] GetProductCategories.Query query)
        {
            return await sender.Send(query);
        }

        public async Task<Result<int>> CreateProductCategory(ISender sender, CreateProductCategory.Command command)
        {
            return await sender.Send(command);
        }

        public async Task<Result> UpdateProductCategory(ISender sender, UpdateProductCategory.Command command)
        {
            return await sender.Send(command);
        }

        public async Task<Result> DeleteProductCategory(ISender sender, int productCategoryId)
        {
            return await sender.Send(new DeleteProductCategory.Command(productCategoryId));
        }

        //public Task<List<GetProductCategories.Response>> GetProductCategories(ISender sender, [AsParameters] GetProductCategories.Query query)
        //{
        //    return sender.Send(query);
        //}

        //public Task<int> CreateProductCategory(ISender sender, CreateProductCategory.Command command)
        //{
        //    return sender.Send(command);
        //}

        //public async Task<IResult> UpdateProductCategory(ISender sender, UpdateProductCategory.Command command)
        //{
        //    await sender.Send(command);

        //    return Results.NoContent();
        //}

        //public async Task<IResult> DeleteProductCategory(ISender sender, int productCategoryId)
        //{
        //    await sender.Send(new DeleteProductCategory.Command(productCategoryId));

        //    return Results.NoContent();
        //}
    }
}
