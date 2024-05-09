using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NCA.Common.Domain.Models;

namespace NCA.Tracks.Domain.Models;

[Table("Genre")]
public partial class Genre : EntityObject
{
    [Key]
    public int GenreId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
