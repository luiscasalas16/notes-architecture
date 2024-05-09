namespace NCA.Tracks.Infrastructure.Repositories
{
    public class AlbumRepository : RepositoryBase<Album>, IAlbumRepository
    {
        public AlbumRepository(ChinookContext context)
            : base(context) { }
    }
}
