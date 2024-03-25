using ASNAOrders.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Handler to implement correct 500 Internal Server Error error handling.
    /// </summary>
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken cancellationToken)
        {

            if (context.Response.StatusCode == StatusCodes.Status500InternalServerError)
            {
                if (ex is BadHttpRequestException)
                {
                    return false;
                }

                var response = new List<ErrorListV1Inner>()
                    {
                        new ErrorListV1Inner()
                        {
                            Code = 500,
                            Description = ex.Message
                        }
                    };

                await context.Response.WriteAsJsonAsync
                (
                    response,
                    new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                    },
                    cancellationToken
                );
            }

            return true;
        }
    }
}
