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

        public async Task<IReadOnlyList<T>> Get(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? order = null, List<Expression<Func<T, object>>>? includes = null, bool tracking = false)
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

        public async Task<T?> GetBy(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetBy(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public async Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
