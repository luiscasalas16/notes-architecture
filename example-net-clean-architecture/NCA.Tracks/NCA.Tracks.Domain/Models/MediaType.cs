using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NCA.Common.Domain.Models;

namespace NCA.Tracks.Domain.Models;

[Table("MediaType")]
public partial class MediaType : EntityObject
{
    [Key]
    public int MediaTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
