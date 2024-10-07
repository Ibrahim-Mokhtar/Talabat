using System.Security.Claims;
using Talabat.Core.Application.Abstraction;

namespace Talabat.APIs.Services
{
    public class LogedInUserService : ILogedInUserService
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;
        public string? UserId { get; }

        public LogedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}
