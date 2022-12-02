using Microsoft.Extensions.Logging;
using webNET_Hits_backend_aspnet_project_2.Services;

namespace webNET_Hits_backend_aspnet_project_2.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var userId = jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                _logger.LogInformation(userId.Value.ToString());
                context.Items["UserId"] = userId.Value;
            }

            await _next(context);
        }
    }
}
