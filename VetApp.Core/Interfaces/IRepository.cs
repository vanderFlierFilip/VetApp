using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDMJasminka.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);

    }

}
