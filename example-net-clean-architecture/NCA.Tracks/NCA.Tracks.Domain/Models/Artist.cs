using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NCA.Common.Domain.Models;

namespace NCA.Tracks.Domain.Models;

[Table("Artist")]
public partial class Artist : EntityObject
{
    public static class Errors
    {
        private static string BaseCode => nameof(Artist);

        public static Error NameMaximumLength => new($"{BaseCode}.{nameof(NameMaximumLength)}", "Cannot exceed 160 characters.");
    }

    [Key]
    public int ArtistId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
