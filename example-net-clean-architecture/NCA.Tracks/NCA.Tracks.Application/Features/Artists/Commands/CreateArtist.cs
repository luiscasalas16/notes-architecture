namespace NCA.Tracks.Application.Features.Artists.Commands
{
    public class CreateArtist
    {
        public class Command : CommandBase<Result<int>>, IMapTo<Artist>
        {
            public string Name { get; set; } = null!;
        }

        public class CommandHandler : CommandHandlerRepositoryBase<Command, Result<int>, IArtistRepository>
        {
            public CommandHandler(IArtistRepository repository, IUnitWork unitWork, IMapper mapper, ILogger logger)
                : base(repository, unitWork, mapper, logger) { }

            public override async Task<Result<int>> Handle(Command request)
            {
                var entity = Mapper.Map<Artist>(request);

                Repository.Insert(entity);

                await UnitWork.Save();

                return Result<int>.Success(entity.ArtistId);
            }
        }

        public class CommandValidator : CommandValidatorBase<Command>
        {
            private static string BaseCode => nameof(CreateArtist);

            private Error NameMinimumLength => new($"{BaseCode}.{nameof(NameMinimumLength)}", "Minimum 5 characters.");

            // csharpier-ignore
            public CommandValidator()
            {
                RuleFor(p => p.Name)
                    .NotNull().WithError(GenericErrors.NotNull)
                    .NotEmpty().WithError(GenericErrors.NotEmpty)
                    .MaximumLength(50).WithError(Artist.Errors.NameMaximumLength)
                    .MinimumLength(5).WithError(NameMinimumLength)
                    ;
            }
        }
    }
}
