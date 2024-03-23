using System.Linq.Expressions;
using NCA.Common.Domain.Models;

namespace NCA.Common.Domain
{
    public interface IRepository { }

    public interface IRepository<T> : IRepository
        where T : EntityObject
    {
        Task<IReadOnlyList<T>> GetAll();

        Task<IReadOnlyList<T>> Get(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> Get(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null, bool disableTracking = true);

        Task<IReadOnlyList<T>> Get(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null,
            bool disableTracking = true
        );

        Task<T?> GetById(int id);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task Delete(T entity);
    }
}
