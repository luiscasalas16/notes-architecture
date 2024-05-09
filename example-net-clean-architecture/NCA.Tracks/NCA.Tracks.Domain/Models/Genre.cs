using NCA.Common.Domain.Models;

namespace NCA.Tracks.Domain.Models;

public partial class Genre : EntityObject
{
    public int GenreId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
