using Domain.Common;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
    }
}
