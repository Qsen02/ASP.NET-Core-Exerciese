using Eventures.Models;
using Eventures.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eventures.Filters
{
    public class LogActionCreateFilter: IAsyncActionFilter
    {
        private readonly ILogger<LogActionCreateFilter> _logger;

        public LogActionCreateFilter(ILogger<LogActionCreateFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (context.HttpContext.Items.TryGetValue("Event", out var ev) &&
            ev is CreateEventViewModel eventModel)
            {
                var httpContext = context.HttpContext;

                var username = httpContext.User.Identity?.Name ?? "Unknown";
                var now = DateTime.Now;

                Event curEvent = httpContext.Items["Event"] as Event;

                _logger.LogInformation(
                    "[{Time}] Administrator {User} create event {Event} ({Start} / {End})",
                    now,
                    username,
                    curEvent?.Name,
                    curEvent?.Start,
                    curEvent?.End
                );
            }
        }
    }
}
