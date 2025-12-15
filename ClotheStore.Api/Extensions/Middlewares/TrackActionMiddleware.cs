using ClotheStore.Api.Extensions.Attributes;
using ClotheStore.Domain.Core;
using ClotheStore.Domain.Models.Tracking;
using ClotheStore.Domain.Repositories;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;

namespace ClotheStore.Api.Extensions.Middlewares
{
    public class TrackActionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TrackActionMiddleware> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppSettings _appSettings;

        public TrackActionMiddleware(RequestDelegate next,
            ILogger<TrackActionMiddleware> logger,
            IServiceProvider serviceProvider,
            IOptions<AppSettings> options)
        {
            _next = next;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _appSettings = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                IPrincipal user = context.User;

                var endpoint = context.GetEndpoint();
                if (endpoint != null && endpoint.Metadata.GetMetadata<SkipTrackingAttribute>() != null ||
                    user?.Identity is null || !user.Identity.IsAuthenticated)
                {
                    await _next(context);
                    return;
                }

                await _next(context);

                var siteInteraction = await GetInteraction(context);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var trackingService = scope.ServiceProvider.GetRequiredService<ITrackActionRepository>();
                    await trackingService.AddAsync(siteInteraction);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error tracking action");
            }
        }

        private async Task<SiteInteraction> GetInteraction(HttpContext context)
        {
            IPrincipal user = context.User;
            var claims = ((ClaimsIdentity)user.Identity!)?.Claims;

            Guid.TryParse(claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value,
                out Guid b2CObjectId);

            var siteInteraction = new SiteInteraction
            {
                UserId = b2CObjectId, // Replace with the actual user's ID from your DB
                Action = context.Request.Path,
                Description = "" // Mimics UTC-5 offset like in the SQL default
            };            

            return siteInteraction;
        }
    }
}
