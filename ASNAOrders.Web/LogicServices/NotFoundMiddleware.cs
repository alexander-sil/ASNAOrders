using ASNAOrders.Web.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// 
    /// </summary>
    public class NotFoundMiddleware : IMiddleware
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new List<ErrorListV1Inner>()
                {
                    new ErrorListV1Inner()
                    {
                        Code = 404,
                        Description = Properties.Resources.KeyNotFoundString
                    }
                };

                await context.Response.WriteAsJsonAsync
                (
                    response,
                    new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                    }
                );
            }
            else
            {
                await next(context);
            }
        }
    }
}
