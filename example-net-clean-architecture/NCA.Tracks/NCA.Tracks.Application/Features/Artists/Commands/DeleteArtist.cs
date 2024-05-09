namespace NCA.Tracks.Application.Features.Artists.Commands
{
    public class DeleteArtist
    {
        public class Command : CommandBase<Result>
        {
            public int ArtistId { get; set; }

            public Command(int ArtistId)
            {
                ArtistId = ArtistId;
            }
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

                await Repository.Delete(entity);

                return Result.Success();
            }
        }
    }
}
