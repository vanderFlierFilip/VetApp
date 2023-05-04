using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VDMJasminka.Core.Interfaces
{
    public interface IReadOnlyRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        T Get(int id);
        Task<T> GetAsync(int id);
    }
}
