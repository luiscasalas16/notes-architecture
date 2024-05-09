namespace NCA.Tracks.Application.Features.Artists.Commands
{
    public class UpdateArtist
    {
        public class Command : CommandBase<Result>, IMapTo<Artist>
        {
            public int ArtistId { get; set; }

            public string Name { get; set; } = null!;
        }

        public class CommandHandler : CommandHandlerRepositoryBase<Command, Result, IArtistRepository>
        {
            public CommandHandler(IArtistRepository repository, IMapper mapper, ILogger logger)
                : base(repository, mapper, logger) { }

            public override async Task<Result> Handle(Command request)
            {
                var entity = await Repository.GetBy(request.ArtistId);

                if (entity == null)
                    return Result.Failure(GenericErrors.NotFound(request.ArtistId));

                Mapper.Map(request, entity, typeof(Command), typeof(Artist));

                await Repository.Update(entity);

                return Result.Success();
            }
        }

        public class CommandValidator : AbstractValidator<Command>
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
