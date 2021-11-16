using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Services
{
    public interface IService<T>
    {
        void Add(T item);
        void Remove(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
