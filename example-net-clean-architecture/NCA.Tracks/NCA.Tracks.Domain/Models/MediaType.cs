using NCA.Common.Domain.Models;

namespace NCA.Tracks.Domain.Models;

public partial class MediaType : EntityObject
{
    public int MediaTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
