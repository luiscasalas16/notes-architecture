using Microsoft.EntityFrameworkCore;

namespace NCA.Tracks.Infrastructure.Repositories;

public partial class ChinookContext : DbContext
{
    public ChinookContext() { }

    public ChinookContext(DbContextOptions<ChinookContext> options)
        : base(options) { }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }
}
