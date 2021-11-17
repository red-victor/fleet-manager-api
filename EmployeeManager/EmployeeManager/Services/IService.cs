using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public interface IService<T>
    {
        Task AddAsync(T item);
        void RemoveAsync(int id);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
