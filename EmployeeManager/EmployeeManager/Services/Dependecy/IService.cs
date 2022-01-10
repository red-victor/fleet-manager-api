using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public interface IService<T>
    {
        Task<T> AddAsync(T item);
        Task RemoveAsync(int id);
        Task<T> UpdateAsync(T item);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
