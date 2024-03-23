﻿using NCA.Common.Infrastructure.Repositories;
using NCA.Production.Domain.Contracts.Repositories;
using NCA.Production.Domain.Models;

namespace NCA.Production.Infrastructure.Repositories
{
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(AdventureWorksDbContext context)
            : base(context) { }
    }
}
