using NCA.Common.Domain.Models;
using NCA.Production.Domain.Errors;

namespace NCA.Production.Domain.Models;

/// <summary>
/// High-level product categorization.
/// </summary>
public partial class ProductCategory : EntityObject
{
    public static class Errors
    {
        private static string BaseCode => nameof(ProductCategory);

        public static Error NameMaximumLength => new($"{BaseCode}.{nameof(NameMaximumLength)}", "Cannot exceed 50 characters.");
    }

    /// <summary>
    /// Primary key for ProductCategory records.
    /// </summary>
    public int ProductCategoryId { get; set; }

    /// <summary>
    /// Category description.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
    /// </summary>
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<ProductSubcategory> ProductSubcategories { get; set; } = new List<ProductSubcategory>();
}
