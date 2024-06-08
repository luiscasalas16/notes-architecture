using System.Linq.Expressions;
using NCA.Common.Domain.Models;

namespace NCA.Common.Application.Repositories
{
    public interface IRepository { }

    // csharpier-ignore
    public interface IRepository<T> : IRepository
        where T : EntityObject
    {
        Task<T?> GetById(int id);

        Task<T?> GetSingle(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? order = null, List<Expression<Func<T, object>>>? includes = null, bool tracking = false);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> predicate);
    }
}
