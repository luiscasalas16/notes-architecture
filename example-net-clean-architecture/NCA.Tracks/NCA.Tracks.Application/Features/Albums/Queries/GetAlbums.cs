namespace NCA.Tracks.Application.Features.Albums.Queries
{
    public class GetAlbums
    {
        public class Query : IRequest<Result<List<Response>>>
        {
            public string? FilterTitle { get; set; }

            public Query(string? filterTitle)
            {
                FilterTitle = filterTitle;
            }
        }

        public class QueryHandler : IRequestHandler<Query, Result<List<Response>>>
        {
            private readonly IAlbumRepository _repository;
            private readonly IMapper _mapper;

            public QueryHandler(IAlbumRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<List<Response>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var entities = await _repository.GetAll();

                return Result.Success(_mapper.Map<List<Response>>(entities));
            }
        }

        public class Response : IMapFrom<Album>
        {
            public int AlbumId { get; set; }
            public string Title { get; set; } = null!;
            public int ArtistId { get; set; }
        }
    }
}
