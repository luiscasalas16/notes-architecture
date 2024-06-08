using Microsoft.EntityFrameworkCore.Storage;
using NCA.Common.Application.Repositories;

namespace NCA.Common.Infrastructure.Repositories
{
    public class UnitWorkTransaction : IUnitWorkTransaction
    {
        private IDbContextTransaction _transaction;

        internal UnitWorkTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _transaction?.Dispose();
            _transaction = null!;
        }
    }
}
