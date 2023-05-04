using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Shared.Dto;
using VDMJasminka.WebApi.Core.Routes;

namespace VDMJasminka.WebApi.Controllers.Auth
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<ActionResult<string>> GetTokenAsync(UserDto request)
        {
            HttpContext.Response.Headers.Add("Cache-Control", "no-store");
            HttpContext.Response.Headers.Add("Pragma", "no-cache");
            return await _authService.CreateTokenForUserAsync(request.Username, request.Password);
        }
    }
}