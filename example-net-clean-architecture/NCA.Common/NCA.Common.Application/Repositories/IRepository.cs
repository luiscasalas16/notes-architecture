using System.Linq.Expressions;
using NCA.Common.Domain.Models;

namespace NCA.Common.Application.Repositories
{
    public interface IRepository { }

    // csharpier-ignore
    public interface IRepository<T> : IRepository
        where T : EntityObject
    {
        Task<IReadOnlyList<T>> Get(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? order = null, List<Expression<Func<T, object>>>? includes = null, bool tracking = false);

        Task<T?> GetBy(int id);

        Task<T?> GetBy(Expression<Func<T, bool>> predicate);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task Delete(T entity);
    }
}
