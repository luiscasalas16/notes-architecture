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
            public CommandHandler(IArtistRepository repository, IMapper mapper, ILogger logger)
                : base(repository, mapper, logger) { }

            public override async Task<Result<int>> Handle(Command request)
            {
                var entity = Mapper.Map<Artist>(request);

                var newEntity = await Repository.Add(entity);

                return Result<int>.Success(newEntity.ArtistId);
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
