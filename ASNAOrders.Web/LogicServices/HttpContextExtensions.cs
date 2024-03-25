using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Logic service to access dynamic host URL based on HttpContext.
    /// </summary>
    public class ParametersHttpContext
    {
        private static IHttpContextAccessor accessor;

        public static HttpContext current => accessor.HttpContext;

        public static string ApplicationUrl => $"{current.Request.Scheme}://{current.Request.Host}{current.Request.PathBase}";

        internal static void Configure(IHttpContextAccessor context)
        {
            accessor = context;
        }
    }

    public static class HttpContextExtensions
    {
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IApplicationBuilder UseHttpContext(this IApplicationBuilder app)
        {
            ParametersHttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            return app;
        }
    }
}
