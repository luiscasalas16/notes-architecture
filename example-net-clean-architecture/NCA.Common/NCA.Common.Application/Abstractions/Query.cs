using AutoMapper;
using NCA.Common.Application.Infrastructure.Log;
using NCA.Common.Application.Repositories;
using MediatR;

namespace NCA.Common.Application.Abstractions
{
    //QueryBase

    public abstract class QueryBase<T> : IRequest<T>;

    //QueryHandlerBase

    public abstract class QueryHandlerBase<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : QueryBase<TResponse>
    {
        protected readonly IMapper _mapper;
        protected IMapper Mapper
        {
            get
            {
                ArgumentNullException.ThrowIfNull(_mapper);
                return _mapper;
            }
        }

        protected readonly ILogger _logger;
        protected ILogger Logger
        {
            get
            {
                ArgumentNullException.ThrowIfNull(_logger);
                return _logger;
            }
        }

        protected QueryHandlerBase()
            : this(null!, null!) { }

        protected QueryHandlerBase(IMapper mapper, ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return await Handle(request);
        }

        public abstract Task<TResponse> Handle(TQuery request);
    }

    //QueryHandlerRepositoryBase

    public abstract class QueryHandlerRepositoryBase<TQuery, TResponse, TRepository> : IRequestHandler<TQuery, TResponse>
        where TQuery : QueryBase<TResponse>
        where TRepository : IRepository
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;
        protected readonly TRepository Repository;

        protected QueryHandlerRepositoryBase(TRepository repository, IMapper mapper, ILogger logger)
        {
            Mapper = mapper;
            Logger = logger;
            Repository = repository;
        }

        public async Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return await Handle(request);
        }

        public abstract Task<TResponse> Handle(TQuery request);
    }
}
