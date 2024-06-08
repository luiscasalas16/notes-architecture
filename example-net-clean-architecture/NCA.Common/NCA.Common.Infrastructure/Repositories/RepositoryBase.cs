using System.Linq.Expressions;
using NCA.Common.Application.Repositories;
using NCA.Common.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace NCA.Common.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepository<T>
        where T : EntityObject
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetSingle(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public async Task<IReadOnlyList<T>> GetAll(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? order = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool tracking = false
        )
        {
            IQueryable<T> query = _context.Set<T>();

            if (tracking)
                query = query.AsNoTracking();

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null)
                query = query.Where(predicate);

            if (order != null)
                return await order(query).ToListAsync();

            return await query.ToListAsync();
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            _context.Set<T>().RemoveRange(_context.Set<T>().Where(predicate));
        }
    }
}
