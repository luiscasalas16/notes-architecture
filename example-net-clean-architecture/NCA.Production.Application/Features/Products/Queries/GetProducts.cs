namespace NCA.Production.Application.Features.Products.Queries
{
    public class GetProducts
    {
        public class Query : IRequest<Result<List<Response>>>
        {
            public string? FilterName { get; set; }

            public Query(string? filterName)
            {
                FilterName = filterName;
            }
        }

        public class QueryHandler : IRequestHandler<Query, Result<List<Response>>>
        {
            private readonly IProductRepository _repository;
            private readonly IMapper _mapper;

            public QueryHandler(IProductRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<List<Response>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var entities = await _repository.GetAll();

                return Result<List<Response>>.Success(_mapper.Map<List<Response>>(entities));
            }
        }

        public class Response : IMapFrom<Product>
        {
            public int ProductId { get; set; }

            public string Name { get; set; } = null!;

            public string ProductNumber { get; set; } = null!;
        }
    }
}
