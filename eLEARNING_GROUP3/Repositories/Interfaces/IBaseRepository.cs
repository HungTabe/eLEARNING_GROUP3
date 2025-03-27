using System.Linq.Expressions;

namespace OnlineLearning.Repositories.Interfaces
{
    // Interface định nghĩa các phương thức cơ bản
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(long? id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(params object[] keyValues);
    }
}
