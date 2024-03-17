using BusinessAccessLayer.Services;
using System.Security.Claims;

namespace PresentationLayer.Infrastucture.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int? UserId { get; }
        public string Username { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = Convert.ToInt32(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
            Username = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        }
    }
}
