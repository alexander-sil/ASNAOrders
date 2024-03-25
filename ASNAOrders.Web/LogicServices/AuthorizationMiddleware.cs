using ASNAOrders.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASNAOrders.Web.LogicServices
{
    /// <summary>
    /// Middleware to implement correct 401 Unauthorized error handling.
    /// </summary>
    public class AuthorizationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);

            string ctype =
                (((context.Request.Method == "POST") && context.Request.Path.ToString().Contains("order"))
                || context.Request.Path.ToString().Contains("nomenclature")
                || context.Request.Path.ToString().Contains("security/oauth"))
                ? "application/vnd.eda.picker.errors.v1+json"
                : "application/json";

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                if ((context.Request.Method == "PUT" || context.Request.Method == "GET") && context.Request.Path.ToString().Contains("order"))
                {

                    string reason;

                    if (!context.Request.Headers.ContainsKey("Authorization"))
                    {
                        reason = "Missing or invalid token.";
                    }
                    else
                    {
                        reason = "Access token has been expired.";
                    }

                    var response = new AuthorizationRequiredResponse()
                    {
                        Reason = reason
                    };

                    await context.Response.WriteAsJsonAsync
                    (
                        response,
                        new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                        },
                        ctype
                    );

                }
                else
                {

                    var response = new List<ErrorListV1Inner>()
                    {
                        new ErrorListV1Inner()
                        {
                            Code = 401,
                            Description = "Unauthorized",
                        }
                    };

                    await context.Response.WriteAsJsonAsync
                    (
                        response,
                        new JsonSerializerOptions()
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                        },
                        ctype
                    );
                }
            }
        }
    }
}
