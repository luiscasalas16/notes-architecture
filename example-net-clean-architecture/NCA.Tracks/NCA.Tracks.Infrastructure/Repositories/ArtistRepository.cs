using NCA.Tracks.Domain.Models;

namespace NCA.Tracks.Infrastructure.Repositories
{
    public class ArtistRepository : RepositoryBase<Artist>, IArtistRepository
    {
        public ArtistRepository(ChinookContext context)
            : base(context) { }
    }
}
