using NCA.Common.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace NCA.Common.Infrastructure.Repositories
{
    public class UnitWorkBase : IUnitWork
    {
        private readonly DbContext _dbContext;

        public UnitWorkBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool HasActiveTransaction => _dbContext.Database.CurrentTransaction != null;

        public async Task<IUnitWorkTransaction> Transaction()
        {
            return new UnitWorkTransaction(await _dbContext.Database.BeginTransactionAsync());
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
