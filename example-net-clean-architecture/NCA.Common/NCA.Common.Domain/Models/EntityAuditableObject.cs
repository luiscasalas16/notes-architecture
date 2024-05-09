namespace NCA.Common.Domain.Models
{
    public abstract class EntityAuditableObject : EntityObject
    {
        [Column("CREATION_DATE")]
        public required DateTime CreationDate { get; set; }

        [Column("CREATION_USER")]
        public required string CreationUser { get; set; }

        [Column("MODIFICATION_DATE")]
        public DateTime? ModificationDate { get; set; }

        [Column("MODIFICATION_USER")]
        public string? ModificationUser { get; set; }
    }
}
