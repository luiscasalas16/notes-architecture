using Microsoft.EntityFrameworkCore;
using NCA.Production.Domain.Models;

namespace NCA.Production.Infrastructure.Repositories
{
    public class AdventureWorksDbContext : DbContext
    {
        public AdventureWorksDbContext(DbContextOptions<AdventureWorksDbContext> options)
            : base(options) { }

        public DbSet<Product>? Products { get; set; }

        public DbSet<ProductCategory>? ProductCategories { get; set; }

        public DbSet<ProductSubcategory>? ProductSubcategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product", schema: "Production");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory", schema: "Production");
            modelBuilder.Entity<ProductSubcategory>().ToTable("ProductSubcategory", schema: "Production");
        }
    }
}
