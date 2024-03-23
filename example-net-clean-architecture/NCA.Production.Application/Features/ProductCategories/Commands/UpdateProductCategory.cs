using AutoMapper;
using FluentValidation;
using NCA.Common.Application.Abstractions;
using NCA.Common.Application.Infrastructure.Log;
using NCA.Common.Application.Maps;
using NCA.Common.Application.Results;
using NCA.Common.Application.Validators;
using NCA.Common.Domain.Models;
using NCA.Production.Domain.Contracts.Repositories;
using NCA.Production.Domain.Errors;
using NCA.Production.Domain.Models;

namespace NCA.Production.Application.Features.ProductCategories.Commands
{
    public class UpdateProductCategory
    {
        public class Command : CommandBase<Result>, IMapTo<ProductCategory>
        {
            public int ProductCategoryId { get; set; }

            public string Name { get; set; } = null!;
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

                //if (entity == null)
                //    throw new NotFoundException(nameof(ProductCategory), request.ProductCategoryId);

                Mapper.Map(request, entity, typeof(Command), typeof(ProductCategory));

                entity.ModifiedDate = DateTime.Now;

                await Repository.Update(entity);

                return Result.Success();
            }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private static string BaseCode => nameof(CreateProductCategory);

            private Error NameMinimumLength => new($"{BaseCode}.{nameof(NameMinimumLength)}", "Minimum 5 characters.");

            // csharpier-ignore
            public CommandValidator()
            {
                RuleFor(p => p.Name)
                    .NotNull().WithError(GenericErrors.NotNull)
                    .NotEmpty().WithError(GenericErrors.NotEmpty)
                    .MaximumLength(50).WithError(ProductCategory.Errors.NameMaximumLength)
                    .MinimumLength(5).WithError(NameMinimumLength)
                    ;
            }
        }
    }
}
