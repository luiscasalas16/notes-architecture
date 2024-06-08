namespace NCA.Tracks.Application.Features.Artists.Commands
{
    public class DeleteArtist
    {
        public class Command : CommandBase<Result>
        {
            public int ArtistId { get; set; }
        }

        public class CommandHandler : CommandHandlerRepositoryBase<Command, Result, IArtistRepository>
        {
            public CommandHandler(IArtistRepository repository, IUnitWork unitWork, IMapper mapper, ILogger logger)
                : base(repository, unitWork, mapper, logger) { }

            public override async Task<Result> Handle(Command request)
            {
                var entity = await Repository.GetById(request.ArtistId);

                if (entity == null)
                    return Result.Failure(GenericErrors.NotFound(request.ArtistId));

                Repository.Delete(entity);

                await UnitWork.Save();

                return Result.Success();
            }
        }
    }
}
