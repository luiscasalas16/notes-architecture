using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NCA.Common.Domain.Models;

namespace NCA.Tracks.Domain.Models;

[Table("Album")]
public partial class Album : EntityObject
{
    [Key]
    public int AlbumId { get; set; }

    public string Title { get; set; } = null!;

    public int ArtistId { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
