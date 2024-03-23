namespace NCA.Production.Application.Features.ProductCategories.Queries
{
    public class GetProductCategories
    {
        public class Query : QueryBase<Result<List<Response>>>
        {
            public string? FilterName { get; set; }

            public Query(string? filterName)
            {
                FilterName = filterName;
            }
        }

        public class QueryHandler : QueryHandlerRepositoryBase<Query, Result<List<Response>>, IProductCategoryRepository>
        {
            public QueryHandler(IProductCategoryRepository repository, IMapper mapper, ILogger logger)
                : base(repository, mapper, logger) { }

            public override async Task<Result<List<Response>>> Handle(Query request)
            {
                var entities = await Repository.GetAll();

                return Result<List<Response>>.Success(Mapper.Map<List<Response>>(entities));
            }
        }

        public class Response : IMapFrom<ProductCategory>
        {
            public int ProductCategoryId { get; set; }

            public string Name { get; set; } = null!;
        }
    }
}
