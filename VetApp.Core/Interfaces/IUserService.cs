using System.Threading.Tasks;
using VDMJasminka.Core.Users;

namespace VDMJasminka.Application.Users
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
    }
}