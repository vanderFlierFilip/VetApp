using System.Security.Claims;
using System.Threading.Tasks;

namespace VDMJasminka.Core.Interfaces
{
    public interface IAuthService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        Task<string> CreateTokenForUserAsync(string username, string password);
        string RefreshTokenForUserAsync(ClaimsPrincipal username);
    }
}