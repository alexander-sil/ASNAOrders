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
    /// Handler to implement correct 400 Bad Request error handling.
    /// </summary>
    public class BadRequestErrorHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken cancellationToken)
        {

            string ctype =
                (((context.Request.Method == "POST") && context.Request.Path.ToString().Contains("order"))
                || context.Request.Path.ToString().Contains("nomenclature")
                || context.Request.Path.ToString().Contains("security/oauth"))
                ? "application/vnd.eda.picker.errors.v1+json"
                : "application/json";

            if (ex is BadHttpRequestException || context.Response.StatusCode == StatusCodes.Status400BadRequest)
            {
                var response = new List<ErrorListV1Inner>()
                {
                    new ErrorListV1Inner()
                    {
                        Code = 400,
                        Description = ex.Message
                    }
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync
                (
                    response,
                    new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                    },
                    ctype,
                    cancellationToken
                );

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
