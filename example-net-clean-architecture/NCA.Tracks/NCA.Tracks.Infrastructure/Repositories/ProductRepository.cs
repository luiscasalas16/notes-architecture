namespace NCA.Production.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AdventureWorksDbContext context)
            : base(context) { }
    }
}
