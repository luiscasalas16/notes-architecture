using AutoMapper;
using NCA.Common.Application.Abstractions;
using NCA.Common.Application.Infrastructure.Log;
using NCA.Common.Application.Results;
using NCA.Production.Domain.Contracts.Repositories;
using NCA.Production.Domain.Errors;

namespace NCA.Production.Application.Features.ProductCategories.Commands
{
    public class DeleteProductCategory
    {
        public class Command : CommandBase<Result>
        {
            public int ProductCategoryId { get; set; }

            public Command(int productCategoryId)
            {
                ProductCategoryId = productCategoryId;
            }
        }

        public class CommandHandler : CommandHandlerRepositoryBase<Command, Result, IProductCategoryRepository>
        {
            public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogger logger)
                : base(repository, mapper, logger) { }

            public override async Task<Result> Handle(Command request)
            {
                var entity = await Repository.GetById(request.ProductCategoryId);

                if (entity == null)
                    return Result.Failure(GenericErrors.NotFound(request.ProductCategoryId));

                await Repository.Delete(entity);

                return Result.Success();
            }
        }
    }
}
