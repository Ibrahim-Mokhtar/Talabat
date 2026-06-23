using System.Security.Claims;
using Talabat.Core.Application.Abstraction;

namespace Talabat.APIs.Services
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string? UserId { get; }

        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
        }

    }
}
