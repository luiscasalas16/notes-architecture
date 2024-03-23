using NCA.Production.Application.Features.Products.Queries;

namespace NCA.Production.ApiRestMin.Endpoints
{
    public class Products : EndpointGroup
    {
        public override void Map(WebApplication app)
        {
            // csharpier-ignore
            app.Group(this)

                .Get(GetProducts)

                ;
        }

        public Task<Result<List<GetProducts.Response>>> GetProducts(ISender sender, [AsParameters] GetProducts.Query query)
        {
            return sender.Send(query);
        }
    }
}
