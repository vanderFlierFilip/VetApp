using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Core.Users;

namespace VDMJasminka.Core.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User entity);
        Task Delete(User entity);
        Task<User> FindByUsername(string username);
        Task<User> Get(int id);
        IQueryable<User> GetAll();
        Task Update(User entity);
    }
}