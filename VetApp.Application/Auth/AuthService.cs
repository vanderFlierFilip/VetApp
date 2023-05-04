using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VDMJasminka.Application.Users;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        public async Task<string> CreateTokenForUserAsync(string username, string password)
        {
            var user = await _userService.GetUserByEmail(username);

            if (user == null)
            {
                throw new ArgumentException(nameof(user));
            }

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                throw new ArgumentException(nameof(user));

            JwtSecurityToken token = CreateToken(Convert.ToString(user.Id), user.Username, user.Role);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string RefreshTokenForUserAsync(ClaimsPrincipal principal)
        {
            var id = principal.FindFirst("id").Value;
            var username = principal.FindFirst("name").Value;
            var role = principal.FindFirst("role")?.Value ?? principal.FindFirst(ClaimTypes.Role).Value;

            var token = CreateToken(id, username, role);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken CreateToken(string id, string name, string role)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", id),
                new Claim("name", name),
                new Claim("role", CaseRole(role))
            };

            var key = new SymmetricSecurityKey(GetBytes(_configuration.GetSection("JWT:SecretKey").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var expires = GetExpirationDate();

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: cred);
            return token;
        }

        private DateTime GetExpirationDate()
        {
            var expiraton = _configuration.GetSection("JWT:ExpirationTimeInMinutes").Value;
            return DateTime.Now.AddMinutes(int.Parse(expiraton));
        }

        private string CaseRole(string input)
        {
            return input[0].ToString().ToUpper() + input.Substring(1).ToLower();
        }

        private bool VerifyPassword(string password, byte[] hash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);

            var computedHash = hmac.ComputeHash(GetBytes(password));

            return computedHash.SequenceEqual(hash);
        }

        private byte[] GetBytes(string from)
        {
            return Encoding.UTF8.GetBytes(from);
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();

            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(GetBytes(password));
        }
    }
}