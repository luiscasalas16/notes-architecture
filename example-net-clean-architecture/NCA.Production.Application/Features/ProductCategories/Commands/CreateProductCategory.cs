using NCA.Common.Application.Validators;

namespace NCA.Production.Application.Features.ProductCategories.Commands
{
    public class CreateProductCategory
    {
        public class Command : CommandBase<Result<int>>, IMapTo<ProductCategory>
        {
            public string Name { get; set; } = null!;
        }

        public class CommandHandler : CommandHandlerRepositoryBase<Command, Result<int>, IProductCategoryRepository>
        {
            public CommandHandler(IProductCategoryRepository repository, IMapper mapper, ILogger logger)
                : base(repository, mapper, logger) { }

            public override async Task<Result<int>> Handle(Command request)
            {
                var entity = Mapper.Map<ProductCategory>(request);

                entity.Rowguid = Guid.NewGuid();
                entity.ModifiedDate = DateTime.Now;

                var newEntity = await Repository.Add(entity);

                return Result<int>.Success(newEntity.ProductCategoryId);
            }
        }

        public class CommandValidator : CommandValidatorBase<Command>
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
