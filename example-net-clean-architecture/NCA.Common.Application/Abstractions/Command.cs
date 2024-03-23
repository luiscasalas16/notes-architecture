using AutoMapper;
using FluentValidation;
using MediatR;
using NCA.Common.Application.Infrastructure.Log;
using NCA.Common.Application.Repositories;

namespace NCA.Common.Application.Abstractions
{
    //CommandBase

    public abstract class CommandBase<T> : IRequest<T>;

    public abstract class CommandBase : CommandBase<Unit>;

    //CommandHandlerBase

    public abstract class CommandHandlerBase
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;

        protected CommandHandlerBase(IMapper mapper, ILogger logger)
        {
            Mapper = mapper;
            Logger = logger;
        }
    }

    public abstract class CommandHandlerBase<TCommand> : CommandHandlerBase, IRequestHandler<TCommand, Unit>
        where TCommand : CommandBase
    {
        protected CommandHandlerBase(IMapper mapper, ILogger logger)
            : base(mapper, logger) { }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            await Handle(request);
            return Unit.Value;
        }

        public abstract Task Handle(TCommand request);
    }

    public abstract class CommandHandlerBase<TCommand, TResponse> : CommandHandlerBase, IRequestHandler<TCommand, TResponse>
        where TCommand : CommandBase<TResponse>
    {
        protected CommandHandlerBase(IMapper mapper, ILogger logger)
            : base(mapper, logger) { }

        public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request);
        }

        public abstract Task<TResponse> Handle(TCommand request);
    }

    //CommandHandlerRepositoryBase

    public abstract class CommandHandlerRepositoryBase<TRepository>
        where TRepository : IRepository
    {
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;
        protected readonly TRepository Repository;

        protected CommandHandlerRepositoryBase(TRepository repository, IMapper mapper, ILogger logger)
        {
            Mapper = mapper;
            Logger = logger;
            Repository = repository;
        }
    }

    public abstract class CommandHandlerRepositoryBase<TCommand, TResponse, TRepository> : CommandHandlerRepositoryBase<TRepository>, IRequestHandler<TCommand, TResponse>
        where TCommand : CommandBase<TResponse>
        where TRepository : IRepository
    {
        protected CommandHandlerRepositoryBase(TRepository repository, IMapper mapper, ILogger logger)
            : base(repository, mapper, logger) { }

        public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            return await Handle(request);
        }

        public abstract Task<TResponse> Handle(TCommand request);
    }

    public abstract class CommandHandlerRepositoryBase<TCommand, TRepository> : CommandHandlerRepositoryBase<TRepository>, IRequestHandler<TCommand, Unit>
        where TCommand : CommandBase
        where TRepository : IRepository
    {
        protected CommandHandlerRepositoryBase(TRepository repository, IMapper mapper, ILogger logger)
            : base(repository, mapper, logger) { }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            await Handle(request);
            return Unit.Value;
        }

        public abstract Task Handle(TCommand request);
    }

    //CommandValidatorBase

    public abstract class CommandValidatorBase<TCommand> : AbstractValidator<TCommand> { }
}
