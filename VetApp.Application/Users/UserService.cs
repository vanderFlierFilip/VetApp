using System.Threading.Tasks;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Users;

namespace VDMJasminka.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.FindByUsername(email);
        }
    }
}