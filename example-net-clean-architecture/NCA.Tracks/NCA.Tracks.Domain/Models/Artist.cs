using NCA.Common.Domain.Models;

namespace NCA.Tracks.Domain.Models;

public partial class Artist : EntityObject
{
    public static class Errors
    {
        private static string BaseCode => nameof(Artist);

        public static Error NameMaximumLength => new($"{BaseCode}.{nameof(NameMaximumLength)}", "Cannot exceed 160 characters.");
    }

    public int ArtistId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
