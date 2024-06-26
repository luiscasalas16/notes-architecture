﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NCA.Common.Domain.Models;

namespace NCA.Tracks.Domain.Models;

[Table("Track")]
public partial class Track : EntityObject
{
    [Key]
    public int TrackId { get; set; }

    public string Name { get; set; } = null!;

    public int? AlbumId { get; set; }

    public int MediaTypeId { get; set; }

    public int? GenreId { get; set; }

    public string? Composer { get; set; }

    public int Milliseconds { get; set; }

    public int? Bytes { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Album? Album { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual MediaType MediaType { get; set; } = null!;
}
