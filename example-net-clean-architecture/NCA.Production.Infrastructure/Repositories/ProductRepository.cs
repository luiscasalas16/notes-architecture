using NCA.Common.Infrastructure.Repositories;
using NCA.Production.Domain.Contracts.Repositories;
using NCA.Production.Domain.Models;

namespace NCA.Production.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AdventureWorksDbContext context)
            : base(context) { }
    }
}
