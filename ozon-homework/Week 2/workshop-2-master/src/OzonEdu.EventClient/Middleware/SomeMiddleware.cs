
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OzonEdu.EventClient.Middleware
{
    public class SomeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SomeMiddleware> _logger;

        public SomeMiddleware(RequestDelegate next, ILogger<SomeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Call SomeMiddleware");
            await _next(context);
        }
    }
}