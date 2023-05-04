using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.WebApi.Core.Filters
{
    public class RefreshTokenSetterAttribute : ActionFilterAttribute
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _contextAccessor;

        public RefreshTokenSetterAttribute(IAuthService authService, IHttpContextAccessor contextAccessor)
        {
            _authService = authService;
            _contextAccessor = contextAccessor;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var token = _authService.RefreshTokenForUserAsync(_contextAccessor.HttpContext.User);
            filterContext.HttpContext.Response.Headers.Add("X-Token", token);
        }
    }
}
