using GLPIService.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GLPIService.WebApi.Services {
    public class CurrentUserService : ICurrentUserService {

        public string Username { get; set; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor) {

            Username = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "APIUser";

        }
    }
}
