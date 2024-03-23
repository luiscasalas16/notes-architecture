namespace NCA.Common.Domain.Models
{
    public abstract class EntityIdAuditableObject : EntityIdObject
    {
        public DateTime CreationDate { get; set; }

        public string? CreationUser { get; set; }

        public DateTime ModificationDate { get; set; }

        public string? ModificationUser { get; set; }
    }
}
